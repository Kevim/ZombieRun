using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaJogador : MonoBehaviour, IMatavel, ICuravel
{
    public LayerMask MascaraChao;

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
            this.statusJogador.Vida = 0;
            this.Vivo = false;
            this.controlaInterface.GameOver();
        }
    }

    public void CurarVida(int quantidadeCura)
    {
        this.statusJogador.Vida = Mathf.Clamp(this.statusJogador.Vida + quantidadeCura, 0, this.statusJogador.VidaInicial);
        controlaInterface.AtualizarSliderVidaJogador();
    }
}
