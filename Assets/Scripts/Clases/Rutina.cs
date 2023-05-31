using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Rutina
{
    public string id;
    public string nombre, descripcion;
    public string usuarioAsociado;

    public Rutina()
    {
        id = Guid.NewGuid().ToString();
    }

    public Rutina(string id, string nombre, string descripcion, string usuarioAsociado)
    {
        this.id = id;
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.usuarioAsociado = usuarioAsociado;
    }

    public void CrearRutina()
    {
        Interfaces.instance.crearRutina.ActivarUICrear(this);
    }

    public void ActualizarRutina()
    {
        Interfaces.instance.crearRutina.ActivarUIActualizar(this);
    }

    public ConfigEjercicio AgregarEjercicio()
    {
        ConfigEjercicio i = new ConfigEjercicio();
        i.CrearConfiguracion(this, AppData.instance.usuarioSalud);
        return i;
    }

    public bool ConfirmarRutina(string nombre, string descripcion)
    {
        // comprueba que todos los campos esten llenos
        if(string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(descripcion))
            return false;

        // comprueba que al menos exita un configejercicio asociado
        if (!AppData.instance.configEjercicios.Exists(obj => obj.rutinaAsociadaID == this.id))
            return false;

        this.nombre = nombre;
        this.descripcion = descripcion;
        this.usuarioAsociado = AppData.instance.usuarioSalud.credenciales.username;

        AppData.instance.AgregarRutina(this);

        Debug.Log("Rutina creada");
        return true;
    }

}
