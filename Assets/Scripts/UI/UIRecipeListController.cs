using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using System;

public class UIRecipeListController : MonoBehaviour
{
    private List<Recipe> recipes;
    private Dictionary<IngredientType, int> inventory;
    private int currentRecipeIndex;

    private UnityAction<GlobalEventArgs> NewIngredientFound;
    private UnityAction<GlobalEventArgs> NewRecipeFound;
    private UnityAction<GlobalEventArgs> RecipeCompleted;

    [SerializeField]
    private VisualTreeAsset recipeListEntryTemplate;
    private Func<VisualElement> makeItem;
    private Action<VisualElement, int> bindItem;

    
    private Label recipeNameLabel;
    private ListView recipeListView;
    private List<UIRecipeListEntryData> currentRecipeIngredientsData;

    #region Mono
    private void Awake()
    {
        //Initialize Recipes List
        recipes = new List<Recipe>();

        //Initialize Inventory
        inventory = new Dictionary<IngredientType, int>();
        foreach (IngredientType type in Enum.GetValues(typeof(IngredientType)))
        {
            inventory.Add(type, 0);
        }

        //Initialize List View Data
        currentRecipeIngredientsData = new List<UIRecipeListEntryData>();
        currentRecipeIngredientsData.Add(new UIRecipeListEntryData());

        // Initialize Visual Elements
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        VisualElement container = root.Q<VisualElement>("Container");
        recipeNameLabel = container.Q<Label>("RecipeName");
        recipeListView = container.Q<ListView>("RecipeListView");
        SetUpRecipeListEntries();

        Button previousButton = container.Q<Button>("PreviousButton");
        previousButton.clicked += OnPreviousButtonClicked;

        Button nextButton = container.Q<Button>("NextButton");
        nextButton.clicked += OnNextButtonClicked;
    }

    private void OnEnable() {
        currentRecipeIndex = 0;

        NewIngredientFound += OnIngredientGet;
        NewRecipeFound += OnRecipeGet;
        RecipeCompleted += OnRecipeComplete;
        GlobalEventManager.AddListener(GlobalEventIndex.IngredientObtained, NewIngredientFound);
        GlobalEventManager.AddListener(GlobalEventIndex.RecipeObtained, NewRecipeFound);
        GlobalEventManager.AddListener(GlobalEventIndex.RecipeCompleted, RecipeCompleted);
    }

    private void OnDisable() {
        GlobalEventManager.RemoveListener(GlobalEventIndex.IngredientObtained, NewIngredientFound);
        GlobalEventManager.RemoveListener(GlobalEventIndex.RecipeObtained, NewRecipeFound);
        GlobalEventManager.RemoveListener(GlobalEventIndex.RecipeCompleted, RecipeCompleted);
        NewIngredientFound -= OnIngredientGet;
        NewRecipeFound -= OnRecipeGet;
        RecipeCompleted -= OnRecipeComplete;
    }
    #endregion

    #region RecipeListView
    private void SetUpRecipeListEntries(){
        recipeListView.makeItem = () =>
        {
            // Instantiate the UXML template for the entry
            var entry = recipeListEntryTemplate.Instantiate();
    
            // Instantiate a controller for the data
            UIRecipeListEntryController entryController = new UIRecipeListEntryController();
    
            // Assign the controller script to the visual element
            entry.userData = entryController;
    
            // Initialize the controller script
            entryController.SetVisualElement(entry);
    
            // Return the root of the instantiated visual tree
            return entry;
        };
    
        // Set up bind function for a specific list entry
        recipeListView.bindItem = (item, index) =>
        {
            if(currentRecipeIngredientsData.Count > 0){
                (item.userData as UIRecipeListEntryController)?.SetEntryData(currentRecipeIngredientsData[index]);
            }
        };
    
        // Set the actual item's source list/array
        recipeListView.itemsSource = currentRecipeIngredientsData;
    }

    private void ChangeShownRecipe(){
        if(currentRecipeIngredientsData.Count <= 0) return;
        if(recipes[currentRecipeIndex] == null) return;

        recipeNameLabel.text = recipes[currentRecipeIndex].RecipeName.ToString();
        ResetRecipeIngredientsData(recipes[currentRecipeIndex]);
    }

    private void ResetRecipeIngredientsData(Recipe recipe){
        if(recipe == null) return;

        currentRecipeIngredientsData.Clear();

        foreach (Ingredient ingredient in recipe.RequiredIngredients)
        {
            UIRecipeListEntryData entryData = new UIRecipeListEntryData();
            entryData.IngredientType = ingredient.IngredientType;
            entryData.RequiredQuantity = ingredient.Number;
            entryData.CurrentQuantity = inventory[ingredient.IngredientType];

            currentRecipeIngredientsData.Add(entryData);
        }

        recipeListView.Refresh();
    }

    private void UpdateRecipeIngredientsData(Ingredient ingredient){
        
        foreach (UIRecipeListEntryData entryData in currentRecipeIngredientsData)
        {
            if(entryData.IngredientType == ingredient.IngredientType){
                entryData.CurrentQuantity = inventory[ingredient.IngredientType];
                break;
            }
        }
        recipeListView.Refresh();
    }
    #endregion

    #region Ingredients
    private void RemoveUsedIngredients(Recipe recipe)
    {
        foreach (Ingredient ingredient in recipe.RequiredIngredients)
        {
            ConsumeIngredient(ingredient);
            UpdateRecipeIngredientsData(ingredient);
        }
    }

    private void ConsumeIngredient(Ingredient ingredient)
    {
        inventory[ingredient.IngredientType] -= ingredient.Number;

        if (inventory[ingredient.IngredientType] <= 0)
        {
            inventory[ingredient.IngredientType] = 0;
        }
    }
    #endregion

    #region Events
    private void OnIngredientGet(GlobalEventArgs message) {
        GlobalEventArgsFactory.IngredientObtainedParses(message, out Ingredient ingredient);
        inventory[ingredient.IngredientType] += ingredient.Number;
        UpdateRecipeIngredientsData(ingredient);
    }

    private void OnRecipeGet(GlobalEventArgs message) {
        GlobalEventArgsFactory.UIRecipeCompletedParses(message, out Recipe recipe);
        recipes.Add(recipe);
        ChangeShownRecipe();
    }

    private void OnRecipeComplete(GlobalEventArgs message) {
        GlobalEventArgsFactory.UIRecipeCompletedParses(message, out Recipe recipe);
        RemoveUsedIngredients(recipe);
        recipes.Remove(recipe);
        ChangeShownRecipe();
    }

    private void OnPreviousButtonClicked(){
        if(recipes.Count > 1){
            currentRecipeIndex = currentRecipeIndex - 1;
            if(currentRecipeIndex < 0){
                currentRecipeIndex = recipes.Count - 1;
            }
            ChangeShownRecipe();
        }
    }

    private void OnNextButtonClicked(){
        if(recipes.Count >= 1){
            currentRecipeIndex = (currentRecipeIndex + 1)%(recipes.Count);
            ChangeShownRecipe();
        }
    }
    #endregion
}
