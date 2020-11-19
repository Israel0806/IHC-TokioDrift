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
    private bool gameStarted;

    [Header("Game Stats")]
    [SyncVar]
    public int score;



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
        camera = GameObject.Find("CinemachineVirtualCamera").GetComponent<CinemachineVirtualCamera>();
        _Minimap = GameObject.Find("MinimapCamera").GetComponent<Minimap>();
        FinishLineTrigger = GameObject.Find("ConfettiTrigger").GetComponent<FinishLineTrigger>();
        FinishLineTrigger.triggerBody = this.Rigidbody;
        gameManager = GameObject.Find("GameManager");
        arcadeKart = this.GetComponent<ArcadeKart>();
        arcadeKart.isLocalPlayer = true;

        _Minimap.player = this.transform;
        camera.m_Follow = this.transform;
        camera.m_LookAt = this.transform;
        //gameManager.GetComponent<GameFlowManager>().playerKart = this.GetComponent<ArcadeKart>();

        gameStarted = false;
        arcadeKart.SetCanMove(false);
        //gameManager.GetComponent<GameFlowManager>().karts[0] = arcadeKart;

    }
    #endregion


    [Client]
    void Update()
    {
        if (!isLocalPlayer) return;
        
    }


}
