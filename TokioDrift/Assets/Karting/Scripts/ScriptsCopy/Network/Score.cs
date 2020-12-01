using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Score : NetworkBehaviour
{    
    [Header("Reference")]
    private GameFlowManager GameFlow ;
    public delegate void ChangeScore(int newValue); 

    [SyncVar]
    public int currentScore;

    [SyncEvent]
    public event ChangeScore EventChangeScore;

    #region Server 
    
    [Server]
    private void SetChangeNewScore(int newValue)
    {
        currentScore = newValue;
        EventChangeScore?.Invoke(newValue); 
    }

    public override void OnStartServer()
    {
        print("score ready!");
        SetChangeNewScore(0); 
        GameFlow = GameObject.Find("GameManager").GetComponent<GameFlowManager>(); 
    } 
    
    [Command]
    private void CmdSetNewScore(int val) => SetChangeNewScore(val);

    #endregion


    #region Client 
    [ClientCallback]
    private void Update()
    {
        if (!hasAuthority) { return;} 
        
        if(currentScore != GameFlow.myScore )// my score is change 
        {
            print("CmdSetNewScore **");
            CmdSetNewScore(GameFlow.myScore);
        }

    }

    #endregion
}
