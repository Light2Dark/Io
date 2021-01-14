using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MidiPlayerTK;

public class MusicNotesReader : MonoBehaviour
{
    public GameObject songNoteOri;
    public GameObject songNotesHolder;
    public LinesScript lines;
    public float rightOffset;
    public float noteMoveSpeed;
    public float timeForNoteDestroy;

    float songDuration, songTempo;
    int noteValue;
    // 52 is E4 which shld be starting position
    float noteDuration, lineSeparation, screenWidth, screenHeight;
    Quaternion rotation;
    Vector3 startPosNote;

    bool musicPaused;

    Dictionary <int, string> NumberNoteDict = new Dictionary<int, string> {
        {22, "A#1"},
        {23, "B1"},
        {24, "C2"},
        {25, "C#2"},
        {26, "D2"},
        {27, "D#2"},
        {28, "E2"},
        {29, "F2"},
        {30, "F#2"},
        {31, "G2"},
        {32, "G#2"},
        {33, "A2"},
        {34, "A#2"},
        {35, "B2"},
        {36, "C3"},
        {37, "C#3"},
        {38, "D3"},
        {39, "D#3"},
        {40, "E3"},
        {41, "F3"},
        {42, "F#3"},
        {43, "G3"},
        {44, "G#3"},
        {45, "A3"},
        {46, "A#3"},
        {47, "B3"},
        {48, "C4"},
        {49, "C#4"},
        {50, "D4"},
        {51, "D#4"},
        {52, "E4"},
        {53, "F4"},
        {54, "F#4"},
        {55, "G4"},
        {56, "G#4"},
        {57, "A4"},
        {58, "A#4"},
        {59, "B4"},
        {60, "C5"},
        {61, "C#5"},
        {62, "D5"},
        {63, "D#5"},
        {64, "E5"},
        {65, "F5"},
        {66, "F#5"},
        {67, "G5"},
        {68, "G#5"},
        {69, "A5"},
        {70, "A#5"},
        {71, "B5"},
        {72, "C6"},
        {73, "C#6"},
        {74, "D6"},
        {75, "D#6"},
        {76, "E6"},
        {77, "F6"},
        {78, "F#6"},
        {79, "G6"},
        {80, "G#6"},
        {81, "A6"},
        {82, "A#6"},
        {83, "B6"},
        {84, "C7"},
        {85, "C#7"},
        {86, "D7"},
        {87, "D#7"},
        {88, "E7"},
        {89, "F7"},
        {90, "F#7"},
        {91, "G7"},
        {92, "G#7"},
        {93, "A7"},
        {94, "A#7"},
        {95, "B7"},
        {96, "C8"}
    };

    /*
    Dictionary <string, int> NumberOfJumpsDict = new Dictionary<string, int> {
        {"C2", -16},
        {"C#2", -16},
        {"D2", -15},
        {"D#2", -15},
        {"E2", -14},
        {"F2", -13}, // 29
        {"F#2", -13},
        {"G2", -12},
        {"G#2", -12},
        {"A2", -11},
        {"A#2", -11},
        {"B2", -10},
        {"C3", -9},
        {"C#3", -9},
        {"D3", -8},
        {"D#3", -8},
        {"E3", -7},
        {"F3", -6}, // 41
        {"F#3", -6},
        {"G3", -5},
        {"G#3", -5},
        {"A3", -4},
        {"A#3", -4},
        {"B3", -3},
        {"C4", -2},
        {"C#4", -2},
        {"D4", -1},
        {"D#4", -1},
        {"E4", 0}, // 52
        {"F4", 1}, // 53
        {"F#4", 1},
        {"G4", 2},
        {"G#4", 2},
        {"A4", 3},
        {"A#4", 3},
        {"B4", 4},
        {"C5", 5},
        {"C#5", 5},
        {"D5", 6},
        {"D#5", 6},
        {"E5", 7},
        {"F5", 8},
        {"F#5", 8},
        {"G5", 9},
        {"G#5", 9},
        {"A5", 10},
        {"A#5", 10},
        {"B5", 11},
        {"C6", 12},
        {"C#6", 12},
        {"D6", 13},
        {"D#6", 13},
        {"E6", 3},
        {"F6", 4},
        {"F#6", 5},
        {"G6", 5},
        {"G#6", 6},
        {"A6", 6},
        {"A#6", 7},
        {"B6", 8},
        {"C7", 8},
        {"C#7", 9},
        {"D7", 9},
        {"D#7", 10},
        {"E7", 10},
        {"F7", 11},
        {"F#7", 12},
        {"G7", 12},
        {"G#7", 13},
        {"A7", 13}
    }; */

    Dictionary <int, int> NumberOfJumpsDict = new Dictionary<int, int> {};

    private void Start() {    

        MakeDictNumJumps();
        lineSeparation = lines.lineSeparation;
        rotation = songNoteOri.transform.rotation;
        Camera mainCam = Camera.main;
        
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        Vector3 rightPos = new Vector3(screenWidth - rightOffset, 0, 0);
        startPosNote = mainCam.ScreenToWorldPoint(rightPos);
    }

    private void Update() {
        // Moving note from right to left, hv to do this at the same time as spawning notes.
        if (!musicPaused) {
            songNotesHolder.transform.Translate(Vector3.left * noteMoveSpeed * Time.deltaTime, Space.World);
        }
    }

    private void MakeNote(int noteValue) {

        string noteName = NumberNoteDict[noteValue];
        int numJumps = NumberOfJumpsDict[noteValue];

        float yPosition = (lineSeparation / 2) * numJumps; // WE STIL NEED TO THINK ABT SHARPS AND FLATS
        Vector3 position = new Vector3 (startPosNote.x, yPosition, 0);

        GameObject songNote = Instantiate(songNoteOri, position, rotation); // starts from 55
        songNote.name = noteValue.ToString();
        songNote.transform.SetParent(songNotesHolder.transform);
    }

    public void Call(List<MPTKEvent> events) {
        foreach (var item in events)
        {
            // 65 is C5 or normal C, 51 is C4 or low C, 79 is C6 or high C.
            noteValue = item.Value;
            noteDuration = item.Duration;
            if (item.Command.ToString() == "NoteOn") {
                MakeNote(noteValue);
            }
        }
    }

    public void PauseNotes(bool pause) {
        musicPaused = pause;
    }

    private void MakeDictNumJumps() {
        int counter = 0;
        int jumpCounter = 0;  // for the 3&4 half step
        int numJumps = -34;
        int numBeforeNextSkip = 7;

        for (int i = -7; i <= 127; i++) {

            if (jumpCounter == numBeforeNextSkip) {
                numJumps++;
                jumpCounter = 0; 
                counter = 0;

                numBeforeNextSkip = numBeforeNextSkip == 7 ? 5 : 7;
            }
            if (counter == 2) {
                numJumps++;
                counter = 0;
            } 
            NumberOfJumpsDict.Add(i, numJumps);
            counter++;

            jumpCounter++;
        }
    }

}