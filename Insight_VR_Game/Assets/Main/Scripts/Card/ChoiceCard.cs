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
}
