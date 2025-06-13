using UnityEngine;

public class FireBallController : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Enemy"))
        {

            if(Vector3.Distance(other.transform.position, this.transform.position) < 2) { 
                Debug.Log("Hit enemy!");
                
                EnemyHealthControllerScript ehcs = other.transform.GetComponent<EnemyHealthControllerScript>();
                
                ehcs.UpdateHealth(-20);

                Destroy(this.gameObject);
            }
        }
        else if(!other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit somthing!");
            Destroy(this.gameObject);
        }
    }
}
