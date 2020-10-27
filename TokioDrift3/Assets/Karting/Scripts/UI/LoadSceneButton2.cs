using UnityEngine;
﻿using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace KartGame.UI
{
    public class LoadSceneButton2 : MonoBehaviour
    {
        [Tooltip("What is the name of the scene we want to load when clicking the button?")]
        public string SceneNameWheel;
		public string SceneNamePhone;
		public string SceneNameAudio;	
		public string SceneNameMain; 
		
        public void LoadTargetScene() 
        {
        	if (PlayerPrefs.GetInt("wheel") == 1){
        		 SceneManager.LoadSceneAsync(SceneNameWheel);	
        			print("1");
        	}else{
        		if (PlayerPrefs.GetInt("phone") == 1){
        			SceneManager.LoadSceneAsync(SceneNamePhone);
        			print("2");
        		}else{
        			if (PlayerPrefs.GetInt("audio") == 1){
        				SceneManager.LoadSceneAsync(SceneNameAudio);
		    			print("3");
		    		}else{
		    			SceneManager.LoadSceneAsync(SceneNameMain);	
		    			print("4");
		    		}
        		}
        	}
        	
        }
        
    }
}
