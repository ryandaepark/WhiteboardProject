using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EmojiButton : MonoBehaviour, IPointerDownHandler
{

    //a button that on the first press, will create an emoji in the target spawn are. On a second or more, it will add a count of how many of that emoji was reacted
    private int likeCount;
    private Text number;
    public GameObject counterBase, emojiBase; 
    private GameObject emoji, counter;
    public Transform spawnPos;  
    private EmojiMain spawnCoordinator;
    // Start is called before the first frame update
    void Start()
    {
        likeCount = 0;
        spawnCoordinator = this.GetComponentInParent<EmojiMain>();
    }

    // Update is called once per frame
    public void OnPointerDown(PointerEventData eventData)
    {
        likeCount +=  1;
        Debug.Log(likeCount);
        if (likeCount == 1)
        {
            emoji = Instantiate(emojiBase, spawnPos);
            emoji.transform.localPosition = new Vector3(spawnCoordinator.startPos.x + (spawnCoordinator.n * spawnCoordinator.interval), spawnCoordinator.startPos.y, spawnCoordinator.startPos.z);
            spawnCoordinator.n += 1;
        }
        else if (likeCount == 2)
        {
            counter = Instantiate(counterBase, emoji.transform);
            number = counter.GetComponentInChildren<Text>();
        }
        else
        {
            number.text = "" + likeCount;
        }


    }
}
