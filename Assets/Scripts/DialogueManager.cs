using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    
    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);
        nameText.text = dialogue.name;

        GameStateManager.instance.isInteracting = true;
        GameStateManager.instance.pause = true;
        GameStateManager.instance.Pause(true);
        
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            GameStateManager.instance.isInteracting = false;
            GameStateManager.instance.pause = false;
            GameStateManager.instance.Pause(false);
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        animator.SetBool("isOpen", false);
    }
}