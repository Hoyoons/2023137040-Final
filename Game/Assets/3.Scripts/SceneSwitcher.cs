using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string targetSceneName; // ¿Ãµø«“ ≈∏∞Ÿ æ¿¿« ¿Ã∏ß

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("∞®¡ˆµ ");
            SceneManager.LoadScene(targetSceneName); // ≈∏∞Ÿ æ¿¿∏∑Œ ¿Ãµø
        }
    }
}
