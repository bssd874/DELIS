using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertiesVisualizer : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public float Multiplyer;
    public float distance;

    public float min;

    public int samples;
    
    void Update()
    {
        float[] x = new float[samples];
        Vector3[] poses = new Vector3[samples]; 
        if (LevelSelectorScript.main.audioSource.isPlaying)
        {
            LevelSelectorScript.main.audioSource.clip.GetData(x, LevelSelectorScript.main.audioSource.timeSamples);
            for (int i = 0; i < x.Length; i++) 
            {
                Vector2 pos = new Vector2(Mathf.Clamp(x[i], min, Mathf.Infinity) * Multiplyer, i * distance);
                poses[i] = pos;
            }
        }
        lineRenderer.SetPositions(poses);
        
        
    }


}
