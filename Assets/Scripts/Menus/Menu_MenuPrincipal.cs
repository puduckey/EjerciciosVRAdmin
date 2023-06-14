using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu_MenuPrincipal : MonoBehaviour
{
    [SerializeField] TMP_Text text_bienvenida;

    public void ActivarUI()
    {
        gameObject.SetActive(true);

        if (AppData.instance.usuarioSalud != null)
            BienvenidaUsuarioSalud();
        else if (AppData.instance.paciente != null)
            BienvenidaUsuarioPaciente();
    }

    void BienvenidaUsuarioSalud()
    {
        text_bienvenida.text = "Bienvenido " + AppData.instance.usuarioSalud.credenciales.username;
    }

    void BienvenidaUsuarioPaciente()
    {
        text_bienvenida.text = "Bienvenido " + AppData.instance.paciente.nombre + " " + AppData.instance.paciente.apellido;
    }

    public void InterfazGestionRutinas()
    {
        Interfaces.instance.menuGestionRutinas.ActivarUI();
    }

    public void InterfazGestionPacientes()
    {
        Interfaces.instance.menuGestionPacientes.ActivarUI();
    }

    public void InterfazAsignacionesDePaciente()
    {
        Interfaces.instance.menuHistorialRutinas.ActivarUI(AppData.instance.paciente);
    }


    public void InterfazInicioSesion()
    {
        Interfaces.instance.menuIniciarSesion.ActivarUI();
        gameObject.SetActive(false);
    }

    public void CerrarSesionPaciente()
    {
        AppData.instance.LimpiarInformacion();
        SceneManager.LoadScene("Menu");
    }
}
