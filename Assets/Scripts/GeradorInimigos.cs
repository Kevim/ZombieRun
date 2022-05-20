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

    private int quantidadeMaxZumbisVivos = 5;

    private int quantidadeZumbisVivos = 0;
    
    public float tempoAumentoDificuldade = 30;
    private float contadorAumentarDificuldade;

    public float DistanciaDoJogadorParaGeracao = 30;

    private GameObject jogador;

    void Start()
    {
        jogador = GameObject.FindWithTag(Constantes.TAG_JOGADOR);
        this.quantidadeMaxZumbisVivos = this.SpawnAmount * 3;
        this.contadorAumentarDificuldade = this.tempoAumentoDificuldade;
        StartCoroutine(GerarNovoZumbi(SpawnAmount));
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad > contadorAumentarDificuldade) {
            this.quantidadeMaxZumbisVivos += SpawnAmount;
            this.contadorAumentarDificuldade = Time.timeSinceLevelLoad + tempoAumentoDificuldade;
        }
        if (Spawn)
        {
            contador += Time.deltaTime;

            if (contador >= SpawnTime && SpawnHabilitado(SpawnAmount))
            {
                StartCoroutine(GerarNovoZumbi(SpawnAmount));
            }
        }
    }

    IEnumerator GerarNovoZumbi(int spawnAmount)
    {
        while (SpawnHabilitado(spawnAmount))
        {
            Vector3 finalPos = Vector3.zero;
            bool hasPos = false;
            while (!hasPos && SpawnHabilitado(spawnAmount))
            {
                float distanciaDoJogador =
                    Vector3
                        .Distance(transform.position,
                        jogador.transform.position);
                if (distanciaDoJogador > DistanciaDoJogadorParaGeracao)
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
                else
                {
                    break;
                }
            }
            if (hasPos)
            {
                ControlaInimigo game = Instantiate(Zumbi, finalPos, transform.rotation).GetComponent<ControlaInimigo>();
                game.geradorInimigo = this;
                this.quantidadeZumbisVivos++;
                this.SpawnLimit--;
                contador = 0;
                spawnAmount--;
            }
            else
            {
                break;
            }
            yield return null;
        }
    }

    private bool SpawnHabilitado(int spawnAmount){
        return spawnAmount > 0 && SpawnLimit > 0 && this.quantidadeZumbisVivos < this.quantidadeMaxZumbisVivos;
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

    public void DiminuirQuantidadeZumbisVivos() {
        this.quantidadeZumbisVivos--;
    }
}
