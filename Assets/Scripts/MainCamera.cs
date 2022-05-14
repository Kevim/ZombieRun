using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject Jogador;
    public float distanciaY;
    private Vector3 distCompensar;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = Jogador.transform.position;
        pos.y = distanciaY;
        pos.z -= distanciaY/2;
        transform.position = pos;
        distCompensar = transform.position - Jogador.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Jogador.transform.position + distCompensar;
    }
}
