using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        Interfaces.instance.menuCrearRutina.ActivarUICrear(this);
    }

    public void ActualizarRutina()
    {
        Interfaces.instance.menuCrearRutina.ActivarUIActualizar(this);
    }

    public void EjecutarRutina()
    {
        List<ConfigEjercicio> configEjercicios = AppData.instance.BuscarConfigEjercicioList(this.id);
        AppData.instance.listaEjerciciosRealizar = configEjercicios;
        SceneManager.LoadScene("RutinaVR");
    }

    public ConfigEjercicio AgregarEjercicio()
    {
        ConfigEjercicio i = new ConfigEjercicio();
        i.CrearConfiguracion(this, AppData.instance.usuarioSalud);
        return i;
    }

    public bool ValidarRutina(string nombre, string descripcion)
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

        AppData.instance.RegistrarRutina(this);

        Debug.Log("Rutina creada");
        return true;
    }


    public Rutina SeleccionarRutina()
    {
        return this;
    }
}
