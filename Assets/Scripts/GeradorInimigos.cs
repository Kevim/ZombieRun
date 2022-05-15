using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorInimigos : MonoBehaviour
{
    public GameObject Zumbi;

    public bool Spawn = true;

    public int SpawnLimit = 1;

    public float SpawnTime = 1;

    public int SpawnAmount = 1;

    public LayerMask layerZumbi;

    private float contador = 0;

    private float distanciaGeracao = 3;
    public float DistanciaDoJogadorParaGeracao = 30;
    private GameObject jogador;

    void Start()
    {
        jogador = GameObject.FindWithTag(Constantes.TAG_JOGADOR);
    }

    // Update is called once per frame
    void Update()
    {
        float distanciaDoJogador = Vector3.Distance(transform.position, jogador.transform.position);
        if (Spawn && distanciaDoJogador > DistanciaDoJogadorParaGeracao)
        {
            contador += Time.deltaTime;

            if (contador >= SpawnTime)
            {
                StartCoroutine(GerarNovoZumbi(SpawnAmount));
            }
        }
    }

    IEnumerator GerarNovoZumbi(int spawnAmount)
    {
        while (spawnAmount > 0 && SpawnLimit > 0)
        {
            Vector3 finalPos = Vector3.zero;
            bool hasPos = false;
            while (!hasPos)
            {
                Vector3 pos = AleatorizarPosicao();
                Collider[] colisores =
                    Physics.OverlapSphere(pos, 1, layerZumbi);
                if (colisores.Length == 0)
                {
                    finalPos = pos;
                    hasPos = true;
                }
                else
                {
                    yield return null;
                }
            }
            Instantiate(Zumbi, finalPos, transform.rotation);
            SpawnLimit--;
            contador = 0;
            spawnAmount--;
            yield return null;
        }
    }

    private Vector3 AleatorizarPosicao()
    {
        Vector3 pos = Random.insideUnitSphere * this.distanciaGeracao;
        pos += transform.position;
        pos.y = 0;
        return pos;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, this.distanciaGeracao);
    }
}
