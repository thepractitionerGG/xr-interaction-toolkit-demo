using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    public void OnGameStart()
    {
        GetComponent<Animator>().SetTrigger("SceneChange");
        StartCoroutine(DisableObject());
    }

    IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
