using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pontuacao : MonoBehaviour
{
    public GameObject scoreValue;

    public static Pontuacao instance;

    public ControlaInterface controlaInterface;

    TextMeshProUGUI textMeshProUGUI;

    public float score = 0f;

    public float scoreMax = 0f;

    private string text;
    private bool coroutinePlaying;

    void Awake()
    {
        instance = this;
        scoreMax = PlayerPrefs.GetFloat("ScoreMax");
    }

    // Start is called before the first frame update
    void Start()
    {
        textMeshProUGUI = scoreValue.GetComponent<TextMeshProUGUI>();
        text = textMeshProUGUI.text;
        setScore(false);
    }

    public void IncreaseScore(float value)
    {
        score += value;
        bool scoreMaxAtualizado = VerificarAtualizaScoreMax();
        setScore (scoreMaxAtualizado);
    }

    public void DecreaseScore(float value)
    {
        score -= value;
        setScore(false);
    }

    private void setScore(bool scoreMaxAtualizado)
    {
        textMeshProUGUI.SetText (text, score);
        controlaInterface.NewRecord.SetActive (scoreMaxAtualizado);
        if (scoreMaxAtualizado)
        {
            if (!coroutinePlaying) {
                coroutinePlaying = true;
                StartCoroutine(blinkScore());
            }
        }
    }

    private IEnumerator blinkScore()
    {
        float cont = 0;
        float cont2 = 0;
        bool active = true;
        while (cont < 5)
        {
            cont += Time.deltaTime;
            cont2 += Time.deltaTime;
            if (cont2 > 0.5)
            {
                scoreValue.SetActive (active);
                active = !active;
                cont2 = 0;
            }
            yield return null;
        }
        coroutinePlaying = false;
    }

    public bool VerificarAtualizaScoreMax()
    {
        if (this.score > this.scoreMax)
        {
            this.scoreMax = this.score;
            PlayerPrefs.SetFloat("ScoreMax", this.scoreMax);
            return true;
        }
        return false;
    }
}
