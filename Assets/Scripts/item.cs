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
        
        // messageText�� ��ȿ���� ���� ���
        if (messageText == null)
        {
            // messageText = GameObject.Find("MessageText")?.GetComponent<Text>();
            if (messageText == null)
            {
                Debug.LogError("MessageText is not assigned and could not be found in the scene.");
            }
        }

        itemMessages = new Dictionary<string, string>
        {
            { "redFlower", "�������� ȹ���Ͽ����ϴ�!" },
            { "redFlower(1)", "������2�� ȹ���Ͽ����ϴ�!" },
            { "blueFlower", "�Ķ����� ȹ���Ͽ����ϴ�!" }
        };
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            // �浹�� ������Ʈ �̸��� ������
            string objectName = collision.otherCollider.gameObject.name;
            Debug.Log("Collided with object: " +  objectName);

            // �޽��� ��������
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
        // ��ųʸ����� �޽��� ��ȯ
        if (itemMessages.ContainsKey(objectName))
        {
            return itemMessages[objectName];
        }
        else
        {
            return "�������� ȹ���Ͽ����ϴ�!";  // �⺻ �޽���
        }
    }

    private void ShowMessage(string message)
    {
        if (messageText != null)
        {
            Debug.Log("ShowMessage called.");

            // ���� �ڷ�ƾ�� ���� ���̸� ����
            if (currentMessageCoroutine != null)
            {
                Debug.Log("Stopping existing coroutine.");
                StopCoroutine(currentMessageCoroutine);
            }

            // �޽��� ǥ��
            messageText.text = message;
            messageText.gameObject.SetActive(true);

            // ���ο� �ڷ�ƾ ����
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
        yield return new WaitForSeconds(messageDisplayTime); // 2�� ���
        Debug.Log("Wait finished.");

        if (messageText != null)
        {
            messageText.text = ""; // �޽��� �ʱ�ȭ
            messageText.enabled = false;
            Debug.Log("Message hidden.");
        }

        // ���� ���� �ڷ�ƾ �ʱ�ȭ
        currentMessageCoroutine = null;
    }

    void Update()
    {

    }
}
