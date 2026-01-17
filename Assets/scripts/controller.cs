using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    public enum State
    {
        none,
        ileri,
        sag,
        sol,
    }

    public State[] states;
    public kyle kyle;
    public questionpanel questionpanel;
    public questiondata[] questiondata;
    private int currentQuestionIndex = 0;
    public UISlotPanel uiSlotPanel;


    public void Startmove()
    {
        StartCoroutine(ProcessStates()); // durumları işleme coroutine başlatma
    }

    IEnumerator ProcessStates()
    {
        

        foreach (UISlot slot in uiSlotPanel.slots) // her durumu sırayla işleme
        {
            GameObject questionObject = kyle.checkquestions(); // soru kontrolü yap
            if (questionObject != null) // eğer sorular tamamlanmadıysa bekle
            {
                
                yield return StartCoroutine(questionpanel.ShowQuestionPanel(questiondata[currentQuestionIndex])); // işi biten kadar bekle
             if (questionpanel.isTrueAnswer == false )
            {
                kyle.animator.SetTrigger("fall"); // yanlış cevap verildiyse fail animasyonunu oynat
                yield break; // yanlış cevap verildiyse işlemi durdur
                
            }

            currentQuestionIndex++; // sonraki soruya geç
            Destroy(questionObject); // soruyu sahneden kaldır
            }
            if (slot.current == null || slot.current.Value == State.none) continue; // eğer slot boşsa atla

            switch (slot.current.Value)
            {
                case State.ileri:
                    kyle.Forward();
                    break;
                case State.sag:
                    kyle.RotateRight();
                    break;
                case State.sol:
                    kyle.RotateLeft();
                    break;
            }
            // Her eylem arasında kısa bir bekleme süresi
            yield return new WaitForSeconds(1.5f);
        }

        Debug.Log("Tüm durumlar işlendi.");
        kyle.check(); // Tüm durumlar tamamlandığında final kontrol çağrısı
    }


    
}
