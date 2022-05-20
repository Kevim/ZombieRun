using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ControlaChefe : MonoBehaviour, IMatavel
{
    private Transform jogador;

    private NavMeshAgent navMeshAgent;

    private Status status;

    private AnimacaoPersonagem animacao;

    private MovimentoPersonagem movimento;

    public GameObject KitMedicoPrefab;

    public Slider Slider;
    public Image ImagemSlider;

    public Color CorDaVidaMin, CorDaVidaMax;
    public GameObject ParticulaSangue;

    private bool vivo = true;

    void Awake()
    {
        this.jogador = GameObject.FindWithTag(Constantes.TAG_JOGADOR).transform;
        this.navMeshAgent = GetComponent<NavMeshAgent>();
        this.status = GetComponent<Status>();
        this.animacao = GetComponent<AnimacaoPersonagem>();
        this.movimento = GetComponent<MovimentoPersonagem>();
    }

    void Start()
    {
        this.navMeshAgent.speed = this.status.Velocidade;
        this.Slider.maxValue = this.status.VidaInicial;
        this.AtualizarInterface();
    }

    void Update()
    {
        this.navMeshAgent.SetDestination(this.jogador.position);
        this.animacao.Mover(this.navMeshAgent.velocity.magnitude);

        if (this.navMeshAgent.hasPath && this.vivo)
        {
            bool estouPertoJogador =
                this.navMeshAgent.remainingDistance <=
                this.navMeshAgent.stoppingDistance;
            this.animacao.Atacar(estouPertoJogador);
            if (estouPertoJogador)
            {
                Vector3 direcao = jogador.position - transform.position;
                movimento.Rotacionar (direcao);
            }
        } else {
            this.animacao.Atacar(false);
        }
    }

    void AtacaJogador()
    {
        int dano = Random.Range(30, 40);
        this.jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    public void JorrarSangue(Vector3 pos, Quaternion rotacao){
        Instantiate(ParticulaSangue, pos, rotacao);
    }


    public void TomarDano(int dano)
    {
        this.status.Vida -= dano;
        this.AtualizarInterface();
        this.VerificaMorte();
    }

    public void VerificaMorte()
    {
        if (this.status.Vida <= 0 && this.vivo)
        {
            this.vivo = false;
            Pontuacao.instance.IncreaseScore(1f);

            //ControlaAudio.instancia.PlayOneShot(this.SomMorte);
            this.animacao.Morrer();
            this.movimento.Morrer();
            this.enabled = false;
            Instantiate(KitMedicoPrefab, transform.position, Quaternion.identity);
            this.navMeshAgent.enabled = false;
            Destroy(gameObject, 2);
        }
    }

    private void AtualizarInterface() {
        this.Slider.value = this.status.Vida;
        float percentVida = (float) this.status.Vida / this.status.VidaInicial;
        this.ImagemSlider.color = Color.Lerp(CorDaVidaMin, CorDaVidaMax, percentVida);
    }
}
