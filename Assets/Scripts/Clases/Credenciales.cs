using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

[System.Serializable]
public class Credenciales
{
    public string username;
    private string password;
    public string rol;

    public Credenciales() { }

    public Credenciales(string username, string password, string rol)
    {
        this.username = username;
        this.password = password;
        this.rol = rol;
    }

    public string GetPassword() { return password; }

    public bool IniciarSesion(string username, string password)
    {
        if (!ValidarDatos(username, password))
            return false;
        return true;
    }

    public bool ValidarDatos(string username, string password)
    {
        if (username == this.username && password == this.password)
            return true;
        return false;
    }

    public async Task<bool> CrearCredenciales(string username, string password, string confirmarPassword, string rol)
    {
        // validadores
        if (password != confirmarPassword) {
            Debug.Log("error en la password");
            return false;
        }

        // comprobar que el username no exista en la bd
        bool respuesta = await AppData.instance.ValidarUsername(username);

        if (!respuesta) {
            Debug.Log("error en el username");
            return false;
        }

        this.username = username;
        this.password = password;
        this.rol = rol;
        return true;
    }
}
