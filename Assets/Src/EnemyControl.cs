using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] private float left;
    [SerializeField] private float right;
    [SerializeField] private float speed;
    private bool isRight = true;
    void Start()
    {

    }
    void Update()
    {
        var x = transform.position.x;
        var scale = transform.localScale;

        if(x <= left) {isRight = true;}
        if(x >= right) {isRight = false;}
        if(isRight)
        {
            scale.x = 1;
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            scale.x = -1;
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        transform.localScale = scale;
    }
}
