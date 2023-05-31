using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paciente
{
    public string id;
    public Credenciales credenciales;
    public int rut;
    public string rut_dv;
    public string nombre, apellido, patologia, usuarioAsociado;

    public Paciente(string id, Credenciales credenciales, int rut, string rut_dv, 
        string nombre, string apellido, string patologia, string usuarioAsociado)
    {
        this.id = id;
        this.credenciales = credenciales;
        this.rut = rut;
        this.rut_dv = rut_dv;
        this.nombre = nombre;
        this.apellido = apellido;
        this.patologia = patologia;
        this.usuarioAsociado = usuarioAsociado;
    }
}
