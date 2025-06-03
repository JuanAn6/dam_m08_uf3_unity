using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{

    public Transform[] asteroids;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnAsteroids());
    }

    private IEnumerator SpawnAsteroids()
    {
        while (true)
        {

            Vector3 pos = Camera.main.ScreenToWorldPoint(
                    new Vector3(UnityEngine.Random.Range(0, Camera.main.pixelWidth), Camera.main.pixelHeight)
                );


            Vector3 position = new Vector3(pos.x, 1f, pos.z);


            yield return new WaitForSeconds(1);
            Instantiate(asteroids[UnityEngine.Random.Range(0,asteroids.Length)], position, Quaternion.identity);

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GameOver()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
