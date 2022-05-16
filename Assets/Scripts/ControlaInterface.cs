using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ControlaInterface : MonoBehaviour
{
    public Slider SliderVidaJogador;
    public GameObject painel;

    private ControlaJogador controlaJogador;
    public TextMeshProUGUI TextoTempoSobrevivencia;
    public TextMeshProUGUI TextoPontuacaoFinal;
    public TextMeshProUGUI TextoPontuacaoMax;
    public GameObject NewRecord;
    string textoTempoSobrevivencia;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        painel.SetActive(false);
        textoTempoSobrevivencia = this.TextoTempoSobrevivencia.text;
        controlaJogador =
            GameObject
                .FindWithTag(Constantes.TAG_JOGADOR)
                .GetComponent<ControlaJogador>();
        SliderVidaJogador.maxValue = controlaJogador.statusJogador.Vida;
        AtualizarSliderVidaJogador();
    }

    public void AtualizarSliderVidaJogador()
    {
        SliderVidaJogador.value = controlaJogador.statusJogador.Vida;
    }

    public void GameOver()
    {
        this.painel.SetActive(true);
        Time.timeScale = 0;
        int min = Mathf.FloorToInt(Time.timeSinceLevelLoad / 60);
        int seg = Mathf.FloorToInt(Time.timeSinceLevelLoad % 60);
        Pontuacao.instance.VerificarAtualizaScoreMax();
        
        this.TextoPontuacaoFinal.SetText(this.TextoPontuacaoFinal.text, Pontuacao.instance.score);
        this.TextoPontuacaoMax.SetText(this.TextoPontuacaoMax.text, Pontuacao.instance.scoreMax);
        this.TextoTempoSobrevivencia.SetText(textoTempoSobrevivencia, min, seg);
    }

    public void Reiniciar() {
        SceneManager.LoadScene("game");
    }
}
