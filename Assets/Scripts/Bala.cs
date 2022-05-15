using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float Velocidade = 20;

    private Rigidbody rigidbodyBala;

    private int danoTiro = 1;

    private float contador = 0;

    void Start()
    {
        rigidbodyBala = GetComponent<Rigidbody>();
    }

    void Update()
    {
        contador += Time.deltaTime;
        if (contador >= 5) {
            Destroy(gameObject);
        }
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
        if (objetoDeColisao.tag == Constantes.TAG_INIMIGO)
        {
            objetoDeColisao
                .GetComponent<ControlaInimigo>()
                .TomarDano(this.danoTiro);
        }
        Destroy (gameObject);
    }
}
