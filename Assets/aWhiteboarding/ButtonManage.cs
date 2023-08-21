using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManage : MonoBehaviour
{
    public GameObject whiteboard, /*note,*/ spawn, playerUI;

    public void MakeWhiteBoard()
    {
        GameObject newwhiteboardObject = Instantiate(whiteboard, spawn.transform);
        newwhiteboardObject.transform.parent = null;
    }

    public void MakeNote()
    {
        //GameObject newNoteObject = Instantiate(note, spawn.transform);
        //newNoteObject.transform.parent = null;

        playerUI.SetActive(true);
    }

    public void minimize()
    {
        spawn.SetActive(!spawn.activeSelf);
    }
}
