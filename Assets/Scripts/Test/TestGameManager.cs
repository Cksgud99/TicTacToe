using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameManager : MonoBehaviour
{
    public void Open()
    {
        PopupPanelController.Instance.ShowPanel("Hello","OK", true ,() =>
        {
            Debug.Log("OK 클릭!!");
        });
    }
}
