using UnityEngine;
using System.IO; // �� ���ӽ����̽��� ���� ������� ���� �ʿ��մϴ�.

public class GameManager : MonoBehaviour
{
    private static GameManager instance;// CharacterManager�� �ν��Ͻ��� ������ ���� ����
    public static GameManager Instance// CharacterManager�� �ν��Ͻ��� ��ȯ�ϴ� ������Ƽ
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

    public UserData userData;// ����� �����͸� �����ϴ� ����

    public UserData UserData// ����� �����͸� ��ȯ�ϰų� �����ϴ� ������Ƽ
    {
        get { return userData; }
        set { userData = value; }
    }

    private void Awake()// GameManager�� �ν��Ͻ��� �ʱ�ȭ�ϴ� �޼���, ����� �̱��� ������ ����Ͽ� �ν��Ͻ��� �����մϴ�.
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);


            // UserData �⺻ �ν��Ͻ� ����
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
        // data ��ü�� JSON ���ڿ��� ��ȯ�մϴ�.
        // JsonUtility.ToJson�� Unity���� �����ϴ� �޼����, ��ü�� JSON ������ ���ڿ��� ������ݴϴ�.
        string json = JsonUtility.ToJson(data, true);

        // ������ ������ ��θ� �����մϴ�.
        // Application.persistentDataPath�� �� �÷������� �����ϰ� ������ ������ �� �ִ� ��θ� �����մϴ�.
        // ������Ʈ ������ �ٱ��� ��θ� �����ؾ� �б� ���� ������Ʈ���� ȣȯ�� �ǹǷ� ������ ��ΰ� �ʿ��Ѱ��Դϴ�.
        string path = Path.Combine(Application.persistentDataPath, $"{data.ID}.json");

        // ��ȯ�� JSON ���ڿ��� ���Ͽ� �����մϴ�.
        // File.WriteAllText�� ������ ��ο� ���ڿ��� �����ϸ�, ������ ������ ���� �����մϴ�.
        File.WriteAllText(path, json);

        // �� �޼��带 ȣ���ϸ� ���� userData�� ���� userdata.json ���Ͽ� ����˴ϴ�.
    }

    public string LoadUserData(string id, string password)
    {
        // ����� ������ ��θ� �����մϴ�.
        string path = Path.Combine(Application.persistentDataPath, $"{id}.json");

        // ������ �����ϴ��� Ȯ���մϴ�.
        if (!File.Exists(path))
        {
            return "�������� �ʴ� ID�Դϴ�."; // ������ ������ ��� �޽����� ��ȯ�մϴ�.
        }
        string json = File.ReadAllText(path);
        UserData loaded = JsonUtility.FromJson<UserData>(json);
        if (loaded.Password != password) // ��й�ȣ�� ��ġ���� ������ false�� ��ȯ�մϴ�.
        {
            return "��й�ȣ�� ��ġ���� �ʽ��ϴ�."; // ��й�ȣ�� ��ġ���� ������ ��� �޽����� ��ȯ�մϴ�.
        }
        userData = loaded;
        return "�α��� ����"; // �α��� ���� �޽����� ��ȯ�մϴ�.

    }
}