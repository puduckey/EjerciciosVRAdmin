using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Button_Paciente : MonoBehaviour
{
    public Paciente paciente;

    public TMP_Text text_indentificacion, text_patologia;

    public void SetPaciente(Paciente paciente)
    {
        this.paciente = paciente;
        text_indentificacion.text = paciente.nombre + " " +
            paciente.apellido + " " + paciente.rut + "-" + paciente.rut_dv;
    }
    public void AsignarRutina()
    {
        paciente.AsignarRutina();
    }

    public void VerHistorial()
    {
        paciente.DesplegarHistorial();
    }

    public void EditarDatos()
    {
        paciente.ModificarPaciente();
    }


}
