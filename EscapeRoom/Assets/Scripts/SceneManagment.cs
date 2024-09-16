using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour
{
   public void SceneRest()
   {
        SceneManager.LoadScene("VrInteraction");
   }

    public void Quit()
    {
        Application.Quit();
    }
}
