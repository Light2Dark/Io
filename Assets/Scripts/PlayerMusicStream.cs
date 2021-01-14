using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiPlayerTK;

public class PlayerMusicStream : MonoBehaviour
{
    public MidiFilePlayer music;
    public MusicNotesReader musicNotes;
    MidiStreamPlayer midiStreamPlayer;

    [Range(0,100)]
    public int volume;

    [Range(0,1000)]
    public int durationNote;

    int notePlayerVal, noteCompVal;
    int lowestNote = 48;
    int highestNote = 59;

    // Start is called before the first frame update
    void Start()
    {
        midiStreamPlayer = this.gameObject.GetComponent<MidiStreamPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space)) {
            MPTKEvent notePlaying = new MPTKEvent() {
                Command = MPTKCommand.NoteOn,
                Value = 55,
                Channel = 0,
                Duration = 1000, // 1 seconds but stop by the new note. See before.
                Velocity = 100, // Sound can vary depending on the velocity
                Delay = 0
            };
            midiStreamPlayer.MPTK_PlayEvent(notePlaying);
        }
        */


        if (Input.GetKeyDown(KeyCode.Space)) {
            if (music.MPTK_IsPaused) {
                UnpauseNotes();
            }
        }

    }

    public void PlayNote(int noteVal) {
        MPTKEvent notePlaying = new MPTKEvent() {
            Command = MPTKCommand.NoteOn,
            Value = noteVal,
            Channel = 0,
            Duration = 400, // 1000 is 1 seconds but stop by the new note. See before.
            Velocity = 100, // Sound can vary depending on the velocity
            Delay = 0
        };
        midiStreamPlayer.MPTK_PlayEvent(notePlaying);
        notePlayerVal = noteVal;
        CheckPlayerNotes();
    }

    public void PauseNotes(string notePassedInValue) { //pauses notes and music being played
        music.MPTK_Pause();
        musicNotes.PauseNotes(true); 
        int.TryParse(notePassedInValue, out noteCompVal);
        
        while (noteCompVal > highestNote) {
            noteCompVal = noteCompVal - 12;
        }

        while (noteCompVal < lowestNote) {
            noteCompVal += 12;
        }
        Debug.Log(noteCompVal);
    }

    public void UnpauseNotes() { // unpauses
        music.MPTK_UnPause();
        musicNotes.PauseNotes(false);
    }

    public void CheckPlayerNotes() {
        if (music.MPTK_IsPaused) {
            if (notePlayerVal == noteCompVal) {
                UnpauseNotes();
            }
        }
    }
}
