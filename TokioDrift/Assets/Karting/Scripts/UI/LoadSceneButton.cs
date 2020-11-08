using UnityEngine;
﻿using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace KartGame.UI
{
    public class LoadSceneButton : MonoBehaviour
    {
        [Tooltip("What is the name of the scene we want to load when clicking the button?")]
        public string SceneName;
			
		public Toggle phone;
		public Toggle wheel;
		public Toggle audio;
		
		public TextMeshProUGUI name;
		 
		
        public void LoadTargetScene() 
        {
        	if (phone.isOn){
        		print(phone.isOn);
        		PlayerPrefs.SetInt("phone", 1);		
        	}else{
        		PlayerPrefs.SetInt("phone", 0);
        	}
        	
        	if (wheel.isOn){
        		PlayerPrefs.SetInt("wheel", 1);
        	}else{
        		PlayerPrefs.SetInt("wheel", 0);
        	}
        	
        	if (audio.isOn){
        		PlayerPrefs.SetInt("audio", 1);
        	}else{
        		PlayerPrefs.SetInt("audio", 0);
        	}
 		    
 		    print(name.text);
 		    PlayerPrefs.SetString("username",name.text);   	
        	PlayerPrefs.Save();
			//print(phone.isOn);
			//print(wheel.isOn);
			//print(audio.isOn);        
            SceneManager.LoadSceneAsync(SceneName);
        }
        
    }
}
