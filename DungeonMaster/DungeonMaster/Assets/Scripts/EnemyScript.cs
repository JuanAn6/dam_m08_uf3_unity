using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyScript : MonoBehaviour
{
    Vector3 target, oldTarget;
    Rigidbody rb;

    Animator anim;

    MazeGeneratorScript msc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        msc = GameObject.FindGameObjectWithTag("GameController").GetComponent<MazeGeneratorScript>();
        Debug.Log("MSC: " + msc);
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        target = this.transform.position;
        target = nextTarget();

    }

    
    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(this.transform.position, target) < 0.4){
            target = nextTarget();
        }

    }

    public float speed;


    private Vector3 nextTarget()
    {
        oldTarget = target;

        Room r = msc.GetRoomPerPosition(this.transform.position);
        Debug.Log("ROOM: "+r);
        List<Vector2> dirs = r.GetDireccionsPossibles();

        Vector2 dirSelect = dirs[Random.Range(0, dirs.Count)];
        Vector3 direccioSpeed = (new Vector3(dirSelect.x, 0f, dirSelect.y) * Room.SIZE);
        
        Vector3 finalTarget = this.transform.position + direccioSpeed;
        rb.linearVelocity = speed * direccioSpeed;
        rb.transform.LookAt(finalTarget);

        return finalTarget;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 vectorDesti = other.gameObject.transform.position - this.transform.position;
            Vector3 finalTarget = this.transform.position + vectorDesti;

            rb.linearVelocity = (speed + 0.5f) * vectorDesti;
            rb.transform.LookAt(finalTarget);

            if(vectorDesti.magnitude < 1)
            {
                anim.SetTrigger("Attack");
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = nextTarget();
        }
        
    }


    public void goTo(Vector3 position)
    {
        oldTarget = target;
        Vector3 vectorADesti = position - this.transform.position;
        rb.linearVelocity = speed * vectorADesti;
        rb.transform.LookAt(position);
        target = position;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            goTo(oldTarget);
        }

    }

}
