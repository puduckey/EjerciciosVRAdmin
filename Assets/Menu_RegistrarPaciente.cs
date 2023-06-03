using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Menu_RegistrarPaciente : MonoBehaviour
{
    [SerializeField] TMP_InputField input_nombre;
    [SerializeField] TMP_InputField input_apellido;
    [SerializeField] TMP_InputField input_rut;
    [SerializeField] TMP_InputField input_rutDv;
    [SerializeField] TMP_InputField input_patologia;
    [SerializeField] TMP_InputField input_username;
    [SerializeField] TMP_InputField input_password;
    [SerializeField] TMP_InputField input_confirmPassword;

    [SerializeField] TMP_Text text_mensaje;


    public void ActivarUI()
    {
        gameObject.SetActive(true);

        input_nombre.text = "";
        input_apellido.text = "";
        input_rut.text = "";
        input_rutDv.text = "";
        input_patologia.text = "";
        input_username.text = "";
        input_password.text = "";
        input_confirmPassword.text = "";
        text_mensaje.text = "";
    }

    public void RegistrarPaciente()
    {
        // validar que los campos no esten vacios;
        if (string.IsNullOrEmpty(input_nombre.text) || string.IsNullOrEmpty(input_apellido.text) ||
            string.IsNullOrEmpty(input_rut.text) || string.IsNullOrEmpty(input_rutDv.text) ||
            string.IsNullOrEmpty(input_patologia.text) || string.IsNullOrEmpty(input_username.text) ||
            string.IsNullOrEmpty(input_password.text) || string.IsNullOrEmpty(input_confirmPassword.text))
        {
            text_mensaje.text = "Porfavor, llene todos los campos";
            return;
        }

        Credenciales newCredenciales = new Credenciales();
        bool respuesta = newCredenciales.CrearCredenciales(input_username.text, input_password.text, input_confirmPassword.text, "paciente");

        if (!respuesta)
        {
            text_mensaje.text = "Hubo un error al crear la credencial, ingrese otro nombre de usuario y valide la contraseña.";
            return;
        }

        Paciente newPaciente = new Paciente();
        respuesta = newPaciente.CrearPaciente(newCredenciales, Convert.ToInt32(input_rut.text), input_rutDv.text,
            input_nombre.text, input_apellido.text, input_patologia.text, AppData.instance.usuarioSalud.credenciales.username);

        if (!respuesta)
        {
            text_mensaje.text = "Se ha producido un error, compruebe los datos ingresados";
            return;
        }
        AppData.instance.RegistrarNuevoPaciente(newPaciente);
    }

    public void Salir()
    {
        gameObject.SetActive(false);
    }
}
