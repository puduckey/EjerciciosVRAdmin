using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interfaces : MonoBehaviour
{
    public static Interfaces instance;

    public Menu_SeleccionarEjercicio menuSeleccionarEjercicio;
    public Menu_AniadirEjercicio menuAniadirEjercicio;
    public Menu_CrearRutina menuCrearRutina;
    public Menu_GestionRutinas menuGestionRutinas;

    public Menu_GestionPacientes menuGestionPacientes;
    public Menu_RegistrarPaciente menuRegistrarPaciente;
    public Menu_AsignarRutina menuAsignarRutina;
    public Menu_HistorialRutinas menuHistorialRutinas;
    public Menu_DetallesRutina menuDetallesRutina;

    public Menu_IniciarSesion menuIniciarSesion;
    public Menu_MenuPrincipal menuPrincipal;

    private void Start()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    public void CargarMenuPaciente()
    {
        SceneManager.LoadScene("MenuPaciente");
    }
}
