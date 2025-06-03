using Unity.VisualScripting;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    public Transform character;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(character.position.x, this.transform.position.y, character.position.z);
    }

}
