//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Item : MonoBehaviour
//{
//    [SerializeField]
//    private string itemName;

//    [SerializeField]
//    private int quantity;

//    [SerializeField]
//    private Sprite sprite;

//    [TextArea]
//    [SerializeField]
//    private string itemDescription;

//    private InventoryManager inventoryManager;

//    // Start is called before the first frame update
//    void Start()
//    {
//        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();


//    }

//    private void OnCollisionEnter2D(Collision2D collision){

//        if(collision.gameObject.tag == "Player"){

//            int leftOverItems = inventoryManager.AddItem(itemName,quantity,sprite, itemDescription);
//            if(leftOverItems <=0){
//                Destroy(gameObject);
//            }
//            else{
//                quantity = leftOverItems;

//            }
//        }
//    }


//    // Update is called once per frame
//    void Update()
//    {

//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField]
    private string itemName;

    [SerializeField]
    private int quantity;

    [SerializeField]
    private Sprite sprite;

    [TextArea]
    [SerializeField]
    private string itemDescription;

    private InventoryManager inventoryManager;
   
    private Text messageText;
    private float messageDisplayTime = 2f;
    private Coroutine currentMessageCoroutine;

    private Dictionary<string, string> itemMessages;

    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        
        // messageText가 유효하지 않은 경우
        if (messageText == null)
        {
            messageText = GameObject.Find("MessageText")?.GetComponent<Text>();
            if (messageText == null)
            {
                Debug.LogError("MessageText is not assigned and could not be found in the scene.");
            }
        }

        itemMessages = new Dictionary<string, string>
        {
            { "redFlower", "빨간꽃을 획득하였습니다!" },
            { "redFlower(1)", "빨간꽃2을 획득하였습니다!" },
            { "blueFlower", "파란꽃을 획득하였습니다!" }
        };
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            // 충돌한 오브젝트 이름을 가져옴
            string objectName = collision.otherCollider.gameObject.name;
            Debug.Log("Collided with object: " +  objectName);

            // 메시지 가져오기
            string message = GetMessageForObject(objectName);
            ShowMessage(message);
            Debug.Log("Message shown.");

            int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite, itemDescription);
            if (leftOverItems <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                quantity = leftOverItems;

            }
        }
    }

    private string GetMessageForObject(string objectName)
    {
        // 딕셔너리에서 메시지 반환
        if (itemMessages.ContainsKey(objectName))
        {
            return itemMessages[objectName];
        }
        else
        {
            return "아이템을 획득하였습니다!";  // 기본 메시지
        }
    }

    private void ShowMessage(string message)
    {
        if (messageText != null)
        {
            Debug.Log("ShowMessage called.");

            // 기존 코루틴이 실행 중이면 중지
            if (currentMessageCoroutine != null)
            {
                Debug.Log("Stopping existing coroutine.");
                StopCoroutine(currentMessageCoroutine);
            }

            // 메시지 표시
            messageText.text = message;
            messageText.gameObject.SetActive(true);

            // 새로운 코루틴 시작
            currentMessageCoroutine = StartCoroutine(HideMessageAfterDelay());
        }
        else
        {
            Debug.LogError("MessageText is null.");
        }
    }

    private IEnumerator HideMessageAfterDelay()
    {
        Debug.Log("HideMessageAfterDelay started.");
        yield return new WaitForSeconds(messageDisplayTime); // 2초 대기
        Debug.Log("Wait finished.");

        if (messageText != null)
        {
            messageText.text = ""; // 메시지 초기화
            messageText.enabled = false;
            Debug.Log("Message hidden.");
        }

        // 실행 중인 코루틴 초기화
        currentMessageCoroutine = null;
    }

    void Update()
    {

    }
}
