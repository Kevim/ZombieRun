using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour
{
    public GameObject Jogador;

    public float Velocidade = 5;

    public float PararDistanciaPersonagem = 2.5F;
    public float RaioVisao = 15F;
    public int DanoMin = 20;
    public int DanoMax = 30;
    public AudioClip SomMorte;
    private AnimacaoPersonagem animacaoPersonagem;
    private Pontuacao pontuacao;
    private ControlaJogador controlaJogador;
    private MovimentoPersonagem movimentoPersonagem;

    // Start is called before the first frame update
    void Start()
    {
        this.animacaoPersonagem = GetComponent<AnimacaoPersonagem>();
        this.movimentoPersonagem = GetComponent<MovimentoPersonagem>();

        this.pontuacao = Pontuacao.instance;
        this.Jogador = GameObject.FindWithTag(Constantes.TAG_JOGADOR);
        this.controlaJogador = this.Jogador.GetComponent<ControlaJogador>();
        CriarZumbiAleatorio();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        float distancia =
            Vector3.Distance(transform.position, this.Jogador.transform.position);

        Vector3 direcao = this.Jogador.transform.position - transform.position;
        if (distancia < RaioVisao){ 

            this.movimentoPersonagem.Rotacionar(direcao);

            if (distancia > PararDistanciaPersonagem)
            {
                this.movimentoPersonagem.Movimentar(direcao.normalized,this.Velocidade);
                this.animacaoPersonagem.Atacar(false);
            }
            else
            {
                // atacar
                this.animacaoPersonagem.Atacar(true);
            }
        }
    }

    public void AtacaJogador()
    {
        int dano = Random.Range(this.DanoMin, this.DanoMax);
        this.controlaJogador.TomarDano(dano);
    }

    void OnDestroy()
    {
        this.pontuacao.IncreaseScore(1f);
        ControlaAudio.instancia.PlayOneShot(this.SomMorte);
    }

    private void CriarZumbiAleatorio() {
        int randomZombieSkin = Random.Range(1, 28);
        transform.GetChild(randomZombieSkin).gameObject.SetActive(true);
    }
}
