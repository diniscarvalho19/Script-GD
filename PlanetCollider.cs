using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCollider : MonoBehaviour
{
    public GameObject DayWall;
    public GameObject NightWall;
    public bool day;

    private void OnTriggerEnter2D(Collider2D other) {
        DayWall.SetActive(day);
        NightWall.SetActive(!day);
    }
}
