using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
    #endregion

    bool endingTurn;

    public bool EndingTurn { get => endingTurn; set => endingTurn = value; }

    public void EndTurn()
    {
        if (!endingTurn)
        {
            Player.Instance.CurrentMana = Player.Instance.MaxMana;
            UIManager.Instance.UpdateManaUI();
            Enemy.Instance.UpdateExposed();
            Enemy.Instance.Attack();
            CardManager.Instance.FlushCards();
            endingTurn = true;
        }

  
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
