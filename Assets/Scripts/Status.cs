using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int VidaInicial = 100;
    [HideInInspector]
    public int Vida;
    public float Velocidade = 15;

    void Awake()
    {
        this.Vida = VidaInicial;
    }
}
