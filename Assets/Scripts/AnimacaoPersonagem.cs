using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoPersonagem : MonoBehaviour
{
    private Animator animator;
    
    void Awake()
    {
        this.animator = GetComponent<Animator>();
    }

    public void Atacar(bool isAtacando){
        animator.SetBool(Constantes.ANIMACAO_ATACANDO, isAtacando);
    }

    public void Mover(float direcao) {
        animator.SetFloat(Constantes.MOVENDO_STRING, direcao);
    }

    public void Morrer(){
        animator.SetTrigger(Constantes.MORRER_STRING);
    }
}
