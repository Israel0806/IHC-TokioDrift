using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;
using TMPro;

public class ControlComunication : MonoBehaviour
{
    //Some references
    
    [Header("Reference")]
    [SerializeField] private ComunicationStates CS = null;
    private TrackController TC = null;
    private OrbController OC = null;

    public TextMeshProUGUI objective1;
    public TextMeshProUGUI objective2;


    void Awake()
    {
        print("ControlComunication ready !");
    }

    void OnEnable()
    {
        // ArcadeKart []karts = FindObjectsOfType<ArcadeKart>();
        // foreach (ArcadeKart kart in karts){
        //     if(kart.isLocalPlayer) CS = kart.GetGameObject().GetComponent<ComunicationStates>();
        // }
        TC = GameObject.Find("===TRACK====").GetComponent<TrackController>();
        OC = GameObject.Find("====ORB=====").GetComponent<OrbController>();
        objective1 = GameObject.Find("TextObjective1").GetComponent<TextMeshProUGUI>();
        objective2 = GameObject.Find("TextObjective2").GetComponent<TextMeshProUGUI>();

        // CS.EventChangeSomeOrbe += HandleChangeOfOrbe;
        // CS.EventChangeSomeTrack += HandleChangeOfTrack;
        print("onEnable CCS");
        print("si");
        print("---------------"); 
    }

    void  onDisable()
    {
        // CS.EventChangeSomeOrbe -= HandleChangeOfOrbe;       
        // CS.EventChangeSomeTrack -= HandleChangeOfTrack;
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

    private void HandleChangeOfTrack(int iden, int playerID, bool state)
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
                if(playerID == 1)
                {
                    string str = objective1.text;
                    int tracks = int.Parse(str.Substring(0, 1));
                    objective1.text = (tracks + 1).ToString() + " / 5";
                } else
                {
                    string str = objective2.text;
                    int tracks = int.Parse(str.Substring(0, 1));
                    objective2.text = (tracks + 1).ToString() + " / 5";
                }
            }
        }   
    }    


    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
