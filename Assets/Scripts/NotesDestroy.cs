using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesDestroy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(other.gameObject.transform.parent.gameObject);
    }
}
