using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_MenuPrincipal : MonoBehaviour
{
    public void ActivarUI()
    {
        gameObject.SetActive(true);
    }

    public void InterfazGestionRutinas()
    {
        Interfaces.instance.menuGestionRutinas.ActivarUI();
    }

    public void InterfazGestionPacientes()
    {
        // Interfaces.instance.menuGestionPacientes.ActivarUI();
    }

    public void InterfazInicioSesion()
    {
        Interfaces.instance.menu_iniciarSesion.ActivarUI();
    }
}
