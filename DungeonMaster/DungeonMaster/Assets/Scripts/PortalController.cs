using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.Controls.AxisControl;

public class PortalController : MonoBehaviour
{

    public Transform Lamp;

    public int NeedKeys = 4;

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
            KeysController kc = other.transform.GetComponent<KeysController>();
            Debug.Log("Player try enter at portal! "+kc.keys);
            if(kc.keys >= NeedKeys)
            {
                SceneManager.LoadScene("GameFinish");
            }
        }

    }


    public void changeActivePortal(bool active)
    {
        Light light = Lamp.GetComponent<Light>();

        if (active)
        {
            light.intensity = 3f;
        }
        else
        {
            light.intensity = 0f;
        }

    }


}
