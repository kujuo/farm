using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : Interactable
{
    public Dialogue dialogue;

    private DialogueManager manager;

    void Start()
    {
        manager = FindObjectOfType<DialogueManager>();
        //gameStateManager = FindObjectOfType<GameStateManager>();
    }

    public override void Interact()
    {
      if (!GameStateManager.Instance.isInteracting) manager.StartDialogue(dialogue);
      else manager.DisplayNextSentence();
    }
}