using UnityEngine;
﻿using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace KartGame.UI
{
    public class LoadSceneButton3 : MonoBehaviour
    {
        [Tooltip("What is the name of the scene we want to load when clicking the button?")]
		public string SceneNamePhone;
		public string SceneNameAudio;	
		public string SceneNameMain; 
		
        public void LoadTargetScene() 
        {
        	
    		if (PlayerPrefs.GetInt("phone") == 1){
    			print("1");
    			SceneManager.LoadSceneAsync(SceneNamePhone);
    		}else{
    			if (PlayerPrefs.GetInt("audio") == 1){
    				print("2");
    				SceneManager.LoadSceneAsync(SceneNameAudio);
	    		}else{
	    			print("3");
	    			SceneManager.LoadSceneAsync(SceneNameMain);	
	    		}
    		}
        	
        	
        }
        
    }
}
