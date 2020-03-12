using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float currHealth;
    [SerializeField] float maxHealth;
    [SerializeField] float damage;

    bool isExposed;
    int exposedTurns;

    public float CurrHealth { get => currHealth; set => currHealth = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float Damage { get => damage; set => damage = value; }
    public bool IsExposed { get => isExposed; set => isExposed = value; }

    void Start()
    {
        
    }

    public void Die()
    {
        UIManager.Instance.FadeInLevelPanel();
    }
    public void UpdateExposed()
    {
        //Need to have logic here on end turn to call this
        if (exposedTurns > 0)
        {
            exposedTurns--;
            Debug.Log("Now exposed for" + exposedTurns);
        }
    }

    public void UpdateExposed(int numTurns)
    {
        if (!isExposed)
        {
            isExposed = true;
            exposedTurns = numTurns;

        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
