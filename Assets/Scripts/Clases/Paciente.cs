using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

[System.Serializable]
public class Paciente
{
    public string id;
    public Credenciales credenciales;
    public int rut;
    public string rut_dv;
    public string nombre, apellido, patologia, usuarioAsociado;

    public Paciente()
    {
        this.id = Guid.NewGuid().ToString();
    }

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
    public bool CrearPaciente(Credenciales credenciales, int rut, string rut_dv,
        string nombre, string apellido, string patologia, string usuarioAsociado)
    {
        if (!ValidarDatos(rut, rut_dv))
            return false;

        this.credenciales = credenciales;
        this.rut = rut;
        this.rut_dv = rut_dv;
        this.nombre = nombre;
        this.apellido = apellido;
        this.patologia = patologia;
        this.usuarioAsociado = usuarioAsociado;

        return true;
    }

    public void ModificarPaciente()
    {

    }

    public void AsignarRutina()
    {

    }

    public void DesplegarHistorial()
    {

    }

    public void ConfirmarAsignacionRutina()
    {

    }

    public bool ValidarDatos(int rut, string rut_dv)
    {
        // Valida el rut
        if (!ValidarRut(rut, rut_dv))
            return false;
        return true;
    }

    public static bool ValidarRut(int rutNumeros, string rutDV)
    {
        string rutCompleto = rutNumeros.ToString() + "-" + rutDV;
        return ValidarRut(rutCompleto);
    }

    public static bool ValidarRut(string rut)
    {
        rut = rut.Replace(".", "").Replace("-", ""); // Remover puntos y guiones

        if (rut.Length < 2)
        {
            return false;
        }

        int cuerpo;
        if (!int.TryParse(rut.Substring(0, rut.Length - 1), out cuerpo))
        {
            return false;
        }

        string dv = rut[rut.Length - 1].ToString().ToUpper();

        int suma = 0;
        int multiplicador = 1;

        while (cuerpo != 0)
        {
            multiplicador++;
            if (multiplicador == 8)
            {
                multiplicador = 2;
            }

            suma += (cuerpo % 10) * multiplicador;
            cuerpo /= 10;
        }

        int resto = suma % 11;
        int digitoVerificador = 11 - resto;
        string dvCalculado = digitoVerificador == 11 ? "0" : (digitoVerificador == 10 ? "K" : digitoVerificador.ToString());

        return dv == dvCalculado;
    }

    public void DesplegarInterfaz()
    {

    }
}
