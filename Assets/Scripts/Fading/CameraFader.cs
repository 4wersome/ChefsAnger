using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFader : MonoBehaviour {
    private ObjectFader fader;

    private GameObject player;
    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (player) {
            Vector3 dir = player.transform.position - transform.position;
            Ray ray = new Ray(transform.position, dir);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if(hit.collider == null) return;
                if(hit.collider.gameObject == player) {
                    if (fader != null) fader.DoFade = false;
                }
                else {
                    fader = hit.collider.gameObject.GetComponent<ObjectFader>();
                    if (fader) fader.DoFade = true;
                }
                
            }
        }
    }
}
