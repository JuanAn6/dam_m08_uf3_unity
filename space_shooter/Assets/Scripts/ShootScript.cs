using UnityEngine;
public class ShootScript : MonoBehaviour
{

    private Rigidbody body;
    private float speed = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.linearVelocity = new Vector3(0,0,speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.CompareTag("Enemy")){

            AsteroidScript asteroidScript = other.gameObject.GetComponent<AsteroidScript>();
            asteroidScript.killYourSelf();
            Destroy(this.gameObject); //Destroy shoot

        }
    }

}
