using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Portal : MonoBehaviour
{
    [SerializeField] List<AudioSource> portalSound;
    public string SceneName;

    private void Awake()
    {
        portalSound.AddRange(gameObject.GetComponentsInChildren<AudioSource>());
    }

    public void MoveStageScene()
    {
        portalSound[0].Play();
        portalSound[1].Play();
        SceneLoader.Instance.LoadScene(SceneName);
    }
}
