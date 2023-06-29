using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SecuenciaRutina : MonoBehaviour
{
    [SerializeField] Animator animator;
    [Space(10)]
    [SerializeField] TextMesh text_contador;
    [SerializeField] Canvas ejercicioCanvas;
    [SerializeField] TMP_Text text_nombreEjercicio, text_descripcion, text_recomendaciones;
    [SerializeField] TMP_Text text_repeticiones, text_series;
    [SerializeField] Image imagen;

    public int esperaInicial;

    List<ConfigEjercicio> configEjercicios = new List<ConfigEjercicio>();

    // Test
    public List<ConfigEjercicio> test = new List<ConfigEjercicio>();

    private void Start()
    {
        // StartCoroutine(XRManager.instance.InitXR());

        ObtenerEjercicios(AppData.instance.listaEjerciciosRealizar);
        ComenzarRutina();
#if !UNITY_EDITOR
        GetComponent<LaunchVR>().TryLaunch();
#endif
    }

    public void ObtenerEjercicios(List<ConfigEjercicio> configEjercicios)
    {
        this.configEjercicios = configEjercicios;
    }

    public void ComenzarRutina()
    {
        StartCoroutine(Secuencia());
    }

    IEnumerator Secuencia() {
        // ESPERA INICIAL
        for (int i = 0; i < esperaInicial; i++)
        {
            text_contador.text = "Espera inicial: " + (esperaInicial - i);
            yield return new WaitForSeconds(1);
        }

        foreach (ConfigEjercicio configEjercicio in configEjercicios)
        {
            MostrarSiguienteEjercicio(configEjercicio);

            StartCoroutine(Temporizador("Comenzamos en: ", 15));
            yield return new WaitForSeconds(15);

            OcultarCanvas();
            animator.runtimeAnimatorController = configEjercicio.ejercicio.animacion;
            float tiempoRepeticion = configEjercicio.ejercicio.animClip.length;
            Debug.Log(tiempoRepeticion);

            for (int i = 0; i < configEjercicio.series; i++)
            {
                for (int j = 0; j < configEjercicio.repeticiones; j++)
                {
                    text_contador.text = "Repeticiones: " + (j + 1);
                    animator.SetTrigger("Repeticion");
                    yield return new WaitForSeconds(tiempoRepeticion);
                }

                if (i + 1 == configEjercicio.repeticiones)
                    break;

                StartCoroutine(Temporizador("Descanso: ", configEjercicio.descansoSeries));
                yield return new WaitForSeconds(configEjercicio.descansoSeries);
            }
            StartCoroutine(Temporizador("Descanso: ", configEjercicio.descanso));
            yield return new WaitForSeconds(configEjercicio.descanso);
        }
        // XRManager.instance.StopXR();
        SceneManager.LoadScene("MenuPaciente");
    }

    IEnumerator Temporizador(string texto, int segundos)
    {
        for (int i = 0; i < segundos; i++)
        {
            text_contador.text = texto + (segundos - i).ToString();
            yield return new WaitForSeconds(1);
        }
    }

    void MostrarSiguienteEjercicio(ConfigEjercicio cEjercicio)
    {
        ejercicioCanvas.enabled = true;
        text_nombreEjercicio.text = "Siguiente ejercicio: " + cEjercicio.ejercicio.nombre;
        text_descripcion.text = cEjercicio.ejercicio.descripcion;
        text_recomendaciones.text = cEjercicio.ejercicio.recomendaciones;
        imagen.sprite = cEjercicio.ejercicio.imagen;
        text_repeticiones.text = "Nº de repeticiones: " + cEjercicio.repeticiones.ToString();
        text_series.text = "Nº de series: " + cEjercicio.series.ToString();
    }

    void OcultarCanvas()
    {
        ejercicioCanvas.enabled = false;
    }
}
