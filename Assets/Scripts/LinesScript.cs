using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinesScript : MonoBehaviour
{
    public float lineSeparation;
    GameObject cleff;

    List<Transform> lines = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        cleff = GameObject.FindGameObjectWithTag("cleff");
        cleff.gameObject.transform.Translate(Vector3.up * lineSeparation, Space.World);

        foreach(Transform lineTransform in this.transform) {
            lines.Add(lineTransform);
        }
        Debug.Log(lines[1].position.y);

        for (int i = 1; i < 5; i++) {
            lines[i].Translate(Vector3.up * lineSeparation * i, Space.World);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
