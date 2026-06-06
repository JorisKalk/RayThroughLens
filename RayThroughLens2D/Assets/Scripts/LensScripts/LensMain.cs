using UnityEngine;

public class LensMain : MonoBehaviour
{
    [Header("Refractive indices")]
    [SerializeField]
    private float lensIndex = 1.5f;

    [Header("Radius of lens (min 3)")]
    [SerializeField]
    private float radius = 5f;

    [Header("Position variable")]
    [SerializeField]
    private float positionOffset;

    enum LensType
    {
        Converging,
        Diverging
    };

    [Header("Converging or Diverging")]
    [SerializeField]
    private LensType lensType = new LensType();


    void Start()
    {

    }

    void Update()
    {
        if (radius < 3f) radius = 3f;


        //temp
        transform.localScale = Vector2.one * radius * 2;
        if (lensType == LensType.Converging)
        {
            if (positionOffset < 0f)
            {
                transform.position = new Vector2(positionOffset + radius, 0);
            }
            else
            {
                transform.position = new Vector2(positionOffset - radius, 0);
            }
        }
        else
        {
            if (positionOffset < 0f)
            {
                transform.position = new Vector2(positionOffset - radius, 0);
            }
            else
            {
                transform.position = new Vector2(positionOffset + radius, 0);
            }
        }
    }

    public float CalculateAngle(Vector2 directionOne, Vector2 directionTwo)
    {
        float temp = 0f;
        return temp;
    }

    /// <summary>
    /// x value is the air index and y value is the lens index.
    /// </summary>
    /// <returns></returns>
    public float ReturnRefractiveIndices()
    {
        return lensIndex;
    }

    public bool IsConverging()
    {
        if (lensType == LensType.Converging)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
