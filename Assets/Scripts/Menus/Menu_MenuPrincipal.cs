using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu_MenuPrincipal : MonoBehaviour
{
    [SerializeField] TMP_Text text_bienvenida;

    public void ActivarUI()
    {
        gameObject.SetActive(true);
        AppData.instance.CapturaDatosBDUsuarioSalud(AppData.instance.usuarioSalud);

        text_bienvenida.text = "Bienvenido " + AppData.instance.usuarioSalud.credenciales.username;
    }

    public void InterfazGestionRutinas()
    {
        Interfaces.instance.menuGestionRutinas.ActivarUI();
    }

    public void InterfazGestionPacientes()
    {
        Interfaces.instance.menuGestionPacientes.ActivarUI();
    }

    public void InterfazInicioSesion()
    {
        Interfaces.instance.menuIniciarSesion.ActivarUI();
        gameObject.SetActive(false);
    }
}
