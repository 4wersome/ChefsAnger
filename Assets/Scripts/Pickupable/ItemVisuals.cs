using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemVisuals : MonoBehaviour
{
    [SerializeField]
    private float ItemScaleMultiplier;
    [SerializeField]
    private float rotationSpeed;

    private Vector3 Rotation;
    void Start()
    {
        transform.localScale *= ItemScaleMultiplier;
        Rotation = new Vector3(0, 1, 0);   
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Rotation * rotationSpeed);
    }
}
