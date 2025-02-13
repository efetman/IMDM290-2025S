using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HeartCreate : MonoBehaviour
{
    GameObject[] spheres;
    static int numSphere = 200; 
    Vector3[] initPos;
    float time = 0f;

    void Start()
    {
        spheres = new GameObject[numSphere];
        initPos = new Vector3[numSphere];

        float r = 5f; // Scaling factor for better visibility
        float sqrt2 = Mathf.Sqrt(2); // Precompute sqrt(2)

        for (int i = 0; i < numSphere; i++)
        {
            spheres[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere); 
            float t = i * 2 * Mathf.PI / numSphere; // Parameter to spread spheres along the shape
            
            // Heart parametric equations
            float x = r * (sqrt2 * Mathf.Pow(Mathf.Sin(t), 3));
            float y = r * (-Mathf.Pow(Mathf.Cos(t), 3) - Mathf.Pow(Mathf.Cos(t), 2) + 2 * Mathf.Cos(t));

            initPos[i] = new Vector3(x, y, 0f); // Set positions
        
            spheres[i].transform.position = initPos[i];

            Renderer sphereRenderer = spheres[i].GetComponent<Renderer>();

            // Alternate between red (~0.0) and pink (~0.85)
            float hue = (i % 10 < 5) ? 0.0f : 0.85f;

            Color color = Color.HSVToRGB(hue, 1f, 1f); // Full saturation and brightness
            sphereRenderer.material.color = color;
        }
    }
    void Update(){
         time += Time.deltaTime * 10f; // Increment time for animation
        
        for (int i = 0; i < numSphere; i++)
        {

            // Update color based on time, creating a pulsating effect
            Renderer sphereRenderer = spheres[i].GetComponent<Renderer>();

            float hue = (i % 10 < 5) ? 0.0f : 0.85f;

              // Introduce a wave effect around the heart using an index-based phase shift
        float phase = (float)i / numSphere * Mathf.PI * 2f; // Offsets each sphere in the oscillation cycle

        // Ensure saturation never goes below 1.0 by offsetting sine wave above 1.0
        float saturation = 1.0f + 0.3f * (0.5f + 0.5f * Mathf.Sin(time * 1.5f + phase)); // Shifts range to [1.0, 1.3]

        // Brightness oscillation (remains dynamic)
        float brightness = 1.0f + 0.3f * Mathf.Sin(time * 1.5f + phase);

            Color color = Color.HSVToRGB(hue, saturation, brightness); // HSV color with time-based changes
            sphereRenderer.material.color = color;

            float popOutOffset = (i % 10 < 5) ? Mathf.Sin(time ) * 1.7f : -Mathf.Sin(time) * 1.7f;

        // Update position (adding Z-movement for pop-out effect)
        spheres[i].transform.position = initPos[i] 
                                        + new Vector3(0, 0, popOutOffset);

         
    }
}
}
