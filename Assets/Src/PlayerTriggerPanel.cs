using UnityEngine;

public class PlayerTriggerPanel : MonoBehaviour
{
    [Header("Panel Loading cần bật")]
    public GameObject loadingPanel;   // Gán panel loading có sẵn
    public string targetTag = "A";    // Tag mà player cần chạm

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return; // tránh bật nhiều lần
        if (other.CompareTag(targetTag))
        {
            hasTriggered = true;
            if (loadingPanel != null)
            {
                loadingPanel.SetActive(true);
                Debug.Log("Panel Loading đã được bật!");
            }
        }
    }
}
