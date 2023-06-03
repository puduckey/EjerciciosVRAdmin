using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Button_Ejercicio : MonoBehaviour
{
    public Ejercicio ejercicio;

    public Image img;
    public TMP_Text text;

    public void SetEjercicio(Ejercicio ej)
    {
        ejercicio = ej;
        img.sprite = ejercicio.imagen;
        text.text = ejercicio.nombre;
    }

    public void ShowDetails()
    {
        Interfaces.instance.menuSeleccionarEjercicio.ShowEjercicioDetails(ejercicio);
    }
}
