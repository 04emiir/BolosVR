using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class bowlingController : MonoBehaviour
{
    [HideInInspector] public List<GameObject> allOriginalPins = new List<GameObject>();
    [HideInInspector] public List<Vector3> allOriginalPinsPosition = new List<Vector3>();
    public GameObject bolosPrefab;
    private int contadorTirada;
    private int contador;
    private int puntos = 0;
    public TextMeshPro numberText;
    public TextMeshPro specialText;
    public AudioSource strike;
    // Start is called before the first frame update
    void Start()
    {
        contador = 0;
        contadorTirada = 0;
        Instantiate(bolosPrefab);
        foreach (var item in GameObject.FindGameObjectsWithTag("currentPins")) {
            allOriginalPins.Add(item);
        }
        foreach (var item in allOriginalPins) {
            allOriginalPinsPosition.Add(item.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void RecalculoOriginal() {
        allOriginalPinsPosition.Clear();
        allOriginalPins.Clear();
        foreach (var item in GameObject.FindGameObjectsWithTag("currentPins")) {
            allOriginalPins.Add(item);
        }
        foreach (var item in allOriginalPins) {
            allOriginalPinsPosition.Add(item.transform.position);
        }
    }

    public void ContadorDeCaidos() {
        contador = 0;
        int iterador = 0;

        List<GameObject> allPinsAfterThrow = new List<GameObject>(GameObject.FindGameObjectsWithTag("currentPins"));
        List<GameObject> toBeDeleted = new List<GameObject>();
        foreach (var pin in allPinsAfterThrow) {
            Vector3 actualPinPosition = pin.transform.position;
            float result_y = actualPinPosition.y - allOriginalPinsPosition[iterador].y;
            float result_x = actualPinPosition.x - allOriginalPinsPosition[iterador].x;
            float result_z = actualPinPosition.z - allOriginalPinsPosition[iterador].z;

            if (Mathf.Abs(result_y) > 0.2f && Mathf.Abs(result_x) > 0.2f && Mathf.Abs(result_z) > 0.2f)
            {
                toBeDeleted.Add(pin);
                contador++;
                
            }

            iterador++;
        }

        foreach (var pin in toBeDeleted) { 
            Destroy(pin.gameObject);
        }
        allPinsAfterThrow.Clear();
        toBeDeleted.Clear();


        puntos += contador;

        if (contadorTirada == 0) {
            if(contador == 10) {
                numberText.text = "10/10";
                specialText.text = "STRIKE";
                strike.Play();
                StartCoroutine(ResetearContador());
            } else {
                numberText.text = contador.ToString() + "/";
                contadorTirada++;
                RecalculoOriginal();
            }
            
        } else {
            numberText.text += contador.ToString();
            if (puntos == 10) {
                strike.Play();
                specialText.text = "SPARE";
            }
            StartCoroutine(ResetearContador());
        }
    }

    IEnumerator ResetearContador() {
        yield return new WaitForSeconds(2f);
        numberText.text = "/";
        specialText.text = "";
        contadorTirada = 0;
        puntos = 0;
        GameObject bolos = GameObject.FindGameObjectWithTag("pinsGroup");
        Destroy(bolos);
        Instantiate(bolosPrefab);
        RecalculoOriginal();
    }
}
