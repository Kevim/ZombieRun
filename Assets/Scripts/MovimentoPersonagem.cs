using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour
{
    private Rigidbody rb;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Movimentar(Vector3 direcao, float velocidade)
    {
        if (direcao != Vector3.zero)
        {
            float eixoX = 0;
            float eixoZ = 0;

            float moveForwardX = transform.forward.x;
            float moveForwardZ = transform.forward.z;
            if (direcao.x != 0)
            {
                if (direcao.z == 0)
                {
                    velocidade *= 0.85f;
                    if (moveForwardX > 0.5 || moveForwardX < -0.5)
                    {
                        int multDirecao = CalcUtils.GetDirecao(direcao.x);
                        eixoX = moveForwardZ * multDirecao;
                        eixoZ = moveForwardX * -1 * multDirecao;
                    }
                    else
                    {
                        int multDirecao =
                            CalcUtils.GetDirecaoInvertida(direcao.x);
                        eixoX = moveForwardZ * -1 * multDirecao;
                        eixoZ = moveForwardX * multDirecao;
                    }
                }
                else
                {
                    int multDirecaoX = CalcUtils.GetDirecao(direcao.x);
                    int multDirecaoZ = CalcUtils.GetDirecao(direcao.z);
                    float direcaoXPositivo =
                        CalcUtils.GetValorPositivo(direcao.x);
                    float direcaoZPositivo =
                        CalcUtils.GetValorPositivo(direcao.z);
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
                int multDirecao = CalcUtils.GetDirecao(direcao.z);
                if (direcao.z < 0)
                {
                    velocidade *= 0.85f;
                }
                eixoX = moveForwardX * multDirecao;
                eixoZ = moveForwardZ * multDirecao;
            }

            Vector3 vector = new Vector3(eixoX, 0, eixoZ);
            this.rb.velocity = vector.normalized * velocidade;
        }
        else
        {
            this.rb.velocity = Vector3.zero;
            // this.rb.angularVelocity = Vector3.zero;
        }
    }

    public void Rotacionar(Vector3 direcao) {
        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        this.rb.MoveRotation(novaRotacao);
    }

    public void Morrer() {
        rb.constraints = RigidbodyConstraints.None;
        rb.velocity = Vector3.zero;
        rb.isKinematic = false;
        GetComponent<Collider>().enabled = false;
    }
}
