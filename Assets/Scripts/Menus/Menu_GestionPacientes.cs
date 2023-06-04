using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu_GestionPacientes : MonoBehaviour
{
    public GameObject botonPacientePrefab;
    public GridLayoutGroup gridContent;
    public TMP_Text text_mensaje;

    public void ActivarUI()
    {
        gameObject.SetActive(true);
        text_mensaje.text = "";

        // limpia instancias de botones
        RectTransform[] objetosUI = gridContent.GetComponentsInChildren<RectTransform>(true);

        foreach (RectTransform objetoUI in objetosUI)
        {
            if (objetoUI.gameObject != gridContent.gameObject)
                Destroy(objetoUI.gameObject);
        }

        List<Paciente> pacientes = AppData.instance.ObtenerPacientes();

        foreach (Paciente paciente in pacientes)
        {
            var i = Instantiate(botonPacientePrefab, gridContent.transform);
            i.GetComponent<Button_GestionPaciente>().ActualizarDatosBoton(paciente);
        }
    }

    public void RegistrarNuevoPaciente()
    {
        Interfaces.instance.menuRegistrarPaciente.ActivarUI(null);
    }

    public void Mensaje(string texto)
    {
        text_mensaje.text = texto;
    }

    public void Salir()
    {
        gameObject.SetActive(false);
    }
}
