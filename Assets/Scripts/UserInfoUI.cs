using UnityEngine;
using TMPro; // TMP_Text 사용 시

public class UserInfoUI : MonoBehaviour
{
    public TMP_Text userNameText;
    public TMP_Text cashAmountText;
    public TMP_Text bankAmountText;

    void Start()
    {
        Refresh(); // 초기화 시 사용자 정보 갱신
    }

    public void Refresh()
    {         
        UserData userData = GameManager.Instance.UserData;
        userNameText.text = userData.userName;
        cashAmountText.text = userData.cashAmount.ToString("N0");// 콤마 표시를 위해 "N0"를 사용하라고 함
        bankAmountText.text = userData.bankAmount.ToString("N0");
    }
}