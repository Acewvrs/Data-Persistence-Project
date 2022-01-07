using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public string nameInput;
    // Start is called before the first frame update
    public void ReadStringInput(string s) 
    {
        nameInput = s;
    }
    
    
    public void LoadMain() 
    {
        SceneManager.LoadScene(1);
    }
}