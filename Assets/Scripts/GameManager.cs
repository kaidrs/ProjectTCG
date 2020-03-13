using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public void EndTurn()
    {
        Player.Instance.CurrentMana = Player.Instance.MaxMana;
        UIManager.Instance.UpdateManaUI();
        CardManager.Instance.FlushCards();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
