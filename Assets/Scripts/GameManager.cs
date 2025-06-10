using UnityEngine;
using System.IO; // 이 네임스페이스는 파일 입출력을 위해 필요합니다.

public class GameManager : MonoBehaviour
{
    private static GameManager instance;// CharacterManager의 인스턴스를 저장할 정적 변수
    public static GameManager Instance// CharacterManager의 인스턴스를 반환하는 프로퍼티
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return instance;
        }
    }

    public UserData userData;// 사용자 데이터를 저장하는 변수

    public UserData UserData// 사용자 데이터를 반환하거나 설정하는 프로퍼티
    {
        get { return userData; }
        set { userData = value; }
    }

    private void Awake()// GameManager의 인스턴스를 초기화하는 메서드, 방어적 싱글턴 패턴을 사용하여 인스턴스를 관리합니다.
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);


            // UserData 기본 인스턴스 생성
            userData = new UserData
            {
                ID = "defaultID",
                Password = "defaultPassword",
                userName = "defaultUserName",
                cashAmount = 0,
                bankAmount = 0
            };
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveUserData(UserData data)
    {
        // data 객체를 JSON 문자열로 변환합니다.
        // JsonUtility.ToJson은 Unity에서 제공하는 메서드로, 객체를 JSON 포맷의 문자열로 만들어줍니다.
        string json = JsonUtility.ToJson(data, true);

        // 저장할 파일의 경로를 지정합니다.
        // Application.persistentDataPath는 각 플랫폼별로 안전하게 파일을 저장할 수 있는 경로를 제공합니다.
        // 프로젝트 폴더의 바깥에 경로를 지정해야 읽기 전용 프로젝트에도 호환이 되므로 안전한 경로가 필요한것입니다.
        string path = Path.Combine(Application.persistentDataPath, $"{data.ID}.json");

        // 변환된 JSON 문자열을 파일에 저장합니다.
        // File.WriteAllText는 지정한 경로에 문자열을 저장하며, 파일이 없으면 새로 생성합니다.
        File.WriteAllText(path, json);

        // 이 메서드를 호출하면 현재 userData의 값이 userdata.json 파일에 저장됩니다.
    }

    public string LoadUserData(string id, string password)
    {
        // 저장된 파일의 경로를 지정합니다.
        string path = Path.Combine(Application.persistentDataPath, $"{id}.json");

        // 파일이 존재하는지 확인합니다.
        if (!File.Exists(path))
        {
            return "존재하지 않는 ID입니다."; // 파일이 없으면 경고 메시지를 반환합니다.
        }
        string json = File.ReadAllText(path);
        UserData loaded = JsonUtility.FromJson<UserData>(json);
        if (loaded.Password != password) // 비밀번호가 일치하지 않으면 false를 반환합니다.
        {
            return "비밀번호가 일치하지 않습니다."; // 비밀번호가 일치하지 않으면 경고 메시지를 반환합니다.
        }
        userData = loaded;
        return "로그인 성공"; // 로그인 성공 메시지를 반환합니다.

    }
}