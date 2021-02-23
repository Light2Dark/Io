using UnityEngine;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public GameObject LinesHolder;
    public float moveSpeed;

    public MusicNotesReader Notes;
    Dictionary <int, int> numJumps;

    // [Range(0, 0.3f)] [SerializeField] private float movementSmoothening;

    LinesScript lines;
    float lineSeparation, xStartPos, yStartPos;
    int noteValue;

    // Start is called before the first frame update
    void Start()
    {
        lines = LinesHolder.GetComponent<LinesScript>();
        lineSeparation = lines.lineSeparation;

        numJumps = Notes.NumberOfJumpsDict;

        this.transform.position = Notes.transform.position;
        xStartPos = this.transform.position.x;
        yStartPos = this.transform.position.y;
    }

    public void Move(int noteValue) {
        int jumps = numJumps[noteValue];
        Vector2 position = new Vector2(xStartPos, (lineSeparation/2) * jumps + yStartPos);
        
        this.transform.position = new Vector3(position.x, position.y, 0);
    }

    public int GetPlayerValue() {
        return noteValue;
    }
}
