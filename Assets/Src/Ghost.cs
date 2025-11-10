using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private float left;
    [SerializeField] private float right;
    [SerializeField] private float speed = 10f;
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
            scale.x = -8;
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            scale.x = 8;
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        transform.localScale = scale;
    }
}
