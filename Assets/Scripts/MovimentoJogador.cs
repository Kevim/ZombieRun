using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoJogador : MovimentoPersonagem
{
    public void RotacionarJogador(LayerMask mascara) {
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);
        RaycastHit impacto;

        if (Physics.Raycast(raio, out impacto, 100, mascara))
        {
            Vector3 posicaoMiraJogador = impacto.point - transform.position;
            posicaoMiraJogador.y = transform.position.y;
            
            Rotacionar(posicaoMiraJogador);
        }
    }
}
