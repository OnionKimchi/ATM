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
    [SerializeField] private LoginAndSignUp loginAndSignUp; // LoginAndSignUp�� �������� ���� ����


    [SerializeField] UserInfoUI userInfoUI; // UserInfoUI�� �������� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        OnBankUI();// ���� �� BankUI�� Ȱ��ȭ�ϰ� �������� ��Ȱ��ȭ�մϴ�.
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
        if (GameManager.Instance.UserData.cashAmount < money) // UserData�� Cash�� �Աݾ׺��� ������ ���â�� ���ϴ�.
        {
            loginAndSignUp.OnErrorPopup("�ܾ��� �����մϴ�.");// �˾� ����â�� ���ϴ�.
            return;
        }
        GameManager.Instance.UserData.cashAmount -= money; // UserData�� Cash���� �Աݾ��� ���ϴ�.
        GameManager.Instance.UserData.bankAmount += money; // UserData�� Money�� �Աݾ��� ��
                                      // �մϴ�.

        userInfoUI.Refresh();// UserInfoUI�� �����մϴ�.
        DepositInputField.text = ""; // �Ա� �Է� �ʵ带 ���ϴ�.


        GameManager.Instance.SaveUserData(GameManager.Instance.UserData); // ���� �� �����մϴ�.
    }

    public void OnWithdraw(int money)
    {
        if (GameManager.Instance.UserData.bankAmount < money) // UserData�� Bank�� ��ݾ׺��� ������ ���â�� ���ϴ�.
        {
            loginAndSignUp.OnErrorPopup("�ܾ��� �����մϴ�.");// �˾� ����â�� ���ϴ�.
            return;
        }
        GameManager.Instance.UserData.cashAmount += money; // UserData�� Cash�� ��ݾ��� ���մϴ�.
        GameManager.Instance.UserData.bankAmount -= money; // UserData�� Bank���� ��ݾ��� ���ϴ�.

        userInfoUI.Refresh();// UserInfoUI�� �����մϴ�.
        WithdrawInputField.text = ""; // ��� �Է� �ʵ带 ���ϴ�.

        GameManager.Instance.SaveUserData(GameManager.Instance.UserData); // ���� �� �����մϴ�.
    }

    public void OnTransfer(int money, string id)
    {

        // json ���Ͽ��� id�� �ش��ϴ� UserData�� �ִ��� Ȯ���մϴ�.
        string path = Path.Combine(Application.persistentDataPath, $"{id}.json");
        if (string.IsNullOrEmpty(id)) // ID�� ��������� ���â�� ���ϴ�.
        {
            loginAndSignUp.OnErrorPopup("ID�� �Է����ּ���."); // �˾� ����â�� ���ϴ�.
            return;
        }
        if (!File.Exists(path)) // �ش� ID�� json ������ ������ ���â�� ���ϴ�.
        {
            loginAndSignUp.OnErrorPopup("�������� �ʴ� ID�Դϴ�."); // �˾� ����â�� ���ϴ�.
            return;
        }
        if (id == GameManager.Instance.UserData.ID) // ID�� �ڱ� �ڽ��̸� ���â�� ���ϴ�.
        {
            loginAndSignUp.OnErrorPopup("�ڱ� �ڽſ��� ��ü�� �� �����ϴ�."); // �˾� ����â�� ���ϴ�.
            return;
        }
        if (GameManager.Instance.UserData.bankAmount < money) // UserData�� Bank�� ��ü�׺��� ������ ���â�� ���ϴ�.
        {
            loginAndSignUp.OnErrorPopup("�ܾ��� �����մϴ�.");// �˾� ����â�� ���ϴ�.
            return;
        }
        string json = File.ReadAllText(path); // �ش� ID�� json ������ �о�ɴϴ�.
        //json ������ UserData ��ü�� ��ȯ�մϴ�.
        UserData targetUserData = JsonUtility.FromJson<UserData>(json); // json ������ UserData ��ü�� ��ȯ�մϴ�.
        targetUserData.bankAmount += money; // Ÿ�� UserData�� Bank�� ��ü���� ���մϴ�.
        GameManager.Instance.UserData.bankAmount -= money; // UserData�� Bank���� ��ü���� ���ϴ�
        GameManager.Instance.SaveUserData(targetUserData); // ����� Ÿ�� UserData�� �����մϴ�.
        GameManager.Instance.SaveUserData(GameManager.Instance.UserData); // ����� UserData�� �����մϴ�.
        userInfoUI.Refresh();// UserInfoUI�� �����մϴ�.
        TransferInputField.text = ""; // ��ü �Է� �ʵ带 ���ϴ�.
        loginAndSignUp.OnErrorPopup(money + "���� " + id + "���� ��ü�Ͽ����ϴ�."); // ��ü ���� �˾��� ���ϴ�.
    }

    public void OnDepositButton(int Amount)
    {
        OnDeposit(Amount); // Amount ��ŭ �Ա��մϴ�.
    }
    public void OnWithdrawalButton(int Amount)
    {
        OnWithdraw(Amount); // Amount ��ŭ ����մϴ�.
    }

    public void OnTransferButton()
    {
        string inputAmoutText = TransferInputField.text;
        string inputIDText = TransferToID.text;

        if (int.TryParse(inputAmoutText, out int transferAmount))
        {
            Debug.Log($"Transfer Amount: {transferAmount}, Transfer To ID: {inputIDText}"); // ����� �α׷� ��ü �ݾװ� ID�� ����մϴ�.
            OnTransfer(transferAmount, inputIDText); // Amount�� id�� ����Ͽ� ��ü�մϴ�.
        }
        else
        {
            loginAndSignUp.OnErrorPopup("��ü�Ұ� ����"); // ��ȯ ���� �� ���� �˾��� ���ϴ�.
            // �Ƹ� ���̵� �ݾ��� �Էµ��� ������ �� ������ �߻��� ���̹Ƿ� OnTransfer()�� ID, �ݾ� ������ �۵����� ���� ���Դϴ�.
        }
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
