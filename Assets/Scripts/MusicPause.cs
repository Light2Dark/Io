using UnityEngine;
using MidiPlayerTK;

public class MusicPause : MonoBehaviour
{
    public PlayerMusicStream musicStream;

    private void OnTriggerEnter2D(Collider2D other) {
        musicStream.PauseNotes(other.transform.parent.name);
    }
}
