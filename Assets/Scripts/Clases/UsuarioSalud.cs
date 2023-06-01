using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UsuarioSalud
{
    public Credenciales credenciales;

    public UsuarioSalud(Credenciales credenciales)
    {
        this.credenciales = credenciales;
    }

    public void DesplegarInterfaz()
    {
        Interfaces.instance.menuPrincipal.ActivarUI();
    }

    public void ConfirmarRutina()
    {

    }

    public void ConfirmarAsignacionRutina()
    {

    }

    public void ConfirmarPaciente()
    {

    }
}
