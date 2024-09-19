using System;
using System.Collections;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public float explosionForce = 500f; 
    public float explosionRadius = 5f;  
    public string bombTag = "BombCube";
    public GameObject particleSystem;
    public GameObject WarningMessage;
    public GameObject IndicatorPArticleSystem;
    public GameObject Detonator;

    public void TriggerExplosion()
    {
        StartCoroutine(Explosion());
    }
   IEnumerator Explosion()
    {
        yield return new WaitForSeconds(4);
        GameObject[] bombCubes = GameObject.FindGameObjectsWithTag(bombTag);
        WarningMessage.SetActive(false);
        foreach (GameObject bombCube in bombCubes)
        {

            float distance = Vector3.Distance(transform.position, bombCube.transform.position);
            bombCube.GetComponent<Rigidbody>().isKinematic = false;

            if (distance <= explosionRadius)
            {
                Rigidbody rb = bombCube.GetComponent<Rigidbody>();
                if (rb != null)
                {

                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
            }
        }

        Detonator.SetActive(false);
        AudioManagerr.instance.PlaySFX(7);

        particleSystem.SetActive(true);

        yield return new WaitForSeconds(2);

        foreach (GameObject bombCube in bombCubes)
        {
            GameObject.Destroy(bombCube);
        }

        particleSystem.SetActive(false);
        IndicatorPArticleSystem.SetActive(false);

        StartCoroutine(EndGame());
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(2);
        Application.Quit();
    }
}
