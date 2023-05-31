using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu_GestionRutinas : MonoBehaviour
{
    public GameObject botonRutinaPrefab;
    public GridLayoutGroup gridContent;

    private void Start()
    {
        ActivarUI();
    }

    public void ActivarUI()
    {
        gameObject.SetActive(true);

        // limpia instancias de botones
        RectTransform[] objetosUI = gridContent.GetComponentsInChildren<RectTransform>(true);

        foreach (RectTransform objetoUI in objetosUI)
        {
            if (objetoUI.gameObject != gridContent.gameObject)
                Destroy(objetoUI.gameObject);
        }

        List<Rutina> rutinas = AppData.instance.ObtenerRutinas();

        foreach(Rutina rutina in rutinas)
        {
            var i = Instantiate(botonRutinaPrefab, gridContent.transform);
            i.GetComponent<Button_GestionRutina>().ActualizarDatosBoton(rutina);
        }
    }

    public void CrearNuevaRutina()
    {
        Rutina newRutina = new Rutina();
        newRutina.CrearRutina();
    }

    public void Salir()
    {
        gameObject.SetActive(false);
    }
}
