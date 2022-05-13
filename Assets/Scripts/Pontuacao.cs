using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pontuacao : MonoBehaviour
{
    public GameObject scoreValue;
    TextMeshProUGUI textMeshProUGUI;
    float score = 0f;
    string text;
    // Start is called before the first frame update
    void Start()
    {
        textMeshProUGUI = scoreValue.GetComponent<TextMeshProUGUI>();
        text = textMeshProUGUI.text;
        setScore();
    }

    public void IncreaseScore(float value) {
        score += value;
        setScore();
    }

    public void DecreaseScore(float value) {
        score -= value;
        setScore();
    }

    private void setScore() {
        textMeshProUGUI.SetText(text, score);
    }
}
