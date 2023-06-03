using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Threading.Tasks;

public class AppData : MonoBehaviour
{
    FirebaseApp firebaseApp;
    public FirebaseFirestore db;

    public static AppData instance;

    // Usuario
    public UsuarioSalud usuarioSalud;
    public Paciente paciente;

    // Listados de objetos almacenados en cache
    public List<Paciente> pacientesUsuario = new List<Paciente>();
    public List<ConfigEjercicio> configEjercicios = new List<ConfigEjercicio>();
    public List<Rutina> rutinas = new List<Rutina>();
    public List<Ejercicio> ejercicios = new List<Ejercicio>();

    public static AppData Instance
    {
        get
        {
            // Si no hay una instancia existente, crea una nueva
            if (instance == null)
            {
                GameObject singletonObject = new GameObject();
                instance = singletonObject.AddComponent<AppData>();
                singletonObject.name = typeof(AppData).ToString() + " (Singleton)";

                // Asegura que el objeto no se destruya al cargar nuevas escenas
                DontDestroyOnLoad(singletonObject);
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        // Obtiene la instancia de la base de datos de FirebaseFirestore
        db = FirebaseFirestore.DefaultInstance;

        // Solo para probar, este metodo debe llamarse una vez el usuario se autentifique
        CapturaDatosBDUsuarioSalud(usuarioSalud);
    }

    public void LimpiarInformacion()
    {
        usuarioSalud = null;
        paciente = null;
        pacientesUsuario.Clear();
        rutinas.Clear();
        configEjercicios.Clear();
    }

    public async void CapturaDatosBDUsuarioSalud(UsuarioSalud usuario)
    {
        // LimpiarDatos
        LimpiarInformacion();

        // AsignarUsuario
        usuarioSalud = usuario;

        // Captura de pacientes asociados
        QuerySnapshot querypaciente = await db.Collection("paciente").WhereEqualTo("usuarioAsociado", usuario.credenciales.username).GetSnapshotAsync();

        foreach (DocumentSnapshot documentSnapshot in querypaciente.Documents)
        {
            Dictionary<string, object> pacienteData = documentSnapshot.ToDictionary();

            Credenciales credencial = await ObtenerCredencial(pacienteData["credencialUsername"].ToString());

            Debug.Log(credencial);

            Paciente obj = new Paciente
            (
                pacienteData["id"].ToString(),
                credencial,
                Convert.ToInt32(pacienteData["rut"]),
                pacienteData["rut_dv"].ToString(),
                pacienteData["nombre"].ToString(),
                pacienteData["apellido"].ToString(),
                pacienteData["patologia"].ToString(),
                pacienteData["usuarioAsociado"].ToString()
            );
            pacientesUsuario.Add(obj);
        }

        // Captura datos de rutinas asociadas 
        QuerySnapshot queryrutina = await db.Collection("rutina").WhereEqualTo("usuarioAsociado", usuario.credenciales.username).GetSnapshotAsync();
        
        foreach (DocumentSnapshot documentSnapshot in queryrutina.Documents)
        {
            Dictionary<string, object> rutinaData = documentSnapshot.ToDictionary();
        
            Rutina obj = new Rutina
            (
                rutinaData["id"].ToString(),
                rutinaData["nombre"].ToString(),
                rutinaData["descripcion"].ToString(),
                rutinaData["usuarioAsociado"].ToString()
            );

            rutinas.Add(obj);
        }

        // Captura datos de los configEjercicio asociadas
        QuerySnapshot queryConfigEjercicio = await db.Collection("configEjercicio").WhereEqualTo("usuarioAsociado", usuario.credenciales.username).GetSnapshotAsync();

        foreach (DocumentSnapshot documentSnapshot in queryConfigEjercicio.Documents)
        {
            Dictionary<string, object> configEjercicioData = documentSnapshot.ToDictionary();

            ConfigEjercicio obj = new ConfigEjercicio
            (
                configEjercicioData["id"].ToString(),
                BuscarEjercicio(Convert.ToInt32(configEjercicioData["ejercicioID"])),
                Convert.ToInt32(configEjercicioData["repeticiones"]),
                Convert.ToInt32(configEjercicioData["duracion"]),
                Convert.ToInt32(configEjercicioData["series"]),
                Convert.ToInt32(configEjercicioData["descansoSeries"]),
                Convert.ToInt32(configEjercicioData["descanso"]),
                configEjercicioData["rutinaAsociadaID"].ToString(),
                configEjercicioData["usuarioAsociado"].ToString(),
                Convert.ToInt32(configEjercicioData["posicion"])
            );

            configEjercicios.Add(obj);
        }
    }

    public Ejercicio BuscarEjercicio(int id)
    {
        foreach(Ejercicio ejercicio in ejercicios)
        {
            if (ejercicio.id == id)
            {
                return ejercicio;
            }
        }

        return null;
    }

    public void AgregarConfigEjercicio(ConfigEjercicio config)
    {
        ConfigEjercicio aux = configEjercicios.Find(obj => obj.id == config.id);

        if (aux != null) // comprueba que el objeto no exista
        {
            // si existe, actualiza el objeto
            aux.ejercicio = config.ejercicio;
            aux.repeticiones = config.repeticiones;
            aux.duracion = config.duracion;
            aux.series = config.series;
            aux.descansoSeries = config.descansoSeries;
            aux.descanso = config.descanso;
        }
        else
        {
            // si no existe, lo agrega a la lista
            configEjercicios.Add(config);
            config.posicion = configEjercicios.IndexOf(config);
        }

        // Insert-Update a base de datos
        Dictionary<string, object> configEjercicioData = new Dictionary<string, object>
        {
            { "id", config.id },
            { "ejercicioID", config.ejercicio.id },
            { "repeticiones", config.repeticiones },
            { "duracion", config.duracion },
            { "series", config.series },
            { "descansoSeries", config.descansoSeries },
            { "descanso", config.descanso },
            { "rutinaAsociadaID", config.rutinaAsociadaID },
            { "usuarioAsociado", config.usuarioAsociado },
            { "posicion", config.posicion }
        };

        DocumentReference docRef = db.Collection("configEjercicio").Document(config.id);

        docRef.SetAsync(configEjercicioData).ContinueWithOnMainThread(task =>
        {
            Debug.Log("ConfigEjercicio guardado en DB");
        });
    }

    public void EliminarConfigEjercicio(ConfigEjercicio config)
    {
        if (configEjercicios.Contains(config))
            configEjercicios.Remove(config);

        // Delete a base de datos
        DocumentReference docRef = db.Collection("configEjercicio").Document(config.id);
        docRef.DeleteAsync();
    }

    public List<Rutina> ObtenerRutinas()
    {
        List<Rutina> rutinasAsociadas = rutinas.FindAll
            (obj => obj.usuarioAsociado == usuarioSalud.credenciales.username);
        return rutinasAsociadas;
    }

    public void AgregarRutina(Rutina rutina)
    {
        Rutina aux = rutinas.Find(obj => obj.id == rutina.id);

        if(aux != null)
        {
            aux.nombre = rutina.nombre;
            aux.descripcion = rutina.descripcion;
            ReordenarPosicionesConfigEjercicios(rutina.id); // reordena las posiciones de las config guardadas
        }
        else
        {
            rutinas.Add(rutina);
        }

        // Insert-Update a base de datos
        Dictionary<string, object> rutinaData = new Dictionary<string, object>
        {
            { "id", rutina.id },
            { "nombre", rutina.nombre },
            { "descripcion", rutina.descripcion },
            { "usuarioAsociado", rutina.usuarioAsociado }
        };

        DocumentReference docRef = db.Collection("rutina").Document(rutina.id);
        docRef.SetAsync(rutinaData).ContinueWithOnMainThread(task =>
        {
            Debug.Log("ConfigEjercicio guardado en DB");
        });
    }

    public void EliminarRutina(Rutina rutina)
    {
        // Eliminar la rutina de la lista
        Rutina aux = rutinas.Find(obj => obj.id == rutina.id);
        rutinas.Remove(rutina);

        // Eliminar la rutina de la BD
        DocumentReference docRef = db.Collection("rutina").Document(rutina.id);
        docRef.DeleteAsync();

        // Obtener las configEjercicio asociadas
        List<ConfigEjercicio> configEjlist = configEjercicios.FindAll(obj => obj.rutinaAsociadaID == rutina.id);

        // Recorrer el listado de las configEjercicio asociadas y borrar
        foreach (ConfigEjercicio configEj in configEjlist)
        {
            EliminarConfigEjercicio(configEj);
        }
    }

    public List<ConfigEjercicio> BuscarConfigEjercicioList(string rutinaID)
    {
        // Función para obtener las configEjercicio filtrados por ID de rutina
        List<ConfigEjercicio> list = configEjercicios.FindAll(obj => obj.rutinaAsociadaID == rutinaID);

        list.Sort((x, y) => x.posicion.CompareTo(y.posicion));

        return list;
    }

    public void ReordenarPosicionesConfigEjercicios(string rutinaID)
    {
        List<ConfigEjercicio> list = configEjercicios.FindAll(obj => obj.rutinaAsociadaID == rutinaID);

        for (int i = 0; i < list.Count; i++)
        {
            list[i].posicion = i;

            Dictionary<string, object> configEjercicioData = new Dictionary<string, object>
            {
                { "posicion", list[i].posicion }
            };

            DocumentReference docRef = db.Collection("configEjercicio").Document(list[i].id);
            docRef.UpdateAsync(configEjercicioData).ContinueWithOnMainThread(task =>
            {
                Debug.Log("ConfigEjercicio " + list[i].id + " actualizado en DB");
            });
        }
    }

    public bool RegistrarNuevoPaciente(Paciente paciente)
    {
        // Registro a la base de datos
        // Registro de paciente
        Dictionary<string, object> pacienteData = new Dictionary<string, object>
        {
            { "id", paciente.id },
            { "nombre", paciente.nombre },
            { "apellido", paciente.apellido },
            { "rut", paciente.rut },
            { "rut_dv", paciente.rut_dv },
            { "patologia", paciente.patologia },
            { "usuarioAsociado", usuarioSalud.credenciales.username },
            { "credencialUsername", paciente.credenciales.username }
        };

        DocumentReference pacienteDoc = db.Collection("paciente").Document(paciente.id);
        pacienteDoc.SetAsync(pacienteData).ContinueWithOnMainThread(task =>
        {
            Debug.Log("Paciente guardado en DB");
        });

        // Registro de credencial
        Dictionary<string, object> credencialData = new Dictionary<string, object>
        {
            { "username", paciente.credenciales.username },
            { "password", paciente.credenciales.GetPassword() },
            { "rol", paciente.credenciales.rol }
        };

        DocumentReference credencialDoc = db.Collection("credenciales").Document(paciente.credenciales.username);
        credencialDoc.SetAsync(credencialData).ContinueWithOnMainThread(task =>
        {
            Debug.Log("Credencial guardado en DB");
            return true;
        });
        return false;
    }


    // Metodo que comprueba si el username esta disponible
    public async Task<bool> ValidarUsername(string username)
    {
        DocumentReference docRef = db.Collection("credencial").Document(username);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        return !snapshot.Exists;
    }

    public async Task<Credenciales> ObtenerCredencial(string username)
    {
        DocumentReference docRef = db.Collection("credenciales").Document(username);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        if (snapshot.Exists)
        {
            Credenciales credencialEncontrada = new Credenciales(
                snapshot.GetValue<string>("username"),
                snapshot.GetValue<string>("password"),
                snapshot.GetValue<string>("rol")
                );

            return credencialEncontrada;
        }

        return null; // Retornar null si no se encuentra el documento
    }

    void OnDisable()
    {
        // Darse de baja del evento Application.quitting
        Application.quitting -= OnApplicationQuitting;
    }

    void OnApplicationQuitting()
    {
        if (firebaseApp != null)
        {
            // Cerrar la conexión con Firebase
            firebaseApp.Dispose();
        }
    }
}
