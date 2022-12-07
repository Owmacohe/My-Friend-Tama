using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChatController : MonoBehaviour
{
    [SerializeField] GameObject comment;
    [SerializeField] int maxComments = 6;
    [SerializeField] float averageCommentSpeed = 2.5f;
    [SerializeField] Color[] nameColours;
    [SerializeField] Vector2 chatterCountRange = new Vector2(30, 50);

    [HideInInspector] public bool evolveMessages;
    [HideInInspector] public bool roundDoneMessages;

    Queue<GameObject> commentQueue = new Queue<GameObject>();
    List<Chatter> chatters = new List<Chatter>();

    class Chatter
    {
        public string Name { get; }
        public Color Colour { get; }

        public Chatter(string name, Color col)
        {
            Name = name;
            Colour = col;
        }
    }

    void Start()
    {
        for (int i = 0; i < Random.Range(chatterCountRange.x, chatterCountRange.y); i++)
        {
            chatters.Add(new Chatter(ChatGenerator.RandomName(), RandomColour()));
        }
        
        Invoke(nameof(RecursiveAddComment), averageCommentSpeed / 2f);
    }

    Color RandomColour()
    {
        return nameColours[Random.Range(0, nameColours.Length)];
    }

    void AddComment(Chatter chatter, string message)
    {
        GameObject temp = Instantiate(comment, transform);

        TMP_Text[] fields = temp.GetComponentsInChildren<TMP_Text>();
        fields[0].text = chatter.Name;
        fields[0].color = chatter.Colour;
        fields[1].text = message;

        if (commentQueue.Count == maxComments)
        {
            Destroy(commentQueue.Dequeue());
        }
        
        commentQueue.Enqueue(temp);
    }

    void RecursiveAddComment()
    {
        AddCommentFromExistingChatter(ChatGenerator.RandomMessage(evolveMessages, roundDoneMessages));
        
        Invoke(
            nameof(RecursiveAddComment), 
            Random.Range(
                -averageCommentSpeed * 0.8f,
                averageCommentSpeed * 0.8f
            ) + averageCommentSpeed
        );
    }

    public void AddCommentFromExistingChatter(string message)
    {
        AddComment(chatters[Random.Range(0, chatters.Count)], message);
    }

    public void AddCommentFromNewChatter(string name, string message)
    {
        Chatter temp = new Chatter(name, RandomColour());
        
        chatters.Add(temp);
        AddComment(temp, message);
    }
    
    public void AddCommentFromOneTimeChatter(string name, string message)
    {
        Chatter temp = new Chatter(name, RandomColour());
        
        AddComment(temp, message);
    }
}