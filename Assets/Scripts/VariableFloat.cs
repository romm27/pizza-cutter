using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Float", menuName = "Variable/ Float")]
public class VariableFloat : SOVariable
{
    [Header("Data")]
    public float value;
}
