using System;
using UnityEngine;
using UnityEngine.UI;

public enum EffectType { BLOCK, LOCK, DISABLE, NONE }

public class LevelDataHolder
{
    static BlueprintData topLeftBlueprint;
    static BlueprintData topRightBlueprint;
    static BlueprintData bottomLeftBlueprint;
    static BlueprintData bottomRightBlueprint;
    static GoalData goal;
    static int level;


    public static void SetGoal(Sprite sprite, Color color)
    {
        goal = new GoalData
        {
            sprite = sprite,
            color = color
        };
    }

    public static void SetTopLeftBlueprint(Color colorOwn = new Color(), EffectType effect = EffectType.NONE, Sprite sprite = null, Color color = new Color())
    {
        topLeftBlueprint = new BlueprintData
        {
            color = colorOwn,
            effect = effect,
            sprite = sprite,
            conditionColor = color,
        };
    }

    public static void SetTopRightBlueprint(Color colorOwn = new Color(), EffectType effect = EffectType.NONE, Sprite sprite = null, Color color = new Color())
    {
        topRightBlueprint = new BlueprintData
        {
            effect = effect,
            sprite = sprite,
            conditionColor = color,
        };
    }

    internal static void SetLevel(int lev)
    {
        level = lev;
    }

    public static void SetBottomLeftBlueprint(Color colorOwn = new Color(), EffectType effect = EffectType.NONE, Sprite sprite = null, Color color = new Color())
    {
        bottomLeftBlueprint = new BlueprintData
        {
            effect = effect,
            sprite = sprite,
            conditionColor = color,
        };
    }

    public static void SetBottomRightBlueprint(Color colorOwn = new Color(), EffectType effect = EffectType.NONE, Sprite sprite = null, Color color = new Color())
    {
        bottomRightBlueprint = new BlueprintData
        {
            effect = effect,
            sprite = sprite,
            conditionColor = color,
        };
    }

    public static void InjectData(ColorsController controller)
    {
        controller.level = level;
        if(goal != null)
        {
            controller.goal.color = goal.color;
            controller.goal.sprite = goal.sprite;
        }
        if(topLeftBlueprint != null)
        {
            if (topLeftBlueprint.effect == EffectType.DISABLE)
                controller.topLeftBlueprint.gameObject.SetActive(false);
            else
            {
                controller.topLeftBlueprint.gameObject.GetComponent<SpriteRenderer>().color = topLeftBlueprint.color;
                if (topLeftBlueprint.effect == EffectType.BLOCK)
                    controller.topLeftBlueprint.Block(topLeftBlueprint.sprite, topLeftBlueprint.conditionColor);
                if (topLeftBlueprint.effect == EffectType.LOCK)
                    controller.topLeftBlueprint.Lock(topLeftBlueprint.sprite, topLeftBlueprint.conditionColor);
            }
        }

        if (topRightBlueprint != null)
        {
            if (topRightBlueprint.effect == EffectType.DISABLE)
                controller.topRightBlueprint.gameObject.SetActive(false);
            else
            {
                controller.topRightBlueprint.gameObject.GetComponent<SpriteRenderer>().color = topRightBlueprint.color;
                if (topRightBlueprint.effect == EffectType.BLOCK)
                    controller.topRightBlueprint.Block(topRightBlueprint.sprite, topRightBlueprint.conditionColor);
                if (topRightBlueprint.effect == EffectType.LOCK)
                    controller.topRightBlueprint.Lock(topRightBlueprint.sprite, topRightBlueprint.conditionColor);
            }
        }

        if (bottomLeftBlueprint != null)
        {
            if (bottomLeftBlueprint.effect == EffectType.DISABLE)
                controller.bottomLeftBlueprint.gameObject.SetActive(false);
            else
            {
                controller.bottomLeftBlueprint.gameObject.GetComponent<SpriteRenderer>().color = bottomLeftBlueprint.color;
                if (bottomLeftBlueprint.effect == EffectType.BLOCK)
                    controller.bottomLeftBlueprint.Block(bottomLeftBlueprint.sprite, bottomLeftBlueprint.conditionColor);
                if (bottomLeftBlueprint.effect == EffectType.LOCK)
                    controller.bottomLeftBlueprint.Lock(bottomLeftBlueprint.sprite, bottomLeftBlueprint.conditionColor);
            }
        }

        if (bottomRightBlueprint != null)
        {
            if (bottomRightBlueprint.effect == EffectType.DISABLE)
                controller.bottomRightBlueprint.gameObject.SetActive(false);
            else
            {
                controller.bottomRightBlueprint.gameObject.GetComponent<SpriteRenderer>().color = bottomRightBlueprint.color;
                if (bottomRightBlueprint.effect == EffectType.BLOCK)
                    controller.bottomRightBlueprint.Block(bottomRightBlueprint.sprite, bottomRightBlueprint.conditionColor);
                if (bottomRightBlueprint.effect == EffectType.LOCK)
                    controller.bottomRightBlueprint.Lock(bottomRightBlueprint.sprite, bottomRightBlueprint.conditionColor);
            }
        }

        ClearData();
    }

    private static void ClearData()
    {
        topLeftBlueprint = topRightBlueprint = bottomLeftBlueprint = bottomRightBlueprint = null;
        goal = null;
    }

    class BlueprintData
    {
        public Color color;
        public EffectType effect;
        public Sprite sprite;
        public Color conditionColor;
    }

    class GoalData
    {
        public Sprite sprite;
        public Color color;
    }
}
