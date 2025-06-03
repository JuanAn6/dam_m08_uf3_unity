using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterScript : MonoBehaviour
{

    private Rigidbody rigidbody;

    public Animator animator;

    public float runningMultiplier = 2f;
    public float speed = 2000f;

    public Transform FireBall;

    public Vector3 lastDirection;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        lastDirection = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //Get the inputs for movment
        //GetAxisRaw make the axis input sharp not smoth 
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        //if is not moving stop the character and dont do nothing
        

        //Set the animations to the character
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        animator.SetBool("isAdvancing", z!= 0 || x!=0 );
        animator.SetBool("isRunning", isRunning);

        if (Input.GetKeyDown(KeyCode.Space) && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Jump"))
        {
            animator.SetTrigger("Jump");
        }

        if (Input.GetMouseButtonDown(0) && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Jump"))
        {
            if(!isRunning && !(z != 0 || x != 0))
            {
                animator.SetTrigger("Throw");
                Vector3 pos = this.transform.position;
                pos.y = 0.5f;

                Transform fireballInstance = Instantiate(FireBall, pos, Quaternion.identity);

                Rigidbody rb_fb = fireballInstance.GetComponent<Rigidbody>();

                int aux_x = 0;
                int aux_z = 0;
                if(lastDirection != null)
                {
                    if (lastDirection.x != 0)
                    {
                        if (lastDirection.x > 0)
                        {
                            aux_x = 1;
                        }
                        else
                        {
                            aux_x = -1;
                        }
                    }
                    if (lastDirection.z != 0)
                    {
                        if (lastDirection.z > 0)
                        {
                            aux_z = 1;
                        }
                        else
                        {
                            aux_z = -1;
                        }
                    }
                }

                Debug.Log("Direction: x:" + aux_x + " z:" + aux_z);

                if(aux_x != 0 || aux_z != 0)
                {
                    Vector3 direction = new Vector3(aux_x, 0, aux_z);
                    rb_fb.linearVelocity = direction * 10f;
                }
                else //Default direction if the character didnt move yet!
                {
                    Vector3 direction = new Vector3(0, 0, -1);
                    rb_fb.linearVelocity = direction * 10f;
                }

                Debug.Log("FireBall!");

            }

        }

        Vector3 inputDirection = new Vector3(-x, 0, -z);
        inputDirection.Normalize();
        if (x != 0 || z != 0)
        {
            lastDirection = inputDirection;
            float aux_speed = (speed * (isRunning ? runningMultiplier : 1));
            //Move the character
            rigidbody.linearVelocity = inputDirection * aux_speed;

            //Rotate the character to the direction where is moving
            rigidbody.rotation = Quaternion.Lerp(rigidbody.rotation, Quaternion.LookRotation(inputDirection), 0.07f);
            //Cancel the rotation inertia
            rigidbody.angularVelocity = Vector3.zero;
        }
        else
        {
            rigidbody.linearVelocity = new Vector3(0, 0, 0);
        }

        //Transform the input so that the character moves based on where it is looking.
        /*
        Vector3 inputDirection = new Vector3(-x, 0, -z);
        inputDirection.Normalize();
        if(x != 0 || z != 0)
        {
            //Rotate the character to the direction where is moving
            rigidbody.rotation = Quaternion.Lerp(rigidbody.rotation, Quaternion.LookRotation(inputDirection), 0.07f);

            //Move the character
            rigidbody.linearVelocity = inputDirection * (speed + (isRunning? runningMultiplier : 1) );

        }
        else
        {
            //Character always look at the cursor
            Vector3 mousePositionScreen = Input.mousePosition;
            Vector3 characterPositionScreen = Camera.main.WorldToScreenPoint(this.transform.position);

            //Tell the position where you are waching to rotate
            Vector3 direccio = new Vector3(
                characterPositionScreen.x - mousePositionScreen.x,
                0,
                characterPositionScreen.y - mousePositionScreen.y);
            //Lerp is like an animation in css to do it smooth not sharp
            rigidbody.rotation = Quaternion.Lerp(rigidbody.rotation, Quaternion.LookRotation(direccio), 0.07f);

            rigidbody.linearVelocity = new Vector3(0, 0, 0);
        }
        
        */

    }
}
