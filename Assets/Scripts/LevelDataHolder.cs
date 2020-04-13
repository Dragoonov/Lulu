using UnityEngine;

public enum EffectType { BLOCK, LOCK, DISABLE }

public class LevelDataHolder
{
    static BlueprintData topLeftBlueprint;
    static BlueprintData topRightBlueprint;
    static BlueprintData bottomLeftBlueprint;
    static BlueprintData bottomRightBlueprint;


    public static void setTopLeftBlueprint(EffectType effect, Sprite sprite = null, Color color = new Color())
    {
        topLeftBlueprint = new BlueprintData
        {
            effect = effect,
            sprite = sprite,
            color = color,
        };
    }

    public static void setTopRightBlueprint(EffectType effect, Sprite sprite = null, Color color = new Color())
    {
        topRightBlueprint = new BlueprintData
        {
            effect = effect,
            sprite = sprite,
            color = color,
        };
    }

    public static void setBottomLeftBlueprint(EffectType effect, Sprite sprite = null, Color color = new Color())
    {
        bottomLeftBlueprint = new BlueprintData
        {
            effect = effect,
            sprite = sprite,
            color = color,
        };
    }

    public static void setBottomRightBlueprint(EffectType effect, Sprite sprite = null, Color color = new Color())
    {
        bottomRightBlueprint = new BlueprintData
        {
            effect = effect,
            sprite = sprite,
            color = color,
        };
    }

    public static void InjectData(ColorsController controller)
    {
        if(topLeftBlueprint != null)
        {
            if (topLeftBlueprint.effect == EffectType.DISABLE)
                controller.topLeftBlueprint.gameObject.SetActive(false);
            else
            {
                if (topLeftBlueprint.effect == EffectType.BLOCK)
                    controller.topLeftBlueprint.Block(topLeftBlueprint.sprite, topLeftBlueprint.color);
                if (topLeftBlueprint.effect == EffectType.LOCK)
                    controller.topLeftBlueprint.Lock(topLeftBlueprint.sprite, topLeftBlueprint.color);
            }
        }

        if (topRightBlueprint != null)
        {
            if (topRightBlueprint.effect == EffectType.DISABLE)
                controller.topRightBlueprint.gameObject.SetActive(false);
            else
            {
                if (topRightBlueprint.effect == EffectType.BLOCK)
                    controller.topRightBlueprint.Block(topRightBlueprint.sprite, topRightBlueprint.color);
                if (topRightBlueprint.effect == EffectType.LOCK)
                    controller.topRightBlueprint.Lock(topRightBlueprint.sprite, topRightBlueprint.color);
            }
        }

        if (bottomLeftBlueprint != null)
        {
            if (bottomLeftBlueprint.effect == EffectType.DISABLE)
                controller.bottomLeftBlueprint.gameObject.SetActive(false);
            else
            {
                if (bottomLeftBlueprint.effect == EffectType.BLOCK)
                    controller.bottomLeftBlueprint.Block(bottomLeftBlueprint.sprite, bottomLeftBlueprint.color);
                if (bottomLeftBlueprint.effect == EffectType.LOCK)
                    controller.bottomLeftBlueprint.Lock(bottomLeftBlueprint.sprite, bottomLeftBlueprint.color);
            }
        }

        if (bottomRightBlueprint != null)
        {
            if (bottomRightBlueprint.effect == EffectType.DISABLE)
                controller.bottomRightBlueprint.gameObject.SetActive(false);
            else
            {
                if (bottomRightBlueprint.effect == EffectType.BLOCK)
                    controller.bottomRightBlueprint.Block(bottomRightBlueprint.sprite, bottomRightBlueprint.color);
                if (bottomRightBlueprint.effect == EffectType.LOCK)
                    controller.bottomRightBlueprint.Lock(bottomRightBlueprint.sprite, bottomRightBlueprint.color);
            }
        }

        ClearData();
    }

    private static void ClearData()
    {
        topLeftBlueprint = topRightBlueprint = bottomLeftBlueprint = bottomRightBlueprint = null;
    }

    class BlueprintData
    {
        public EffectType effect;
        public Sprite sprite;
        public Color color;
    }
}
