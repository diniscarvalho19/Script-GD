using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFall : MonoBehaviour
{
    
    public HatController player;

    private void OnTriggerEnter2D(Collider2D other) {
        player.FemoralNerve(true);
    }
}
