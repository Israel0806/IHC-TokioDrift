using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnItOff : MonoBehaviour
{
    public Text endGame;
    private NetworkManagerCar NetworkManager;

    void Start()
    {
        NetworkManager = this.GetComponent<NetworkManagerCar>();
    }

    public void StopServer()
    {
        NetworkManager.StopServer();
    }

    void Update()
    {
        if (endGame.text == "1")
            StopServer();

    }
}
