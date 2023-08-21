using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public GameObject note, spawn;
    public GameObject inputfield;
    public GameObject playerUI;

    public void MakeNote()
    {
        var input = inputfield.gameObject.GetComponent<TMPro.TMP_InputField>();
        GameObject newNoteObject = Instantiate(note, spawn.transform);
        newNoteObject.transform.parent = null;
        newNoteObject.GetComponent<PostItTop>().notetext.text = input.text;
        input.text = "";
        playerUI.SetActive(false);
    }
}
