using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class FollowCamSprite : MonoBehaviour
{

    [SerializeField]
    private bool remainUpright = true;

    // use camera forward vector (all sprites will use same vector), otherwise face directly at camera (all sprites slightly different depending on position)
    [SerializeField]
    private bool useCameraVector = false;

    [SerializeField]
    private bool followEditorCamera = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        Camera cam;
        if (followEditorCamera && !Application.isPlaying && UnityEditor.SceneView.lastActiveSceneView.camera)
            cam = UnityEditor.SceneView.lastActiveSceneView.camera;
        else
            cam = Camera.main;
#else
        Camera cam = Camera.main;
#endif

        Vector3 direction;
        if (useCameraVector)
            direction = Vector3.Scale(cam.transform.forward, new Vector3(-1, -1, -1)); // vector aligned with camera forward direction
        else
            direction = (cam.transform.position - transform.position).normalized; // vector pointing towards to camera

        // if we want to remain upright, then remove the verticle dimension of the vector
        if (remainUpright)
            direction.Scale(new Vector3(1, 0, 1));

        // create rotation to look in direction
        Quaternion rotation = Quaternion.LookRotation(direction);

        transform.rotation = rotation;
    }
}
