using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVisibleAction : StateAction {
    // Start is called before the first frame update

    private GameObject gameObject;

    public SetVisibleAction(GameObject gameObject) {
        this.gameObject = gameObject;
    }

    public override void OnEnter() {
        gameObject.SetActive(false);
    }

    public override void OnExit() {
        gameObject.SetActive(true);
    }
}
