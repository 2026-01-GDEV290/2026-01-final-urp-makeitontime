using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
public class MapColliderCombiner : MonoBehaviour
{
    private void Start()
    {
        CombineSnapColliders();
    }
    public void CombineSnapColliders()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        
        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
        }
        
        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combine);
        
        MeshCollider mc = gameObject.AddComponent<MeshCollider>();
        mc.sharedMesh = combinedMesh;
        mc.convex = false;
    }
}
}
