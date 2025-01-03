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
            Debug.LogError("CraftingPanel이 연결되지 않았습니다. Inspector에서 설정해주세요.");
        }
    }

    void Update()
    {
        // M키 입력
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
            Debug.Log($"CraftingPanel {(craftingPanel.activeSelf ? "활성화" : "비활성화")}됨");
        }
    }
}
