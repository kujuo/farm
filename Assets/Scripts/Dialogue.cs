using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Dialogue
{
    [Header("Conversation")]
    public string name;
    [TextArea(3, 10)]
    public string[] sentences;
}