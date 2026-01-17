using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questionpanel : MonoBehaviour
{
    public bool isAnswered = false;
    public bool isTrueAnswer = false;
    public TMPro.TextMeshProUGUI questionText;
    public TMPro.TextMeshProUGUI[] answerTexts;
    private questiondata currentQuestionData;
    public void AnswerQuestion()
    {
        isAnswered = true;
        gameObject.SetActive(false); // soruyu cevapladıktan sonra paneli kapat
    }

    public IEnumerator ShowQuestionPanel(questiondata questionData)
    {
        currentQuestionData = questionData;
        isAnswered = false;
        questionText.text = questionData.questionText;
        for (int i = 0; i < answerTexts.Length; i++)
        {
            if (i < questionData.answerOptions.Length)
            {
                answerTexts[i].text = questionData.answerOptions[i];
            }
            else
            {
                answerTexts[i].transform.parent.gameObject.SetActive(false); // gereksiz butonları gizle
            }
        }
        gameObject.SetActive(true); // paneli göster
        // Burada kullanıcıdan cevap bekleme kodu eklenebilir
        yield return new WaitUntil(() => isAnswered); // cevap verilene kadar bekle
    }

public void Answerindex(int index)
    {
        isTrueAnswer = (index == currentQuestionData.correctAnswerIndex);
        AnswerQuestion();
    }                 
}
