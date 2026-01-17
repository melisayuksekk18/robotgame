using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class kyle : MonoBehaviour
{
    public Animator animator;
    
    public void Jump()
    {
        animator.SetTrigger("jump");
    }

    public void Forward()
    {
        animator.SetBool("run", true);
        StartCoroutine(Forwarding()); // coroutine başlatma
    }

    IEnumerator Forwarding() // fonksiyonun içimdeki bekletmeler için
    {
        float t = 0;
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + transform.forward * 2.5f; // 5 birim ileri
        while (t < 1f)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, t); // yumuşak pozisyon geçisi
            yield return null;
        }
        Idle(); // hareket tamamlandığında idle durumuna geç
    }

    void Idle()
    {
        animator.SetBool("run", false);
    }

    public void Collect()
    {
        animator.SetTrigger("collect");
    }

    public void RotateRight()
    {
        StartCoroutine(Rotating(90));
    }
    public void RotateLeft()
    {
        StartCoroutine(Rotating(-90));
    }
    IEnumerator Rotating(int degrees) // döndürme işlemi için coroutine
    {
        float t = 0;
        Quaternion startRot = transform.rotation;
        Quaternion endRot = startRot * Quaternion.Euler(0, degrees, 0); // belirtilen dereceye göre döndür
        while (t < 1f)
        {
            t += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startRot, endRot, t); // yumuşak dönüş
            yield return null;
        }
    }

    public void check () // bitiş kontrolü için
    {
        bool isFinished = false;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Finish"))
            {
                isFinished = true;
            }
        }
        if (isFinished)
        {
            Debug.Log("Level Completed!");
            animator.SetTrigger("celebrate");
        }
        else
        {
            Debug.Log("Level Failed. Try Again!");
            animator.SetTrigger("fail");
        }
    }

    public GameObject checkquestions () // soru kontrolü için
    {
        GameObject question = null;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("question"))
            {
                question = hitCollider.gameObject;
            }
        }
        return question;
    }

}
