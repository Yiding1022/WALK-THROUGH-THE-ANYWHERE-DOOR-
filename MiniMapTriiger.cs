using UnityEngine;

public class TriggerExitActivator : MonoBehaviour
{
    public GameObject actorA; // 引用Actor A

    void Start()
    {
        if (actorA != null)
        {
            actorA.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (actorA != null && !actorA.activeSelf)
            {
                actorA.SetActive(true);
                Debug.Log("角色离开触发器，Actor A 已激活!");
            }
        }
    }
}
