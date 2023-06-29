using System;

[Serializable]
public class ConfigEjercicioEjec
{
    public string id;
    public int ejercicioId;
    public int repeticiones, duracion, series, descansoSeries, descanso;
    public string rutinaAsociadaID;
    public string usuarioAsociado;

    public ConfigEjercicioEjec(string id, int ejercicioId, int repeticiones, int duracion, int series, int descansoSeries, int descanso, string rutinaAsociadaID, string usuarioAsociado)
    {
        this.id = id;
        this.ejercicioId = ejercicioId;
        this.repeticiones = repeticiones;
        this.duracion = duracion;
        this.series = series;
        this.descansoSeries = descansoSeries;
        this.descanso = descanso;
        this.rutinaAsociadaID = rutinaAsociadaID;
        this.usuarioAsociado = usuarioAsociado;
    }
}
