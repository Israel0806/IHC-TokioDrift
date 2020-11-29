using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class ComunicationStates : NetworkBehaviour
{

    
    [Header("Reference")]
    // [SerializeField] private Orb CS = null;
    [SerializeField] private TrackController TC;
    [SerializeField] private OrbController OC;

    //state 1 = delete it
    //state 2 = activate for all(only in case of tracks )

    public delegate void ChangeSomeOrbe(int changeOrb, bool state); 
    public delegate void ChangeSomeTrack(int changeTrack, bool state);

    [SyncEvent]
    public event ChangeSomeOrbe EventChangeSomeOrbe;
    [SyncEvent]
    public event ChangeSomeTrack EventChangeSomeTrack;

    #region Server 
    
    [Server]
    private void SetChangeOrbe(int changeOrb)
    {
        EventChangeSomeOrbe?.Invoke(changeOrb, true); 
    }

    [Server]
    private void SetChangeTrack(int changeTrack)
    {
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
            if (track != null && track.isRepaired) CmdSetChangeTrack(track.identification);           
        }
            
         // Verify orbs 
        foreach (Orb orb in OC.orbs)
        {
            if (orb != null && orb.isCollected) CmdSetChangeOrbe(orb.identification);   
        }               
    }

    #endregion
}
