using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour, IMatavel
{
    public LayerMask MascaraChao;

    public GameObject TextoGameOver;

    public bool Vivo = true;

    public ControlaInterface controlaInterface;

    public AudioClip SomDano;
    [HideInInspector]
    public Status statusJogador;

    private Vector3 direcao;

    private AnimacaoPersonagem animacaoPersonagem;

    private MovimentoJogador movimentoJogador;

    private float vertical;

    private float horizontal;

    void Start()
    {
        Time.timeScale = 1;
        TextoGameOver.SetActive(false);
        this.animacaoPersonagem = GetComponent<AnimacaoPersonagem>();
        this.movimentoJogador = GetComponent<MovimentoJogador>();
        this.statusJogador = GetComponent<Status>();
    }

    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        animacaoPersonagem.Mover(direcao.magnitude);

        if (!Vivo && Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene("game");
        }
    }

    private void FixedUpdate()
    {
        this.movimentoJogador.Movimentar(this.direcao, this.statusJogador.Velocidade);

        this.movimentoJogador.RotacionarJogador(this.MascaraChao);
    }

    public void TomarDano(int valorDano)
    {
        this.statusJogador.Vida -= valorDano;
        controlaInterface.AtualizarSliderVidaJogador();
        ControlaAudio.instancia.PlayOneShot(this.SomDano);
        VerificaMorte();
    }

    public void VerificaMorte()
    {
        if (this.statusJogador.Vida <= 0)
        {
            Time.timeScale = 0;
            this.statusJogador.Vida = 0;
            this.Vivo = false;
            this.TextoGameOver.SetActive(true);
        }
    }
}
