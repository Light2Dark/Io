using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiPlayerTK;

public class PlayerMusicStream : MonoBehaviour
{
    public MidiFilePlayer music;
    public MusicNotesReader musicNotes;
    public int notesMissed;
    MidiStreamPlayer midiStreamPlayer;

    public PlayerMovement player;

    [Range(0,100)]
    public int volume;

    [Range(0,1000)]
    public int durationNote;

    int notePlayerVal = -2;
    int noteCompVal = -1;
    int lowestNote = 48;
    int highestNote = 71;  // should change to 71
    int octaveDifference = 12;

    public event System.Action TooManyMistakes;
    int limitMistakes = 100;
    bool gameOver;

    public bool pauseNotesSetting;
    bool creativeMode;

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

        if ( (notesMissed >= limitMistakes) && !gameOver && creativeMode == false) {
            if (TooManyMistakes != null) {
                TooManyMistakes();
            }
            gameOver = true;
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
        player.Move(noteVal);
        midiStreamPlayer.MPTK_PlayEvent(notePlaying);
        notePlayerVal = noteVal;
    }


    public void PauseNotes() { //pauses notes and music being played
        music.MPTK_Pause();
        musicNotes.PauseNotes(true); 
    }

    
    public void UnpauseNotes() { // unpauses
        if(music.MPTK_IsPaused) {
            music.MPTK_UnPause();
            musicNotes.PauseNotes(false);
        }
    }

    public void ReceiveNotes(string notePassedInValue) { // the note passed from MIDI is stored in noteCompVal
        int.TryParse(notePassedInValue, out noteCompVal);
        
        while (noteCompVal > highestNote) {
            noteCompVal = noteCompVal - octaveDifference;
        }

        while (noteCompVal < lowestNote) {
            noteCompVal += octaveDifference;
        }
    }

    public bool CheckPlayerNotes() {
        return noteCompVal == notePlayerVal ? true : false;
    }

    public void SetPauseNotesSetting(bool set) {
        pauseNotesSetting = set;
    } 

    public void SetLimitMistakes(int limit) {
        limitMistakes = limit;
    }

    public void SetCreativeMode(bool set) {
        creativeMode = set;
    }
}
