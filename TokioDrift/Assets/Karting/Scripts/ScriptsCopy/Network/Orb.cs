using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using KartGame.KartSystems;

public class Orb : MonoBehaviour
{
    private KartController asd;
    public Rigidbody rigidBody;

    [Header("Orb stat")]
    public int  trackAsignationForOrbe = -1;
    public bool isCollected = false;

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    public GameObject GetObject()
    {
        return gameObject;
    }

    //void OnCollisionEnter(Collision target)
    //{
    //    if (target.gameObject.tag == "Player")
    //    {
    //        print("bang");
    //        target.gameObject.GetComponent<KartController>().TrackAssign = trackAsignationForOrbe;
    //        isCollected = true;
    //    }
    //}

    void OnTriggerEnter(Collider co)
    {
        //Hit another player
        if (co.tag.Equals("Player") && co.GetComponent<KartController>().TrackAssign == -1)
        {
            //set the tracker corresponce  
            isCollected = true;
            co.GetComponent<KartController>().TrackAssign = trackAsignationForOrbe;
        }
    }

}
