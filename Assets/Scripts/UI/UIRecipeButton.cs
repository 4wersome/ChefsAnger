using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIRecipeButton : MonoBehaviour
{
    private VisualElement recipesListElement;


    private void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        recipesListElement = root.Q<VisualElement>("RecipeIngredients");

        Button recipeButton = root.Q<Button>("RecipeButton");
        recipeButton.clicked += OnButtonClicked;
    }

    private void OnButtonClicked () {
        recipesListElement.visible = !recipesListElement.visible;
    }
}
