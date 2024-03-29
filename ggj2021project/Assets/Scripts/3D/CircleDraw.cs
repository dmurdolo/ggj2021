using UnityEngine;
using System.Collections;

public class CircleDraw : MonoBehaviour {   
  float theta_scale = 0.01f;   //Set lower to add more points
  int size;   // Total number of points in circle
  public float radius = 4f;
  LineRenderer lineRenderer;

  void Awake ()
  {       
    float sizeValue = (2.0f * Mathf.PI) / theta_scale; 
    size = (int)sizeValue;
    size++;
    lineRenderer = gameObject.AddComponent<LineRenderer>();
    lineRenderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
    
    //lineRenderer.SetWidth(0.02f, 0.02f); //thickness of line
    lineRenderer.startWidth = 0.1f;
    lineRenderer.endWidth = 0.1f;

    //lineRenderer.SetVertexCount(size); 
    lineRenderer.positionCount = size;     
  }

  void Update () {      
    Vector3 pos;
    float theta = 0f;
    for(int i = 0; i < size; i++){          
      theta += (2.0f * Mathf.PI * theta_scale);         
      float x = radius * Mathf.Cos(theta);
      float y = radius * Mathf.Sin(theta);          
      x += gameObject.transform.position.x;
      y += gameObject.transform.position.y;
      pos = new Vector3(x, y, 0);
      lineRenderer.SetPosition(i, pos);
    }
  }
}