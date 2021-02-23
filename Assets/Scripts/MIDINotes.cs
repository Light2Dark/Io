using System.Collections;           // right now, we're not recording notes played simultaneously.
using System.Collections.Generic;
using UnityEngine;

public class MIDINotes : MonoBehaviour
{
    public float timeToPlayNote = 3f;

    PlayerMusicStream musicStream;
    bool userPlayedNote = false;
    bool passedTrigger;
    bool noteTimerRanOut;

    bool pauseNotesSetting;

    private void OnTriggerEnter2D(Collider2D other) {
        passedTrigger = true;
        musicStream.ReceiveNotes(this.transform.parent.name); //// EHEHEHEHHE?????? IM DOING THIS AGAIN????
    }

    private void Start() {
        musicStream = GameObject.FindGameObjectWithTag("Music Stream").GetComponent<PlayerMusicStream>();
        pauseNotesSetting = musicStream.pauseNotesSetting;
    }

    private void Update() {
        if (passedTrigger) {

            if (pauseNotesSetting) { // music should pause
                musicStream.PauseNotes();
            }

            timeToPlayNote -= Time.deltaTime; // timer is started
            // musicStream.ReceiveNotes(this.transform.parent.name);

        } else return;

        if(!noteTimerRanOut) {  // if not timer has not reached 0, keep checking the note
            userPlayedNote = musicStream.CheckPlayerNotes(); // this method is returning false if user doesnt play anything
            if (userPlayedNote)  {
                Debug.Log("note hit");
                musicStream.UnpauseNotes();
                Destroy(this.transform.parent.gameObject, 0.2f);
            }
        }

        if (timeToPlayNote <= 0 && pauseNotesSetting == false) { // note timer only runs out if pause notes is not selected
            noteTimerRanOut = true;
        }

        if (noteTimerRanOut) { // user does not play note and time runs out to play note
            musicStream.notesMissed += 1;
            userPlayedNote = true;
            Destroy(this.transform.parent.gameObject);
        }
    }
}
