using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupBank : MonoBehaviour
{
    [SerializeField] private GameObject BankUI;
    [SerializeField] private GameObject DepositUI;
    [SerializeField] private GameObject WithdrawUI;
    [SerializeField] private GameObject TransferUI;
    [SerializeField] private GameObject Deposit10000;
    [SerializeField] private GameObject Deposit30000;
    [SerializeField] private GameObject Deposit50000;
    [SerializeField] private GameObject Withdrawal10000;
    [SerializeField] private GameObject Withdrawal30000;
    [SerializeField] private GameObject Withdrawal50000;
    [SerializeField] private GameObject PopupError;
    [SerializeField] private TMP_InputField DepositInputField;
    [SerializeField] private TMP_InputField WithdrawInputField;
    [SerializeField] private TMP_InputField TransferInputField;
    [SerializeField] private TMP_InputField TransferToID;
    [SerializeField] private LoginAndSignUp loginAndSignUp; // LoginAndSignUp을 가져오기 위한 변수


    [SerializeField] UserInfoUI userInfoUI; // UserInfoUI를 가져오기 위한 변수

    // Start is called before the first frame update
    void Start()
    {
        OnBankUI();// 실행 시 BankUI를 활성화하고 나머지는 비활성화합니다.
    }

    public void OnBankUI()
    {
        BankUI.SetActive(true);
        DepositUI.SetActive(false);
        WithdrawUI.SetActive(false);
        TransferUI.SetActive(false);
    }

    public void OnDepositUI()
    {
        BankUI.SetActive(false);
        DepositUI.SetActive(true);
        WithdrawUI.SetActive(false);
        TransferUI.SetActive(false);
    }

    public void OnWithdrawUI()
    {
        BankUI.SetActive(false);
        DepositUI.SetActive(false);
        WithdrawUI.SetActive(true);
        TransferUI.SetActive(false);
    }

    public void OnTransferUI()
    {
        BankUI.SetActive(false);
        DepositUI.SetActive(false);
        WithdrawUI.SetActive(false);
        TransferUI.SetActive(true);
    }
    public void OnDeposit(int money)
    {
        if (GameManager.Instance.UserData.cashAmount < money) // UserData의 Cash가 입금액보다 적으면 경고창을 띄웁니다.
        {
            loginAndSignUp.OnErrorPopup("잔액이 부족합니다.");// 팝업 오류창을 띄웁니다.
            return;
        }
        GameManager.Instance.UserData.cashAmount -= money; // UserData의 Cash에서 입금액을 뺍니다.
        GameManager.Instance.UserData.bankAmount += money; // UserData의 Money에 입금액을 더
                                      // 합니다.

        userInfoUI.Refresh();// UserInfoUI를 갱신합니다.
        DepositInputField.text = ""; // 입금 입력 필드를 비웁니다.


        GameManager.Instance.SaveUserData(GameManager.Instance.UserData); // 변경 후 저장합니다.
    }

    public void OnWithdraw(int money)
    {
        if (GameManager.Instance.UserData.bankAmount < money) // UserData의 Bank가 출금액보다 적으면 경고창을 띄웁니다.
        {
            loginAndSignUp.OnErrorPopup("잔액이 부족합니다.");// 팝업 오류창을 띄웁니다.
            return;
        }
        GameManager.Instance.UserData.cashAmount += money; // UserData의 Cash에 출금액을 더합니다.
        GameManager.Instance.UserData.bankAmount -= money; // UserData의 Bank에서 출금액을 뺍니다.

        userInfoUI.Refresh();// UserInfoUI를 갱신합니다.
        WithdrawInputField.text = ""; // 출금 입력 필드를 비웁니다.

        GameManager.Instance.SaveUserData(GameManager.Instance.UserData); // 변경 후 저장합니다.
    }

    public void OnTransfer(int money, string id)
    {

        // json 파일에서 id에 해당하는 UserData가 있는지 확인합니다.
        string path = Path.Combine(Application.persistentDataPath, $"{id}.json");
        if (string.IsNullOrEmpty(id)) // ID가 비어있으면 경고창을 띄웁니다.
        {
            loginAndSignUp.OnErrorPopup("ID를 입력해주세요."); // 팝업 오류창을 띄웁니다.
            return;
        }
        if (!File.Exists(path)) // 해당 ID의 json 파일이 없으면 경고창을 띄웁니다.
        {
            loginAndSignUp.OnErrorPopup("존재하지 않는 ID입니다."); // 팝업 오류창을 띄웁니다.
            return;
        }
        if (id == GameManager.Instance.UserData.ID) // ID가 자기 자신이면 경고창을 띄웁니다.
        {
            loginAndSignUp.OnErrorPopup("자기 자신에게 이체할 수 없습니다."); // 팝업 오류창을 띄웁니다.
            return;
        }
        if (GameManager.Instance.UserData.bankAmount < money) // UserData의 Bank가 이체액보다 적으면 경고창을 띄웁니다.
        {
            loginAndSignUp.OnErrorPopup("잔액이 부족합니다.");// 팝업 오류창을 띄웁니다.
            return;
        }
        string json = File.ReadAllText(path); // 해당 ID의 json 파일을 읽어옵니다.
        //json 파일을 UserData 객체로 변환합니다.
        UserData targetUserData = JsonUtility.FromJson<UserData>(json); // json 파일을 UserData 객체로 변환합니다.
        targetUserData.bankAmount += money; // 타겟 UserData의 Bank에 이체액을 더합니다.
        GameManager.Instance.UserData.bankAmount -= money; // UserData의 Bank에서 이체액을 뺍니다
        GameManager.Instance.SaveUserData(targetUserData); // 변경된 타겟 UserData를 저장합니다.
        GameManager.Instance.SaveUserData(GameManager.Instance.UserData); // 변경된 UserData를 저장합니다.
        userInfoUI.Refresh();// UserInfoUI를 갱신합니다.
        TransferInputField.text = ""; // 이체 입력 필드를 비웁니다.
        loginAndSignUp.OnErrorPopup(money + "원을 " + id + "에게 이체하였습니다."); // 이체 성공 팝업을 띄웁니다.
    }

    public void OnDepositButton(int Amount)
    {
        OnDeposit(Amount); // Amount 만큼 입금합니다.
    }
    public void OnWithdrawalButton(int Amount)
    {
        OnWithdraw(Amount); // Amount 만큼 출금합니다.
    }

    public void OnTransferButton()
    {
        string inputAmoutText = TransferInputField.text;
        string inputIDText = TransferToID.text;

        if (int.TryParse(inputAmoutText, out int transferAmount))
        {
            Debug.Log($"Transfer Amount: {transferAmount}, Transfer To ID: {inputIDText}"); // 디버그 로그로 이체 금액과 ID를 출력합니다.
            OnTransfer(transferAmount, inputIDText); // Amount와 id를 사용하여 이체합니다.
        }
        else
        {
            loginAndSignUp.OnErrorPopup("이체불가 오류"); // 변환 실패 시 오류 팝업을 띄웁니다.
            // 아마 아이디나 금액이 입력되지 않으면 이 에러가 발생할 것이므로 OnTransfer()의 ID, 금액 에러는 작동하지 않을 것입니다.
        }
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
