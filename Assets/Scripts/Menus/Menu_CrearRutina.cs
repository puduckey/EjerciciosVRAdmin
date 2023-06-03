using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu_CrearRutina : MonoBehaviour
{
    public Rutina rutina;

    public GridLayoutGroup gridContent;
    public GameObject configEjercicioPrefab;
    public TMP_Text failureText;

    [Header("Menus")]
    [SerializeField] Menu_AniadirEjercicio menu_aniadirEjercicio;

    [Header("Inputs")]
    [SerializeField] TMP_InputField input_nombre;
    [SerializeField] TMP_InputField input_descripcion;

    [Header("Buttons")]
    [SerializeField] Button btn_eliminar;
    [SerializeField] Button btn_aniadirEj;
    [SerializeField] Button btn_crear;

    public void ActivarUICrear(Rutina rutina)
    {
        gameObject.SetActive(true);
        this.rutina = rutina;


        ActualizarUI(true);
    }

    public void ActivarUIActualizar(Rutina rutina)
    {
        gameObject.SetActive(true);
        this.rutina = rutina;

        input_nombre.text = rutina.nombre;
        input_descripcion.text = rutina.descripcion;
        failureText.text = "";

        ActualizarUI(false);
    }

    public void ActualizarUI(bool borrarCampos)
    {
        // limpia los campos
        if(borrarCampos)
        {
            input_nombre.text = "";
            input_descripcion.text = "";
            failureText.text = "";
        }

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
            var obj = Instantiate(configEjercicioPrefab, gridContent.transform);
            obj.GetComponent<Button_ConfigEjercicio>().ActualizarDatos(i + 1, list[i]);
        }

    }

    public void AniadirEjercicio()
    {
        ConfigEjercicio configEj = rutina.AgregarEjercicio();

        menu_aniadirEjercicio.ActivarUINuevo(configEj);

    }

    public void EditarEjercicio(ConfigEjercicio configEjercicio)
    {
        menu_aniadirEjercicio.ActivarUIEditar(configEjercicio);
    }

    public void EliminarEjercicio(ConfigEjercicio configEjercicio)
    {
        AppData.instance.EliminarConfigEjercicio(configEjercicio);
        ActualizarUI(false);
    }

    public void CrearRutina()
    {
        bool respuesta = rutina.ConfirmarRutina(input_nombre.text, input_descripcion.text);
        if (respuesta)
        {
            gameObject.SetActive(false);
            Interfaces.instance.menuGestionRutinas.ActivarUI();
        }
        else
        {
            failureText.text = "Porfavor, rellene todos los campos.";
        }
    }

}
