using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugAction : StateAction {

    private string onEnterString;
    private string onExitString;
    private string onUpdateString;

    private bool printOnUpdate;

    public DebugAction(string onEnterString = "", string onExitString = "", string onUpdateString = "") {
        this.onEnterString = onEnterString;
        this.onExitString = onExitString;
        if (onUpdateString == "") printOnUpdate = false;
        else printOnUpdate = true;
        this.onUpdateString = onUpdateString;
    }

    public override void OnEnter() {
        Debug.Log(onEnterString);
    }

    public override void OnExit() {
        Debug.Log(onExitString);
    }

    public override void OnUpdate() {
        if(printOnUpdate) Debug.Log(onUpdateString);
    }
}
