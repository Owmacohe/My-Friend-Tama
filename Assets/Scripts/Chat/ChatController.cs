using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChatController : MonoBehaviour
{
    [SerializeField] GameObject comment;
    [SerializeField] int maxComments = 6;
    [SerializeField] float averageCommentSpeed = 2;
    [SerializeField] Color[] nameColours;

    Queue<GameObject> commentQueue = new Queue<GameObject>();

    void Start()
    {
        Invoke(nameof(AddComment), averageCommentSpeed);
    }

    void AddComment()
    {
        AddComment(ChatGenerator.RandomName(), ChatGenerator.RandomMessage());
        
        Invoke(
            nameof(AddComment), 
            Random.Range(
                -averageCommentSpeed * 0.8f,
                averageCommentSpeed * 0.8f
            ) + averageCommentSpeed
        );
    }

    void AddComment(string name, string message)
    {
        GameObject temp = Instantiate(comment, transform);

        TMP_Text[] fields = temp.GetComponentsInChildren<TMP_Text>();
        fields[0].text = name;
        fields[0].color = nameColours[Random.Range(0, nameColours.Length)];
        fields[1].text = message;

        if (commentQueue.Count == maxComments)
        {
            Destroy(commentQueue.Dequeue());
        }
        
        commentQueue.Enqueue(temp);
    }
}