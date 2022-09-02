using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineMesh : MonoBehaviour
{
    List<Material> allMaterials;
    [SerializeField][Tooltip("if true, dismantling mesh with each material," +
        "else, combine all mesh to one mesh and use one material")] 
    bool dismantlingType = true;
    
    private void Start()
    {
        if (dismantlingType)
        {
            MergeMesh();
        }
        else
        {
            MyMergeMesh();
        }

    }

    void MyMergeMesh()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        List<CombineInstance> combine = new List<CombineInstance>();
        for (int i = 1; i < meshFilters.Length; i++)
        {
            Mesh targetMesh = meshFilters[i].sharedMesh;
            for (int j = 0; j < targetMesh.subMeshCount; j++)
            {
                CombineInstance combineInstance = new CombineInstance();
                combineInstance.mesh = targetMesh;
                combineInstance.transform = meshFilters[i].transform.localToWorldMatrix;
                if (j > 0)
                {
                    combineInstance.subMeshIndex = j;
                }
                combine.Add(combineInstance);
            }
            meshFilters[i].gameObject.SetActive(false);
        }
        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine.ToArray());
        transform.gameObject.SetActive(true);
    }

    void MergeMesh()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

        Material[] allMaterials = new Material[meshRenderers[1].materials.Length];
        for (int i = 0; i < meshRenderers[1].materials.Length; i++)
        {
            allMaterials[i] = meshRenderers[1].materials[i];
        }
        List<List<CombineInstance>> combine2 = new List<List<CombineInstance>>();

        for (int i = 1; i < meshFilters[1].sharedMesh.subMeshCount + 1; i++)
        {
            Mesh mShared = meshFilters[1].sharedMesh;
            List<CombineInstance> combine = new List<CombineInstance>();

            for (int j = 1; j < meshFilters.Length; j++)
            {
                CombineInstance ci = new CombineInstance();
                ci.mesh = mShared;
                ci.transform = meshFilters[j].transform.localToWorldMatrix;
                ci.subMeshIndex = i - 1;
                combine.Add(ci);
            }
            combine2.Add(combine);
            //meshFilters[i].gameObject.SetActive(false);
        }
        int count = meshFilters[1].sharedMesh.subMeshCount;
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
        for (int i = 0; i < count; i++)
        {
            GameObject combineObject = new GameObject($"combine{i}");
            combineObject.transform.SetParent(transform);
            combineObject.AddComponent<MeshFilter>();
            combineObject.AddComponent<MeshRenderer>();

            var combineFilter = combineObject.GetComponent<MeshFilter>();
            var combineRenderer = combineObject.GetComponent<MeshRenderer>();

            combineObject.SetActive(false);
            combineFilter.mesh = new Mesh();

            combineFilter.mesh.CombineMeshes(combine2[i].ToArray());
            combineRenderer.material = allMaterials[i];

            combineObject.SetActive(true);

        }
        

    }
}
