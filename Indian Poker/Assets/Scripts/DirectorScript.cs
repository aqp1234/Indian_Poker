using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectorScript : MonoBehaviour
{
    public void setBattingChipText(int battingchipcount)
    {
        GameObject.Find("BattingText").GetComponent<Text>().text = "배팅한 칩 개수 : " + battingchipcount; // 배팅한 칩 개수 UI를 변경하기 위한 코드
    }
    public void setMyChipText(int mychipcount)
    {
        GameObject.Find("MyChipText").GetComponent<Text>().text = "남은 칩 개수 : " + mychipcount; // 남은 칩 개수 UI를 변경하기 위한 코드
    }
}
