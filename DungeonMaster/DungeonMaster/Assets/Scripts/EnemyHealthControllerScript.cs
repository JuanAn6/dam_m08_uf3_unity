using System;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class EnemyHealthControllerScript : MonoBehaviour
{

    public int MaxHealth = 200;
    private int Health = 200;
    public bool IamBoss = false;
    public Transform AuxKey;

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

        //Probability of drop a key if is a boss
        if (IamBoss)
        {
            Vector3 pos = this.gameObject.transform.position;
            Transform aux = AuxKey;
            aux.position = pos + new Vector3(0, 0.5f, 0);
            Instantiate(aux);
        }

        Destroy(this.gameObject);
    }

}
