using UnityEngine;


//hardcoded
//make sure that lens 1 is always handled first and lens 2 always second
public class RayMain : MonoBehaviour
{
    private LineRenderer ray;

    

    [SerializeField]
    private float maxRayLength = 20.0f;

    [Header("RefractiveIndices")]
    [SerializeField]
    private float airIndex = 1f;

    [Header("RayVectors")]
    [SerializeField]
    private Vector2 rayStartPos = new Vector2(-3, 0);
    [SerializeField]
    private Vector2 rayStartDir = new Vector2(1, 0);

    [Header("LensReferences")]
    [SerializeField]
    private LensMain lensOne;
    [SerializeField]
    private LensMain lensTwo;

    [Header("LensLayers")]
    [SerializeField]
    private LayerMask lensLayerOne;
    [SerializeField]
    private LayerMask lensLayerTwo;


    //Calculation variables
    private Vector2 currentPos;
    private Vector2 currentDir;
    private int currentRayPositions;
    private float rayLengthLeft;


    void Start()
    {
        ray = GetComponent<LineRenderer>();
        
        SetupLineRenderer();
    }

    private void SetupLineRenderer()
    {
        ray.positionCount = 2;
        ray.startColor = Color.red;
        ray.endColor = Color.red;
        ray.startWidth = 0.2f;
        ray.endWidth = 0.2f;
        ray.SetPosition(0, rayStartPos);
        ray.SetPosition(1, rayStartPos + (rayStartDir * maxRayLength));
    }

    void Update()
    {
        rayStartDir.Normalize();
        currentPos = rayStartPos;
        currentDir = rayStartDir;
        rayLengthLeft = maxRayLength;
        SetupLineRenderer();


        SimulateRayToLensOne();
    }

    private void SimulateRayToLensOne()
    {
        currentDir.Normalize();

        RaycastHit2D hit;

        if (lensOne.IsConverging())
        {
            hit = RayTargetPos(lensLayerOne);
        }
        else
        {
            hit = InvertedRayTargetPos(lensLayerOne);
        }


        if (hit.collider == null)
        {
            ray.SetPosition(ray.positionCount - 1, currentPos + (currentDir * rayLengthLeft));
        }
        else
        {
            ray.SetPosition(ray.positionCount - 1, hit.point);

            float lensRefIndex = lensOne.ReturnRefractiveIndices();


            if (lensOne.IsConverging() )
            {
                currentDir = CalculateInsertAngle(currentDir, hit.normal, airIndex, lensRefIndex);
            }
            else
            {
                currentDir = CalculateExitAngle(currentDir, hit.normal, airIndex, lensRefIndex);
            }

                rayLengthLeft -= Vector2.Distance(currentPos, hit.point);
            currentPos = hit.point;
            ray.positionCount++;
            //ray.SetPosition(ray.positionCount - 1, currentPos + (currentDir * rayLengthLeft));
            SimulateRayToLensTwo();
        }
    }

    private void SimulateRayToLensTwo()
    {
        

        currentDir.Normalize();

        RaycastHit2D hit;

        if (lensTwo.IsConverging())
        {
            hit = InvertedRayTargetPos(lensLayerTwo);
        }
        else
        {
            hit = RayTargetPos(lensLayerTwo);
        }


        if (hit.collider == null)
        {
            ray.SetPosition(ray.positionCount - 1, currentPos + (currentDir * rayLengthLeft));
        }
        else
        {
            ray.SetPosition(ray.positionCount - 1, hit.point);

            float lensRefIndex = lensTwo.ReturnRefractiveIndices();

            if (lensTwo.IsConverging() )
            {
                currentDir = CalculateExitAngle(currentDir, hit.normal, airIndex, lensRefIndex);
            }
            else
            {
                currentDir = CalculateInsertAngle(currentDir, hit.normal, airIndex, lensRefIndex);
            }


                rayLengthLeft -= Vector2.Distance(currentPos, hit.point);
            currentPos = hit.point;
            ray.positionCount++;
            ray.SetPosition(ray.positionCount - 1, currentPos + (currentDir * rayLengthLeft));
        }
    }

    private RaycastHit2D RayTargetPos(LayerMask targetLens)
    {
        return Physics2D.Raycast(currentPos, currentDir, rayLengthLeft, targetLens);
    }

    private RaycastHit2D InvertedRayTargetPos(LayerMask targetLens)
    {
        Vector2 invertedPos = currentPos + (currentDir * rayLengthLeft);
        return Physics2D.Raycast(invertedPos, -currentDir, rayLengthLeft, targetLens);
    }

    private Vector2 CalculateInsertAngle(Vector2 dir, Vector2 impactNormal,
        float airRefIndex, float lensRefIndex)
    {
        float impactAngle = Mathf.Acos(Vector2.Dot(dir, impactNormal));
        float newAngle = Mathf.Asin((airRefIndex * Mathf.Sin(impactAngle)) / lensRefIndex);
        return (new Vector2(Mathf.Cos(newAngle), Mathf.Sin(newAngle))) - impactNormal;
    }

    private Vector2 CalculateExitAngle(Vector2 dir, Vector2 impactNormal,
        float airRefIndex, float lensRefIndex)
    {
        float impactAngle = Mathf.Acos(Vector2.Dot(dir, impactNormal));
        float newAngle = Mathf.Asin((lensRefIndex * Mathf.Sin(impactAngle)) / airRefIndex);
        
        if (rayStartDir.y > 0)
        {
            return (new Vector2(Mathf.Cos(newAngle), Mathf.Sin(-newAngle))) + impactNormal;
        }


        return (new Vector2(Mathf.Cos(newAngle), Mathf.Sin(newAngle))) + impactNormal;
    }
}
