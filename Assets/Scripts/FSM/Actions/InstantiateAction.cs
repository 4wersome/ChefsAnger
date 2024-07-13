using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateAction : StateAction {
    private GameObject gameObject;
    private Vector3 position;
    private Quaternion rotation;

    public InstantiateAction(GameObject gameObject, Vector3 position, Quaternion rotation) {
        this.gameObject = gameObject;
        this.position = position;
        this.rotation = rotation;
    }

    public override void OnEnter() {
        Object.Instantiate(gameObject, position, rotation);
    }
}
