using UnityEngine;

public class TailingScript : MonoBehaviour
{
    public int N;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Material material = GetComponent<Renderer>().material;
        
        float a = Random.Range(0, N) / (float)N;
        float b = Random.Range(0, N) / (float)N;

        material.SetTextureOffset("_MainTex", 
            new Vector2(a,b)
            );
    }

}
