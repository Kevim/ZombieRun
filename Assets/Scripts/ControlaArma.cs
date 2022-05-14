using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaArma : MonoBehaviour
{
    public GameObject Bala;
    public GameObject CanoArma;
    public float fireRate = 0.1f;
    public AudioClip SomTiro;
    private float contador = 0f;

    // Start is called before the first frame update
    void Start()
    {
        contador = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;

        if (contador >= fireRate && (Input.GetButtonDown("Fire1") || Input.GetMouseButton(0))){
            Instantiate(Bala, CanoArma.transform.position, CanoArma.transform.rotation);
            ControlaAudio.instancia.PlayOneShot(this.SomTiro);
            contador = 0;
        }
    }
}
