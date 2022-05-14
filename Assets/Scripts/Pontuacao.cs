using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pontuacao : MonoBehaviour
{
    public GameObject scoreValue;

    public static Pontuacao instance;

    TextMeshProUGUI textMeshProUGUI;

    float score = 0f;

    string text;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        textMeshProUGUI = scoreValue.GetComponent<TextMeshProUGUI>();
        text = textMeshProUGUI.text;
        setScore();
    }

    public void IncreaseScore(float value)
    {
        score += value;
        setScore();
    }

    public void DecreaseScore(float value)
    {
        score -= value;
        setScore();
    }

    private void setScore()
    {
        textMeshProUGUI.SetText (text, score);
    }
}
