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

    TMP_InputField input_username, input_password;
    TMP_Text txt_mensaje;

    void Start()
    {
        db = AppData.instance.db;
    }

    public void ActivarUI()
    {
        this.gameObject.SetActive(true);
        input_username.text = "";
        input_password.text = "";
        txt_mensaje.text = "";
    }

    public void IniciarSesionClick()
    {
        string username = input_username.text;
        string password = input_password.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            txt_mensaje.text = "Por favor, completa todos los campos.";
            return;
        }

        DocumentReference docRef = db.Collection("credenciales").Document(username);

        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {

            DocumentSnapshot snapshot = task.Result;

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
                    this.gameObject.SetActive(false);
            }
            else {
                txt_mensaje.text = "Error: usuario no encontrado";
            }
        });
    }
}
