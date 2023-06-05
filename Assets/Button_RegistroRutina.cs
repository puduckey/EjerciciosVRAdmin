using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Button_RegistroRutina : MonoBehaviour
{
    AsignacionRutina asignacionRutina;

    [SerializeField] TMP_Text text_nombre;
    [SerializeField] TMP_Text text_desc;
    [SerializeField] TMP_Text text_fecha;
    [SerializeField] TMP_Text text_estado;
    [SerializeField] Button btn_cancelar;

    public void ActualizarDatos(AsignacionRutina asignacionRutina)
    {
        string textoEstado = "";

        switch (asignacionRutina.estado)
        {
            case 0:
                textoEstado = "Pendiente";
                text_estado.color = Color.yellow;
                btn_cancelar.gameObject.SetActive(true);
                break;
            case 1:
                textoEstado = "Realizado";
                text_estado.color = Color.green;
                btn_cancelar.gameObject.SetActive(false);
                break;
            case 2:
                textoEstado = "Cancelado";
                text_estado.color = Color.red;
                btn_cancelar.gameObject.SetActive(false);
                break;
        }

        text_nombre.text = asignacionRutina.rutina.nombre;
        text_desc.text = asignacionRutina.rutina.descripcion;
        text_fecha.text = asignacionRutina.fecha + " " + asignacionRutina.hora;
        text_estado.text = textoEstado;
        this.asignacionRutina = asignacionRutina;
    }

    public void CancelarAsignacion()
    {
        Interfaces.instance.menuHistorialRutinas.CancelarAsignacionRutina(asignacionRutina);
    }
}
