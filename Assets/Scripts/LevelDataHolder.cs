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
    static TempHolderData tempHolderData;
    static int level;

    public static void SetGoal(Sprite sprite, Color color)
    {
        goal = new GoalData
        {
            sprite = sprite,
            color = color
        };
    }

    public static void SetTempHolderData(Color color)
    {
        tempHolderData = new TempHolderData
        {
            color = color,
        };
    }

    public static void SetTopLeftBlueprint(Color colorOwn = new Color(),int spawnAmount = 999, EffectType effect = EffectType.NONE, Sprite sprite = null, Color color = new Color())
    {
        topLeftBlueprint = new BlueprintData
        {
            color = colorOwn,
            effect = effect,
            sprite = sprite,
            conditionColor = color,
            spawnAmount = spawnAmount,
        };
    }

    public static void SetTopRightBlueprint(Color colorOwn = new Color(), int spawnAmount = 999, EffectType effect = EffectType.NONE, Sprite sprite = null, Color color = new Color())
    {
        topRightBlueprint = new BlueprintData
        {
            color = colorOwn,
            effect = effect,
            sprite = sprite,
            conditionColor = color,
            spawnAmount = spawnAmount,
        };
    }

    internal static void SetLevel(int lev)
    {
        level = lev;
    }

    public static void SetBottomLeftBlueprint(Color colorOwn = new Color(), int spawnAmount = 999, EffectType effect = EffectType.NONE, Sprite sprite = null, Color color = new Color())
    {
        bottomLeftBlueprint = new BlueprintData
        {
            color = colorOwn,
            effect = effect,
            sprite = sprite,
            conditionColor = color,
            spawnAmount = spawnAmount,
        };
    }

    public static void SetBottomRightBlueprint(Color colorOwn = new Color(), int spawnAmount = 999, EffectType effect = EffectType.NONE, Sprite sprite = null, Color color = new Color())
    {
        bottomRightBlueprint = new BlueprintData
        {
            color = colorOwn,
            effect = effect,
            sprite = sprite,
            conditionColor = color,
            spawnAmount = spawnAmount,
        };
    }

    private static void InjectToBlueprint(BlueprintController controller, BlueprintData data)
    {
        if (data != null)
        {
            if (data.effect == EffectType.DISABLE)
                controller.gameObject.SetActive(false);
            else
            {
                controller.spawnAmount = data.spawnAmount;
                controller.gameObject.GetComponent<SpriteRenderer>().color = data.color;
                if (data.effect == EffectType.BLOCK)
                    controller.Block(data.sprite, data.conditionColor);
                if (data.effect == EffectType.LOCK)
                    controller.Lock(data.sprite, data.conditionColor);
            }
        }
    }

    public static void InjectData(ColorsController controller)
    {
        controller.level = level;

        if (goal != null)
        {
            controller.goal.color = goal.color;
            controller.goal.sprite = goal.sprite;
        }
        if (tempHolderData != null)
        {
            controller.tempColorHolder.gameObject.GetComponent<SpriteRenderer>().color = tempHolderData.color;
        }
        else
        {
            controller.tempColorHolder.gameObject.SetActive(false);
        }
        InjectToBlueprint(controller.topLeftBlueprint, topLeftBlueprint);
        InjectToBlueprint(controller.topRightBlueprint, topRightBlueprint);
        InjectToBlueprint(controller.bottomLeftBlueprint, bottomLeftBlueprint);
        InjectToBlueprint(controller.bottomRightBlueprint, bottomRightBlueprint);
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
        public int spawnAmount;
    }

    class GoalData
    {
        public Sprite sprite;
        public Color color;
    }

    class TempHolderData
    {
        public Color color;
    }
}
