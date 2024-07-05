using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRecipeListEntryData
{
    private IngredientType ingredientType;
    private int currentQuantity;
    private int requiredQuantity;

    public IngredientType IngredientType { get { return ingredientType; } set { ingredientType = value; } }
    public int CurrentQuantity { get{ return currentQuantity; } set { currentQuantity = value; } }
    public int RequiredQuantity { get{ return requiredQuantity; } set { requiredQuantity = value; } }

    public void UpdateQuantity(int value)
    {
        currentQuantity += value;
    }
}
