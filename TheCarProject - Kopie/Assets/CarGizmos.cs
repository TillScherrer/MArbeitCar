using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGizmos : MonoBehaviour
{
    [SerializeField] Transform FL;
    [SerializeField] Transform FR;
    [SerializeField] Transform BL;
    [SerializeField] Transform BR;

    [SerializeField] Transform FL_Air;
    [SerializeField] Transform FR_Air;
    [SerializeField] Transform BL_Air;
    [SerializeField] Transform BR_Air;

    [SerializeField] Transform FL3D;
    [SerializeField] Transform FR3D;
    [SerializeField] Transform BL3D;
    [SerializeField] Transform BR3D;

    [SerializeField] Transform FL_UpCap;
    [SerializeField] Transform FR_UpCap;
    [SerializeField] Transform BL_UpCap;
    [SerializeField] Transform BR_UpCap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //public void VisualiseWheels(Vector3 fr, Vector3 br, float frontRadius, float backRadius, float frontOffHight, float backOffHight)
    public void VisualiseWheels(bool showFrontWheelSettings, Vector3 fr, float fOffHight, float fRadius, float fUpperCap,
                      bool fUse3DCollision, float fInward, float fOutward,
                                bool showRearWheelSettings, Vector3 br, float bOffHight, float bRadius, float bUpperCap,
                      bool bUse3DCollision, float bInward, float bOutward)
    {
        Vector3 fl = fr + 2 * fr.x * Vector3.left;
        Vector3 bl = br + 2 * br.x * Vector3.left;

        if (showFrontWheelSettings)
        {
            FL_Air.gameObject.SetActive(true);
            FR_Air.gameObject.SetActive(true);
            FL_UpCap.gameObject.SetActive(true);
            FR_UpCap.gameObject.SetActive(true);
            FL_Air.localPosition = fl + new Vector3(0, -fOffHight, 0);
            FR_Air.localPosition = fr + new Vector3(0, -fOffHight, 0);
            FL_Air.localScale = fRadius * Vector3.one;
            FR_Air.localScale = fRadius * Vector3.one;
            FL_UpCap.localPosition = fl + new Vector3(0, fRadius + fUpperCap, 0);
            FR_UpCap.localPosition = fr + new Vector3(0, fRadius + fUpperCap, 0);
            FL_UpCap.localScale = fRadius * Vector3.one;
            FR_UpCap.localScale = fRadius * Vector3.one;

            if (fUse3DCollision)
            {
                FL.gameObject.SetActive(false);
                FR.gameObject.SetActive(false);
                FL3D.gameObject.SetActive(true);
                FR3D.gameObject.SetActive(true);
                FL3D.localPosition = fl + Vector3.left * (fOutward - fInward) / 2;
                FR3D.localPosition = fr + Vector3.right * (fOutward - fInward) / 2;
                FL3D.localScale = new Vector3(fRadius * 2, (fInward + fOutward) / 2, fRadius * 2);
                FR3D.localScale = new Vector3(fRadius * 2, (fInward + fOutward) / 2, fRadius * 2);

            }
            else
            {
                FL3D.gameObject.SetActive(false);
                FR3D.gameObject.SetActive(false);
                FL.gameObject.SetActive(true);
                FR.gameObject.SetActive(true);
                FL.localPosition = fl;
                FR.localPosition = fr;
                FL.localScale = fRadius * Vector3.one;
                FR.localScale = fRadius * Vector3.one;
            }
        }
        else
        {
            FL.gameObject.SetActive(false);
            FR.gameObject.SetActive(false);
            FL3D.gameObject.SetActive(false);
            FR3D.gameObject.SetActive(false);
            FL_Air.gameObject.SetActive(false);
            FR_Air.gameObject.SetActive(false);
            FL_UpCap.gameObject.SetActive(false);
            FR_UpCap.gameObject.SetActive(false);
        }


        if (showRearWheelSettings)
        {
            BL_Air.gameObject.SetActive(true);
            BR_Air.gameObject.SetActive(true);
            BL_UpCap.gameObject.SetActive(true);
            BR_UpCap.gameObject.SetActive(true);
            BL_Air.localPosition = bl + new Vector3(0, -bOffHight, 0);
            BR_Air.localPosition = br + new Vector3(0, -bOffHight, 0);
            BL_Air.localScale = bRadius * Vector3.one;
            BR_Air.localScale = bRadius * Vector3.one;
            BL_UpCap.localPosition = bl + new Vector3(0, bRadius + bUpperCap, 0);
            BR_UpCap.localPosition = br + new Vector3(0, bRadius + bUpperCap, 0);
            BL_UpCap.localScale = bRadius * Vector3.one;
            BR_UpCap.localScale = bRadius * Vector3.one;

            if (bUse3DCollision)
            {
                BL.gameObject.SetActive(false);
                BR.gameObject.SetActive(false);
                BL3D.gameObject.SetActive(true);
                BR3D.gameObject.SetActive(true);
                BL3D.localPosition = bl + Vector3.left * (bOutward - bInward) / 2;
                BR3D.localPosition = br + Vector3.right * (bOutward - bInward) / 2;
                BL3D.localScale = new Vector3(bRadius * 2, (bInward + bOutward) / 2, bRadius * 2);
                BR3D.localScale = new Vector3(bRadius * 2, (bInward + bOutward) / 2, bRadius * 2);

            }
            else
            {
                BL3D.gameObject.SetActive(false);
                BR3D.gameObject.SetActive(false);
                BL.gameObject.SetActive(true);
                BR.gameObject.SetActive(true);
                BL.localPosition = bl;
                BR.localPosition = br;
                BL.localScale = bRadius * Vector3.one;
                BR.localScale = bRadius * Vector3.one;
            }
        }
        else
        {
            BL.gameObject.SetActive(false);
            BR.gameObject.SetActive(false);
            BL3D.gameObject.SetActive(false);
            BR3D.gameObject.SetActive(false);
            BL_Air.gameObject.SetActive(false);
            BR_Air.gameObject.SetActive(false);
            BL_UpCap.gameObject.SetActive(false);
            BR_UpCap.gameObject.SetActive(false);
        }

        

        
        
    }
}
