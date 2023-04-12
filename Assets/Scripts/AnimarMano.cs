using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimarMano : MonoBehaviour
{
    public InputActionProperty pincharAnimacion;
    public InputActionProperty agarrarAnimacion;
    public Animator animacion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float valuePinchar = pincharAnimacion.action.ReadValue<float>();
        animacion.SetFloat("Trigger", valuePinchar);

        float valueAgarrar = agarrarAnimacion.action.ReadValue<float>();
        animacion.SetFloat("Grip", valueAgarrar);
    }
}
