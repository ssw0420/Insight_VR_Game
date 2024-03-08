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
        PlayerController.instance.BlackHoleState = true;
        //GameObject BlackHolePrefab = Resources.Load("Prefabs/Skill/BlackHole Sphere") as GameObject;
        DestroyCards();
    }

    public void ChoiceIceBall()
    {
        PlayerController.instance.IceState = true;
        //GameObject IceBallPrefab = Resources.Load("Prefabs/Skill/IceBall Sphere") as GameObject;
        DestroyCards();
    }

    public void ChoiceUpgrade_1()
    {
        PlayerController.instance.DmgState = true;
        DestroyCards();
    }

    public void ChoiceUpgrade_2()
    {
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
                    ChoiceIceBall();
                    WeaponManager.instance.OnIce();
                    MonsterManager.Instance.ReadSpawnFile();
                    BgmManager.Instance.StartRoundAudio();
                    Destroy(other.gameObject);
                    break;
                case CardType.PowerUp:
                    ChoiceUpgrade_1();
                    MonsterManager.Instance.ReadSpawnFile();
                    BgmManager.Instance.StartRoundAudio();
                    Destroy(other.gameObject);
                    break;
                case CardType.Heal:
                    ChoiceUpgrade_2();
                    MonsterManager.Instance.ReadSpawnFile();
                    BgmManager.Instance.StartRoundAudio();
                    Destroy(other.gameObject);
                    break;
            }
        }
    }
}
