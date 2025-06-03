using UnityEngine;

public class HeartScript : MonoBehaviour
{

    public int modifier;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        modifier = 80;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            HealthControllerScript healthScript = other.gameObject.GetComponent<HealthControllerScript>();
            healthScript.UpdateHealth(modifier);
            Debug.Log("Trigger heart!  "+modifier);
            
            Destroy(this.gameObject);

        }
    }
}
