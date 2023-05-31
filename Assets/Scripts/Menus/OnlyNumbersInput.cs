using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class OnlyNumbersInput : MonoBehaviour
{
    private TMP_InputField inputField;

    private void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(string value)
    {
        // Elimina todos los caracteres que no sean n�meros
        string processedValue = new string(value.Where(char.IsDigit).ToArray());

        // Actualiza el valor del InputField solo con los n�meros v�lidos
        inputField.text = processedValue;
    }
}
