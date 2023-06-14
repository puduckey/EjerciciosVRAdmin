using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu_HistorialRutinas : MonoBehaviour
{
    public Paciente paciente;
    public TMP_Text texto;
    public GameObject botonAsignacion;
    public GridLayoutGroup gridContent;

    public void ActivarUI(Paciente paciente)
    {
        gameObject.SetActive(true);
        this.paciente = paciente;
        texto.text = "Registro de " + paciente.nombre + " " + paciente.apellido;

        // limpia instancias de botones
        RectTransform[] objetosUI = gridContent.GetComponentsInChildren<RectTransform>(true);

        foreach (RectTransform objetoUI in objetosUI)
        {
            if (objetoUI.gameObject != gridContent.gameObject)
                Destroy(objetoUI.gameObject);
        }

        List<AsignacionRutina> asignacionRutinas = AppData.instance.ObtenerAsignacionRutinas(paciente);

        Debug.Log(asignacionRutinas);
        Debug.Log(asignacionRutinas.Count);

        DesplegarTodo(asignacionRutinas);
    }

    void DesplegarTodo(List<AsignacionRutina> asignacionRutinas)
    {
        foreach (AsignacionRutina asignacion in asignacionRutinas)
        {
            Debug.Log(asignacion);
            var i = Instantiate(botonAsignacion, gridContent.transform);
            i.GetComponent<Button_RegistroRutina>().ActualizarDatos(asignacion);
        }
    }

    public void RealizarRutina(Rutina rutina)
    {
        rutina.EjecutarRutina();
    }

    public void MostrarDetallesRutina(Rutina rutina)
    {
        Interfaces.instance.menuDetallesRutina.ActivarUI(rutina);
    }

    public void CancelarAsignacionRutina(AsignacionRutina asignacion)
    {
        asignacion.ActualizarEstado(2);
        ActivarUI(paciente); // refresca la interfaz
    }
}
