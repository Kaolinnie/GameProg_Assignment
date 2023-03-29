using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float Sensitivity {
        get { return sensitivity; }
        set { sensitivity = value; }
    }
    
    float sensitivity = 2f;

    Vector2 rotation = new Vector2(0,0);
    const string xAxis = "Mouse X"; //Strings in direct code generate garbage, storing and re-using them creates no garbage

    void Update(){
        rotation.x += Input.GetAxis(xAxis) * sensitivity;
        transform.localRotation =Quaternion.AngleAxis(rotation.x, Vector3.up);
    }
}