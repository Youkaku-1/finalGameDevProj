using UnityEngine;

public class MapLoop: MonoBehaviour
{
    [Header("Prefabs to Move")]
    public GameObject[] objectsToMove;

    [Header("Movement Settings")]
    public float moveSpeed = 0f;       
    public float resetPositionZ = 0f; 
    public float endPositionZ = 0f;  

    void Update()
    {
        if (objectsToMove == null || objectsToMove.Length == 0)
            return;

        foreach (GameObject obj in objectsToMove)
        {
            if (obj == null) continue;

            // Move toward -Z
            obj.transform.Translate(0f, 0f, -moveSpeed * Time.deltaTime, Space.World);

            // Loop when passing the end position
            if (obj.transform.position.z <= endPositionZ)
            {
                Vector3 pos = obj.transform.position;
                pos.z = resetPositionZ;
                obj.transform.position = pos;
            }
        }
    }
}
