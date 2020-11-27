using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class ComunicationStates : NetworkBehaviour
{

    //private int sendOrbToEliminate;
    //private int sendTrackToQuitFire;
    //[SyncVar]
    //private int orbe
    
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
               
    }
    #endregion
}
