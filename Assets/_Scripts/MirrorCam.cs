using UnityEngine;

public class MirrorCam : MonoBehaviour
{
    Camera rearView;
    void Start()
    {
        rearView = GetComponent<Camera>();

        Matrix4x4 mat = rearView.projectionMatrix;
        mat *= Matrix4x4.Scale(new Vector3(-1, 1, 1));
        rearView.projectionMatrix = mat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
