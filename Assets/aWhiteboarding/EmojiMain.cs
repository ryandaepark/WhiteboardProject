using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiMain : MonoBehaviour
{
    //counts the number of emojis active in rder to detyermine where a new one would be placed by EmojiButton
    public int n;
    public float interval;
    public Vector3 startPos;

    public void Awake()
    {
        n = 0;
    }


}
