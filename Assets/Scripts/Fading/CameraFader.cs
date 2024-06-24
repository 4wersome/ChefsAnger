using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFader : MonoBehaviour {

    [SerializeField] private Transform lookAt;
    private ObjectFader fader;
    private GameObject player;
    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CapsuleCollider>().gameObject;
     
            lookAt = player.transform;
        
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (player) {
            Vector3 dir = lookAt.position - transform.position;
            Ray ray = new Ray(transform.position, dir);
#if DEBUG
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);   
#endif
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if(hit.collider == null) return;
                if(hit.collider.gameObject == player) {
                    if (fader != null) {
                        fader.DoFade = false;
                    }
                }
                else {
                    fader = hit.collider.gameObject.GetComponent<ObjectFader>();
                    if (fader) fader.DoFade = true;
                }
                
            }
        }
    }
}
