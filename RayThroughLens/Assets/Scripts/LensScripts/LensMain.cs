using UnityEngine;

public class LensMain : MonoBehaviour
{
    [Header("Refractive indices")]
    [SerializeField]
    private float airIndex = 0f;
    [SerializeField]
    private float lensIndex = 1.5f;

    [Header("Radiuses of lens sides (min 3)")]
    [SerializeField]
    private float leftSideRadius = 5f;
    [SerializeField]
    private float rightSideRadius = 5f;

    enum LensType
    {
        Converging,
        Diverging
    };

    [Header("Converging or Diverging")]
    [SerializeField]
    private LensType leftLensType = new LensType();
    [SerializeField]
    private LensType rightLensType = new LensType();
    

    void Start()
    {
        
    }

    void Update()
    {
        if (leftSideRadius < 3f) leftSideRadius = 3f;
        if (rightSideRadius < 3f) rightSideRadius = 3f;
    }

    public float LeftSideImpactAngle(Vector3 impactPoint, Vector3 impactDirection)
    {
        Vector3 lensSphereCenter = Vector3.zero;
        if (leftLensType == LensType.Converging)
        {

        }

        float temp = 0f;
        return temp;
    }

    public float RightSideImpactAngle(Vector3 impactPoint, Vector3 impactDirection)
    {


        float temp = 0f;
        return temp;
    }

    public float CalculateAngle(Vector3 directionOne, Vector3 directionTwo)
    {
        float temp = 0f;
        return temp;
    }
}
