using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
//using System.Drawing;
using UnityEngine;
using UnityEngine.VFX;

public class TestPointColStop : MonoBehaviour
{

    public Rigidbody rb;

    Vector3 lastColPoint = Vector3.zero;
    Vector3 localSpaceLastColPoint = Vector3.zero;
    int hadTriggerPrevFrame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (hadTriggerPrevFrame == 2)
        {
            hadTriggerPrevFrame = 1;
        }
        else if (hadTriggerPrevFrame == 1)
        {
            hadTriggerPrevFrame = 0;
            Debug.Log("Vel at Point after Col = " + rb.GetPointVelocity(lastColPoint));
        }
        Debug.DrawRay(transform.TransformPoint(localSpaceLastColPoint), Vector3.up, Color.blue);
    }


    private void OnTriggerEnter(Collider other)
    {
        Vector3 colPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
        //Debug.Log("col, pointVelY= "+);
        

        if (rb.GetPointVelocity(colPoint).y < 0)
        {
            
            Vector3 angularChange;
            Vector3 directionalChange;
            float impulse;
            ImpulseNeededToStopDirectionalMovementAtPoint(colPoint,Vector3.up, out angularChange, out directionalChange, out impulse);
            //Debug.Log("Added Force " + powerNeeded+ " at "+colPoint);

            //Debug.Log("angularVel before= "+rb.angularVelocity);
            //rb.angularVelocity += angularChange;
            //rb.velocity += directionalChange;
            //Debug.Log("angularChange= " + angularChange);
            //Debug.Log("directionalChange= " + directionalChange);
            //Debug.Log("angularVel after= " + rb.angularVelocity);
            rb.AddForceAtPosition(Vector3.up * impulse, colPoint, ForceMode.Impulse);

            hadTriggerPrevFrame = 2;
            lastColPoint = colPoint;
            localSpaceLastColPoint = transform.InverseTransformPoint(lastColPoint);
            Debug.DrawRay(colPoint, Vector3.up, Color.red, 10000);
        }
    }



    private void ImpulseNeededToStopDirectionalMovementAtPoint(Vector3 Point, Vector3 ImpulseDirToCancleCurrent, out Vector3 angularChange, out Vector3 directionalChange, out float neededImpulse)
    {
        
        //part of the currentSpeed going in opposide direction as the counteracting impulse 
        float neededV = Vector3.Dot(rb.GetPointVelocity(Point), -ImpulseDirToCancleCurrent.normalized);
        Debug.Log("neededV= " + neededV + "  actualV.y= " + rb.GetPointVelocity(Point).y);

        //Debug.Log("Point vel:" + rb.GetPointVelocity(Point));
        //Debug.Log("vel to cancle out: " + neededV);

        ////in local Space, because Unitys inertiaTensor is in local space
        //Vector3 localPoint = transform.InverseTransformPoint(Point);
        //localPoint = new Vector3(localPoint.x * transform.localScale.x, localPoint.y * transform.localScale.y, localPoint.z * transform.localScale.z);
        //Vector3 localDir = transform.InverseTransformDirection(ImpulseDirToCancleCurrent.normalized);

        //Vector3 speedPerAngularVelocity = Vector3.Cross(localPoint, localDir); //how much one unit roation around each axis would move the point in direction of force
        //Vector3 angularDirections = new Vector3(speedPerAngularVelocity.x > 0 ? 1 : speedPerAngularVelocity.x < 0 ? -1 : 0,
        //                                        speedPerAngularVelocity.y > 0 ? 1 : speedPerAngularVelocity.y < 0 ? -1 : 0,
        //                                        speedPerAngularVelocity.z > 0 ? 1 : speedPerAngularVelocity.z < 0 ? -1 : 0);
        //speedPerAngularVelocity = new Vector3(speedPerAngularVelocity.x == 0 ? 0.00000000000000000000000001f : Mathf.Abs(speedPerAngularVelocity.x), //prevent Division by Zeros for an orthogonal rotation axis
        //                                      speedPerAngularVelocity.y == 0 ? 0.00000000000000000000000001f : Mathf.Abs(speedPerAngularVelocity.y),
        //                                      speedPerAngularVelocity.z == 0 ? 0.00000000000000000000000001f : Mathf.Abs(speedPerAngularVelocity.z));
        ////Debug.Log("speedPerAngularVelocity = " + speedPerAngularVelocity);
        //if (rb.inertiaTensor.x == 0 || rb.inertiaTensor.y == 0 || rb.inertiaTensor.z == 0) Debug.LogError("It is not allowed to lock the rigidbodys rotation while using this script");


        //Vector3 s = speedPerAngularVelocity;//short name
        //Vector3 j = rb.inertiaTensor; //short name

        //float mass = rb.mass;
        //float multiplierAllSpeeds = 2 * j.x * j.y * j.z * mass * neededV / (s.x * s.x * j.y * j.z * mass + s.y * s.y * j.x * j.z * mass + s.z * s.z * j.x * j.y * mass + j.x * j.y * j.z);
        ////speed provided by each rotation
        //float vx = s.x * s.x / (2 * j.x) * multiplierAllSpeeds;
        //float vy = s.y * s.y / (2 * j.y) * multiplierAllSpeeds;
        //float vz = s.z * s.z / (2 * j.z) * multiplierAllSpeeds;
        //float vdir = 1 / (2 * mass) * multiplierAllSpeeds;
        ////angular Velocities
        //float wx = vx / s.x;
        //float wy = vy / s.y;
        //float wz = vz / s.z;
        //Debug.Log("vx= " + vx + "  vy= " + vy + "  vz= " + vz + "  vdir= " + vdir);
        //Debug.Log("wx= " + wx + "  wy= " + wy + "  wz= " + wz + "  vdir= "+vdir + "  allMultiplier= "+multiplierAllSpeeds+"  neededV= "+neededV);


        ////kinetic Energy of each speed
        //float kx = 0.5f * j.x * Mathf.Pow(wx, 2);
        //float ky = 0.5f * j.y * Mathf.Pow(wy, 2);
        //float kz = 0.5f * j.z * Mathf.Pow(wz, 2); //2
        //float kdir = 0.5f * mass * Mathf.Pow(vdir, 2); //2
        //float totalK = kx + ky + kz + kdir; //4  //2
        //float scaledK = Mathf.Sqrt(Mathf.Pow(kx,2) + Mathf.Pow(ky,2) + Mathf.Pow(kz,2) + Mathf.Pow(kdir,2));
        //float scaler = scaledK / totalK;
        //neededImpulse = (wx * j.x + wy * j.y + wz * j.z + vdir * mass) * scaler;
        //angularChange= transform.TransformDirection(new Vector3(wx * angularDirections.x, wy * angularDirections.y, wz * angularDirections.z));
        //directionalChange = ImpulseDirToCancleCurrent * vdir;



        //in local Space, because Unitys inertiaTensor is in local space
        Vector3 localPoint = Point-rb.worldCenterOfMass;
        //localPoint = new Vector3(localPoint.x * transform.localScale.x, localPoint.y * transform.localScale.y, localPoint.z * transform.localScale.z);
        Vector3 localDir = ImpulseDirToCancleCurrent.normalized;

        Vector3 speedPerAngularVelocity = Vector3.Cross(localPoint, localDir); //how much one unit roation around each axis would move the point in direction of force
        Vector3 angularDirections = new Vector3(speedPerAngularVelocity.x > 0 ? 1 : speedPerAngularVelocity.x < 0 ? -1 : 0,
                                                speedPerAngularVelocity.y > 0 ? 1 : speedPerAngularVelocity.y < 0 ? -1 : 0,
                                                speedPerAngularVelocity.z > 0 ? 1 : speedPerAngularVelocity.z < 0 ? -1 : 0);
        speedPerAngularVelocity = new Vector3(speedPerAngularVelocity.x == 0 ? 0.00000000000000000000000001f : Mathf.Abs(speedPerAngularVelocity.x), //prevent Division by Zeros for an orthogonal rotation axis
                                              speedPerAngularVelocity.y == 0 ? 0.00000000000000000000000001f : Mathf.Abs(speedPerAngularVelocity.y),
                                              speedPerAngularVelocity.z == 0 ? 0.00000000000000000000000001f : Mathf.Abs(speedPerAngularVelocity.z));
        //Debug.Log("speedPerAngularVelocity = " + speedPerAngularVelocity);
        if (rb.inertiaTensor.x == 0 || rb.inertiaTensor.y == 0 || rb.inertiaTensor.z == 0) Debug.LogError("It is not allowed to lock the rigidbodys rotation while using this script");


        Vector3 s = speedPerAngularVelocity;//short name
        Vector3 j = Quaternion.Inverse(rb.rotation)*(rb.inertiaTensor); //short name // WARNING: this might be wrong!!!
        j = new Vector3(Mathf.Abs(j.x), Mathf.Abs(j.y), Mathf.Abs(j.z));             // ""
        Debug.Log("j= " + j);

        float mass = rb.mass;
        float multiplierAllSpeeds = 2 * j.x * j.y * j.z * mass * neededV / (s.x * s.x * j.y * j.z * mass + s.y * s.y * j.x * j.z * mass + s.z * s.z * j.x * j.y * mass + j.x * j.y * j.z);
        //speed provided by each rotation
        float vx = s.x * s.x / (2 * j.x) * multiplierAllSpeeds;
        float vy = s.y * s.y / (2 * j.y) * multiplierAllSpeeds;
        float vz = s.z * s.z / (2 * j.z) * multiplierAllSpeeds;
        float vdir = 1 / (2 * mass) * multiplierAllSpeeds;
        //angular Velocities
        float wx = vx / s.x;
        float wy = vy / s.y;
        float wz = vz / s.z;
        Debug.Log("vx= " + vx + "  vy= " + vy + "  vz= " + vz + "  vdir= " + vdir);
        Debug.Log("wx= " + wx + "  wy= " + wy + "  wz= " + wz + "  vdir= " + vdir + "  allMultiplier= " + multiplierAllSpeeds + "  neededV= " + neededV);


        //kinetic Energy of each speed
        float kx = 0.5f * j.x * Mathf.Pow(wx, 2);
        float ky = 0.5f * j.y * Mathf.Pow(wy, 2);
        float kz = 0.5f * j.z * Mathf.Pow(wz, 2); //2
        float kdir = 0.5f * mass * Mathf.Pow(vdir, 2); //2
        float totalK = kx + ky + kz + kdir; //4  //2
        float scaledK = Mathf.Sqrt(Mathf.Pow(kx, 2) + Mathf.Pow(ky, 2) + Mathf.Pow(kz, 2) + Mathf.Pow(kdir, 2));
        float scaler = scaledK / totalK;
        neededImpulse = (wx * j.x + wy * j.y + wz * j.z + vdir * mass) * scaler;
        angularChange = new Vector3(wx * angularDirections.x, wy * angularDirections.y, wz * angularDirections.z);
        directionalChange = ImpulseDirToCancleCurrent * vdir;









    }
    //Vector3 speedPerAngularVelocity = new Vector3(localDir.z * localPoint.y - localDir.y * localPoint.z,
    //localDir.x * localPoint.z - localDir.z * localPoint.x,
    // localDir.y * localPoint.x - localDir.x * localPoint.y);


    //Vector3 costPerSpeedForEachAxis = new Vector3(rb.inertiaTensor.x / speedPerAngularVelocity.x, rb.inertiaTensor.y / speedPerAngularVelocity.y, rb.inertiaTensor.z / speedPerAngularVelocity.z);
    //costPerSpeedForEachAxis = new Vector3(Mathf.Abs(costPerSpeedForEachAxis.x), Mathf.Abs(costPerSpeedForEachAxis.y), Mathf.Abs(costPerSpeedForEachAxis.z)); //The impulse needed 
    //                                                                                                                                                         //in that manner mass is "costPerSpeedForDirectionalImpulse"
    //Vector3 m = costPerSpeedForEachAxis; //short name

    //Debug.Log("vx: " + vx + ", vy:" + vy + ", vz:" + vz + ", vdir:" + vdir);
    //Debug.Log("mx: " + m.x + ", my:" + m.y + ", mz:" + m.z + ", mdir:" + mass);
    //Debug.Log("ix: " + vx*m.x + ", iy:" + vy*m.y + ", iz:" + vz*m.z + ", idir:" + vdir*mass);
    //float neededImpulse = (vx * m.x + vy * m.y + vz * m.z + vdir * mass);// * 0.6f;
}
