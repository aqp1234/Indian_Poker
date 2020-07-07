using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExitScript : MonoBehaviour
{
    public void ExitGame()
    {
#if UNITY_EDITOR 
        UnityEditor.EditorApplication.isPlaying = false; // 유니티 에디터 종료
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
