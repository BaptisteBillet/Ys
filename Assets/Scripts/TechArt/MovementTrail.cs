using UnityEngine;
using System.Collections;

[RequireComponent(typeof (LineRenderer))]
public class MovementTrail : MonoBehaviour {
    
    //Helpers
    private LineRenderer m_Line;
    private Transform m_Transform;
    private Material m_LineMaterial;



    private Vector3[] m_Positions;
    private Vector3[] m_directions;
    private float m_TimeSinceUpdate = 0f;
    private float m_LineSegment= 0f;
    private int m_CurrentNumberOfPoints = 2;
    private bool m_AllPointsAdded = false;

    [SerializeField]
    private int m_NumberOfPoints = 10;
    [SerializeField]
    private float m_UpdateSpeed = 0.25f;
    [SerializeField]
    private float m_RiseSpeed = 0.25f;
    [SerializeField]
    private float m_Spread = 0.2f;

    private Vector3 m_TempVec3;
  
    // Use this for initialization
    void Start () {

        //Helper initialization
        m_Transform = this.GetComponent<Transform>();
        m_Line = this.GetComponent<LineRenderer>();
        m_LineMaterial = m_Line.material;

        m_LineSegment = 1 / m_NumberOfPoints;

        m_Positions = new Vector3[m_NumberOfPoints];
        m_directions = new Vector3[m_NumberOfPoints];

        m_Line.SetVertexCount(m_CurrentNumberOfPoints);

        for( int i = 0; i < m_CurrentNumberOfPoints; i++)
        {
            m_TempVec3 = GetSmokeVec();
            m_directions[i] = m_TempVec3;
            m_Positions[i] = m_Transform.position;
            m_Line.SetPosition(i, m_Positions[i]);

        }
    }

    private Vector3 GetSmokeVec()
    {
        Vector3 smokeVec;
        smokeVec = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f),0 );
        smokeVec.Normalize();
        smokeVec *= m_Spread;

        return smokeVec;
    }




    // Update is called once per frame
    void Update () {
        m_TimeSinceUpdate += Time.deltaTime;

        if(m_TimeSinceUpdate < m_UpdateSpeed)
        {
            m_TimeSinceUpdate -= m_UpdateSpeed;

            UpdateTrailPoints();
        }

        DrawTrail();
    }

    void UpdateTrailPoints()
    {
        // Add points until the target number is reached.
        if (!m_AllPointsAdded)
        {
            m_CurrentNumberOfPoints++;
            m_Line.SetVertexCount(m_CurrentNumberOfPoints);
            m_TempVec3 = GetSmokeVec();
            m_directions[0] = m_TempVec3;
            m_Positions[0] = m_Transform.position;
            m_Line.SetPosition(0, m_Positions[0]);
        }

        if (!m_AllPointsAdded && (m_CurrentNumberOfPoints == m_NumberOfPoints))
        {
            m_AllPointsAdded = true;
        }

        // Make each point in the line take the position and direction of the one before it (effectively removing the last point from the line and adding a new one at transform position).
        for (int i = m_CurrentNumberOfPoints - 1; i > 0; i--)
        {
            m_TempVec3 = m_Positions[i - 1];
            m_Positions[i] = m_TempVec3;
            m_TempVec3 = m_directions[i - 1];
            m_directions[i] = m_TempVec3;
        }

        m_TempVec3 = GetSmokeVec();
        m_directions[0] = m_TempVec3; // Remember and give 0th point a direction for when it gets pulled up the chain in the next line update.
    }

    void DrawTrail()
    {
        for( int i = 1; i<m_CurrentNumberOfPoints; i++)
        {
            m_TempVec3 = m_Positions[i];
            m_TempVec3 += m_directions[i] * Time.deltaTime;
            m_Positions[i] = m_TempVec3;

            m_Line.SetPosition(i, m_Positions[i]);
        }

        m_Positions[0] = m_Transform.position; // 0 point always follows the transform directly
        m_Line.SetPosition(0, m_Positions[0]);


        // If we're at the maximum number of points, tweak the offset so that the last line segment is "invisible" (i.e. off the top of the texture) when it disappears.
        // Makes the change less jarring and ensures the texture doesn't jump.
        if (m_AllPointsAdded)
        {
            m_LineMaterial.mainTextureOffset = new Vector2(m_LineSegment * (m_TimeSinceUpdate / m_UpdateSpeed) , 0);
        }
    }
}
