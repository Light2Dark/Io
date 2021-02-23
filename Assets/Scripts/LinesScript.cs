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

        for (int i = 1; i < 5; i++) { // for first 5 lines, treble cleff.
            lines[i].Translate(Vector3.up * lineSeparation * i, Space.World);
        }

        int m = 2;
        for (int i = 5; i < 10; i++) {
            lines[i].Translate(Vector3.down * lineSeparation * m, Space.World);
            m++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
