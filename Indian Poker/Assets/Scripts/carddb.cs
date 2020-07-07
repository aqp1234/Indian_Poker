using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.Runtime.InteropServices.WindowsRuntime;

public class carddb : MonoBehaviour
{
    public List<string> card;
    // Start is called before the first frame update
    void Start()
    {
        List<string> cardkind = new List<string> { "D", "H", "C", "S" }; // Diamond, Heart, Club, Spade 의 첫글자를 따서 선언
        card = new List<string>();
        foreach(string k in cardkind)
        {
            for(int b = 1; b <= 10; b++)
            {
                string strb = b.ToString("D2"); // int형 1,2 이런 숫자를 string형 "01", "02" 이런 식으로 변경하는 소스코드
                card.Add(k + "" + strb); // D01, D02, H03 식으로 list 배열인 card에 추가
            }
        }
        Shufflecardfunc(card);
    }

    public void Shufflecardfunc(List<string> list) // 긁어온 코드
    {
        RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        int n = list.Count;
        while (n > 1)
        {
            byte[] box = new byte[1];
            do provider.GetBytes(box);
            while (!(box[0] < n * (byte.MaxValue / n)));
            int k = (box[0] % n);
            n--;
            string value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

}


