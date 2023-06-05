using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_HistorialRutinas : MonoBehaviour
{
    public Paciente paciente;
    public GameObject botonAsignacion;
    public GridLayoutGroup gridContent;

    public void ActivarUI(Paciente paciente)
    {
        gameObject.SetActive(true);
        this.paciente = paciente;

        // limpia instancias de botones
        RectTransform[] objetosUI = gridContent.GetComponentsInChildren<RectTransform>(true);

        foreach (RectTransform objetoUI in objetosUI)
        {
            if (objetoUI.gameObject != gridContent.gameObject)
                Destroy(objetoUI.gameObject);
        }

        List<AsignacionRutina> asignacionRutinas = AppData.instance.ObtenerAsignacionRutinas(paciente);

        foreach (AsignacionRutina asignacion in asignacionRutinas)
        {
            var i = Instantiate(botonAsignacion, gridContent.transform);
            i.GetComponent<Button_RegistroRutina>().ActualizarDatos(asignacion);
        }
    }

    public void CancelarAsignacionRutina(AsignacionRutina asignacion)
    {
        asignacion.ActualizarEstado(2);
        ActivarUI(paciente); // refresca la interfaz
    }
}
