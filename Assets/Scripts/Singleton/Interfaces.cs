using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interfaces : MonoBehaviour
{
    public static Interfaces instance;

    public Menu_SeleccionarEjercicio menuSeleccionarEjercicio;
    public Menu_AniadirEjercicio menuAniadirEjercicio;
    public Menu_CrearRutina menuCrearRutina;
    public Menu_GestionRutinas menuGestionRutinas;
    public Menu_IniciarSesion menu_iniciarSesion;
    public Menu_MenuPrincipal menuPrincipal;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }
}
