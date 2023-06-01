using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Firestore;
using Firebase.Extensions;

public class Menu_IniciarSesion : MonoBehaviour
{
    FirebaseFirestore db;

    [SerializeField] TMP_InputField input_username, input_password;
    [SerializeField] TMP_Text txt_mensaje;


    public void ActivarUI()
    {
        this.gameObject.SetActive(true);
        input_username.text = "";
        input_password.text = "";
        txt_mensaje.text = "";
    }

    public async void IniciarSesionClick()
    {
        db = AppData.instance.db;

        string username = input_username.text;
        string password = input_password.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            txt_mensaje.text = "Por favor, completa todos los campos.";
            return;
        }

        DocumentReference docRef = db.Collection("credenciales").Document(username);

        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        if (snapshot.Exists)
        {
            Dictionary<string, object> credencialData = snapshot.ToDictionary();

            Credenciales credenciales = new Credenciales
            (
                credencialData["username"].ToString(),
                credencialData["password"].ToString(),
                credencialData["rol"].ToString()
            );

            bool resultado = credenciales.IniciarSesion(username, password);

            if (!resultado)
                txt_mensaje.text = "Error: datos incorrectos";
            else
            {
                if (credenciales.rol == "usuarioSalud")
                {
                    UsuarioSalud usuarioSalud = new UsuarioSalud(credenciales);
                    AppData.instance.usuarioSalud = usuarioSalud;

                    Interfaces.instance.menuPrincipal.ActivarUI();

                    Debug.Log("Usuario Salud identificado");
                }
                else if (credenciales.rol == "paciente")
                {
                    // Obtener la data del paciente
                    //
                    // Paciente paciente = new Paciente(...)
                    //
                    // 
                    Debug.Log("Usuario Paciente identificado");
                }

                this.gameObject.SetActive(false);
            }
        }
        else
        {
            txt_mensaje.text = "Error: usuario no encontrado";
        }
    }
}
