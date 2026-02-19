using UnityEngine;

public class FixedMaterialAssigner : MonoBehaviour
{
    [Header("三种固定材质")]
    public Material material1;
    public Material material2;
    public Material material3;

    [Header("分配选项")]
    [Tooltip("是否包含未激活的子对象")]
    public bool includeInactiveChildren = true;

    void Start()
    {
        AssignRandomMaterials();
    }

    public void AssignRandomMaterials()
    {
        if (material1 == null || material2 == null || material3 == null)
        {
            Debug.LogError("三种材质都必须设置！");
            return;
        }

        MeshRenderer[] childRenderers = GetComponentsInChildren<MeshRenderer>(includeInactiveChildren);

        if (childRenderers.Length == 0)
        {
            Debug.LogWarning("没有找到任何子Mesh Renderer");
            return;
        }

        foreach (MeshRenderer renderer in childRenderers)
        {
            int randomIndex = Random.Range(0, 3); // 0, 1 或 2

            switch (randomIndex)
            {
                case 0:
                    renderer.material = material1;
                    break;
                case 1:
                    renderer.material = material2;
                    break;
                case 2:
                    renderer.material = material3;
                    break;
            }
        }

        Debug.Log($"已为 {childRenderers.Length} 个子对象分配随机材质");
    }

    [ContextMenu("随机分配材质")]
    public void AssignRandomMaterialsEditor()
    {
        AssignRandomMaterials();
    }
}