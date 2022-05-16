using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedico : MonoBehaviour
{
    private int valorCura = 15;

    private int tempoDestruicao = 5;

    void Start()
    {
        Destroy (gameObject, tempoDestruicao);  
    }

    private void OnTriggerEnter(Collider objetoColisao)
    {
        if (objetoColisao.tag == Constantes.TAG_JOGADOR)
        {
            objetoColisao.GetComponent<ControlaJogador>().CurarVida(valorCura);
            Destroy (gameObject);
        }
    }
}
