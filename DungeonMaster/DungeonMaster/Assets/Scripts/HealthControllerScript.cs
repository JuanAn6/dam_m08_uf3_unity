using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthControllerScript : MonoBehaviour
{

    public Slider Slider;
    public Image Bar;
    public int MaxHealth = 200;
    private int Health = 200;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Health = MaxHealth;
        Slider.value = MaxHealth;
        Slider.maxValue = MaxHealth;
        Slider.minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Slider.value = Health;
        Bar.color = Color.Lerp(Color.red, Color.green, (float)(Health / Slider.maxValue));
    }

    //Negative
    internal void UpdateHealth(int healthScript)
    {
        //Si el personaje se queda sin vida se acaba el juego
        if((Health + healthScript) <= 0 )
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            //Mantiene el valor entre los dos del final
            Health = Math.Clamp(Health + healthScript, 0, MaxHealth);
        }

    }

}
