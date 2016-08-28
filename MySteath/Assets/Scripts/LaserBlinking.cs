using UnityEngine;
using System.Collections;

public class LaserBlinking : MonoBehaviour {

    public float onTime;
    public float offTime;

    private float timer;
    private Renderer laserRenderer;
    private Light laserLight;


    void Awake()
    {
        laserRenderer = GetComponent<Renderer>();
        laserLight = GetComponent<Light>();
        timer = 0f;
    }

    void SwitchBeam()
    {
        timer = 0.0f;
        laserRenderer.enabled = !laserRenderer.enabled;
        laserLight.enabled = !laserLight.enabled;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (laserRenderer.enabled && timer >= onTime)
        {
            SwitchBeam();
        }
        if(!laserRenderer.enabled && timer >= offTime)
        {
            SwitchBeam();
        }
    }
}
