using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FOV : MonoBehaviour
{


    void Start () {
	
		Collider2D playerCollider = GameObject.Find("Hat").GetComponent<Collider2D>();
        Collider2D fovCollider =  GameObject.Find("Fov").GetComponent<Collider2D>();	
        Collider2D floorCollider =  GameObject.Find("Floor").GetComponent<Collider2D>();	
		Physics2D.IgnoreCollision(fovCollider, playerCollider);
        Physics2D.IgnoreCollision(fovCollider, floorCollider);
        
		
    }


}

