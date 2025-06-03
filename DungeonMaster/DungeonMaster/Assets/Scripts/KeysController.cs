using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.UI;

public class KeysController : MonoBehaviour
{
    public Image imagePrefab;
    public Canvas mainCanvas;

    public Transform Portal;
    private PortalController PortalController;

    public int keys = 0;
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
        if (other.gameObject.CompareTag("Key"))
        {
            Image image = Instantiate(imagePrefab, mainCanvas.transform);
            image.transform.position = new Vector3(image.transform.position.x, image.transform.position.y + (keys * 50 + 5));
            Debug.Log("image.transform.position.x" + image.transform.position.x + ", image.transform.position.y" + image.transform.position.y);
            keys++;
            
            Debug.Log("KEYS: " + keys + " need keys: "+ PortalController.NeedKeys);

            if (keys >= PortalController.NeedKeys)
            {
                Debug.Log("Active portal!");
                Portal.GetComponent<PortalController>().changeActivePortal(true);
            }

            Destroy(other.gameObject);

        }
    }


    public void setPortal(Transform portal)
    {
        Portal = portal;
        PortalController = Portal.GetComponent<PortalController>();
    }


    

}
