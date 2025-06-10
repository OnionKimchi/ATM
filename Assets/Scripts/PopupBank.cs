using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupBank : MonoBehaviour
{
    [SerializeField] private GameObject BankUI;
    [SerializeField] private GameObject DepositUI;
    [SerializeField] private GameObject WithdrawUI;
    [SerializeField] private GameObject Deposit10000;
    [SerializeField] private GameObject Deposit30000;
    [SerializeField] private GameObject Deposit50000;
    [SerializeField] private GameObject Withdrawal10000;
    [SerializeField] private GameObject Withdrawal30000;
    [SerializeField] private GameObject Withdrawal50000;
    [SerializeField] private GameObject PopupError;
    [SerializeField] private TMP_InputField DepositInputField;
    [SerializeField] private TMP_InputField WithdrawInputField;
    [SerializeField] private LoginAndSignUp loginAndSignUp; // LoginAndSignUp을 가져오기 위한 변수

    private UserData userData;// UserData를 가져오기 위한 변수

    [SerializeField] UserInfoUI userInfoUI; // UserInfoUI를 가져오기 위한 변수

    // Start is called before the first frame update
    void Start()
    {
        OnBankUI();// 실행 시 BankUI를 활성화하고 나머지는 비활성화합니다.
    }

    void Awake()
    {
        userData = GameManager.Instance.UserData; // GameManager에서 UserData를 가져옵니다.
    }

    public void OnBankUI()
    {
        BankUI.SetActive(true);
        DepositUI.SetActive(false);
        WithdrawUI.SetActive(false);
    }

    public void OnDepositUI()
    {
        BankUI.SetActive(false);
        DepositUI.SetActive(true);
        WithdrawUI.SetActive(false);
    }

    public void OnWithdrawUI()
    {
        BankUI.SetActive(false);
        DepositUI.SetActive(false);
        WithdrawUI.SetActive(true);
    }
    public void OnDeposit(int money)
    {
        if (userData.cashAmount < money) // UserData의 Cash가 입금액보다 적으면 경고창을 띄웁니다.
        {
            loginAndSignUp.OnErrorPopup("잔액이 부족합니다.");// 팝업 오류창을 띄웁니다.
            return;
        }
        userData.cashAmount -= money; // UserData의 Cash에서 입금액을 뺍니다.
        userData.bankAmount += money; // UserData의 Money에 입금액을 더
                                      // 합니다.

        userInfoUI.Refresh();// UserInfoUI를 갱신합니다.


        GameManager.Instance.SaveUserData(userData); // 변경 후 저장합니다.
    }

    public void OnWithdraw(int money)
    {
        if (userData.bankAmount < money) // UserData의 Bank가 출금액보다 적으면 경고창을 띄웁니다.
        {
            loginAndSignUp.OnErrorPopup("잔액이 부족합니다.");// 팝업 오류창을 띄웁니다.
            return;
        }
        userData.cashAmount += money; // UserData의 Cash에 출금액을 더합니다.
        userData.bankAmount -= money; // UserData의 Bank에서 출금액을 뺍니다.

        userInfoUI.Refresh();// UserInfoUI를 갱신합니다.

        GameManager.Instance.SaveUserData(userData); // 변경 후 저장합니다.
    }

    public void OnDepositButton(int Amount)
    {
        OnDeposit(Amount); // Amount 만큼 입금합니다.
    }
    public void OnWithdrawalButton(int Amount)
    {
        OnWithdraw(Amount); // Amount 만큼 출금합니다.
    }
    public void OnDepositInputFieldText()
    {
        string inputText = DepositInputField.text; // 입력 필드에서 텍스트를 가져옵니다.
        if (int.TryParse(inputText, out int depositAmount)) // 입력된 텍스트를 정수로 변환합니다.
        {
            OnDeposit(depositAmount); // 변환된 금액을 입금합니다.
        }
        else
        {
            loginAndSignUp.OnErrorPopup("입금불가 오류"); // 변환 실패 시 오류 팝업을 띄웁니다.
        }
    }

    public void OnWithdrawInputFieldText()
    {
        string inputText = WithdrawInputField.text; // 입력 필드에서 텍스트를 가져옵니다.
        if (int.TryParse(inputText, out int withdrawAmount)) // 입력된 텍스트를 정수로 변환합니다.
        {
            OnWithdraw(withdrawAmount); // 변환된 금액을 출금합니다.
        }
        else
        {
            loginAndSignUp.OnErrorPopup("출금불가 오류"); // 변환 실패 시 오류 팝업을 띄웁니다.
        }
    }
}
