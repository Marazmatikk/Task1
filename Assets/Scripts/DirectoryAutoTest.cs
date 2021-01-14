using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectoryAutoTest : MonoBehaviour
{
    [SerializeField]
    Button button;
    
    DirectoryData directoryData;

    string testKey = "8-999-66-55-404";
    Citizen testValue = new Citizen("Test Testov", "Street Test");

    void Start()
    {
        directoryData = new DirectoryData();
        button.onClick.AddListener(AutoTest);
    }

    void AutoTest()
    {
        Debug.Log("//Test: Start");

        {
            directoryData.AddCitizenData(testKey, testValue);
            if (directoryData.Data.Count != 0)
                Debug.Log("//Test: Add to directory <color=#22ad02>Successful</color>");
            else
                Debug.Log("//Test: Add to directory <color=#fb0915>Failed</color>");
        }
        
        {
            if (directoryData.SearchByNumber(testKey) != null)
                Debug.Log("//Test: SearchByNumber <color=#22ad02>Successful</color>");
            else
                Debug.Log("//Test: SearchByNumber <color=#fb0915>Failed</color>");
        }

        {
            if (directoryData.SearchByName(testValue.name) != null)
                Debug.Log("//Test: SearchByName <color=#22ad02>Successful</color>");
            else
                Debug.Log("//Test: SearchByName <color=#fb0915>Failed</color>");
        }
        
        {
            directoryData.RemoveCitizenData(testKey);
            if(directoryData.Data.Count == 0)
                Debug.Log("//Test: Remove from directory <color=#22ad02>Successful</color>");
            else
                Debug.Log("//Test: Remove from directory <color=#fb0915>Failed</color>");
        }
    }
}
