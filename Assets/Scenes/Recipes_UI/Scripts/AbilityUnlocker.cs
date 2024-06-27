using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbilityUnlocker : MonoBehaviour
{
    [SerializeField]
    RecipeNameEnum recipeName;

    private UnityAction<GlobalEventArgs> onUnlock;
    void Start()
    {
        onUnlock += UnlockAbility;
        GlobalEventManager.AddListener(GlobalEventIndex.UIRecipeUnlock, onUnlock);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void  UnlockAbility(GlobalEventArgs message)
    {
        GlobalEventArgsFactory.UIRecipeCompletedParses(message, out Recipe recipeUnlocked);
        if (recipeUnlocked.RecipeName == recipeName)
        {
        gameObject.SetActive(false);
        }
    }

    private void Reset()
    {
        gameObject.SetActive(true);
    }
}
