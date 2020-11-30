﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using KartGame.KartSystems;

public class Orb : MonoBehaviour
{
    private KartController asd;
    public Rigidbody rigidBody;
    public int identification;

    [Header("Orb stat")]
    public int  trackAsignationForOrbe = -1;
    public bool isCollected = false;

    // public delegate void ChangeSomeOrbe(int changeOrb, bool state);

    // [SyncEvent]
    // public event ChangeSomeOrbe EventChangeSomeOrbe;

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    public GameObject GetObject()
    {
        return gameObject;
    }

    /*
    #region Server 
    
    [Server]
    private void SetChangeOrbe(int changeOrb)
    {
        print("SetChangeOrbe");
        EventChangeSomeOrbe?.Invoke(changeOrb, true); 
    }

    [Command]
    private void CmdSetChangeOrbe(int val) => SetChangeOrbe(val);

    #endregion
    */

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
         print("UNO-1O");
        //Hit another player
        if (co.tag.Equals("Player") && co.GetComponent<KartController>().TrackAssign == -1)
        {
            print("UNO-20");   
            //set the tracker corresponce  
            isCollected = true;
            print("UNO-30");
            co.GetComponent<KartController>().TrackAssign = trackAsignationForOrbe;
            print("UNO-40");
        }
    }
/*
    #region Client 
    
    [ClientCallback]
    private void Update()
    {
        if (!hasAuthority) { return;} 
        if (isCollected) CmdSetChangeOrbe(identification);               
    }

    #endregion
*/
}
