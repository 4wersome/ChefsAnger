using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private Vector3 DistanceToPlayer;
    // Start is called before the first frame update

    void Start()
    {
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position+DistanceToPlayer;
    }
}
