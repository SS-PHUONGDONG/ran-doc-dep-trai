using UnityEngine;

public class LifeTimeDestroy : MonoBehaviour
{
    [SerializeField] private float Time;
    
    private void Start() {
        Destroy(this.gameObject, Time);
    }
}
