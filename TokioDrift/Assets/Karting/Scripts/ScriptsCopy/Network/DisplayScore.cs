﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using KartGame.KartSystems;

public class DisplayScore : MonoBehaviour
{
    //Some references
    [Header("Show score text ")]
    public TextMeshProUGUI textScore1;
    public TextMeshProUGUI textScore2;

    [Header("Reference")]
    private Score _Score = null;

    private int score1;
    private int score2;

    
    private void onEnable()
    {
        ArcadeKart []karts = FindObjectsOfType<ArcadeKart>();
        foreach (ArcadeKart kart in karts)
            if(kart.isLocalPlayer)
                _Score = kart.GetGameObject().GetComponent<Score>();

        _Score.EventChangeScore += HandleChangeScore;
    }

    private void onDisable()
    {
         _Score.EventChangeScore -= HandleChangeScore;
    }

    private void HandleChangeScore(int newValue)
    {
        
        //Tengo el score local y lo pongo en mi score local de texto
        //Y busco al otro jugador su score y lo represento en la otra casilla
        ArcadeKart []karts = FindObjectsOfType<ArcadeKart>();
        foreach (ArcadeKart kart in karts)
        {
            if(kart.isLocalPlayer)
            {
                score1 = kart.GetGameObject().GetComponent<Score>().currentScore;
            }else{
                score2 = kart.GetGameObject().GetComponent<Score>().currentScore;
            }

        }
        textScore1.text = (score1).ToString();
        textScore2.text = (score2).ToString();
    }    

    // Update is called once per frame
    // void Update()
    // {
        
    // }

}