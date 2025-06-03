using UnityEngine;

public class AsteroidScript : MonoBehaviour
{

    private Rigidbody body;

    public Transform explosion;

    public float rotationSpeed = 0.5f;
    public float fallSpeed = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.angularVelocity = rotationSpeed * Random.insideUnitSphere;
        body.linearVelocity = new Vector3(0, 0, -fallSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void killYourSelf()
    {
        if(explosion != null)
        {
            //Quaternion es per la rotació
            Transform ex = Instantiate(explosion, this.transform.position, Quaternion.identity);
            
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<SphereCollider>().enabled = false;

            Destroy(ex.gameObject, 0.9f);
            //DestroyInmediete(explosion.gameObject, 1f);
            Destroy(this.gameObject, 1f);
        }
    }

    
    private void OnDestroy()
    {

    }

}
