using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;

public class ControlComunication : MonoBehaviour
{
    //Some references
    
    [Header("Reference")]
    private ComunicationStates CS = null;
    [SerializeField] private TrackController TC = null;
    [SerializeField] private OrbController OC = null;

    void Awake()
    {
        print("ControlComunication ready !");
    }

    void OnEnable()
    {
        ArcadeKart []karts = FindObjectsOfType<ArcadeKart>();
        foreach (ArcadeKart kart in karts){
            if(kart.isLocalPlayer) CS = kart.GetGameObject().GetComponent<ComunicationStates>();
        }
                
        CS.EventChangeSomeOrbe += HandleChangeOfOrbe;
        CS.EventChangeSomeTrack += HandleChangeOfTrack;
        print("onEnable CCS");
        print("si");
        print("---------------"); 
    }

    void  onDisable()
    {
        CS.EventChangeSomeOrbe -= HandleChangeOfOrbe;       
        CS.EventChangeSomeTrack -= HandleChangeOfTrack;
        print("onDisable CCS");
        print("si");
        print("---------------");
    }

    private void HandleChangeOfOrbe(int iden, bool state)
    {
        OC.auxAllOrbsCollected = true;
        foreach (Orb orb in OC.orbs)
        {
            if (orb != null && orb.identification == iden && state)
            {
                //print("Other player change his orb");
                OC.ActivateTrack(orb.trackAsignationForOrbe);
                //destroyOrd(orb);
                print("Number of Orb");
                print(iden);
                print("******************");
                orb.DestroyGameObject();
                OC.auxAllOrbsCollected = false;
            }
        }
        if (OC.auxAllOrbsCollected) OC.allOrbsCollected = true;
    }

    private void HandleChangeOfTrack(int iden, bool state)
    {
        //change my orbs and track with this new information 
        foreach (Track track in TC.tracks){
            // print("xxxxxxxxxxxxxxxxxxxxxxx");
            // print(track.identification);
            // print("xxxxxxxxxxxxxxxxxxxxxxx");
            if (track != null && track.identification == iden  && state)
            {
                print("Number of Track");
                print(iden);
                print("******************");
                track.TrackRepaired();
                track.isReady = true;
            }
        }   
    }    


    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
