using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Button_ConfigEjercicio : MonoBehaviour
{
    [SerializeField] ConfigEjercicio configEjercicio;

    [Header("UI")]
    [SerializeField] TMP_Text text_num;
    [SerializeField] Image image;
    [SerializeField] TMP_Text text_nombre;
    [SerializeField] TMP_Text text_rep;
    [SerializeField] TMP_Text text_series;
    [SerializeField] TMP_Text text_descansoSeries;
    [SerializeField] TMP_Text text_descanso;

    public void ActualizarDatos(int num, ConfigEjercicio config)
    {
        text_num.text = num.ToString();
        image.sprite = config.ejercicio.imagen;
        text_nombre.text = config.ejercicio.nombre;
        text_rep.text = config.repeticiones.ToString() + " rep.";
        text_series.text = config.series.ToString();
        text_descansoSeries.text = config.descansoSeries.ToString() + " seg.";
        text_descanso.text = config.descanso.ToString() + " seg.";

        configEjercicio = config;
    }
    public void Editar()
    {
        FindObjectOfType<Menu_CrearRutina>().EditarEjercicio(configEjercicio);
    }

    public void Eliminar()
    {
        FindObjectOfType<Menu_CrearRutina>().EliminarEjercicio(configEjercicio);
    }
}
