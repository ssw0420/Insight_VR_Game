using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.EventSystems;

public class WeaponManager : MonoBehaviour
{
    enum SkillType 
    { 
        Ice = 0,
        BlackHole = 1,
        None = 2,
    }
    [SerializeField]SkillType s_Type;

    //index
    //0 = IceLance
    //1 = BlackHole
    enum SkillState
    {
        Idle,
        Fire,
        Cooldown,
        Reloading,
    }
    [SerializeField]List<SkillState> s_State = new List<SkillState>();

    XRGrabInteractable interactable;

    Renderer render;

    [Header("Skill Look")]
    [SerializeField] List<Material> skillMaterials;
    [SerializeField]List<ParticleSystem> skillEffect;

    [Header("Skill Info")]
    [SerializeField] List<float> cooldown_time;// = 8.0f;
    [SerializeField] List<float> using_time;// = 4.5f;
    float iceLance_time;

    [Header("Skill Object")]
    [SerializeField] List<GameObject> skillPrefab;

    private void Awake()
    {
        render = GetComponent<Renderer>();
        interactable = GetComponent<XRGrabInteractable>();

        ParticleSystem[] effect = GetComponentsInChildren<ParticleSystem>();
        skillEffect.AddRange(effect);
    }

    private void Start()
    {
        render.material = skillMaterials[0];

        //test code
        //if(Frist Skill is Ice)
        s_Type = SkillType.Ice;
        s_State.Add(SkillState.Idle);
        s_State.Add(SkillState.Idle);

        foreach (ParticleSystem effect in skillEffect)
            effect.Stop();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (s_Type == SkillType.None)
                return;

            if (s_State[(int)SkillType.Ice] == SkillState.Fire || s_State[(int)SkillType.BlackHole] == SkillState.Fire)
                return;

            switch (s_Type)
            {
                case SkillType.Ice:
                    s_Type = SkillType.BlackHole;
                    break;
                case SkillType.BlackHole:
                    s_Type = SkillType.Ice;
                    break;
            }

            SkillChange();
        }
    }

    void SkillChange()
    {
        bool isCoolTime = CheckCoolTime((int)s_Type);

        if (isCoolTime)
            render.material = skillMaterials[(int)SkillType.None];
        else
            render.material = skillMaterials[(int)s_Type];
    }

    bool CheckCoolTime(int skillType)
    {
        if (s_State[skillType] == SkillState.Cooldown)
            return true;
        return false;
    }

    public void ShootSkill()
    {
        switch (s_Type)
        {
            case SkillType.Ice:
                ShootIceLance();
                break;
            case SkillType.BlackHole:
                ShootBlackHole();
                break;
        }
    }

    void ShootIceLance()
    {
        if (s_Type != SkillType.Ice)
            return;

        if (s_State[(int)SkillType.Ice] == SkillState.Fire || s_State[(int)SkillType.Ice] == SkillState.Cooldown || s_State[(int)SkillType.Ice] == SkillState.Reloading)
            return;
        using_time[(int)SkillType.Ice] = 4.5f;
        s_State[0] = SkillState.Fire;
        Debug.Log("스킬 사용");
    }

    void ShootBlackHole()
    {
        if (s_Type != SkillType.BlackHole)
            return;

        if (s_State[(int)SkillType.BlackHole] == SkillState.Fire || s_State[(int)SkillType.BlackHole] == SkillState.Cooldown || s_State[(int)SkillType.BlackHole] == SkillState.Reloading)
            return;

        GameObject blackHole = Instantiate(skillPrefab[(int)SkillType.BlackHole], transform);
        s_State[(int)SkillType.BlackHole] = SkillState.Fire;
        StartCoroutine(ChangeBall());
    }

    private void FixedUpdate()
    {
        switch (s_State[(int)SkillType.Ice])
        {
            case SkillState.Idle:
                UpdateIdle((int)SkillType.Ice);
                break;
            case SkillState.Fire:
                UpdateIceLanceFire();
                break;
            case SkillState.Cooldown:
                UpdateCooldown((int)SkillType.Ice);
                break;
            case SkillState.Reloading:
                break;
        }

        switch (s_State[(int)SkillType.BlackHole])
        {
            case SkillState.Idle:
                UpdateIdle((int)SkillType.BlackHole);
                break;
            case SkillState.Fire:
                UpdateBlackHoleFire();
                break;
            case SkillState.Cooldown:
                UpdateCooldown((int)SkillType.BlackHole);
                break;
            case SkillState.Reloading:
                break;
        } 
    }

    void UpdateIdle(int skillType)
    {
        cooldown_time[skillType] = 8.0f;
        using_time[skillType] = 4.5f;
    }

    void UpdateIceLanceFire()
    {
        using_time[(int)SkillType.Ice] -= Time.fixedDeltaTime;
        iceLance_time += Time.fixedDeltaTime;

        if (iceLance_time >= 0.2f)
        {
            iceLance_time = 0.0f;
            GameObject iceLance = Instantiate(skillPrefab[(int)SkillType.Ice], transform);
        }

        if (using_time[(int)SkillType.Ice] <= 0.0f)
        {
            cooldown_time[(int)SkillType.Ice] = 8.0f;
            s_State[(int)SkillType.Ice] = SkillState.Cooldown;
            StartCoroutine(ChangeBall());
            Debug.Log("스킬 사용 종료");
        }
    }

    void UpdateBlackHoleFire()
    {
        using_time[(int)SkillType.BlackHole] -= Time.fixedDeltaTime;

        if (using_time[(int)SkillType.BlackHole] <= 0.0f)
        {
            cooldown_time[(int)SkillType.BlackHole] = 8.0f;
            s_State[(int)SkillType.BlackHole] = SkillState.Cooldown;   
            Debug.Log("스킬 사용 종료");
        }
    }

    IEnumerator ChangeBall()
    {
        skillEffect[0].Play();
        yield return new WaitForSeconds(0.8f);
        render.material = skillMaterials[2];
    }

    void UpdateCooldown(int skillType)
    {
        cooldown_time[skillType] -= Time.fixedDeltaTime;

        if (cooldown_time[skillType] <= 0.0f)
        {
            StartCoroutine(Reload(skillType));
            s_State[skillType] = SkillState.Reloading;
            Debug.Log("스킬 쿨타임 종료");
        }
    }

    IEnumerator Reload(int skillType)
    {
        if ((int)s_Type != skillType)
        {
            yield return new WaitForSeconds(1.7f);
            s_State[skillType] = SkillState.Idle;
            yield break;
        }

        Debug.Log("스킬 재장전 중");
        skillEffect[1].Play();
        yield return new WaitForSeconds(1.7f);
        render.material = skillMaterials[skillType];
        s_State[skillType] = SkillState.Idle;
    }
}