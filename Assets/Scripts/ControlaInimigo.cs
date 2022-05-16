using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour, IMatavel
{
    public GameObject Jogador;

    public float PararDistanciaPersonagem = 2.5F;

    public float RaioVisao = 15F;

    public int DanoMin = 20;

    public int DanoMax = 30;

    public AudioClip SomMorte;

    public GameObject KitMedicoPrefab;

    private AnimacaoPersonagem animacaoInimigo;

    private ControlaJogador controlaJogador;

    private MovimentoPersonagem movimentoInimigo;

    private Status status;

    private Vector3 direcao;

    private Vector3 posicaoAleatoria;

    private float contadorVagar = 0f;

    private float tempoEntrePosicoesAleatorias = 4f;

    private float percentagemDropKitMedico = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        this.animacaoInimigo = GetComponent<AnimacaoPersonagem>();
        this.movimentoInimigo = GetComponent<MovimentoPersonagem>();
        this.status = GetComponent<Status>();

        this.Jogador = GameObject.FindWithTag(Constantes.TAG_JOGADOR);
        this.controlaJogador = this.Jogador.GetComponent<ControlaJogador>();
        CriarZumbiAleatorio();
    }

    void FixedUpdate()
    {
        float distancia =
            Vector3
                .Distance(transform.position, this.Jogador.transform.position);

        if (distancia < RaioVisao)
        {
            this.contadorVagar = 0;
            andarAteJogadorOuAtacar (distancia);
        }
        else
        {
            vagar();
        }
        this.movimentoInimigo.Rotacionar(this.direcao);
        this.animacaoInimigo.Mover(this.direcao.magnitude);
    }

    private void andarAteJogadorOuAtacar(float distancia)
    {
        this.direcao = this.Jogador.transform.position - transform.position;

        if (distancia > PararDistanciaPersonagem)
        {
            movimentar();
            this.animacaoInimigo.Atacar(false);
        }
        else
        {
            // atacar
            this.animacaoInimigo.Atacar(true);
        }
    }

    private void movimentar()
    {
        this
            .movimentoInimigo
            .Movimentar(this.direcao.normalized, this.status.Velocidade);
    }

    private void vagar()
    {
        this.contadorVagar -= Time.deltaTime;
        if (contadorVagar <= 0)
        {
            this.posicaoAleatoria = aleatorizarPosicao();
            this.contadorVagar = this.tempoEntrePosicoesAleatorias;
        }
        bool longeDoDestino =
            Vector3.Distance(transform.position, posicaoAleatoria) > 0.15;
        if (longeDoDestino)
        {
            this.direcao = this.posicaoAleatoria - transform.position;
            movimentar();
        }
    }

    private Vector3 aleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * 10;
        posicao += transform.position;
        posicao.y = transform.position.y;
        return posicao;
    }

    public void AtacaJogador()
    {
        int dano = Random.Range(this.DanoMin, this.DanoMax);
        this.controlaJogador.TomarDano(dano);
    }

    private void CriarZumbiAleatorio()
    {
        int randomZombieSkin = Random.Range(1, 28);
        transform.GetChild(randomZombieSkin).gameObject.SetActive(true);
    }

    public void TomarDano(int dano)
    {
        this.status.Vida -= dano;
        this.VerificaMorte();
    }

    public void VerificaMorte()
    {
        if (this.status.Vida <= 0)
        {
            Pontuacao.instance.IncreaseScore(1f);
            ControlaAudio.instancia.PlayOneShot(this.SomMorte);
            Destroy (gameObject);
            this.VerificaDroparKitMedico(this.percentagemDropKitMedico);
        }
    }

    private void VerificaDroparKitMedico(float percentagemDrop)
    {
        if (Random.value <= percentagemDrop)
        {
            Instantiate(KitMedicoPrefab,
            transform.position,
            Quaternion.identity);
        }
    }
}
