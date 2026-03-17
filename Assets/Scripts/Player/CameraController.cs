using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    CinemachineCamera cm;
    [SerializeField] ParticleSystem particleSystem; 

    [Header("FOV Settings")]
    [SerializeField] float minFov = 40f;
    [SerializeField] float maxFov = 80f;

    [SerializeField] float zoomSpeedMod = 5;
    [SerializeField] float zoomDuration = 1f;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        cm = GetComponent<CinemachineCamera>();
    }
    public void changeCameraFov(float speed)
    {

        StopAllCoroutines();
        StartCoroutine(changeFOVRoutine(speed));

        if (speed >= 1 && particleSystem.isStopped )
        {
            particleSystem.Play();
        }
        else if(particleSystem.isPlaying && speed < 1)
        {
            particleSystem.Stop();
        }
    }

    IEnumerator changeFOVRoutine(float speed)
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
