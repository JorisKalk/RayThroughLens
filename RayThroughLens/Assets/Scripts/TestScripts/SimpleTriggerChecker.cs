using UnityEngine;

public class SimpleTriggerChecker : MonoBehaviour
{
    public bool hasTouched = false;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("touch");
        if (!hasTouched) hasTouched = true;
        else if (hasTouched)
        {
            other.gameObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }
}
