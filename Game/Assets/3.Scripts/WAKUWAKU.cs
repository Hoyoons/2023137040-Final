using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WAKUWAKU : MonoBehaviour
{
    public string targetSceneName; // �Ѿ ���� �̸��� ���⿡ �������ּ���.

    private void Start()
    {
        Button button = GetComponent<Button>(); // ���� ������Ʈ�� ��ư ������Ʈ�� �����ɴϴ�.
        button.onClick.AddListener(LoadTargetScene); // ��ư Ŭ�� �̺�Ʈ�� �Լ��� �߰��մϴ�.
    }

    public void LoadTargetScene()
    {
        SceneManager.LoadScene(targetSceneName); // ������ ������ �̵��մϴ�.
    }
}
