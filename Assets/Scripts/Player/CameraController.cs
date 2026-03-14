using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    CinemachineCamera cm;

    [SerializeField] float minFov = 40f;
    [SerializeField] float maxFov = 80f;

    [SerializeField] float zoomSpeedMod = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        cm = GetComponent<CinemachineCamera>();
    }
    public void changeCameraFov(float speed, float time)
    {
        StartCoroutine(changeFOVRoutine(speed,time));
    }

    IEnumerator changeFOVRoutine(float speed, float zoomDuration)
    {
        float startFOV = cm.Lens.FieldOfView;
        float targetFov = Mathf.Clamp(startFOV+(speed*zoomSpeedMod),minFov,maxFov);
        float percentage;

        float elapsedTime = 0f;
        while (elapsedTime < zoomDuration)
        {
          percentage = elapsedTime/ zoomDuration;
          elapsedTime += Time.deltaTime;
          cm.Lens.FieldOfView = Mathf.Lerp(startFOV, targetFov, percentage); // go from start to target of an increasing percentage of time
          yield return null;
        }
    cm.Lens.FieldOfView = targetFov; // check line
    }
}
