using UnityEngine;
using TMPro; // TMP_Text ��� ��

public class UserInfoUI : MonoBehaviour
{
    public TMP_Text userNameText;
    public TMP_Text cashAmountText;
    public TMP_Text bankAmountText;

    void Start()
    {
        Refresh(); // �ʱ�ȭ �� ����� ���� ����
    }

    public void Refresh()
    {         
        UserData userData = GameManager.Instance.UserData;
        userNameText.text = userData.userName;
        cashAmountText.text = userData.cashAmount.ToString("N0");// �޸� ǥ�ø� ���� "N0"�� ����϶�� ��
        bankAmountText.text = userData.bankAmount.ToString("N0");
    }
}