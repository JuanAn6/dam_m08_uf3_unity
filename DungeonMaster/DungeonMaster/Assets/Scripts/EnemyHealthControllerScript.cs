using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthControllerScript : MonoBehaviour
{

    public int MaxHealth = 200;
    private int Health = 200;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Health = MaxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Negative
    internal void UpdateHealth(int healthScript)
    {
        //Mantiene el valor entre los dos del final
        Health = Math.Clamp(Health + healthScript, 0, MaxHealth);

        if(Health == 0)
        {
            KillMySelf();
        }
    }

    void KillMySelf()
    {

        //Probability of drop a key



        Destroy(this.gameObject);
    }

}
