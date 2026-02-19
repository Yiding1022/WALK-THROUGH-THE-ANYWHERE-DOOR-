using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneBSwitcher : MonoBehaviour
{
    public string sceneAName = "SceneA";
    public float countdownDuration = 20f;
    public TextMeshProUGUI countdownText;

    private float currentTime;

    void Start()
    {
        currentTime = countdownDuration;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;

        countdownText.text = $"∑µªÿµπº∆ ±: {Mathf.Ceil(currentTime)}√Î";

        if (currentTime <= 0f)
        {
            SceneManager.LoadScene(sceneAName);
        }
    }
}