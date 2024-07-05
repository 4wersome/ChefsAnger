using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIRecipeListEntryController
{
    private Label ingredientNameLabel;
    private Label currentNumberLabel;
    private Label requiredNumberLabel;
    
    public void SetVisualElement(VisualElement visualElement)
    {
        ingredientNameLabel = visualElement.Q<Label>("IngredientName");
        currentNumberLabel = visualElement.Q<Label>("CurrentNumber");
        requiredNumberLabel = visualElement.Q<Label>("RequiredNumber");
    }
    
    public void SetEntryData(UIRecipeListEntryData data)
    {
        ingredientNameLabel.text = data.IngredientType.ToString();
        currentNumberLabel.text = data.CurrentQuantity.ToString();
        requiredNumberLabel.text = data.RequiredQuantity.ToString();
    }
}
