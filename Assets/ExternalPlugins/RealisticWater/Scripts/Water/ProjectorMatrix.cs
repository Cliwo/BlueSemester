using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ProjectorMatrix : MonoBehaviour {

    public string[] GlobalMatrixNames;
    public Transform[] ProjectiveTranforms;
    public bool UpdateOnStart;
    public bool CanUpdate = true;

    private Transform t;

    void Start()
    {
        t = transform;
        if (UpdateOnStart) UpdateMatrix();
    }

    void Update()
    {
        if (!UpdateOnStart) UpdateMatrix();
#if UNITY_EDITOR
        if(!Application.isPlaying) UpdateMatrix();
#endif
    }

    public void UpdateMatrix()
    {
        if (!CanUpdate)
            return;
        if (GlobalMatrixNames == null || ProjectiveTranforms == null || GlobalMatrixNames.Length == 0 || ProjectiveTranforms.Length == 0)
            return;
        for (int i = 0; i < GlobalMatrixNames.Length; i++)
        {
            Shader.SetGlobalMatrix(GlobalMatrixNames[i], ProjectiveTranforms[i].worldToLocalMatrix * t.localToWorldMatrix);
        }
    }
}

