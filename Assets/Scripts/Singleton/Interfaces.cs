using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interfaces : MonoBehaviour
{
    public static Interfaces instance;

    public Menu_SeleccionarEjercicio seleccionarEjercicio;
    public Menu_AniadirEjercicio aniadirEjercicio;
    public Menu_CrearRutina crearRutina;
    public Menu_GestionRutinas gestionRutinas;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }
}
