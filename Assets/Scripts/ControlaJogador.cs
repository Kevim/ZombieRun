using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour
{
    public float Velocidade = 10;

    public LayerMask MascaraChao;

    public GameObject TextoGameOver;

    public bool Vivo = true;

    private static string MOVENDO_STRING = "Movendo";

    private Vector3 direcao;

    private Rigidbody rigidbodyJogador;

    private Animator animatorJogador;

    void Start()
    {
        Time.timeScale = 1;
        TextoGameOver.SetActive(false);
        rigidbodyJogador = GetComponent<Rigidbody>();
        animatorJogador = GetComponent<Animator>();
    }

    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        if (direcao != Vector3.zero)
        {
            animatorJogador.SetBool(MOVENDO_STRING, true);
        }
        else
        {
            animatorJogador.SetBool(MOVENDO_STRING, false);
        }

        if (!Vivo && Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene("game");
        }
    }

    private void FixedUpdate()
    {
        rigidbodyJogador
            .MovePosition(rigidbodyJogador.position +
            (direcao * Velocidade * Time.deltaTime));

        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);
        RaycastHit impacto;

        if (Physics.Raycast(raio, out impacto, 100, MascaraChao))
        {
            Vector3 posicaoMiraJogador = impacto.point - transform.position;
            posicaoMiraJogador.y = transform.position.y;
            Quaternion novaRotacao =
                Quaternion.LookRotation(posicaoMiraJogador);
            rigidbodyJogador.MoveRotation (novaRotacao);
        }
    }
}
