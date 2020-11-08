using UnityEngine;
﻿using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace KartGame.UI
{
    public class LoadSceneButton5 : MonoBehaviour
    {
        [Tooltip("What is the name of the scene we want to load when clicking the button?")]	
		public string SceneNameMain; 
		
        public void LoadTargetScene() 
        {
			SceneManager.LoadSceneAsync(SceneNameMain);		
        }
        
    }
}
