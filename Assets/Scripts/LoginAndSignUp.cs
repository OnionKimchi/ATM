using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class LoginAndSignUp : MonoBehaviour
{
    [SerializeField] private TMP_InputField loginIdInputField; // �α��� ID �Է� �ʵ�
    [SerializeField] private TMP_InputField loginPasswordInputField; // �α��� ��й�ȣ �Է� �ʵ�
    [SerializeField] private TMP_InputField signUpIdInputField; // ȸ������ ID �Է� �ʵ�
    [SerializeField] private TMP_InputField signUpPasswordInputField; // ȸ������ ��й�ȣ �Է� �ʵ�
    [SerializeField] private TMP_InputField signUpNameInputField; // ȸ������ �̸� �Է� �ʵ�


    [SerializeField] private GameObject popupLogin; // �α��� �˾� UI
    [SerializeField] private GameObject popupSignUp; // ȸ������ �˾� UI
    [SerializeField] private GameObject popupBank; // ���� �˾� UI
    [SerializeField] private GameObject popupSignUpSuccess; // ȸ������ ���� �˾� UI
    [SerializeField] private GameObject popupSignUpFail; // ȸ������ ���� �˾� UI
    void Start()
    {
        OnLoginUI(); // ���� �� �α��� UI�� Ȱ��ȭ�մϴ�.
    }

    public void OnLoginUI()
    {
        popupLogin.SetActive(true); // �α��� �˾� UI Ȱ��ȭ
        popupSignUp.SetActive(false); // ȸ������ �˾� UI ��Ȱ��ȭ
        popupBank.SetActive(false); // ���� �˾� UI ��Ȱ��ȭ
    }

    public void OnSignUpUI()
    {
        popupLogin.SetActive(false); // �α��� �˾� UI ��Ȱ��ȭ
        popupSignUp.SetActive(true); // ȸ������ �˾� UI Ȱ��ȭ
        popupBank.SetActive(false); // ���� �˾� UI ��Ȱ��ȭ
    }

    public void OnBankUI()
    {
        popupLogin.SetActive(false); // �α��� �˾� UI ��Ȱ��ȭ
        popupSignUp.SetActive(false); // ȸ������ �˾� UI ��Ȱ��ȭ
        popupBank.SetActive(true); // ���� �˾� UI Ȱ��ȭ
    }

    public void OnSignUp()
    {
        string id = signUpIdInputField.text; // ID �Է� �ʵ忡�� �ؽ�Ʈ ��������
        string password = signUpPasswordInputField.text; // ��й�ȣ �Է� �ʵ忡�� �ؽ�Ʈ ��������
        string userName = signUpNameInputField.text; // �̸� �Է� �ʵ忡�� �ؽ�Ʈ ��������
        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(password)|| string.IsNullOrEmpty(userName)) // ID, ��й�ȣ, �̸� �� �ϳ��� ��������� ���â ǥ��
        {
            Debug.LogWarning("ID�� ��й�ȣ�� ��� �Է����ּ���.");
            return;
        }
        // ȸ������ ������ ���⿡ �߰��մϴ�.

        // ����� ������ ��θ� �����մϴ�.
        string path = Path.Combine(Application.persistentDataPath, $"{id}.json");

        if (File.Exists(path)) // �̹� ���� ID�� ����� ������ ������ ���â ǥ��
        {
            popupSignUpFail.SetActive(true); // ȸ������ ���� �˾� UI Ȱ��ȭ
            return;
        }

        // ID�� �ߺ����� ������ ȸ������ ���� �˾� ���


        // ���ο� ȸ������ ������ UserData ��ü�� �����մϴ�.
        UserData newUserData = new UserData
        {
            ID = id, // ID ����
            Password = password, // ��й�ȣ ����
            userName = userName, // ����� �̸� ����
            cashAmount = 100000, // �ʱ� ���� �ݾ�
            bankAmount = 50000 // �ʱ� ���� �ݾ�
        };

        GameManager.Instance.SaveUserData(newUserData); // GameManager�� ���� UserData ����


        // ȸ������ �� �α��� UI�� ���ư���
        OnLoginUI();
    }

    public void OnLogin()
    {
        string id = loginIdInputField.text; // ID �Է� �ʵ忡�� �ؽ�Ʈ ��������
        string password = loginPasswordInputField.text; // ��й�ȣ �Է� �ʵ忡�� �ؽ�Ʈ ��������
        GameManager.Instance.LoadUserData(id, password); // GameManager�� ���� UserData �ҷ�����
        switch (GameManager.Instance.LoadUserData(id, password))
        {
            case "�������� �ʴ� ID�Դϴ�.": // ID�� �������� �ʴ� ���
                Debug.LogWarning("�������� �ʴ� ID�Դϴ�. ȸ�������� ���ּ���.");
                break;
            case "��й�ȣ�� ��ġ���� �ʽ��ϴ�.":
                Debug.LogWarning("��й�ȣ�� ��ġ���� �ʽ��ϴ�. �ٽ� �Է����ּ���.");
                break;
                case "�α��� ����":
                Debug.Log("�α��� ����!"); // �α��� ���� �޽��� ���
                break;
            default: 
                break;
        }
        popupLogin.SetActive(false); // �α��� �˾� UI ��Ȱ��ȭ
        popupBank.SetActive(true); // ���� �˾� UI Ȱ��ȭ
    }
}
