using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Collections;
using UnityEngine.Rendering;

public class PostIt : MonoBehaviour
{
    public GameObject postit;

    //Emoji Button Script
    public GameObject counterBase; //counter gameobject
    public Transform spawnPos;
    public float interval; //How far to space out emoji counter display
    public Vector3 startPos; //Starting location (set to bottom left corner of note)
    public GameObject emote_like, emote_dislike, emote_star, emote_pin; 

    private int clicks_like, clicks_dislike, clicks_star, clicks_pin; // number of clicks per emote
    private Text[] emoji_counts; //Preserve Text objects per emoji

    //Text Edit Functionlities
    //public Text postit_text; //value of post it text
    private GameObject text_input; //Input Field for text

    //Change Post it color
    public Material[] materials;
    private Queue<Material> colors; 
    public MeshRenderer note;

    void Start()
    {
        //Initialize emoji click count values
        emoji_counts = new Text[4];
        clicks_like = 0;
        clicks_dislike = 0;
        clicks_star = 0;
        clicks_pin = 0;

        //initialize text input
        text_input = GameObject.FindGameObjectWithTag("Text Input");

        //initialize materials
        colors = new Queue<Material>();
        foreach (Material i in materials)
        {
            colors.Enqueue(i);
        }
    }

    //Emoji and emoji count handler
    private void EmojiClicked (GameObject emojiBase, int spot, int emote_clicks)
    {
        if (emote_clicks == 1)
        {
            GameObject emoji = Instantiate(emojiBase, spawnPos);
            emoji.transform.localPosition = new Vector3(startPos.x + (spot * interval), startPos.y, startPos.z);
            GameObject counter = Instantiate(counterBase, emoji.transform);
            Text number = counter.GetComponentInChildren<Text>();
            // Save number of clicks per emote
            emoji_counts.SetValue(number, spot);

        }
        else
        {
            emoji_counts[spot].text = "" + emote_clicks;
        }
    }

    public void LikeClicked()
    {
        clicks_like++;
        EmojiClicked(emote_like, 0, clicks_like);
    }

    public void DislikeClicked()
    {
        clicks_dislike++;
        EmojiClicked(emote_dislike, 1, clicks_dislike);
    }

    public void StarClicked()
    {
        clicks_star++;
        EmojiClicked(emote_star, 2, clicks_star);
    }

    public void PinClicked()
    {
        clicks_pin++;
        EmojiClicked(emote_pin, 3, clicks_pin);
    }


    /*
    //CURRENTLY KEYBOARD FOR DESKTOP NONEXISTENT, REWORK ON TEXT WILL BE FINALIZED WHEN ACCEPTED.
    public TouchScreenKeyboard overlayKeyboard;
    private bool typing;
    public Text words; //the target text to edit
    public string mid;

    private void Awake()
    {
        typing = false;
    }

    private void Update()
    {
        if (typing)
        {
            mid = overlayKeyboard.text;
            words.text = mid;
            if (overlayKeyboard != null && overlayKeyboard.status == TouchScreenKeyboard.Status.Done)
            {
                typing = false;
            }
        }
    }

    //NOT WORKING RIGHT NOW
    public void TextEditClicked()
    {
        //spawn InputField to player camera -> on enter copy and paste value to the post it -> clear
        changeNoteText();
        
    }

    private void changeNoteText()
    {
        typing = true;
        overlayKeyboard = TouchScreenKeyboard.Open(words.text, TouchScreenKeyboardType.Default);
    }*/

    //Change post it color
    public void PostItColorClicked()
    {
        note.material = (Material)colors.Dequeue();
        colors.Enqueue(note.material);
    }

    public void Destroy()
    {
        Destroy(postit);
    }
}
