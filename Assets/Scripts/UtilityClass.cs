using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityClass
{
    public static Vector2 AngleToVector(float angle, Transform transform, float multiplier = 1) {
        float storedZ = transform.eulerAngles.z;
        Vector3 temp = transform.eulerAngles;
        Vector3 result = Vector3.zero;
        temp.z = angle;
        transform.eulerAngles = temp;
        result = transform.up;
        temp.z = storedZ;
        transform.eulerAngles = temp;
        //Debug.Log(result);
        return new Vector2(-result.x * multiplier, result.y * multiplier);
    }
}
