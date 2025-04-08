using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LifeBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void ChangeMaxLife(float vidaMax)
    {
        slider.maxValue = vidaMax;
    }

    public void ChangeActualLife(float cantidadVida)
    {
        slider.value = cantidadVida;
    }

    public void InicializeBar(float cantidadVid)
    {
        ChangeMaxLife(cantidadVid);
        ChangeActualLife(cantidadVid);
    }
}