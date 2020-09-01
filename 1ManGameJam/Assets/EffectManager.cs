using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EffectManager : MonoBehaviour
{
    public Controller controller;
    public PostProcessVolume postProcessVolume;


    private void Awake()
    {
        controller = FindObjectOfType<Controller>();
        postProcessVolume = GetComponent<PostProcessVolume>();
    }

    void Update()
    {
        postProcessVolume.weight = Mathf.Clamp(-0.5f + 1.5f * (controller.transform.position.y - controller.boundary.yMin) / (controller.boundary.yMax - controller.boundary.yMin - 0.5f), 0f, 1f);
    }
}
