using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using System.Collections;

public class UIRecipeQuestPopUp : MonoBehaviour
{
    [SerializeField]
    private float popUpDuration;
    [SerializeField]
    private float fadeSpeed;

    private const string obtainedRecipe = "Obtained Recipe:";
    private const string completedRecipe = "Completed Recipe:";

    private UnityAction<GlobalEventArgs> NewRecipeFound;
    private UnityAction<GlobalEventArgs> RecipeCompleted;

    private void OnEnable() {
        NewRecipeFound += OnRecipeGet;
        RecipeCompleted += OnRecipeComplete;
        GlobalEventManager.AddListener(GlobalEventIndex.RecipeObtained, NewRecipeFound);
        GlobalEventManager.AddListener(GlobalEventIndex.RecipeCompleted, RecipeCompleted);
    }

    private void OnDisable() {
        GlobalEventManager.RemoveListener(GlobalEventIndex.RecipeObtained, NewRecipeFound);
        GlobalEventManager.RemoveListener(GlobalEventIndex.RecipeCompleted, RecipeCompleted);
        NewRecipeFound -= OnRecipeGet;
        RecipeCompleted -= OnRecipeComplete;
    }

    private void OnRecipeGet (GlobalEventArgs message) {
        ShowPopUpWithRecipeName(message, obtainedRecipe);
    }

    private void OnRecipeComplete (GlobalEventArgs message) {
        ShowPopUpWithRecipeName(message, completedRecipe);
    }

    private void ShowPopUpWithRecipeName(GlobalEventArgs message, string questMessage){
        GlobalEventArgsFactory.UIRecipeCompletedParses(message, out Recipe recipe);

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        VisualElement popUp = root.Q<VisualElement>("PopUp");
        Label questRecipeLabel = popUp.Q<Label>("QuestRecipe");
        questRecipeLabel.text = questMessage;
        Label recipeNameLabel = popUp.Q<Label>("RecipeName");
        recipeNameLabel.text = recipe.RecipeName.ToString();

        StartCoroutine(FadeInOut(popUp));
    }

    private IEnumerator FadeInOut(VisualElement element){
            // fade in
            yield return Fade(element, 1);
            // wait
            yield return new WaitForSeconds(popUpDuration);
            // fade out
            yield return Fade(element, 0);
    }

    private IEnumerator Fade(VisualElement element, float targetAlpha)
    {
        while(element.style.opacity.value != targetAlpha)
        {
            float newAlpha = Mathf.MoveTowards(element.style.opacity.value, targetAlpha, fadeSpeed * Time.deltaTime);
            element.style.opacity = new StyleFloat(newAlpha);
            yield return new WaitForEndOfFrame();
        }
    }
}