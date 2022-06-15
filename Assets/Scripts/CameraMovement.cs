using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    
    void FixedUpdate()
    {
        Vector3 position = this.target.position;
        position.z = this.transform.position.z;
        this.transform.position = position;
    }
}
