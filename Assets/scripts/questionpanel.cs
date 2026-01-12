using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questionpanel : MonoBehaviour
{
    public bool isAnswered = false;
    public bool isTrueAnswer = false;

    public void AnswerQuestion()
    {
        isAnswered = true;
        gameObject.SetActive(false); // soruyu cevapladıktan sonra paneli kapat
    }

    public IEnumerator ShowQuestionPanel()
    {
        gameObject.SetActive(true); // paneli göster
        // Burada kullanıcıdan cevap bekleme kodu eklenebilir
        yield return new WaitUntil(() => isAnswered); // cevap verilene kadar bekle
    }

    public void AnswerTrue()
    {
        isTrueAnswer = true;
        AnswerQuestion();
    }

    public void AnswerFalse()
    {
        isTrueAnswer = false;
        AnswerQuestion();
    }
}
