using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonFileManager : MonoBehaviour
{
    [SerializeField]
    private string fileName = "configData.json";
    [SerializeField]
    private string folderName = "Data"; // Ruta de la carpeta que contiene el archivo JSON
    [SerializeField]
    private string packageName = "com.aCompany.EjerciciosVR";

    public void SaveListToJson(List<ConfigEjercicioEjec> objectList)
    {
        string jsonData = JsonUtility.ToJson(objectList);

        string folderPath = Path.Combine(Application.persistentDataPath, folderName);
        string filePath = Path.Combine(folderPath, fileName);

        // Crea el directorio si no existe
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Guarda el archivo JSON
        File.WriteAllText(filePath, jsonData);

        Debug.Log("Archivo JSON guardado en: " + filePath);
    }

    public List<ConfigEjercicioEjec> LoadListFromJson() 
    {
        string path = "/storage/emulated/0/Android/data/com.aCompany.EjerciciosVR/files/Data/configData.json";

        if (File.Exists(path))
        {
            // Leemos el contenido del archivo
            string jsonData = File.ReadAllText(path);
            return JsonUtility.FromJson<List<ConfigEjercicioEjec>>(jsonData);
        }
        else
        {
            Debug.LogError("El archivo no existe en la ruta especificada: " + path);
            return null;
        }
    }
}
