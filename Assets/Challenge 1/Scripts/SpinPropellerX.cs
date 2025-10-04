using UnityEngine;

public class SpinPropellerX : MonoBehaviour
{
    public float spinSpeed = 1000f;  // You can adjust this in Inspector

    void Update()
    {
        // Rotate around the local Z axis (forward)
        transform.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);
    }
}
