using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bool", menuName = "Variable/ Bool")]
public class VariableBool : SOVariable
{
    [Header("Data")]
    public bool value;
}
