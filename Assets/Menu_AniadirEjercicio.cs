using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu_AniadirEjercicio : MonoBehaviour
{
    public ConfigEjercicio configEjercicio;

    public TMP_Text ejercicioSelectedText;
    public TMP_Text alertaText;

    [Header("Inputs")]
    public Ejercicio ejercicioSelected;
    [SerializeField] TMP_InputField input_repeticiones;
    [SerializeField] TMP_InputField input_series;
    [SerializeField] TMP_InputField input_descansoSeries;
    [SerializeField] TMP_InputField input_descanso;

    [Header("Menus")]
    public Menu_SeleccionarEjercicio menu_seleccionarEjercicio;
    public Menu_CrearRutina menu_crearRutina;

    public void ActivarUINuevo(ConfigEjercicio confg)
    {
        gameObject.SetActive(true);

        configEjercicio = confg;

        // limpia los campos
        AsignarEjercicio(null);
        input_repeticiones.text = "";
        input_series.text = "";
        input_descansoSeries.text = "";
        input_descanso.text = "";

        Alerta("", Color.white);
    }

    public void ActivarUIEditar(ConfigEjercicio confg)
    {
        gameObject.SetActive(true);

        configEjercicio = confg;

        // actualiza los campos
        AsignarEjercicio(confg.ejercicio);
        input_repeticiones.text = confg.repeticiones.ToString();
        input_series.text = confg.series.ToString();
        input_descansoSeries.text = confg.descansoSeries.ToString();
        input_descanso.text = confg.descanso.ToString();

        Alerta("", Color.white);
    }


    public void SeleccionarEjercicio()
    {
        menu_seleccionarEjercicio.ActivarUI();
    }

    public void AsignarEjercicio(Ejercicio e)
    {
        if (e == null)
        {
            ejercicioSelectedText.text = "No seleccionado";
            return;
        }

        ejercicioSelected = e;
        ejercicioSelectedText.text = e.nombre;
    }

    public void AceptarConfiguracion()
    {
        int repeticiones, series, descansoSeries, descanso;

        // validacion y conversion
        if (ejercicioSelected == null)
        {
            Debug.Log("Seleccione un ejercicio");
            Alerta("Seleccione un ejercicio", Color.red);
            return;
        }

        if (int.TryParse(input_repeticiones.text, out repeticiones) &&
            int.TryParse(input_series.text, out series) &&
            int.TryParse(input_descansoSeries.text, out descansoSeries) &&
            int.TryParse(input_descanso.text, out descanso))
        {
            // Las conversiones fueron exitosas, los valores se asignaron correctamente a las variables

            if (repeticiones <= 0 || series <= 0 || descansoSeries <= 0 || descanso <= 0)
            {
                Debug.Log("Al menos uno de los valores ingresados no es un número válido.");
                Alerta("Al menos uno de los valores ingresados no es un número válido.", Color.red);
                return; 
            }
        }
        else
        {
            // Al menos una de las conversiones falló, uno o más campos de entrada no contienen números enteros válidos
            Debug.Log("Al menos uno de los valores ingresados no es un número entero válido.");
            Alerta("Al menos uno de los valores ingresados no es un número válido.", Color.red);
            return;
        }

        // asinacion
        configEjercicio.ConfigurarEjercicio(ejercicioSelected, repeticiones, 0, series, descansoSeries, descanso);
        configEjercicio.AgregarEjercicio();

        // retornar al menu
        RetonarMenuCrearRutina();

        Debug.Log("Configuracion realizada con exito");
        Alerta("Configuracion realizada con exito", Color.green);
    }

    public void RetonarMenuCrearRutina()
    {
        menu_crearRutina.ActualizarUI(false);
        gameObject.SetActive(false);
    }

    private void Alerta(string msg, Color color)
    {
        alertaText.text = msg;
        alertaText.color = color;
    }
}
