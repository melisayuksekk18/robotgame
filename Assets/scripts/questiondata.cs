using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question Data", menuName = "Question Data", order = 0)]
public class questiondata : ScriptableObject
{
    [Multiline] public string questionText;
    public string[] answerOptions;
    public int correctAnswerIndex;
 
}
