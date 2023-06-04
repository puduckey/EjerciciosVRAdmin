using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu_AsignarRutina : MonoBehaviour
{
    public Paciente pacienteSeleccionado;

    [SerializeField] GameObject botonRutinaPrefab;
    [SerializeField] TMP_Text text_title;
    [SerializeField] GridLayoutGroup gridContent;

    public void ActivarUI(Paciente paciente)
   {
        gameObject.SetActive(true);

        text_title.text = "Asignar rutina a " + paciente.nombre + " " + paciente.apellido;

        pacienteSeleccionado = paciente;

        // limpia instancias de botones
        RectTransform[] objetosUI = gridContent.GetComponentsInChildren<RectTransform>(true);

        foreach (RectTransform objetoUI in objetosUI)
        {
            if (objetoUI.gameObject != gridContent.gameObject)
                Destroy(objetoUI.gameObject);
        }

        List<Rutina> rutinas = AppData.instance.ObtenerRutinas();

        foreach (Rutina rutina in rutinas)
        {
            var i = Instantiate(botonRutinaPrefab, gridContent.transform);
            i.GetComponent<Button_GestionRutina>().ActualizarDatosBoton(rutina);
        }
    }

    public void Asignar(Rutina rutina)
    {
        pacienteSeleccionado.AsignarRutina(rutina);
        Interfaces.instance.menuGestionPacientes.Mensaje("Rutina " + rutina.nombre + 
            " asignada exitosamente a paciente " + pacienteSeleccionado.nombre + " " + pacienteSeleccionado.apellido);
        Salir();
    }

    public void Salir()
    {
        gameObject.SetActive(false);
    }
}
