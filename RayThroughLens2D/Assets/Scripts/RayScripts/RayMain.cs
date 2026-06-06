using UnityEngine;


//hardcoded
//make sure that lens 1 is always handled first and lens 2 always second
public class RayMain : MonoBehaviour
{
    private LineRenderer ray;

    [SerializeField]
    private LayerMask lensLayer;

    [SerializeField]
    private float maxRayLength = 20.0f;

    [Header("LensVectors")]
    [SerializeField]
    private Vector2 rayStartPos = new Vector2(-3, 0);
    [SerializeField]
    private Vector2 rayStartDir = new Vector2(1, 0);


    //Calculation variables
    private Vector2 currentPos;
    private Vector2 currentDir;
    private float rayLengthLeft;


    void Start()
    {
        ray = GetComponent<LineRenderer>();
        rayStartDir.Normalize();
        SetupLineRenderer();
    }

    private void SetupLineRenderer()
    {
        ray.positionCount = 2;
        ray.startColor = Color.red;
        ray.endColor = Color.red;
        ray.startWidth = 0.1f;
        ray.endWidth = 0.1f;
        ray.SetPosition(0, rayStartPos);
        ray.SetPosition(1, rayStartPos + (rayStartDir * 10));
    }

    void Update()
    {
        currentPos = rayStartPos;
        currentDir = rayStartDir;
        rayLengthLeft = maxRayLength;

        SimulateRay();
    }

    private void SimulateRay()
    {
        currentDir.Normalize();
        RaycastHit2D hit = RayTargetPos();
        if (hit.collider == null)
        {
            ray.SetPosition(1, currentPos + (currentDir * 10));
        }
        else
        {
            ray.SetPosition(1, hit.point);
        }
    }

    private RaycastHit2D RayTargetPos()
    {
        return Physics2D.Raycast(rayStartPos, rayStartDir, rayLengthLeft, lensLayer);
    }

    private Vector2 CalculateNewAngle(Vector2 dir, Vector2 impactNormal, float airRefIndex, float lensRefIndex)
    {


        return Vector2.zero;
    }
}
