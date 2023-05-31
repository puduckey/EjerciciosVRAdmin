using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Credenciales
{
    public string username;
    private string password;
    public int rol; // rol: 0 = usuarioSalud | 1 = paciente

    public Credenciales(string username, string password, int rol)
    {
        this.username = username;
        this.password = password;
        this.rol = rol;
    }

    public void IniciarSesion()
    {

    }

    public void ValidarDatos()
    {

    }

    public void CrearCredenciales()
    {

    }

    public void ModificarCredenciales()
    {

    }
}
