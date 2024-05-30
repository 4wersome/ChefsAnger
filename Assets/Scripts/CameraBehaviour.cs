using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;


    private Vector3 DistanceToPlayer;
    // Start is called before the first frame update

    void Start()
    {
        DistanceToPlayer = new Vector3(0, 10, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position+DistanceToPlayer;
    }
}
