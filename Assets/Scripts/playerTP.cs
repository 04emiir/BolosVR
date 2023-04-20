using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTP : MonoBehaviour
{
    public Transform transformZone;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "tp_Inicio") {
            Vector3 gameZone = new Vector3(transformZone.position.x, transformZone.position.y, transformZone.position.z);
            gameObject.transform.position = gameZone;
        }
    }
}
