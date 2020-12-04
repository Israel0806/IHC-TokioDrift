using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class ComunicationStates : NetworkBehaviour
{

    
    [Header("Reference")]
    // [SerializeField] private Orb CS = null;
    private TrackController TC;
    private OrbController OC;

    
    public TextMeshProUGUI objective1;
    public TextMeshProUGUI objective2;

    //state 1 = delete it
    //state 2 = activate for all(only in case of tracks )

    // public delegate void ChangeSomeOrbe(int changeOrb, bool state); 
    // public delegate void ChangeSomeTrack(int changeTrack, int playerID, bool state);

    // [SyncEvent]
    // public event ChangeSomeOrbe EventChangeSomeOrbe;
    // [SyncEvent]
    // public event ChangeSomeTrack EventChangeSomeTrack;

    public override void OnStartServer()
    {
        // TC = GameObject.Find("===TRACK====").GetComponent<TrackController>();
        // OC = GameObject.Find("====ORB=====").GetComponent<OrbController>();
        print("ComunicationStates ready !");
    } 
    
    [ClientRpc]
    public void RpcSetChangeOrbe(int iden)
    {
        OC.auxAllOrbsCollected = true;
        foreach (Orb orb in OC.orbs)
        {
            if (orb != null && orb.identification == iden )
            {
                //print("Other player change his orb");
                OC.ActivateTrack(orb.trackAsignationForOrbe);
                //destroyOrd(orb);
                print("Number of Orb");
                print(iden);
                print("******************");
                orb.DestroyGameObject();
                OC.auxAllOrbsCollected = false;
                OC.SetOrbs();
                break;
            }
        }
        if (OC.auxAllOrbsCollected) OC.allOrbsCollected = true;
        // print("SetChangeOrbe");
        // print(changeOrb);
        // print("---------------"); 
        // EventChangeSomeOrbe?.Invoke(changeOrb, true); 
    }

    [ClientRpc]
    public void RpcSetChangeTrack(int iden, int playerID)
    {
        foreach (Track track in TC.tracks){
            // print("xxxxxxxxxxxxxxxxxxxxxxx");
            // print(track.identification);
            // print("xxxxxxxxxxxxxxxxxxxxxxx");
            if (track != null && track.identification == iden)
            {
                print("Number of Track");
                print(iden);
                print("******************");
                track.TrackRepaired();
                track.isReady = true;
                if(playerID == 1)
                {
                    string str = objective1.text;
                    int tracks = int.Parse(str.Substring(0, 1));
                    objective1.text = (tracks + 1).ToString() + " / 5";
                } else {
                    string str = objective2.text;
                    int tracks = int.Parse(str.Substring(0, 1));
                    objective2.text = (tracks + 1).ToString() + " / 5";
                }
            }
        }
        // print("SetChangeTrack");
        // print(changeTrack);
        // print("---------------"); 
        // EventChangeSomeTrack?.Invoke(changeTrack, playerID, true); 
    }

    [Command]
    private void CmdSetChangeOrbe(int val) => RpcSetChangeOrbe(val);

    [Command]
    private void CmdSetChangeTrack(int val, int playerID) => RpcSetChangeTrack(val, playerID);
    

    void Start()
    {
        TC = GameObject.Find("===TRACK====").GetComponent<TrackController>();
        OC = GameObject.Find("====ORB=====").GetComponent<OrbController>();

        if(TC == null)
            print("Not TC");
        if(OC == null)
            print("Not OC");
        
        objective1 = GameObject.Find("TextObjective1").GetComponent<TextMeshProUGUI>();
        objective2 = GameObject.Find("TextObjective2").GetComponent<TextMeshProUGUI>();
        
        print("ComunicationStates ready client !");
    }

    [Client]
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
                CmdSetChangeTrack(track.identification, track.playerID);
            }           
        }
            
         // Verify orbs 
        foreach (Orb orb in OC.orbs)
        {
            //print("Verify Orb_CS");
            if (orb != null && orb.isCollected) CmdSetChangeOrbe(orb.identification);   
        }               
    }

}
