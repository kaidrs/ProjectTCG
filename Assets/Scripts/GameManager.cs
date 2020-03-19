using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    Enemy enemy;

    public bool EndingTurn { get => endingTurn; set => endingTurn = value; }

    public void EndTurn()
    {
        if (!endingTurn)
        {
            Player.Instance.CurrentMana = Player.Instance.MaxMana;
            UIManager.Instance.UpdateManaUI();
            Player.Instance.CalcAttackArmor();
            enemy.UpdateExposed();
            if (enemy.CurrHealth > 0)
            {
                enemy.Attack();
            }

            CardManager.Instance.FlushCards(true);
            endingTurn = true;
        }
    }

    public void CreateEnemy(int index)
    {

        enemy.MaxHealth = (index *= Random.Range(15,35));
        enemy.CurrHealth = enemy.MaxHealth;
        enemy.Damage = index *= 10;

        foreach(var obj in enemy.EnemyRenders)
        {
            obj.enabled = false;
        }

        int randomSkin = Random.Range(1, enemy.EnemyRenders.Length);
        enemy.EnemyRenders[randomSkin].enabled = true;
    }

    public void ResetLevel()
    {
        Player.Instance.CurrentMana = Player.Instance.MaxMana;
        Player.Instance.TotalArmor = 0;
        //Need to reset all status of player & enemy...
        UIManager.Instance.UpdateManaUI();
        UIManager.Instance.UpdateEnemyHP();
        UIManager.Instance.UpdatePlayerArmor();
    }

    // Start is called before the first frame update
    void Start()
    {
        enemy = Enemy.Instance;
    }

}
