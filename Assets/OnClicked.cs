using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClicked : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TaskOnClick()
    {
        if (this.gameObject.tag == "Shop")
        {
            UIManager.Instance.ShowShopPanel(true);
        }
        else
        {
            LevelManager.Instance.JoinLevel(LevelManager.Instance.LevelIndex);
            GetComponent<Button>().interactable = false;

        }
    }


}
