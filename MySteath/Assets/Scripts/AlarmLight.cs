using UnityEngine;
using System.Collections;

public class AlarmLight : MonoBehaviour {

    public float fadeSpeed = 2.0f;  // 渐变速度
    public float highInstensity = 4.0f; // 最大亮度值
    public float lowInstensity = 0.5f; // 最小亮度值
    public float changeMargin = 0.2f;
    public bool alarmOn;
    private float targetIntensity;
    private Light alarmLight;

    void Awake()
    {
        alarmLight = GetComponent<Light>();
        alarmLight.intensity = 0.0f;
        targetIntensity = highInstensity;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(alarmOn)
        {
            alarmLight.intensity = Mathf.Lerp(alarmLight.intensity, targetIntensity, fadeSpeed * Time.deltaTime);
            CheckTargetIntensity();
        }
        else
        {
            alarmLight.intensity = Mathf.Lerp(alarmLight.intensity, 0.0f, fadeSpeed * Time.deltaTime);
        }
	}

    void CheckTargetIntensity()
    {
        if (Mathf.Abs(targetIntensity - alarmLight.intensity) < changeMargin)
        {
            if (targetIntensity == highInstensity)
            {
                targetIntensity = lowInstensity;
            }
            else
            {
                targetIntensity = highInstensity;
            }
        }
    }
}
