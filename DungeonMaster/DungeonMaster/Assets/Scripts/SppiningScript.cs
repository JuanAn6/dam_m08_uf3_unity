using UnityEngine;

public class SppiningScript : MonoBehaviour
{
    public float speed = 4.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 5f;
        GetComponent<Rigidbody>().angularVelocity = speed * Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
