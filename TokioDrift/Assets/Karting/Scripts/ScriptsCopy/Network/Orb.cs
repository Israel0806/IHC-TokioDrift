using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using KartGame.KartSystems;

public class Orb : MonoBehaviour
{
    private KartController asd;
    public Rigidbody rigidBody;
    public int identification;
    public int playerID;

    [Header("Orb stat")]
    public int  trackAsignationForOrbe = -1;
    public bool isCollected = false;

    // public delegate void ChangeSomeOrbe(int changeOrb, bool state);

    // [SyncEvent]
    // public event ChangeSomeOrbe EventChangeSomeOrbe;

    public void DestroyGameObject()
    {
        Destroy(gameObject, 1);
    }

    public GameObject GetObject()
    {
        return gameObject;
    }

    void OnTriggerEnter(Collider co)
    {
        ArcadeKart kart = co.GetComponent<ArcadeKart>();
        //Hit another player
        if (co.tag.Equals("Player") && kart.TrackAssign == -1 && co.gameObject.GetComponent<KartController>().playerID == playerID)
        {
            //set the tracker corresponce  
            isCollected = true;
            kart.TrackAssign = trackAsignationForOrbe;
        }
    }
}
