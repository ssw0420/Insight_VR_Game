using UnityEngine;

public class ChoiceCard : MonoBehaviour
{
    enum CardType
    {
        BlackHole,
        Ice,
        PowerUp,
        Heal,
    }

    public static ChoiceCard instance;
    [SerializeField] CardType cardType;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ChoiceBlackHole()
    {
        Debug.Log("��Ȧ �Լ� Ȱ��ȭ");
        PlayerController.instance.BlackHoleState = true;
        GameObject BlackHolePrefab = Resources.Load("Prefabs/Skill/BlackHole Sphere") as GameObject;
        if(BlackHolePrefab != null ) { Debug.Log("��Ȧ ������ ���� ����"); }
        DestroyCards();
    }

    public void ChoiceIceBall()
    {
        Debug.Log("���̽� �Լ� Ȱ��ȭ");
        PlayerController.instance.IceState = true;
        GameObject IceBallPrefab = Resources.Load("Prefabs/Skill/IceBall Sphere") as GameObject;
        if (IceBallPrefab != null) { Debug.Log("���̽� ������ ���� ����"); }
        DestroyCards();
    }

    public void ChoiceUpgrade_1()
    {
        Debug.Log("���׷��̵� 1 �Լ� Ȱ��ȭ");
        PlayerController.instance.DmgState = true;
        DestroyCards();
    }

    public void ChoiceUpgrade_2()
    {
        Debug.Log("���׷��̵� 2 �Լ� Ȱ��ȭ");
        PlayerController.instance.HealthState = true;

        DestroyCards();
    }

    public void DestroyCards()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == LayerMask.NameToLayer("Card"))
            {
                Destroy(obj);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Arrow"))
        {
            switch (cardType) {
                case CardType.BlackHole:
                    ChoiceBlackHole();
                    WeaponManager.instance.OnBlackHole();
                    MonsterManager.Instance.ReadSpawnFile();
                    BgmManager.Instance.StartRoundAudio();
                    Destroy(other.gameObject);
                    break;
                case CardType.Ice:
                    ChoiceCard.instance.ChoiceIceBall();
                    WeaponManager.instance.OnIce();
                    MonsterManager.Instance.ReadSpawnFile();
                    BgmManager.Instance.StartRoundAudio();
                    Destroy(other.gameObject);
                    break;
                case CardType.PowerUp:
                    PlayerController.instance.DmgState = true;
                    ChoiceCard.instance.ChoiceUpgrade_1();
                    MonsterManager.Instance.ReadSpawnFile();
                    BgmManager.Instance.StartRoundAudio();
                    Destroy(other.gameObject);
                    break;
                case CardType.Heal:
                    ChoiceCard.instance.ChoiceUpgrade_2();
                    MonsterManager.Instance.ReadSpawnFile();
                    BgmManager.Instance.StartRoundAudio();
                    Destroy(other.gameObject);
                    break;
            }
        }
    }
}
