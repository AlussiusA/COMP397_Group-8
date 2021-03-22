using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "Data/SceneData")]
public class SceneDataSO : ScriptableObject
{
    // Player
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    // health and ship parts here
}
