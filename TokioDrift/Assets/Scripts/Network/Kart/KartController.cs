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
    private GameObject gameManager;

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
        gameManager = GameObject.Find("GameManager");
        arcadeKart = this.GetComponent<ArcadeKart>();

        camera.m_Follow = this.transform;
        camera.m_LookAt = this.transform;
        //gameManager.GetComponent<GameFlowManager>().playerKart = this.GetComponent<ArcadeKart>();
        
        arcadeKart.SetCanMove(false);

        if (networkManager.numPlayers == 2)
            gameManager.GetComponent<GameFlowManager>().GameReady();

    }
    #endregion


    [Client]
    void Update()
    {
        if (!isLocalPlayer) return;
        
    }


}
