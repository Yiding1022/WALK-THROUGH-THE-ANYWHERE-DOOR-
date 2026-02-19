using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneASwitcher : MonoBehaviour
{
    public string sceneBName = "SceneB";
    public string triggerID; // 每个触发器的唯一标识

    void Start()
    {
        if (PlayerPrefs.GetInt($"TriggerUsed_{triggerID}", 0) == 1)
        {
            GetComponent<Collider>().enabled = false;

            GetComponent<Renderer>().material.color = Color.gray;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PlayerPrefs.GetInt($"TriggerUsed_{triggerID}", 0) == 0)
        {
            PlayerPrefs.SetFloat("LastTriggerPosX", transform.position.x);
            PlayerPrefs.SetFloat("LastTriggerPosY", transform.position.y);
            PlayerPrefs.SetFloat("LastTriggerPosZ", transform.position.z);
            PlayerPrefs.SetString("LastTriggerID", triggerID);
            PlayerPrefs.SetInt($"TriggerUsed_{triggerID}", 1);

            PlayerPrefs.Save();

            SceneManager.LoadScene(sceneBName);
        }
    }

    public static void ResetAllTriggers()
    {
        PlayerPrefs.DeleteKey("LastTriggerPosX");
        PlayerPrefs.DeleteKey("LastTriggerPosY");
        PlayerPrefs.DeleteKey("LastTriggerPosZ");
        PlayerPrefs.DeleteKey("LastTriggerID");

        for (int i = 1; i <= 10; i++) // 假设最多10个触发器
        {
            PlayerPrefs.DeleteKey($"TriggerUsed_Trigger{i}");
        }

        PlayerPrefs.Save();
    }
}