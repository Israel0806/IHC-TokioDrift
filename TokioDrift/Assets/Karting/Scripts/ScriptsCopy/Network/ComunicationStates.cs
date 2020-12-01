using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class ComunicationStates : NetworkBehaviour
{

    
    [Header("Reference")]
    // [SerializeField] private Orb CS = null;
    private TrackController TC;
    private OrbController OC;

    //state 1 = delete it
    //state 2 = activate for all(only in case of tracks )

    public delegate void ChangeSomeOrbe(int changeOrb, bool state); 
    public delegate void ChangeSomeTrack(int changeTrack, bool state);

    [SyncEvent]
    public event ChangeSomeOrbe EventChangeSomeOrbe;
    [SyncEvent]
    public event ChangeSomeTrack EventChangeSomeTrack;

    #region Server

    public override void OnStartServer()
    {
        TC = GameObject.Find("===TRACK====").GetComponent<TrackController>();
        OC = GameObject.Find("====ORB=====").GetComponent<OrbController>();
        print("ComunicationStates ready !");
    } 
    
    [Server]
    private void SetChangeOrbe(int changeOrb)
    {
        print("SetChangeOrbe");
        print(changeOrb);
        print("---------------"); 
        EventChangeSomeOrbe?.Invoke(changeOrb, true); 
    }

    [Server]
    private void SetChangeTrack(int changeTrack)
    {
        print("SetChangeTrack");
        print(changeTrack);
        print("---------------"); 
        EventChangeSomeTrack?.Invoke(changeTrack, true); 
    }

    [Command]
    private void CmdSetChangeOrbe(int val) => SetChangeOrbe(val);

    [Command]
    private void CmdSetChangeTrack(int val) => SetChangeTrack(val);
    

    #endregion


    #region Client 
    [ClientCallback]
    private void Update()
    {
        
        if (!hasAuthority) { return;} 
        // Verify tracks 
        foreach (Track track in TC.tracks)
        {
            //print("Verify tracks_ CS");
            
            if (track != null && track.isRepaired && !track.isReady ) 
            {
                track.isReady = true;
                CmdSetChangeTrack(track.identification);
            }           
        }
            
         // Verify orbs 
        foreach (Orb orb in OC.orbs)
        {
            //print("Verify Orb_CS");
            if (orb != null && orb.isCollected) CmdSetChangeOrbe(orb.identification);   
        }               
    }

    #endregion
}
