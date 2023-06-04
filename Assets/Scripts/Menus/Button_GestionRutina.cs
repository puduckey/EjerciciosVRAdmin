using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Button_GestionRutina : MonoBehaviour
{
    [SerializeField] Rutina rutina;

    [SerializeField] TMP_Text text_nombre, text_desc;

    public void ActualizarDatosBoton(Rutina rutina)
    {
        text_nombre.text = rutina.nombre;
        text_desc.text = rutina.descripcion;

        this.rutina = rutina;
    }

    public void AsignarRutina()
    {
        Interfaces.instance.menuAsignarRutina.Asignar(rutina);
    }

    public void EditarDatos()
    {
        rutina.ActualizarRutina();
    }
}
