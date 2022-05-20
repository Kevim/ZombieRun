using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float Velocidade = 30;

    private Rigidbody rigidbodyBala;

    private int danoTiro = 1;

    void Start()
    {
        rigidbodyBala = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbodyBala
            .MovePosition(rigidbodyBala.position +
            (transform.forward * Velocidade * Time.deltaTime));
    }

    void OnTriggerEnter(Collider objetoDeColisao)
    {
        Quaternion rotacao = Quaternion.LookRotation(-transform.forward);
        switch (objetoDeColisao.tag)
        {
            case Constantes.TAG_INIMIGO:
                ControlaInimigo inimigo =
                    objetoDeColisao.GetComponent<ControlaInimigo>();
                inimigo.TomarDano(this.danoTiro);
                inimigo.JorrarSangue(transform.position, rotacao);
                break;
            case Constantes.TAG_CHEFE:
                ControlaChefe chefe =
                    objetoDeColisao.GetComponent<ControlaChefe>();
                chefe.TomarDano(this.danoTiro);
                chefe.JorrarSangue(transform.position, rotacao);
                break;
        }
        Destroy (gameObject);
    }
}
