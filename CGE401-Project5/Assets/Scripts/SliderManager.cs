using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
  public Slider progressBar;
  private int currentProgress = 0; // Tracks the current progress value
  private int requiredProgress = 10;

  void Start()
        {
            UpdateProgressBar();
            progressBar.maxValue = requiredProgress;
            progressBar.value = 0;
        }    
  
     // Call this method when a minigame is won
     public void InfectedCured()
     {
        currentProgress++; // Increment progress by one unit
        UpdateProgressBar();
     }   


   private void UpdateProgressBar()
   {
            progressBar.value = currentProgress;

            if (progressBar != null)
            {
                progressBar.value = currentProgress;
               
                if (currentProgress >= progressBar.maxValue)
                {
                     Debug.Log("All animals cured!");
                }
            
                else
                {
                    Debug.LogError("Progress Bar Slider is not assigned in the Inspector!");
                }

            }
   }
}    

