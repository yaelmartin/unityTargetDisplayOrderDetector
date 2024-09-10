using System;
using UnityEngine;

namespace TargetDisplayOrderDetector
{
    public class Checker : MonoBehaviour
    {
        [SerializeField] private DisplayUiInstance displayCheckerInstance;
        
        [SerializeField] private int editorDisplayCount = 8;

        private bool isEditor = false;
        
        void Start()
        {
#if UNITY_EDITOR
            isEditor = true;
#endif   
            
            int totalDisplays = Display.displays.Length; //https://docs.unity3d.com/ScriptReference/Display-displays.html

            // In the editor, use the provided `editorDisplayCount` to simulate additional displays.
            if(isEditor){totalDisplays = editorDisplayCount;} 

            Debug.Log("Displays connected: " + totalDisplays);

            // Set text for the primary display (index 0)
            displayCheckerInstance.displayText.text = $"Display 1<br>Main<br>Screens: {totalDisplays}";

            // Loop through and activate additional displays if available
            for (int i = 1; i < Math.Min(totalDisplays,8); i++)
            {
                if(!isEditor) {Display.displays[i].Activate();}
                
                GameObject duplicate = Instantiate(displayCheckerInstance.gameObject, transform, true);
                DisplayUiInstance instance = duplicate.GetComponent<DisplayUiInstance>();
                
                instance.displayCanvas.targetDisplay = i;
                instance.displayCamera.targetDisplay = i;
                instance.displayText.text = $"Display {i + 1}";
            }
        }
    }
}