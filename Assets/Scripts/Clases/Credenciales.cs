using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Credenciales
{
    public string username;
    private string password;
    public string rol;

    public Credenciales(string username, string password, string rol)
    {
        this.username = username;
        this.password = password;
        this.rol = rol;
    }

    public bool IniciarSesion(string username, string password)
    {
        if (!ValidarDatos(username, password))
            return false;

        if (rol == "usuarioSalud")
            Interfaces.instance.menuPrincipal.ActivarUI();

        return true;
    }

    public bool ValidarDatos(string username, string password)
    {
        if (username == this.username && password == this.password)
            return true;
        return false;
    }

    public void CrearCredenciales()
    {

    }

    public void ModificarCredenciales()
    {

    }
}
