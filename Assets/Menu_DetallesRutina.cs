using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu_DetallesRutina : MonoBehaviour
{
    public Rutina rutina;

    public GridLayoutGroup gridContent;
    public GameObject ejercicioPrefab;
    public TMP_Text failureText;

    [Header("Text")]
    [SerializeField] TMP_Text text_nombre;
    [SerializeField] TMP_Text text_descripcion;

    public void ActivarUI(Rutina rutina)
    {
        gameObject.SetActive(true);
        this.rutina = rutina;

        text_nombre.text = rutina.nombre;
        text_descripcion.text = rutina.descripcion;
        failureText.text = "";

        ListarEjercicios();
    }

    public void ListarEjercicios()
    {
        // limpia las instancias
        RectTransform[] objetosUI = gridContent.GetComponentsInChildren<RectTransform>(true);

        foreach (RectTransform objetoUI in objetosUI)
        {
            if (objetoUI.gameObject != gridContent.gameObject)
                Destroy(objetoUI.gameObject);
        }

        // actualiza el listado de configEjercicios asociados a la rutina
        List<ConfigEjercicio> list = AppData.instance.BuscarConfigEjercicioList(rutina.id);

        if (list.Count == 0)
            return;

        for (int i = 0; i < list.Count; i++)
        {
            var obj = Instantiate(ejercicioPrefab, gridContent.transform);
            obj.GetComponent<Button_ConfigEjercicio>().ActualizarDatos(i + 1, list[i]);
        }
    }
}
