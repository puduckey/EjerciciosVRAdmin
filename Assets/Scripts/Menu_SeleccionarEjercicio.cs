using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class Menu_SeleccionarEjercicio : MonoBehaviour
{
    public Ejercicio ejercicioSelected;

    public GameObject ejercicioPrefab;
    public GridLayoutGroup gridLayout;

    [Header("Detalles")]
    [SerializeField] TMP_Text nombre;
    [SerializeField] TMP_Text desc, tipo, nivel, equipo, recom;
    [SerializeField] Image image;


    [Header("Menus")]
    public Menu_AniadirEjercicio Menu_ani;

    void Start()
    {
        List<Ejercicio> ejercicios = AppData.instance.ejercicios;

        foreach(Ejercicio ejercicio in ejercicios)
        {
            var obj = Instantiate(ejercicioPrefab, gridLayout.transform);
            obj.GetComponent<Button_Ejercicio>().SetEjercicio(ejercicio);
        }

        ShowEjercicioDetails(ejercicios[0]);
    }

    public void ActivarUI()
    {
        gameObject.SetActive(true);
    }

    public void ShowEjercicioDetails(Ejercicio ejercicio)
    {
        nombre.text = ejercicio.nombre;
        desc.text = ejercicio.descripcion;
        tipo.text = ejercicio.tipo;
        nivel.text = ejercicio.nivel;
        equipo.text = ejercicio.equipo;
        recom.text = ejercicio.recomendaciones;
        image.sprite = ejercicio.imagen;

        ejercicioSelected = ejercicio;
    }

    public void Confirmar()
    {
        Menu_ani.AsignarEjercicio(ejercicioSelected);
        gameObject.SetActive(false);
    }
}
