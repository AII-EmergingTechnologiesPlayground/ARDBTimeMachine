using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Windows;

public class ButtonHandler : MonoBehaviour
{
    public void SwitchScene()
    {
       SceneManager.LoadScene(EventSystem.current.currentSelectedGameObject.name, LoadSceneMode.Single);
    } 
    
}
