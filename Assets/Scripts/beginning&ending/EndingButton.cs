using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingButton : MonoBehaviour
{
    public void GoToGame() {
        SceneManager.LoadScene("SampleScene");
    }
}
