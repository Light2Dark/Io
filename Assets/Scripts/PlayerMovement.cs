using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject LinesHolder;
    public float moveSpeed;

    // [Range(0, 0.3f)] [SerializeField] private float movementSmoothening;

    Rigidbody2D player;

    LinesScript lines;
    float lineSeparation, xStartPos, yStartPos;
    int noteValue;

    // Start is called before the first frame update
    void Start()
    {
        lines = LinesHolder.GetComponent<LinesScript>();
        lineSeparation = lines.lineSeparation;
        noteValue = 55; // 55 is E bottomE

        player = this.GetComponent<Rigidbody2D>();
        xStartPos = this.transform.position.x;
        yStartPos = this.transform.position.y;
    }

    void Update() {
        
    }

    public void Move(int steps) {
        float jumps = steps * lineSeparation/2;
        Vector2 position = new Vector2(xStartPos, yStartPos + jumps);
        player.MovePosition(position * moveSpeed * Time.fixedDeltaTime);   
    }

    public int GetPlayerValue() {
        return noteValue;
    }
}
