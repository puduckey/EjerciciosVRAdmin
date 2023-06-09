using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class ConfigEjercicio
{
    public string id;
    public Ejercicio ejercicio;
    public int repeticiones, duracion, series, descansoSeries, descanso;
    public string rutinaAsociadaID;
    public string usuarioAsociado;
    public int posicion;

    public ConfigEjercicio()
    {
        id = Guid.NewGuid().ToString();
    }

    public ConfigEjercicio(string id)
    {
        this.id = id;
    }

    public ConfigEjercicio(string id, Ejercicio ejercicio, int repeticiones, 
        int duracion, int series, int descansoSeries, int descanso, 
        string rutinaAsociadaID, string usuarioAsociado, int posicion)
    {
        this.id = id;
        this.ejercicio = ejercicio;
        this.repeticiones = repeticiones;
        this.duracion = duracion;
        this.series = series;
        this.descansoSeries = descansoSeries;
        this.descanso = descanso;
        this.rutinaAsociadaID = rutinaAsociadaID;
        this.usuarioAsociado = usuarioAsociado;
        this.posicion = posicion;
    }

    public void CrearConfiguracion(Rutina rutinaAsociada, UsuarioSalud usuario)
    {
        this.rutinaAsociadaID = rutinaAsociada.id;
        this.usuarioAsociado = usuario.credenciales.username;
    }

    public void ConfigurarEjercicio(Ejercicio ejercicio, int repeticiones, 
        int duracion, int series, int descansoSeries, int descanso)
    {
        this.ejercicio = ejercicio;
        this.repeticiones = repeticiones;
        this.duracion = duracion;
        this.series = series;
        this.descansoSeries = descansoSeries;
        this.descanso = descanso;
    }

    public void AgregarEjercicio()
    {
        AppData.instance.RegistrarConfigEjercicio(this);
    }
}
