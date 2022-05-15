using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorInimigos : MonoBehaviour
{

    public GameObject Zumbi;
    public int SpawnLimite = 1;
    public float SpawnTime = 1;
    private float contador = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;

        if (SpawnLimite > 0 && contador >= SpawnTime) {
            SpawnLimite--;
            contador = 0;
            Instantiate(Zumbi, transform.position, transform.rotation);
        }
        
    }
}
