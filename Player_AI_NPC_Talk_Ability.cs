using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AI_NPC_Talk_Ability : MonoBehaviour
{
    public GameObject TalkFrame;
    private void OnTriggerEnter(Collider other)
    {
        TalkFrame.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        TalkFrame.SetActive(false);
    }
}
