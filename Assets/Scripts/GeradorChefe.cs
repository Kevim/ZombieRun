using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour
{
    private float tempoParaGerarChefe = 0;

    public float tempoEntreGeracoes = 60;

    public GameObject ChefePrefab;

    private ControlaInterface controlaInterface;

    public Transform[] PosicoesParaGerar;

    private GameObject jogador;

    // Start is called before the first frame update
    void Start()
    {
        this.tempoParaGerarChefe = tempoEntreGeracoes;
        this.controlaInterface =
            GameObject.FindObjectOfType<ControlaInterface>();
        this.jogador = GameObject.FindWithTag(Constantes.TAG_JOGADOR);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad > tempoParaGerarChefe)
        {
            GerarChefe();
            this.tempoParaGerarChefe =
                Time.timeSinceLevelLoad + tempoEntreGeracoes;
        }
    }

    void GerarChefe()
    {
        Vector3 pos = PosicaoParaGerarChefe();
        Instantiate(ChefePrefab, pos, Quaternion.identity);
        controlaInterface.ExibirTextoChefeApareceu();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 3);
    }

    Vector3 PosicaoParaGerarChefe()
    {
        IDictionary<float, Vector3> posicoes = new Dictionary<float, Vector3>();
        ArrayList distancias = new ArrayList();
        float menorDistancia = float.MaxValue;
        foreach (Transform spawnPoint in PosicoesParaGerar)
        {
            float distanciaCalculada =
                Vector3
                    .Distance(spawnPoint.position, jogador.transform.position);

            distancias.Add (distanciaCalculada);
            posicoes.Add(distanciaCalculada, spawnPoint.position);

            if (menorDistancia > distanciaCalculada)
            {
                menorDistancia = distanciaCalculada;
            }
        }
        distancias.Remove (menorDistancia);
        int distanciaRandom = Random.Range(0, distancias.Count);
        float distanciaEscolhida = (float) distancias[distanciaRandom];
        return posicoes[distanciaEscolhida];
    }
}
