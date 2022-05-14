using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlaInterface : MonoBehaviour
{
    public Slider SliderVidaJogador;
    private ControlaJogador controlaJogador;

    // Start is called before the first frame update
    void Start()
    {
        controlaJogador = GameObject.FindWithTag(Constantes.TAG_JOGADOR).GetComponent<ControlaJogador>();
        SliderVidaJogador.maxValue = controlaJogador.Vida;
        AtualizarSliderVidaJogador();
    }

    public void AtualizarSliderVidaJogador() {
        SliderVidaJogador.value = controlaJogador.Vida;
    }
}
