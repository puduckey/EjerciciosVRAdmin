using System;

[System.Serializable]
public class AsignacionRutina
{
    public string id;
    public Rutina rutina;
    public string fecha, hora;
    public int estado;
    public Paciente paciente;
    public string usuarioAsociado;

    public AsignacionRutina()
    {
        this.id = Guid.NewGuid().ToString();
    }

    public AsignacionRutina(string id, Rutina rutina, string fecha, string hora, int estado, Paciente paciente, string usuarioAsociado)
    {
        this.id = id;
        this.rutina = rutina;
        this.fecha = fecha;
        this.hora = hora;
        this.estado = estado;
        this.paciente = paciente;
        this.usuarioAsociado = usuarioAsociado;
    }

    public void AsignarRutina(Rutina rutina)
    {
        this.rutina = rutina;
    }

    /// <summary>
    /// 0 = pendiente
    /// 1 = realizado
    /// 2 = cancelado
    /// </summary>
    /// <param name="estado"></param>
    public void ActualizarEstado(int estado)
    {
        this.estado = estado;
        AppData.instance.ActualizarEstadoAsignacion(this, estado);
    }

    public Rutina LeerRutina()
    {
        return rutina;
    }
}
