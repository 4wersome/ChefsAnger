using UnityEngine.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GlobalEvent : UnityEvent<GlobalEventArgs> {

}

public static class GlobalEventManager {


    private static GlobalEvent[] globalEvents;

    static GlobalEventManager() {
        globalEvents = new GlobalEvent[Enum.GetValues((typeof(GlobalEventIndex))).Length];
        for (int i = 0; i < globalEvents.Length; i++) {
            globalEvents[i] = new GlobalEvent();
        }
    }

    public static void AddListener(GlobalEventIndex eventToListen, UnityAction<GlobalEventArgs> call) {
        globalEvents[(int)eventToListen].AddListener(call);
    }

    public static void RemoveListener(GlobalEventIndex eventToRemove, UnityAction<GlobalEventArgs> call) {
        globalEvents[(int)eventToRemove].RemoveListener(call);
    }

    public static void CastEvent(GlobalEventIndex eventToCast, GlobalEventArgs message) {
#if DEBUG
       // Debug.Log(eventToCast + GlobalEventArgsFactory.GetDebugString(eventToCast, message));
#endif
        globalEvents[(int)eventToCast]?.Invoke(message);
    }

}

[System.Serializable]
public class GlobalEventArgs : EventArgs {
    public ExtendedVariable[] args;

    public ExtendedVariable GetVariableByName(string name) {
        for (int i = 0; i < args.Length; i++) {
            if (args[i].VariableName == name) {
                return args[i];
            }
        }
        return null;
    }

    public GlobalEventArgs() {

    }

    public GlobalEventArgs(ExtendedVariable[] args) {
        this.args = args;
    }

    public static bool operator ==(GlobalEventArgs a, GlobalEventArgs b) {
        if (a.args.Length != b.args.Length) return false;
        for (int i = 0; i < a.args.Length; i++) {
            if (!(ExtendedVariable.StrictEquals(a.args[i], b.args[i]))) {
                return false;
            }
        }
        return true;
    }

    public static bool operator !=(GlobalEventArgs a, GlobalEventArgs b) {
        return !(a == b);
    }

    public override bool Equals(object obj) {
        var args = obj as GlobalEventArgs;
        return args != null &&
               EqualityComparer<ExtendedVariable[]>.Default.Equals(this.args, args.args);
    }

    public override int GetHashCode() {
        var hashCode = 1134202432;
        hashCode = hashCode * -1521134295 + EqualityComparer<ExtendedVariable[]>.Default.GetHashCode(args);
        return hashCode;
    }



}


public enum GlobalEventIndex {
    CAMERAPlayerSpawn,
    CAMERAPlayerDeath,
    CAMERAOnPlayerTakingDmg,
    PlayerHealthUpdated,
    PlayerMovement,
    EnableGamepad,
    UIRecipeUnlock,
    RecipeObtained,
    RecipeCompleted,
    IngredientObtained,
    PlayerDefenceUpdated,
    PlayerAttackUpdated
}

