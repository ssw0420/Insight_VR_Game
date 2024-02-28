using Unity.VisualScripting;
using UnityEngine;

public class ChoiceCard : MonoBehaviour
{
    public static ChoiceCard instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public bool CardChoice = false;

    public bool BlackHoleChoice = false;
    public bool IceBallChoice = false;
    public bool Upgrade_1_Choice = false;
    public bool Upgrade_2_Choice = false;

    public void ChoiceBlackHole()
    {
        Debug.Log("��Ȧ �Լ� Ȱ��ȭ");
        CardChoice = true;
        BlackHoleChoice = true;
        PlayerController.instance.BlackHoleState = true;
        GameObject BlackHolePrefab = Resources.Load("Prefabs/Skill/BlackHole Sphere") as GameObject;
        if(BlackHolePrefab != null ) { Debug.Log("��Ȧ ������ ���� ����"); }
        DestroyCards();
    }

    public void ChoiceIceBall()
    {
        Debug.Log("���̽� �Լ� Ȱ��ȭ");
        IceBallChoice = true;
        PlayerController.instance.IceState = true;
        GameObject IceBallPrefab = Resources.Load("Prefabs/Skill/IceBall Sphere") as GameObject;
        if (IceBallPrefab != null) { Debug.Log("���̽� ������ ���� ����"); }
        DestroyCards();
    }

    public void ChoiceUpgrade_1()
    {
        Debug.Log("���׷��̵� 1 �Լ� Ȱ��ȭ");
        Upgrade_1_Choice = true;
        PlayerController.instance.DmgState = true;
        DestroyCards();
    }

    public void ChoiceUpgrade_2()
    {
        Debug.Log("���׷��̵� 2 �Լ� Ȱ��ȭ");
        Upgrade_2_Choice = true;
        PlayerController.instance.HealthState = true;

        //PlayerStats.Instance.AddHealth(5);
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

    public void Update()
    {
        if (BlackHoleChoice)
        {
            
            transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        }
    }
}
