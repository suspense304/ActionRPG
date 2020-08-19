using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransition : MonoBehaviour
{
    public GameObject fadePanel;
    public GameObject fadeOutPanel;
    public float fadeWait;
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;

    void Awake()
    {
        if (fadePanel != null)
        {
            GameObject go = Instantiate(fadePanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(go, 1);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            playerStorage.initialPosition = playerPosition;
            StartCoroutine(FadeInOut());
        }
    }

    IEnumerator FadeInOut()
    {
        if(fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while(!asyncOperation.isDone)
        {
            yield return null;
        }
    }




}
