using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusandMinusButtonScript : MonoBehaviour
{
    public int mychipcount = 20; // 남은 칩 개수
    public int pluscount = 0; // 베팅한 칩 개수
    public void plusButtonClick()
    {
        if(pluscount < 20)
        {
            if(mychipcount <= 0)
            {
                return; // 만약 남은 칩 개수가 0이라면 아래 소스코드가 실행 되지 않도록 처리
            }
            GameObject.Find("chip20_"+(mychipcount-1)/20+"(Clone)").transform.GetChild((mychipcount - 1) % 20).gameObject.SetActive(false); // 남은 칩 개수가 -1 됬기 때문에 남은 칩 개수로 판단하여 해당 칩을 InActive처리
            GameObject.Find("CasinoChip20_Red_00").transform.GetChild(pluscount).gameObject.SetActive(true); // 베팅 칩이 +1 됬기 때문에 베팅칩의 제일 위의 칩을 Active처리
            if (pluscount < 20) // 최대 20개까지만 처리 하기 위해서 조건문 선언
            {
                mychipcount--;
                pluscount++;
            }
            GameObject.Find("DirectorObject").GetComponent<DirectorScript>().setBattingChipText(pluscount); // UI변경하는 DirectorScript의 setBattingChipText 실행
            GameObject.Find("DirectorObject").GetComponent<DirectorScript>().setMyChipText(mychipcount); // UI변경하는 DirectorScript의 setMyChipText 실행
        }
    }
    public void minusButtonClick()
    {
        if (mychipcount <= 0)
        {
            return; // 만약 남은 칩 개수가 0이라면 아래 소스코드가 실행 되지 않도록 처리
        }
        if (pluscount > 0)
        {
            mychipcount++;
            pluscount--;
        }
        GameObject.Find("chip20_" + (mychipcount - 1) / 20 + "(Clone)").transform.GetChild((mychipcount - 1) % 20).gameObject.SetActive(true); // 남은 칩 개수가 +1 됬기 때문에 남은 칩 개수로 판단하여 해당 칩을 Active처리
        GameObject.Find("CasinoChip20_Red_00").transform.GetChild(pluscount).gameObject.SetActive(false); // 베팅 칩이 -1 됬기 때문에 베팅칩의 제일 위의 칩을 InActive처리
        GameObject.Find("DirectorObject").GetComponent<DirectorScript>().setBattingChipText(pluscount); // UI변경하는 DirectorScript의 setBattingChipText 실행
        GameObject.Find("DirectorObject").GetComponent<DirectorScript>().setMyChipText(mychipcount); // UI변경하는 DirectorScript의 setMyChipText 실행
    }

    public void getwinChip(int chipcount)
    {
        for(int i = 1; i <= chipcount; i++)
        {
            if(mychipcount < 100) // 남은 칩 개수를 100개로 제한 위해 조건문 선언
            {
                mychipcount++; // 남은 칩 개수 ++
                GameObject.Find("chip20_" + (mychipcount - 1) / 20 + "(Clone)").transform.GetChild((mychipcount - 1) % 20).gameObject.SetActive(true); // 남은 칩 개수를 판단하여 해당 칩을 Active 처리
            }
        }
    }
}
