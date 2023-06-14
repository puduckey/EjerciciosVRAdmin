using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ejercicio", menuName = "Ejercicio", order = 1)]
public class Ejercicio : ScriptableObject
{
    public int id;
    public string nombre, descripcion, tipo, nivel;
    public Sprite imagen;
    public RuntimeAnimatorController animacion;
    public AnimationClip animClip;
    public string equipo, recomendaciones;
}
