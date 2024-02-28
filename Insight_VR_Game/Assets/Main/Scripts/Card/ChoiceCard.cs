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
        Debug.Log("블랙홀 함수 활성화");
        PlayerController.instance.BlackHoleState = true;
        GameObject BlackHolePrefab = Resources.Load("Prefabs/Skill/BlackHole Sphere") as GameObject;
        if(BlackHolePrefab != null ) { Debug.Log("블랙홀 프리팹 연결 성공"); }
        DestroyCards();
    }

    public void ChoiceIceBall()
    {
        Debug.Log("아이스 함수 활성화");
        PlayerController.instance.IceState = true;
        GameObject IceBallPrefab = Resources.Load("Prefabs/Skill/IceBall Sphere") as GameObject;
        if (IceBallPrefab != null) { Debug.Log("아이스 프리팹 연결 성공"); }
        DestroyCards();
    }

    public void ChoiceUpgrade_1()
    {
        Debug.Log("업그레이드 1 함수 활성화");
        PlayerController.instance.DmgState = true;
        DestroyCards();
    }

    public void ChoiceUpgrade_2()
    {
        Debug.Log("업그레이드 2 함수 활성화");
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
