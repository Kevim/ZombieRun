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
    public TextMeshProUGUI TextoChefeApareceu;
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

    public void ExibirTextoChefeApareceu(){
        StartCoroutine(ExibirESumirTextoChefeApareceu(2, this.TextoChefeApareceu));
    }

    IEnumerator ExibirESumirTextoChefeApareceu(float tempoSumir, TextMeshProUGUI texto) {
        texto.gameObject.SetActive(true);
        Color corTexto = texto.color;
        corTexto.a = 1;
        texto.color = corTexto;
        yield return new WaitForSeconds(1);
        float contador = 0;
        while(corTexto.a > 0) {
            contador += Time.deltaTime / tempoSumir;
            corTexto.a = Mathf.Lerp(1, 0, contador);
            texto.color = corTexto;
            if (corTexto.a == 0) {
                texto.gameObject.SetActive(false);
            }
            yield return null;
        }
    }
}
