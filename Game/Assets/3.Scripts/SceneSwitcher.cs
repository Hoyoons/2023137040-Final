using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string targetSceneName; // �̵��� Ÿ�� ���� �̸�

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("������");
            SceneManager.LoadScene(targetSceneName); // Ÿ�� ������ �̵�
        }
    }
}
