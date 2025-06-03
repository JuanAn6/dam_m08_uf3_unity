using UnityEngine;

public class HostiaScript : MonoBehaviour
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
        if (other.gameObject.CompareTag("Player"))
        {
            HealthControllerScript ehcs = other.gameObject.GetComponent<HealthControllerScript>();
            ehcs.UpdateHealth(-20);

        }
    }
}
