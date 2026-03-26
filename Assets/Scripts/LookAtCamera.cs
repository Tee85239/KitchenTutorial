using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Mode mode;

    
    private enum Mode
    {
        LookAt,
        LookAtInverted,
        CameraForeward,
        CameraForewardInverted
            
    }


    private void LateUpdate()
    {

        switch (mode) {
            case Mode.LookAt:
        transform.LookAt(Camera.main.transform);
                break;
                case Mode.LookAtInverted:
                Vector3 dirFromCam = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCam);
                break;
                case Mode.CameraForeward:
                transform.forward = Camera.main.transform.forward;
                break;
                case Mode.CameraForewardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
    }
    }
}
