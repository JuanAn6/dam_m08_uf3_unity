using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour, HealthListener
{
    public Transform spaceShip;
    private Transform spaceShipChild;
    public Camera camera;

    private Slider slider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spaceShipChild = spaceShip.Find("HealthBarPosition");
        slider = GetComponent<Slider>();
        SpaceShipScript ss = spaceShip.GetComponent<SpaceShipScript>();
        ss.registerHealthListener(this);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = camera.WorldToScreenPoint(spaceShipChild.transform.position);
        this.transform.position = screenPos;
    }

    
    public void OnHealthChanged(int healthPoints)
    {
        slider.value = healthPoints;
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
    }
}
