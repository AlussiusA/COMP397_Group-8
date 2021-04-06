using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "Data/SceneData")]
[System.Serializable]
public class SceneDataSO : ScriptableObject
{
    // Player
    public Vector3 playerPosition = new Vector3(0, 6.5f, -0.3f);
    public Quaternion playerRotation;
    // health and ship parts here
    public int health = 100;
    public bool[] collectedParts;
}
