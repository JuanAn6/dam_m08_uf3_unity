using Assets.Scripts;
using System.Collections;
using UnityEngine;

public class SpaceShipScript : MonoBehaviour
{
    public float normalSpeed = 1.2f;
    public float shiftSpeed = 2f;
    public Rigidbody target;

    public Transform shotPrefab;
    public Transform[] shotingPoints;
    public int currentShoot = 0;

    public bool canShoot = true;

    public Camera camera;

    public int marge = 30;

    private GameControllerScript gameController;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GetComponent<Rigidbody>();

        //Recollir el scritpt del game contoller
        gameController = GameObject.FindGameObjectWithTag("GameController")
                                   .GetComponent<GameControllerScript>();

        //StartCoroutine(RemoveMySelf());
    }

    //private IEnumerator RemoveMySelf()
    //{
    //    yield return new WaitForSeconds(5);
    //}


    // Update is called once per frame
    void Update()
    {
        float vx = Input.GetAxis("Horizontal");
        float vz = Input.GetAxis("Vertical");

        Vector3 screenCoordinates = camera.WorldToScreenPoint(this.transform.position);

        if (screenCoordinates.y <= marge)
        {
            if (vz <= 0) vz = 0;
        }else if (screenCoordinates.y >= camera.pixelHeight - marge)
        {
            if (vz >= 0) vz = 0;
        }

        float speed = Input.GetKey(KeyCode.LeftShift) ? shiftSpeed : normalSpeed;

        target.linearVelocity = speed*(new Vector3(vx, 0, vz));

        target.rotation = Quaternion.Euler(0, 0, -(vx * 20));


        if (Input.GetButton("Fire1") && canShoot)
        {       
            Instantiate(shotPrefab, shotingPoints[currentShoot]);
            currentShoot = (currentShoot + 1) % shotingPoints.Length;
            StartCoroutine(StopFunction());
        }


        
        

        if(screenCoordinates.x < 0 )
        {
            float x = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, 0)).x;

            this.transform.position = new Vector3(x, this.transform.position.y, this.transform.position.z);

        }
        else if (screenCoordinates.x > camera.pixelWidth)
        {
            float x = camera.ScreenToWorldPoint(new Vector3(0, 0)).x;
            
            this.transform.position = new Vector3(x, this.transform.position.y, this.transform.position.z);

        }

    }


    private IEnumerator StopFunction()
    {
        
        canShoot = false;
        yield return new WaitForSeconds(0.3f);
        canShoot = true;
       
    }
    public int health = 6;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            health--;
            Debug.Log("Vida: " + health);
            if(listener != null)
            {
                listener.OnHealthChanged(health);
            }
            //Elimina el asteroide
            other.gameObject.GetComponent<AsteroidScript>().killYourSelf();

            if(health <= 0)
            {
                gameController.GameOver();
            }
        }
    }

    private HealthListener listener;
    public void registerHealthListener(HealthListener listener)
    {
        this.listener = listener;
        this.listener.SetMaxHealth(this.health);
    }

}
