using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    public enum State
    {
        ileri,
        sag,
        sol,
        atla,
        collect,
    }

    public State[] states;
    public kyle kyle;
    public questionpanel questionpanel;


    void Start()
    {
        StartCoroutine(ProcessStates()); // durumları işleme coroutine başlatma
    }

    IEnumerator ProcessStates()
    {

        foreach (State state in states) // her durumu sırayla işleme
        {
            if (kyle.checkquestions()) // eğer sorular tamamlanmadıysa bekle
            {
                yield return StartCoroutine(questionpanel.ShowQuestionPanel()); // işi biten kadar bekle
            }
            switch (state)
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
                case State.atla:
                    kyle.Jump();
                    break;
                case State.collect:
                    kyle.Collect();
                    break;
            }
            // Her eylem arasında kısa bir bekleme süresi
            yield return new WaitForSeconds(1.5f);
        }

        Debug.Log("Tüm durumlar işlendi.");
        kyle.check(); // Tüm durumlar tamamlandığında final kontrol çağrısı
    }


    
}
