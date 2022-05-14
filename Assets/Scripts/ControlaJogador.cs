using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour
{
    public float Velocidade = 10;

    public float VelocidadeRotacao = 5;

    public LayerMask MascaraChao;

    public GameObject TextoGameOver;

    public int Vida = 100;

    public bool Vivo = true;

    public ControlaInterface controlaInterface;

    public AudioClip SomDano;

    private static string MOVENDO_STRING = "Movendo";

    private Vector3 direcao;

    private Rigidbody rigidbodyJogador;

    private Animator animatorJogador;

    private float vertical;

    private float horizontal;

    void Start()
    {
        Time.timeScale = 1;
        TextoGameOver.SetActive(false);
        this.rigidbodyJogador = GetComponent<Rigidbody>();
        this.animatorJogador = GetComponent<Animator>();
    }

    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        if (direcao != Vector3.zero)
        {
            this.animatorJogador.SetBool(MOVENDO_STRING, true);
        }
        else
        {
            this.animatorJogador.SetBool(MOVENDO_STRING, false);
        }

        if (!Vivo && Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene("game");
        }
    }

    private void FixedUpdate()
    {
        this.movePlayer();

        /* this
            .rigidbodyJogador
            .MovePosition(this.rigidbodyJogador.position +
            (direcao * Velocidade * Time.deltaTime)); */
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);
        RaycastHit impacto;

        if (Physics.Raycast(raio, out impacto, 100, MascaraChao))
        {
            Vector3 posicaoMiraJogador = impacto.point - transform.position;
            posicaoMiraJogador.y = transform.position.y;
            // Debug.Log("impacto: "+impacto.point);
            // Debug.Log("Mira: "+posicaoMiraJogador);
            Quaternion novaRotacao = Quaternion.LookRotation(posicaoMiraJogador);
            
            this.rigidbodyJogador.MoveRotation(novaRotacao);
            // this.rigidbodyJogador.AddTorque(posicaoMiraJogador);
            // if (novaRotacao.y != transform.rotation.y && novaRotacao.w != transform.rotation.w) {
            //     this.rigidbodyJogador.AddRelativeTorque(0,novaRotacao.y*0.0025f,0);
            //     Debug.Log("jogador: "+ transform.rotation);
            //     Debug.Log("novaRotacao: "+ novaRotacao);
            //     Debug.Log("novaRotacao: "+ novaRotacao.x);
            //     Debug.Log("novaRotacao: "+ novaRotacao.y);
            //     Debug.Log("novaRotacao: "+ novaRotacao.z);
            //     Debug.Log("novaRotacao: "+ novaRotacao.w);
            // }
        }
    }

    private void movePlayer()
    {
        if (direcao != Vector3.zero)
        {
            float velocidade = this.Velocidade;
            float eixoX = 0;
            float eixoZ = 0;

            float moveForwardX = transform.forward.x;
            float moveForwardZ = transform.forward.z;
            if (this.direcao.x != 0)
            {
                if (this.direcao.z == 0)
                {
                    velocidade *= 0.85f;
                    if (moveForwardX > 0.5 || moveForwardX < -0.5)
                    {
                        int multDirecao = CalcUtils.GetDirecao(this.direcao.x);
                        eixoX = moveForwardZ * multDirecao;
                        eixoZ = moveForwardX * -1 * multDirecao;
                    }
                    else
                    {
                        int multDirecao =
                            CalcUtils.GetDirecaoInvertida(this.direcao.x);
                        eixoX = moveForwardZ * -1 * multDirecao;
                        eixoZ = moveForwardX * multDirecao;
                    }
                }
                else
                {
                    int multDirecaoX = CalcUtils.GetDirecao(this.direcao.x);
                    int multDirecaoZ = CalcUtils.GetDirecao(this.direcao.z);
                    float direcaoXPositivo =
                        CalcUtils.GetValorPositivo(this.direcao.x);
                    float direcaoZPositivo =
                        CalcUtils.GetValorPositivo(this.direcao.z);
                    float moveForwardXPositivo =
                        CalcUtils.GetValorPositivo(moveForwardX);
                    float moveForwardZPositivo =
                        CalcUtils.GetValorPositivo(moveForwardZ);
                    velocidade *= 0.3f;
                    eixoX =
                        (moveForwardXPositivo + direcaoXPositivo) *
                        multDirecaoX;
                    eixoZ =
                        (moveForwardZPositivo + direcaoZPositivo) *
                        multDirecaoZ;
                }
            }
            else
            {
                int multDirecao = CalcUtils.GetDirecao(this.direcao.z);
                if (this.direcao.z < 0)
                {
                    velocidade *= 0.85f;
                }
                eixoX = moveForwardX * multDirecao;
                eixoZ = moveForwardZ * multDirecao;
            }

            /* if (this.direcao.z < 0) {
                eixoZ *= -1;
                eixoX *= -1;
            } */
            Vector3 vector = new Vector3(eixoX, 0, eixoZ);

            // Debug.Log("Forward: " + transform.forward);
            // Debug.Log("direcao: " + direcao);
            // Debug.Log("vector: " + vector);
            Vector3 angularVelocity = new Vector3(1, 1, 1);
            this.rigidbodyJogador.velocity = vector * velocidade;
            //Debug.Log("velocity: " + this.rigidbodyJogador.velocity);
        }
        else
        {
            this.rigidbodyJogador.velocity = Vector3.zero;
            this.rigidbodyJogador.angularVelocity = Vector3.zero;
        }
    }

    public void TomarDano(int valorDano)
    {
        this.Vida -= valorDano;
        controlaInterface.AtualizarSliderVidaJogador();
        ControlaAudio.instancia.PlayOneShot(this.SomDano); 
        verificaMorte();
    }

    private void verificaMorte()
    {
        if (this.Vida <= 0)
        {
            Time.timeScale = 0;
            this.Vida = 0;
            this.Vivo = false;
            this.TextoGameOver.SetActive(true);
        }
    }
}
