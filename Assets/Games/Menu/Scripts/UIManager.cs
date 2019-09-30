using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

public class UIManager : MonoBehaviour
{
    
    public void PlayGame1()
    {
        SceneManager.LoadScene(1);
    }
    public void PlayGame2()
    {
        SceneManager.LoadScene(2);
    }
    public void PlayGame3()
    {
        SceneManager.LoadScene(3);
    }
    public void BackToManu()
    {
        SceneManager.LoadScene(0);
        

    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
