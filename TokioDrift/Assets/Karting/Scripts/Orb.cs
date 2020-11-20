using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Orb : NetworkBehaviour
{
    
    public Rigidbody rigidBody;

    [Header("Orb stat")]
    public int  trackAsignationForOrbe = -1;
    public bool isCollected = false;
    //public GameObject source;

    //Start the server
    /*public override void OnStartServer()
        {
        
        }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
    [ServerCallback]
    void OnTriggerEnter(Collider co)
    {
        //Hit another player
        if (co.tag.Equals("Player") && co.GetComponent<KartController>().TrackAssign == -1 )
        {
            //set the tracker corresponce  
            co.GetComponent<KartController>().TrackAssign = trackAsignationForOrbe;
            isCollected = true;

        }
    }

}
