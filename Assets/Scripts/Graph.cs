﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField]
    Transform pointPrefab = default;
    [SerializeField, Range(10, 100)]
    int resolution = 10;
    Transform[] points;
    void Awake()
    {
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;
        Vector3 position = Vector3.zero;
        points = new Transform[resolution];
        for (int i = 0; i < resolution; i++)
        {
            Transform point = Instantiate(pointPrefab);
            points[i] = point;
            point.SetParent(transform, false);
            position.x = (i + 0.5f) * step - 1f;
            position.y = position.x * position.x * position.x;
            point.localPosition = position;
            point.localScale = scale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.time;
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            Vector3 position = point.localPosition;
            position.y = Mathf.Sin(Mathf.PI * (position.x + time));
            point.localPosition = position;
        }
    }
}
