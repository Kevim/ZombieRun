using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaMenu : MonoBehaviour
{
    public GameObject BotaoSair;

    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_STANDALONE || UNITY_EDITOR
            BotaoSair.SetActive(true);
        #endif
    }

    public void Jogar()
    {
        StartCoroutine(MudarCena(Constantes.SCENE_GAME));
    }

    IEnumerator MudarCena(string name) {
        yield return new WaitForSecondsRealtime(0.3f);
        SceneManager.LoadScene(name);
    }

    public void Sair(){
        StartCoroutine(FecharJogo());
    }

    IEnumerator FecharJogo() {
        yield return new WaitForSecondsRealtime(0.3f);
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
