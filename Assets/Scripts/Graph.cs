using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField]
    Transform pointPrefab = default;
    [SerializeField, Range(10, 100)]
    int resolution = 10;
    Transform[] points;
    float duration;
    [SerializeField]
    FunctionLibrary.FunctionName function = default;
    [SerializeField, Min(0f)]
    float functionDuration = 1f;
    void Awake()
    {
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;
        points = new Transform[resolution * resolution];
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = Instantiate(pointPrefab);
            point.SetParent(transform, false);
            point.localScale = scale;
            points[i] = point;
        }
    }

    // Update is called once per frame
    void Update()
    {
        duration += Time.deltaTime;
        if (duration >= functionDuration)
        {
            duration -= functionDuration;
            function = FunctionLibrary.GetNextFunctionName(function);
        }
        UpdateFunction();
    }

    void UpdateFunction()
    {
        float time = Time.time;
        float step = 2f / resolution;
        float v = 0.5f * step - 1f;
        FunctionLibrary.Function f = FunctionLibrary.GetFunction(function);
        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++)
        {
            if (x == resolution)
            {
                x = 0;
                z += 1;
                v = (z + 0.5f) * step - 1f;
            }
            float u = (x + 0.5f) * step - 1f;
            points[i].localPosition = f(u, v, time);
        }
    }
}
