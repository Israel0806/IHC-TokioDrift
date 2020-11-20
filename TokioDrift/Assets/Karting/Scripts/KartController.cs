using UnityEngine;
using Mirror;
using System.Collections.Generic;
using Cinemachine;
using KartGame.KartSystems;

/*
	Documentation: https://mirror-networking.com/docs/Guides/NetworkBehaviour.html
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkBehaviour.html
*/

public class KartController : NetworkBehaviour
{
    private ArcadeKart arcadeKart;
    private NetworkManager networkManager;
    private CinemachineVirtualCamera camera;
    private Minimap _Minimap;
    private SimpleTrigger FinishLineTrigger;
    private GameObject gameManager;

    private Track[] tracks;
    private Orb[] orbs;

    [Header("Game Stats")]
    [SyncVar]
    public int score;

    //El objeto orbe le asigana al juegador el track que debe ir  a conseguir
    public int TrackAssign = -1;


    #region Start & Stop Callbacks


    /// <summary>
    /// This is invoked on clients when the server has caused this object to be destroyed.
    /// <para>This can be used as a hook to invoke effects or do client specific cleanup.</para>
    /// </summary>
    public override void OnStopClient() { }

    /// <summary>
    /// Called when the local player object has been set up.
    /// <para>This happens after OnStartClient(), as it is triggered by an ownership message from the server. This is an appropriate place to activate components or functionality that should only be active for the local player, such as cameras and input.</para>
    /// </summary>
    public override void OnStartLocalPlayer() {
        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManagerCar>();
        
        // CAMERA
        camera = GameObject.Find("CinemachineVirtualCamera").GetComponent<CinemachineVirtualCamera>();
        _Minimap = GameObject.Find("MinimapCamera").GetComponent<Minimap>();

        // TRIGGERS
        // FinishLine
        FinishLineTrigger = GameObject.Find("ConfettiTrigger").GetComponent<SimpleTrigger>();
        FinishLineTrigger.triggerBody = this.GetComponent<Rigidbody>();
        // track triggers
        //tracks = FindObjectsOfType<Track>();
        //foreach (Track track in tracks)
        //{
        //    track.GetComponent<SimpleTrigger>().triggerBody = this.GetComponent<Rigidbody>();
        //}
        // orb triggers

        gameManager = GameObject.Find("GameManager");

        /// Kart
        arcadeKart = this.GetComponent<ArcadeKart>();
        arcadeKart.isLocalPlayer = true;

        _Minimap.player = this.transform;
        camera.m_Follow = this.transform;
        camera.m_LookAt = this.transform;

        arcadeKart.SetCanMove(false);
    }
    #endregion


    [Client]
    void Update()
    {
        if (!isLocalPlayer) return;
        
    }


}
