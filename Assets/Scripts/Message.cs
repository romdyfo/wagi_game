using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class Message : MonoBehaviour
{
    public GameObject MessageSlot;

    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;

    public TMP_Text TextMessage;

    private HashSet<string> collectedItems = new HashSet<string>();



    private void Start()
    {
        
        
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        
        collectedItems.Add(itemName); 
        TextMessage.text = $"{itemName}을(를) 획득했습니다.";
        MessageSlot.SetActive(true);
        
        Invoke("PrintMessage", 1.5f);
        return 0;
    }

    private void PrintMessage(){
        if(collectedItems.Contains("end")){
            MessageSlot.SetActive(false);
        }
        else if (collectedItems.Contains("노트") && collectedItems.Contains("연필") && collectedItems.Contains("알약"))
        {
            TextMessage.text = "재료를 모두 획득했다! 이제 과학실로 가서 치료제를 제조하자!";
            collectedItems.Add("end");
            Invoke("PrintMessage", 2);
        }
        else{
            MessageSlot.SetActive(false);
        }    
    }




    // Start is called before the first frame update
    
}

