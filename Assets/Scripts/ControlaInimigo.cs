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

    private Animator animatorInimigo;

    private Rigidbody rigidbodyInimigo;
    private Pontuacao pontuacao;
    private ControlaJogador controlaJogador;

    // Start is called before the first frame update
    void Start()
    {
        this.Jogador = GameObject.FindWithTag(Constantes.TAG_JOGADOR);
        
        this.controlaJogador = this.Jogador.GetComponent<ControlaJogador>();

        int randomZombieSkin = Random.Range(1, 28);
        transform.GetChild(randomZombieSkin).gameObject.SetActive(true);

        this.animatorInimigo = GetComponent<Animator>();
        this.rigidbodyInimigo = GetComponent<Rigidbody>();

        this.pontuacao = GameObject.FindWithTag(Constantes.TAG_PONTUACAO).GetComponent<Pontuacao>();
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

            Quaternion novaRotacao = Quaternion.LookRotation(direcao);
            this.rigidbodyInimigo.MoveRotation (novaRotacao);

            if (distancia > PararDistanciaPersonagem)
            {
                this.rigidbodyInimigo
                    .MovePosition(this.rigidbodyInimigo.position +
                    (direcao.normalized * Velocidade * Time.deltaTime));

                this.animatorInimigo.SetBool(Constantes.ANIMACAO_ATACANDO, false);
            }
            else
            {
                // atacar
                this.animatorInimigo.SetBool(Constantes.ANIMACAO_ATACANDO, true);
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
}
