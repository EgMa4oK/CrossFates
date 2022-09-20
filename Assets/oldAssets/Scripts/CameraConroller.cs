using UnityEngine;

public class CameraConroller : MonoBehaviour
{
    [SerializeField] private Transform target;


    private void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    
    



}
