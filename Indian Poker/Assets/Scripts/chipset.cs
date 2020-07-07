using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chipset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        chipsetfunc();
    }

    void chipsetfunc()
    {
        GameObject myCasinoChip = Resources.Load("Prefab/CasinoItem/CasinoChip20_Red_00") as GameObject; // CasinoChip20_Red_00의 리소스 가져옴 해당 리소스 변경시 실제 에셋도 변경됨
        for (int i = 0; i < 20; i++)
        {
            myCasinoChip.transform.GetChild(i).gameObject.SetActive(false); // Prefab 내부의 자식을 전부 Inactive 선언
        }
        myCasinoChip.name = "chip20_1"; // 이름 설정
        Instantiate(myCasinoChip, new Vector3(0.44f, 0.86f, -9.55f), Quaternion.identity); // 인스턴스화
        myCasinoChip.name = "chip20_2";
        Instantiate(myCasinoChip, new Vector3(0.48f, 0.86f, -9.55f), Quaternion.identity);
        myCasinoChip.name = "chip20_3";
        Instantiate(myCasinoChip, new Vector3(0.52f, 0.86f, -9.55f), Quaternion.identity);
        myCasinoChip.name = "chip20_4";
        Instantiate(myCasinoChip, new Vector3(0.56f, 0.86f, -9.55f), Quaternion.identity);
        for (int i = 0; i < 20; i++)
        {
            myCasinoChip.transform.GetChild(i).gameObject.SetActive(true); // Prefab 내부의 자식을 전부 Active 선언
        }
        myCasinoChip.name = "chip20_0";
        Instantiate(myCasinoChip, new Vector3(0.4f, 0.86f, -9.55f), Quaternion.identity); // 해당 인스턴스화 된 것이 초기에 있는 20개의 칩
        GameObject.Find("PlusButton").GetComponent<Button>().interactable = false;
        GameObject.Find("MinusButton").GetComponent<Button>().interactable = false;
        GameObject.Find("OKButton").GetComponent<Button>().interactable = false; // 초기 설정으로 버튼 1,2,3 비활성화
    }
}
