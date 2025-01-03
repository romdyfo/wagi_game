using UnityEngine;

public class ExperimentTable : MonoBehaviour
{
    public GameObject craftingPanel;

    void Start()
    {
  
        if (craftingPanel != null)
        {
            craftingPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("CraftingPanel�� ������� �ʾҽ��ϴ�. Inspector���� �������ּ���.");
        }
    }

    void Update()
    {
        // MŰ �Է�
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleCraftingPanel();
        }
    }

    private void ToggleCraftingPanel()
    {
        if (craftingPanel != null)
        {
       
            craftingPanel.SetActive(!craftingPanel.activeSelf);
            Debug.Log($"CraftingPanel {(craftingPanel.activeSelf ? "Ȱ��ȭ" : "��Ȱ��ȭ")}��");
        }
    }
}
