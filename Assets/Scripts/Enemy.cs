using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{

    #region Singleton
    private static Enemy instance = null;
    public static Enemy Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Enemy>();
            }
            return instance;
        }
    }
    #endregion

    // Start is called before the first frame update
    [SerializeField] float currHealth;
    [SerializeField] float maxHealth;
    [SerializeField] int damage;

    [SerializeField] SkinnedMeshRenderer[] enemyRenders;

    Animator anim;
    bool isExposed;
    int statusIndex;
    int exposedTurns;

    public float CurrHealth { get => currHealth; set => currHealth = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public int Damage { get => damage; set => damage = value; }
    public bool IsExposed { get => isExposed; set => isExposed = value; }
    public int StatusIndex { get => statusIndex; set => statusIndex = value; }
    public SkinnedMeshRenderer[] EnemyRenders { get => enemyRenders; set => enemyRenders = value; }

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void TakeDamage(int damage)
    {
        if(CurrHealth >= 0)
        {
            anim.SetTrigger("GetHit");
            if (IsExposed)
            {
                CurrHealth -= damage * 2;
                Debug.Log("enemy hp now: " + "... EXPOSED ATTACK(x2)");

            }
            else
            {
                CurrHealth -= damage;
                Debug.Log("enemy hp now: " + CurrHealth);
            }
            if(currHealth <= 0)
            {
                Die();
                
            }
        }

        //else if (CurrHealth <= 0)
        //{
        //    Die();
        //}
        UIManager.Instance.UpdateEnemyHP();
    }

    public void Attack()
    {
        Player.Instance.TakeDamage(damage);
        UIManager.Instance.UpdatePlayerHp();
        anim.SetTrigger("Attack");
        Debug.Log("attacke player for"  + damage.ToString() + ", hp now : "+Player.Instance.CurrHealth.ToString());
    }

    public void Die()
    {
        UIManager.Instance.FadeInLevelPanel();
        Player.Instance.Gold += (10 + (LevelManager.Instance.LevelIndex * 5));
    }
    public void UpdateExposed()
    {
        //Need to have logic here on end turn to call this
        if (exposedTurns > 1)
        {
            exposedTurns--;
            UIManager.Instance.EnemyStatusIndicator[0].GetComponentInChildren<TextMeshProUGUI>().text = exposedTurns.ToString(); // Shorten , make better function to handle updating in UIManager
        }
        else
        {
            isExposed = false;
            UIManager.Instance.EnemyStatusIndicator[0].SetActive(false);
        }
    }

    public void UpdateExposed(int numTurns)
    {
        if (!isExposed)
        {
            isExposed = true;
            exposedTurns = numTurns;
            UIManager.Instance.EnemyStatusIndicator[0].SetActive(true);
            UIManager.Instance.EnemyStatusIndicator[0].GetComponentInChildren<TextMeshProUGUI>().text = exposedTurns.ToString();
        }
        else
        {
            exposedTurns = numTurns;
            UIManager.Instance.EnemyStatusIndicator[0].GetComponentInChildren<TextMeshProUGUI>().text = exposedTurns.ToString();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
