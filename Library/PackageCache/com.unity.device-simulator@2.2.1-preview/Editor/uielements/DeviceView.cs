using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Unity.DeviceSimulator
{
    internal class DeviceView : IMGUIContainer
    {
        private Material m_PreviewMaterial;
        private Material m_DeviceMaterial;
        private Matrix4x4 m_DeviceToView;
        private Matrix4x4 m_ScreenToView;

        private int m_ScreenWidth;
        private int m_ScreenHeight;
        private float m_Rotation;
        private float m_Scale;
        private ScreenOrientation m_ScreenOrientation;
        private Vector4 m_BorderSize;
        private Vector4 m_ScreenInsets;
        private Texture m_PreviewTexture;
        private Texture m_OverlayTexture;

        private bool m_ShowSafeArea;
        private Rect m_SafeArea;
        private float m_SafeAreaLineWidth = 2f;
        private Color m_SafeAreaColor = Color.green;

        private Mesh m_SafeAreaMesh;
        private Mesh m_ScreenMesh;
        private Mesh m_OverlayMesh;
        private Mesh m_ProceduralOverlayMesh;

        public Quaternion Rotation
        {
            private get => Quaternion.Euler(0, 0, m_Rotation);
            set
            {
                m_Rotation = value.eulerAngles.z;
                ComputeBoundingBoxAndTransformations();
            }
        }

        public float Scale
        {
            private get => m_Scale;
            set
            {
                m_Scale = value;
                ComputeBoundingBoxAndTransformations();
            }
        }

        public Texture PreviewTexture
        {
            private get => m_PreviewTexture;
            set
            {
                m_PreviewTexture = value;
            }
        }

        public Texture OverlayTexture
        {
            private get => m_OverlayTexture;
            set
            {
                if (m_OverlayTexture != value)
                    MarkDirtyRepaint();
                m_OverlayTexture = value;
            }
        }

        public Vector4 ScreenInsets
        {
            private get => m_ScreenInsets;
            set
            {
                m_ScreenInsets = value;
                MarkDirtyRepaint();
            }
        }

        public ScreenOrientation ScreenOrientation
        {
            private get => m_ScreenOrientation;
            set
            {
                m_ScreenOrientation = value;
                MarkDirtyRepaint();
            }
        }

        public bool ShowSafeArea
        {
            private get => m_ShowSafeArea;
            set
            {
                m_ShowSafeArea = value;
                MarkDirtyRepaint();
            }
        }

        public Rect SafeArea
        {
            private get => m_SafeArea;
            set
            {
                m_SafeArea = value;
                MarkDirtyRepaint();
            }
        }

        public float SafeAreaLineWidth
        {
            private get => m_SafeAreaLineWidth;
            set
            {
                m_SafeAreaLineWidth = value;
                MarkDirtyRepaint();
            }
        }

        public Color SafeAreaColor
        {
            private get => m_SafeAreaColor;
            set
            {
                m_SafeAreaColor = value;
                MarkDirtyRepaint();
            }
        }

        public Matrix4x4 ViewToScreen { get; private set; }
        public event Action OnViewToScreenChanged;

        public DeviceView(Quaternion rotation, float scale)
        {
            m_Scale = scale;
            m_Rotation = rotation.eulerAngles.z;
            ComputeBoundingBoxAndTransformations();

            onGUIHandler += OnIMGUIRendered;
        }

        public void SetDevice(int screenWidth, int screenHeight, Vector4 borderSize)
        {
            m_ScreenWidth = screenWidth;
            m_ScreenHeight = screenHeight;
            m_BorderSize = borderSize;
            ComputeBoundingBoxAndTransformations();
        }

        private void OnIMGUIRendered()
        {
            if (EditorApplication.isPlaying && !EditorApplication.isPaused)
                EditorGUIUtility.keyboardControl = 0;

            var type = Event.current.type;
            if (type == EventType.Repaint)
            {
                if (PreviewTexture != null)
                {
                    if (m_PreviewMaterial == null)
                        m_PreviewMaterial = GUI.blitMaterial;
                    if (m_DeviceMaterial == null)
                        m_DeviceMaterial = new Material(Shader.Find("Hidden/Internal-GUITextureClip"));

                    DrawScreen();
                    DrawOverlay();
                    DrawSafeArea();
                }
            }

            if (type != EventType.Repaint && type != EventType.Layout && type != EventType.Used)
            {
                var useEvent = true;
                if (!Event.current.isKey || (!EditorApplication.isPlaying || EditorApplication.isPaused))
                    return;

                EditorGUIUtility.QueueGameViewInputEvent(Event.current);

                // Don't use command events, or they won't be sent to other views.
                if (type == EventType.ExecuteCommand || type == EventType.ValidateCommand)
                    useEvent = false;

                if (useEvent)
                    Event.current.Use();
            }
        }

        private void DrawSafeArea()
        {
            if (!ShowSafeArea)
                return;

            CreateSafeAreaMesh();

            m_PreviewMaterial.mainTexture = null;
            m_PreviewMaterial.SetPass(0);

            Graphics.DrawMeshNow(m_SafeAreaMesh, m_ScreenToView);
        }

        private void DrawScreen()
        {
            if (PreviewTexture == null)
                return;

            CreateScreenMesh();

            m_PreviewMaterial.mainTexture = PreviewTexture;
            m_PreviewMaterial.SetPass(0);

            Graphics.DrawMeshNow(m_ScreenMesh, m_ScreenToView);
        }

        private void DrawOverlay()
        {
            if (m_OverlayTexture == null)
            {
                DrawOverlayProcedural();
                return;
            }

            CreateOverlayMesh();

            m_DeviceMaterial.mainTexture = m_OverlayTexture;
            m_DeviceMaterial.SetPass(0);

            Graphics.DrawMeshNow(m_OverlayMesh, m_DeviceToView);
        }

        private void DrawOverlayProcedural()
        {
            m_PreviewMaterial.mainTexture = null;
            m_PreviewMaterial.SetPass(0);

            if (m_ProceduralOverlayMesh == null)
                CreateProceduralOverlayMesh();

            Graphics.DrawMeshNow(m_ProceduralOverlayMesh, m_DeviceToView);
        }

        private void ComputeBoundingBoxAndTransformations()
        {
            var width = m_ScreenWidth + m_BorderSize.x + m_BorderSize.z;
            var height = m_ScreenHeight + m_BorderSize.y + m_BorderSize.w;

            var rotateScale = Matrix4x4.TRS(Vector3.zero, Rotation, new Vector3(Scale, Scale, 1));

            var vertices = new Vector3[4];
            vertices[0] = rotateScale.MultiplyPoint(new Vector3(0, 0, Vertex.nearZ));
            vertices[1] = rotateScale.MultiplyPoint(new Vector3(width, 0, Vertex.nearZ));
            vertices[2] = rotateScale.MultiplyPoint(new Vector3(width, height, Vertex.nearZ));
            vertices[3] = rotateScale.MultiplyPoint(new Vector3(0, height, Vertex.nearZ));

            var min = new Vector2(float.MaxValue, float.MaxValue);
            var max = new Vector2(float.MinValue, float.MinValue);

            foreach (var vertex in vertices)
            {
                if (vertex.x < min.x)
                    min.x = vertex.x;
                if (vertex.x > max.x)
                    max.x = vertex.x;
                if (vertex.y < min.y)
                    min.y = vertex.y;
                if (vertex.y > max.y)
                    max.y = vertex.y;
            }

            var boundingBox = max - min;
            style.width = boundingBox.x;
            style.height = boundingBox.y;

            m_DeviceToView = Matrix4x4.Translate(new Vector3(boundingBox.x / 2, boundingBox.y / 2)) * rotateScale * Matrix4x4.Translate(new Vector3(-width / 2, -height / 2));
            m_ScreenToView = Matrix4x4.Translate(new Vector3(boundingBox.x / 2, boundingBox.y / 2)) * rotateScale * Matrix4x4.Translate(new Vector3(-width / 2 + m_BorderSize.x, -height / 2 + m_BorderSize.y));

            ViewToScreen = m_ScreenToView.inverse;
            OnViewToScreenChanged?.Invoke();

            MarkDirtyRepaint();
        }

        private void CreateSafeAreaMesh()
        {
            var scaledLineWidth = m_SafeAreaLineWidth / Scale;

            var vertices = new Vector3[8];
            vertices[0] = new Vector3(SafeArea.x, SafeArea.y, Vertex.nearZ);
            vertices[1] = new Vector3(SafeArea.x + SafeArea.width, SafeArea.y, Vertex.nearZ);
            vertices[2] = new Vector3(SafeArea.x + SafeArea.width, SafeArea.y + SafeArea.height, Vertex.nearZ);
            vertices[3] = new Vector3(SafeArea.x, SafeArea.y + SafeArea.height, Vertex.nearZ);
            vertices[4] = new Vector3(SafeArea.x + scaledLineWidth, SafeArea.y + scaledLineWidth, Vertex.nearZ);
            vertices[5] = new Vector3(SafeArea.x + SafeArea.width - scaledLineWidth, SafeArea.y + scaledLineWidth, Vertex.nearZ);
            vertices[6] = new Vector3(SafeArea.x + SafeArea.width - scaledLineWidth, SafeArea.y + SafeArea.height - scaledLineWidth, Vertex.nearZ);
            vertices[7] = new Vector3(SafeArea.x + scaledLineWidth, SafeArea.y + SafeArea.height - scaledLineWidth, Vertex.nearZ);

            if (m_SafeAreaMesh == null)
                m_SafeAreaMesh = new Mesh();
            else
                m_SafeAreaMesh.Clear();

            m_SafeAreaMesh.vertices = vertices;
            m_SafeAreaMesh.colors = new[] {m_SafeAreaColor, m_SafeAreaColor, m_SafeAreaColor, m_SafeAreaColor, m_SafeAreaColor, m_SafeAreaColor, m_SafeAreaColor, m_SafeAreaColor};
            m_SafeAreaMesh.triangles = new[] {0, 4, 1, 1, 4, 5, 1, 5, 6, 1, 6, 2, 6, 7, 2, 7, 3, 2, 0, 3, 4, 3, 7, 4};
        }

        private void CreateScreenMesh()
        {
            var vertices = new Vector3[4];
            vertices[0] = new Vector3(ScreenInsets.x, ScreenInsets.y, Vertex.nearZ);
            vertices[1] = new Vector3(m_ScreenWidth - ScreenInsets.z, ScreenInsets.y, Vertex.nearZ);
            vertices[2] = new Vector3(m_ScreenWidth - ScreenInsets.z, m_ScreenHeight - ScreenInsets.w, Vertex.nearZ);
            vertices[3] = new Vector3(ScreenInsets.x, m_ScreenHeight - ScreenInsets.w, Vertex.nearZ);

            var portraitUvs = new[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };
            var uvs = new Vector2[4];

            var startPos = 0;
            switch (ScreenOrientation)
            {
                case ScreenOrientation.Portrait:
                    startPos = 0;
                    break;
                case ScreenOrientation.LandscapeRight:
                    startPos = 1;
                    break;
                case ScreenOrientation.PortraitUpsideDown:
                    startPos = 2;
                    break;
                case ScreenOrientation.LandscapeLeft:
                    startPos = 3;
                    break;
            }

            for (int index = 0; index < 4; ++index)
            {
                var uvIndex = (index + startPos) % 4;
                uvs[index] = portraitUvs[uvIndex];
            }

            if (m_ScreenMesh == null)
                m_ScreenMesh = new Mesh();
            else
                m_ScreenMesh.Clear();

            m_ScreenMesh.vertices = vertices;
            m_ScreenMesh.uv = uvs;
            m_ScreenMesh.triangles = new[] {0, 1, 3, 1, 2, 3};
        }

        private void CreateOverlayMesh()
        {
            var width = m_ScreenWidth + m_BorderSize.x + m_BorderSize.z;
            var height = m_ScreenHeight + m_BorderSize.y + m_BorderSize.w;

            var vertices = new Vector3[4];
            vertices[0] = new Vector3(0, 0, Vertex.nearZ);
            vertices[1] = new Vector3(width, 0, Vertex.nearZ);
            vertices[2] = new Vector3(width, height, Vertex.nearZ);
            vertices[3] = new Vector3(0, height, Vertex.nearZ);

            if (m_OverlayMesh == null)
                m_OverlayMesh = new Mesh();
            else
                m_OverlayMesh.Clear();

            m_OverlayMesh.vertices = vertices;
            m_OverlayMesh.uv = new[] {new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, 0)};
            m_OverlayMesh.triangles = new[] {0, 1, 3, 1, 2, 3};
        }

        private void CreateProceduralOverlayMesh()
        {
            var width = m_ScreenWidth + m_BorderSize.x + m_BorderSize.z;
            var height = m_ScreenHeight + m_BorderSize.y + m_BorderSize.w;

            const float padding = 10;;

            var vertices = new Vector3[8];
            vertices[0] = new Vector3(0, 0, Vertex.nearZ);
            vertices[1] = new Vector3(width, 0, Vertex.nearZ);
            vertices[2] = new Vector3(width, height, Vertex.nearZ);
            vertices[3] = new Vector3(0, height, Vertex.nearZ);
            vertices[4] = new Vector3(0 + padding, 0 + padding, Vertex.nearZ);
            vertices[5] = new Vector3(width - padding, 0 + padding, Vertex.nearZ);
            vertices[6] = new Vector3(width - padding, height - padding, Vertex.nearZ);
            vertices[7] = new Vector3(0 + padding, height - padding, Vertex.nearZ);

            var outerColor = EditorGUIUtility.isProSkin ? new Color(217f / 255, 217f / 255, 217f / 255) : new Color(100f / 255, 100f / 255, 100f / 255);

            if (m_ProceduralOverlayMesh == null)
                m_ProceduralOverlayMesh = new Mesh();
            else
                m_ProceduralOverlayMesh.Clear();

            m_ProceduralOverlayMesh.vertices = vertices;
            m_ProceduralOverlayMesh.colors = new[]
            {
                outerColor, outerColor, outerColor, outerColor, outerColor, outerColor, outerColor, outerColor
            };
            m_ProceduralOverlayMesh.triangles = new[]
            {
                0, 4, 1, 1, 4, 5, 1, 5, 6, 1, 6, 2, 6, 7, 2, 7, 3, 2, 0, 3, 4, 3, 7, 4
            };
        }
    }
}
