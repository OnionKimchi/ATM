using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class LoginAndSignUp : MonoBehaviour
{
    [SerializeField] private TMP_InputField loginIdInputField; // 로그인 ID 입력 필드
    [SerializeField] private TMP_InputField loginPasswordInputField; // 로그인 비밀번호 입력 필드
    [SerializeField] private TMP_InputField signUpIdInputField; // 회원가입 ID 입력 필드
    [SerializeField] private TMP_InputField signUpPasswordInputField; // 회원가입 비밀번호 입력 필드
    [SerializeField] private TMP_InputField signUpNameInputField; // 회원가입 이름 입력 필드


    [SerializeField] private GameObject popupLogin; // 로그인 팝업 UI
    [SerializeField] private GameObject popupSignUp; // 회원가입 팝업 UI
    [SerializeField] private GameObject popupBank; // 은행 팝업 UI
    [SerializeField] private GameObject popupError; // 에러 팝업 UI
    [SerializeField] private TMPro.TMP_Text errorText; // 에러 메시지를 표시할 텍스트 컴포넌트
    void Start()
    {
        OnLoginUI(); // 시작 시 로그인 UI를 활성화합니다.
    }

    public void OnLoginUI()
    {
        popupLogin.SetActive(true); // 로그인 팝업 UI 활성화
        popupSignUp.SetActive(false); // 회원가입 팝업 UI 비활성화
        popupBank.SetActive(false); // 은행 팝업 UI 비활성화
        popupError.SetActive(false); // 에러 팝업 UI 비활성화
    }

    public void OnSignUpUI()
    {
        popupLogin.SetActive(false); // 로그인 팝업 UI 비활성화
        popupSignUp.SetActive(true); // 회원가입 팝업 UI 활성화
        popupBank.SetActive(false); // 은행 팝업 UI 비활성화
        popupError.SetActive(false); // 에러 팝업 UI 비활성화
    }

    public void OnBankUI()
    {
        popupLogin.SetActive(false); // 로그인 팝업 UI 비활성화
        popupSignUp.SetActive(false); // 회원가입 팝업 UI 비활성화
        popupBank.SetActive(true); // 은행 팝업 UI 활성화
        popupError.SetActive(false); // 에러 팝업 UI 비활성화
    }

    public void OnSignUp()
    {
        string id = signUpIdInputField.text; // ID 입력 필드에서 텍스트 가져오기
        string password = signUpPasswordInputField.text; // 비밀번호 입력 필드에서 텍스트 가져오기
        string userName = signUpNameInputField.text; // 이름 입력 필드에서 텍스트 가져오기
        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(password)|| string.IsNullOrEmpty(userName)) // ID, 비밀번호, 이름 중 하나라도 비어있으면 경고창 표시
        {
            OnErrorPopup("ID, 비밀번호, 이름을 모두 입력해주세요."); // 에러 팝업 UI 활성화
            return;
        }
        // 회원가입 로직을 여기에 추가합니다.

        // 저장된 파일의 경로를 지정합니다.
        string path = Path.Combine(Application.persistentDataPath, $"{id}.json");

        if (File.Exists(path)) // 이미 같은 ID로 저장된 파일이 있으면 경고창 표시
        {
            OnErrorPopup("이미 존재하는 ID입니다. 다른 ID를 사용해주세요."); // 에러 팝업 UI 활성화
            return;
        }

        // ID가 중복되지 않으면 회원가입 성공 팝업 출력
        OnErrorPopup("회원가입 성공! 로그인 화면으로 돌아갑니다."); // 에러 팝업 UI 활성화


        // 새로운 회원가입 정보를 UserData 객체로 생성합니다.
        UserData newUserData = new UserData
        {
            ID = id, // ID 설정
            Password = password, // 비밀번호 설정
            userName = userName, // 사용자 이름 설정
            cashAmount = 100000, // 초기 현금 금액
            bankAmount = 50000 // 초기 은행 금액
        };

        GameManager.Instance.SaveUserData(newUserData); // GameManager를 통해 UserData 저장


        // 회원가입 후 로그인 UI로 돌아가기
        OnLoginUI();
    }

    public void OnLogin()
    {
        string id = loginIdInputField.text; // ID 입력 필드에서 텍스트 가져오기
        string password = loginPasswordInputField.text; // 비밀번호 입력 필드에서 텍스트 가져오기
        GameManager.Instance.LoadUserData(id, password); // GameManager를 통해 UserData 불러오기
        switch (GameManager.Instance.LoadUserData(id, password))
        {
            case "ID 없음": // ID가 존재하지 않는 경우
                OnErrorPopup("존재하지 않는 ID입니다. 회원가입을 먼저 해주세요."); // 에러 팝업 UI 활성화
                break;
            case "비밀번호 불일치":
                OnErrorPopup("비밀번호가 일치하지 않습니다. 다시 입력해주세요."); // 에러 팝업 UI 활성화
                break;
            case "로그인 성공":
                OnBankUI();
                OnErrorPopup("로그인 성공!"); // 로그인 성공 메시지 표시
                break;
            default:
                Debug.LogError("예외 처리"); // 예외 처리: 알 수 없는 오류 발생
                break;
        }
    }
    
    public void OnErrorPopup(string errorMessage)
    {
        errorText.text = errorMessage; // 에러 메시지 설정
        popupError.SetActive(true); // 에러 팝업 UI 활성화
    }

    public void OnCloseErrorPopup()
    {
        errorText.text = ""; // 에러 메시지 초기화
        popupError.SetActive(false); // 에러 팝업 UI 비활성화
    }
}

