using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ballLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public float gravity;
    private Vector3 originalTransform;

    public bowlingController bC;

    public GameObject panel;

    public bool hasPlayed = false;
    public AudioSource hit;


    void FixedUpdate() {
        this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down * gravity 
            * this.gameObject.GetComponent<Rigidbody>().mass);
    }

    public void Start() {
        hit = GameObject.Find("hitSound").GetComponent<AudioSource>();
        originalTransform = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        bC = GameObject.Find("BowlingController").GetComponent<bowlingController>();
    }

   

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Bola_Reset") {
            GameObject[] balls = GameObject.FindGameObjectsWithTag("Bola");
            hasPlayed = !hasPlayed;
            foreach (var item in balls) {
                if (item == gameObject) {
                    item.transform.position = originalTransform;
                }
                DisableBalls(item);

            }
            StartCoroutine(Reiniciar());
        }

    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "currentPins") {
            if (!hasPlayed) {
                hit.Play();
                hasPlayed = !hasPlayed;
            }
        } 
    }

    IEnumerator Reiniciar() {
        yield return new WaitForSeconds(3);
        bC.ContadorDeCaidos();
        yield return new WaitForSeconds(1);

        GameObject[] balls = GameObject.FindGameObjectsWithTag("Bola");
        foreach (var item in balls) {

            EnableBalls(item);

        }

    }

    public void ShowData() {
        panel.SetActive(true);
    }

    public void HideData() {
        panel.SetActive(false);
    }

    public void HideBall() {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Bola");
        foreach (var item in balls) {
            if (item == gameObject) {
                continue;
            } 
            DisableBalls(item);

        }
    }

    public void ShowBall() {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Bola");
        
        foreach (var item in balls) {
            if (item == gameObject) {
                continue;
            }
            EnableBalls(item);

        }
    }

    private void DisableBalls(GameObject item) {
        item.GetComponent<XRGrabInteractable>().enabled = false;
        item.GetComponent<MeshRenderer>().enabled = false;
        item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
    }

    private void EnableBalls(GameObject item) {
        item.GetComponent<XRGrabInteractable>().enabled = true;
        item.GetComponent<MeshRenderer>().enabled = true;
        item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }

}
