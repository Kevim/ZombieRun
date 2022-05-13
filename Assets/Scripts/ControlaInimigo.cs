using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour
{
    public GameObject Jogador;

    public float Velocidade = 5;

    public float pararDistanciaPersonagem = 2.5F;

    private static string ATACANDO_STRING = "Atacando";
    private static string JOGADOR_STRING = "Jogador";
    private static string PONTUACAO_STRING = "Pontuacao";

    private Animator animatorInimigo;

    private Rigidbody rigidbodyInimigo;
    private Pontuacao pontuacao;

    // Start is called before the first frame update
    void Start()
    {
        Jogador = GameObject.FindWithTag(JOGADOR_STRING);
        int randomZombieSkin = Random.Range(1, 28);
        transform.GetChild(randomZombieSkin).gameObject.SetActive(true);

        animatorInimigo = GetComponent<Animator>();
        rigidbodyInimigo = GetComponent<Rigidbody>();

        pontuacao = GameObject.FindWithTag(PONTUACAO_STRING).GetComponent<Pontuacao>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        float distancia =
            Vector3.Distance(transform.position, Jogador.transform.position);

        Vector3 direcao = Jogador.transform.position - transform.position;

        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        rigidbodyInimigo.MoveRotation (novaRotacao);

        if (distancia > pararDistanciaPersonagem)
        {
            rigidbodyInimigo
                .MovePosition(rigidbodyInimigo.position +
                (direcao.normalized * Velocidade * Time.deltaTime));

            animatorInimigo.SetBool(ATACANDO_STRING, false);
        }
        else
        {
            // atacar
            animatorInimigo.SetBool(ATACANDO_STRING, true);
        }
    }

    public void AtacaJogador()
    {
        Time.timeScale = 0;
        ControlaJogador controlaJogador =
            Jogador.GetComponent<ControlaJogador>();
        controlaJogador.TextoGameOver.SetActive(true);
        controlaJogador.Vivo = false;
    }

    void OnDestroy()
    {
        pontuacao.IncreaseScore(1f);
    }
}
