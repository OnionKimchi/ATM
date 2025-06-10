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
    [SerializeField] private LoginAndSignUp loginAndSignUp; // LoginAndSignUp�� �������� ���� ����

    private UserData userData;// UserData�� �������� ���� ����

    [SerializeField] UserInfoUI userInfoUI; // UserInfoUI�� �������� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        OnBankUI();// ���� �� BankUI�� Ȱ��ȭ�ϰ� �������� ��Ȱ��ȭ�մϴ�.
    }

    void Awake()
    {
        userData = GameManager.Instance.UserData; // GameManager���� UserData�� �����ɴϴ�.
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
        if (userData.cashAmount < money) // UserData�� Cash�� �Աݾ׺��� ������ ���â�� ���ϴ�.
        {
            loginAndSignUp.OnErrorPopup("�ܾ��� �����մϴ�.");// �˾� ����â�� ���ϴ�.
            return;
        }
        userData.cashAmount -= money; // UserData�� Cash���� �Աݾ��� ���ϴ�.
        userData.bankAmount += money; // UserData�� Money�� �Աݾ��� ��
                                      // �մϴ�.

        userInfoUI.Refresh();// UserInfoUI�� �����մϴ�.


        GameManager.Instance.SaveUserData(userData); // ���� �� �����մϴ�.
    }

    public void OnWithdraw(int money)
    {
        if (userData.bankAmount < money) // UserData�� Bank�� ��ݾ׺��� ������ ���â�� ���ϴ�.
        {
            loginAndSignUp.OnErrorPopup("�ܾ��� �����մϴ�.");// �˾� ����â�� ���ϴ�.
            return;
        }
        userData.cashAmount += money; // UserData�� Cash�� ��ݾ��� ���մϴ�.
        userData.bankAmount -= money; // UserData�� Bank���� ��ݾ��� ���ϴ�.

        userInfoUI.Refresh();// UserInfoUI�� �����մϴ�.

        GameManager.Instance.SaveUserData(userData); // ���� �� �����մϴ�.
    }

    public void OnDepositButton(int Amount)
    {
        OnDeposit(Amount); // Amount ��ŭ �Ա��մϴ�.
    }
    public void OnWithdrawalButton(int Amount)
    {
        OnWithdraw(Amount); // Amount ��ŭ ����մϴ�.
    }
    public void OnDepositInputFieldText()
    {
        string inputText = DepositInputField.text; // �Է� �ʵ忡�� �ؽ�Ʈ�� �����ɴϴ�.
        if (int.TryParse(inputText, out int depositAmount)) // �Էµ� �ؽ�Ʈ�� ������ ��ȯ�մϴ�.
        {
            OnDeposit(depositAmount); // ��ȯ�� �ݾ��� �Ա��մϴ�.
        }
        else
        {
            loginAndSignUp.OnErrorPopup("�ԱݺҰ� ����"); // ��ȯ ���� �� ���� �˾��� ���ϴ�.
        }
    }

    public void OnWithdrawInputFieldText()
    {
        string inputText = WithdrawInputField.text; // �Է� �ʵ忡�� �ؽ�Ʈ�� �����ɴϴ�.
        if (int.TryParse(inputText, out int withdrawAmount)) // �Էµ� �ؽ�Ʈ�� ������ ��ȯ�մϴ�.
        {
            OnWithdraw(withdrawAmount); // ��ȯ�� �ݾ��� ����մϴ�.
        }
        else
        {
            loginAndSignUp.OnErrorPopup("��ݺҰ� ����"); // ��ȯ ���� �� ���� �˾��� ���ϴ�.
        }
    }
}
