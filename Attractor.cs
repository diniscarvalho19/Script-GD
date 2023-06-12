using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public LayerMask AttractionLayer;
    public float gravity = -10;
    [SerializeField] private float effectionRadius = 10;
    public List<Collider2D> AttractedObjects = new List<Collider2D>();
    [HideInInspector] public Transform planetTransform;
    public GameObject player;
    private bool final = false;

    public CameraFollow cameraFollow;
    
    void Awake()
    {
        planetTransform = GetComponent<Transform>();
    }

    void Update()
    {
        SetAttractedObjects();
    }

    void FixedUpdate()
    {
        AttractObjects();
        if(AttractedObjects.Count > 1){
            effectionRadius = 60.0f;
            StartCoroutine(ChangeGravity(-15f));
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, effectionRadius);
    }

    void SetAttractedObjects()
    {
        AttractedObjects = Physics2D.OverlapCircleAll(planetTransform.position, effectionRadius, AttractionLayer).ToList();// can be optimized
    }

    void AttractObjects()
    {
        for (int i = 0; i < AttractedObjects.Count; i++)
        {
            AttractedObjects[i].GetComponent<Attractable>().Attract(this);
        }
    }

    private IEnumerator ChangeGravity(float gValue){
        yield return new WaitForSeconds(1.0f);
        gravity = gValue;
        cameraFollow.ActivateRotate();
        yield return new WaitForSeconds(1.0f);
        if(!final)
            player.GetComponent<Rigidbody2D>().mass = 15;
        player.GetComponent<HatController>().FemoralNerve(false);
    }

    public void TheEnd(){
        final = true;
    }

}

