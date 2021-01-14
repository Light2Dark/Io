using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// [RequireComponent(typeof(LineRenderer))]
public class CircleNote : MonoBehaviour
{
    public Vector3 adjustmentSize;

    LinesScript lines;

    void Start() {
        lines = GameObject.FindGameObjectWithTag("Lines").GetComponent<LinesScript>();
        Vector3 size = new Vector3(1,1,0);
        size = size * lines.lineSeparation + adjustmentSize;
        this.transform.localScale = size;
    }

    void Update() {
        Vector3 size = new Vector3(1,1,0);
        size = size * lines.lineSeparation + adjustmentSize;
        this.transform.localScale = size;
    }


/* LINE RENDERER METHOD

     public int vertexCount = 40; // 4 vertices == square
     public float lineWidth = 0.2f;

     private LineRenderer lineRenderer;
     private LinesScript lines;
     private float radius;
 
     private void Awake()
     {
         lineRenderer = GetComponent<LineRenderer>();
         SetupCircle();
     }

     private void Start() {
         lines = GameObject.FindWithTag("Lines").GetComponent<LinesScript>();
         radius = lines.lineSeparation/2;
     }
 
     private void SetupCircle()
     {
         lineRenderer.widthMultiplier = lineWidth;
 
         float deltaTheta = (2f * Mathf.PI) / vertexCount;
         float theta = 0f;
 
         lineRenderer.positionCount = vertexCount;
         for (int i=0; i<lineRenderer.positionCount; i++)
         {
             Vector3 pos = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f);
             lineRenderer.SetPosition(i, pos);
             theta += deltaTheta;
         }
     }
 
 #if UNITY_EDITOR
     private void OnDrawGizmos()
     {
         float deltaTheta = (2f * Mathf.PI) / vertexCount;
         float theta = 0f;
 
         Vector3 oldPos = Vector3.zero;
         for (int i=0; i<vertexCount + 1; i++)
         {
             Vector3 pos = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f);
             Gizmos.DrawLine(oldPos, transform.position + pos);
             oldPos = transform.position + pos;
 
             theta += deltaTheta;
         }
     }
 #endif
 */
 }
