using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WAKUWAKU : MonoBehaviour
{
    public string targetSceneName; // 넘어갈 씬의 이름을 여기에 설정해주세요.

    private void Start()
    {
        Button button = GetComponent<Button>(); // 현재 오브젝트의 버튼 컴포넌트를 가져옵니다.
        button.onClick.AddListener(LoadTargetScene); // 버튼 클릭 이벤트에 함수를 추가합니다.
    }

    public void LoadTargetScene()
    {
        SceneManager.LoadScene(targetSceneName); // 설정한 씬으로 이동합니다.
    }
}
