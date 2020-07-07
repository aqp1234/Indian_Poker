using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    int counter = 0; // counter 변수는 게임 플레이한 횟수
    string mycard;
    string npccard;
    string mycardkind;
    string mycardnumber;
    string npccardkind;
    string npccardnumber;

    public void nextGame()
    {
        counter++;
        if (counter > 20) // 남은 카드가 없을때 (게임 플레이 20번 했을때)
        {
            GameObject.Find("ResultText").GetComponent<Text>().text = "게임이 끝났습니다.\n당신의 점수 : "
                + GameObject.Find("ButtonManager").GetComponent<PlusandMinusButtonScript>().mychipcount + "\n남은 카드 개수가 0이라 게임이 종료됩니다."; //노란색 Text 변경
            GameObject.Find("PlusButton").GetComponent<Button>().interactable = false;
            GameObject.Find("MinusButton").GetComponent<Button>().interactable = false;
            GameObject.Find("OKButton").GetComponent<Button>().interactable = false;
            GameObject.Find("NextGameButton").GetComponent<Button>().interactable = false; // 게임 종료됬기 때문에 버튼 1,2,3,4 비활성화
            GameObject.Find("Panel").transform.Find("GameExitButton").gameObject.SetActive(true); // 게임 종료됬기 때문에 게임종료버튼인 버튼 5 Active처리
            return;
        }
        Destroy(GameObject.Find("mycardprefab(Clone)"));
        Destroy(GameObject.Find("npccardprefab(Clone)")); // 이전 게임의 카드 삭제
        List<string> card = GameObject.Find("carddbObject").GetComponent<carddb>().card; // 초기 랜덤 정렬한 리스트 가져오기
        string mycard = card[0]; // 첫번째 값 가져옴
        GameObject.Find("carddbObject").GetComponent<carddb>().card.RemoveAt(0); // 가져온 값 List에서 삭제
        string npccard = card[0]; // 다시 첫번째 값 가져옴
        GameObject.Find("carddbObject").GetComponent<carddb>().card.RemoveAt(0); // 가져온 값 List에서 삭제
        mycardkind = mycard.Substring(0, 1); // 가져온 값(카드 문양 확인)에서 앞글자만 자르기
        mycardnumber = mycard.Substring(1); // 나머지값(카드 숫자 확인) 반환
        mycardkind = kindfunc(mycardkind); // 카드 문양 변경 ex) "C" > "Club"
        npccardkind = npccard.Substring(0, 1);
        npccardnumber = npccard.Substring(1);
        npccardkind = kindfunc(npccardkind);
        GameObject mycardprefab = Resources.Load("Prefab/BackColor_Blue/Blue_PlayingCards_" + mycardkind + "" + mycardnumber + "_00") as GameObject;
        GameObject npccardprefab = Resources.Load("Prefab/BackColor_Blue/Blue_PlayingCards_" + npccardkind + "" + npccardnumber + "_00") as GameObject; // 카드 Prefab 가져오기
        mycardprefab.transform.localScale += new Vector3(0.03f, 0.03f, 0.03f);
        npccardprefab.transform.localScale += new Vector3(0.03f, 0.03f, 0.03f); // 카드 위치 설정
        mycardprefab.name = "mycardprefab";
        npccardprefab.name = "npccardprefab"; // 카드 이름 설정
        Instantiate(npccardprefab, new Vector3(0, 0.84f, -8.9f), Quaternion.Euler(0.0f, 0, 0));
        Instantiate(mycardprefab, new Vector3(0, 0.84f, -9.4f), Quaternion.Euler(180.0f, 0, 0)); // 카드 인스턴스화 / 플레이어 카드는 안보이게 설정 위해서 Rotation의 x값 180.0f으로 변경
        GameObject.Find("PlusButton").GetComponent<Button>().interactable = true;
        GameObject.Find("MinusButton").GetComponent<Button>().interactable = true;
        GameObject.Find("OKButton").GetComponent<Button>().interactable = true; // 게임 진행 위해 버튼 1,2,3 활성화
        GameObject.Find("ResultText").GetComponent<Text>().text = "배팅해주세요"; // 노란색 UI변경
    }

    public void commitchip()
    {
        
        GameObject.Find("mycardprefab(Clone)").transform.rotation = Quaternion.Euler(0.0f, 0, 0); // 플레이어 카드 확인 위해 Rotation x값 수정
        GameObject.Find("PlusButton").GetComponent<Button>().interactable = false;
        GameObject.Find("MinusButton").GetComponent<Button>().interactable = false;
        GameObject.Find("OKButton").GetComponent<Button>().interactable = false; // 버튼 1,2,3 비활성화
        if (Convert.ToInt32(mycardnumber) > Convert.ToInt32(npccardnumber)) // 플레이어의 숫자가 높으면 무조건 우승
        {
            wingame(); 
        }
        else if(Convert.ToInt32(mycardnumber) < Convert.ToInt32(npccardnumber)) //플레이어의 숫자가 작으면 무조건 패배
        {
            losegame();
        }
        else if(Convert.ToInt32(mycardnumber) == Convert.ToInt32(npccardnumber)) // 플레이어의 숫자랑 NPC숫자가 동일하면 문양으로 판단
        {
            switch (mycardkind)
            {
                case "Spade": // 숫자가 같을때 문양이 Spade면 무조건 승리
                    wingame();
                    break;
                case "Diamond": 
                    switch (npccardkind) // 내 카드가 Diamond일때 상대가 Spade면 패배 / 나머지일때는 승리
                    {
                        case "Spade":
                            losegame();
                            break;
                        default:
                            wingame();
                            break;
                    }
                    break;
                case "Heart":
                    switch (npccardkind) // 내 카드가 Heart일때 상대가 Club이면 승리 / 나머지일때는 패배
                    {
                        case "Club":
                            wingame();
                            break;
                        default:
                            losegame();
                            break;
                    }
                    break;
                case "Club": // 숫자가 같을때 문양이 Club이면 무조건 패배
                    losegame();
                    break;
            }
        }
        int mychipcount = GameObject.Find("ButtonManager").GetComponent<PlusandMinusButtonScript>().mychipcount; // 남은 칩 값 가져오기
        GameObject.Find("DirectorObject").GetComponent<DirectorScript>().setMyChipText(mychipcount); // 남은 칩 개수 UI 변경
        GameObject.Find("ButtonManager").GetComponent<PlusandMinusButtonScript>().pluscount = 0; // 게임이 종료됬으니 다음 게임을 위해 베팅한 칩 개수 0으로 초기화
        int pluscount = GameObject.Find("ButtonManager").GetComponent<PlusandMinusButtonScript>().pluscount; //베팅한 칩 개수 값 가져오기 (0으로 수정 후 가져와야됨)
        GameObject.Find("DirectorObject").GetComponent<DirectorScript>().setBattingChipText(pluscount); // 베팅한 칩 개수 UI 변경
        if (mychipcount <= 0) // 남은 칩이 없을때
        {
            GameObject.Find("ResultText").GetComponent<Text>().text += "\n남은 칩 개수가 0이라 게임이 종료됩니다."; // 게임 종료 UI선언
            GameObject.Find("NextGameButton").GetComponent<Button>().interactable = false; // 다음 게임 못하도록 버튼4 비활성화
            GameObject.Find("Panel").transform.Find("GameExitButton").gameObject.SetActive(true); // 게임 종료 버튼 Active 처리
        }
    }

    public void wingame()
    {
        GameObject.Find("ButtonManager").GetComponent<PlusandMinusButtonScript>()
                .getwinChip(GameObject.Find("ButtonManager").GetComponent<PlusandMinusButtonScript>().pluscount * 2); // 이겼을때 2배의 칩을 얻게 하는 코드
        for (int i = 0; i < 20; i++)
        {
            GameObject.Find("CasinoChip20_Red_00").transform.GetChild(i).gameObject.SetActive(false); //배팅한 칩 전부 InActive 처리
        }
        GameObject.Find("ResultText").GetComponent<Text>().text = "당신이 이겼습니다."; // 노란색 UI 변경
    }

    public void losegame()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject.Find("CasinoChip20_Red_00").transform.GetChild(i).gameObject.SetActive(false); //배팅한 칩 전부 InActive 처리
        }
        GameObject.Find("ResultText").GetComponent<Text>().text = "당신이 졌습니다."; // 노란색 UI 변경
    }

    string kindfunc(string kind) // 카드 문양 변경 함수
    {
        switch (kind)
        {
            case "C":
                kind = "Club";
                break;
            case "H":
                kind = "Heart";
                break;
            case "D":
                kind = "Diamond";
                break;
            case "S":
                kind = "Spade";
                break;
        }
        return kind;
    }
}
