using UnityEngine;

public class MaterialExplosion : MonoBehaviour
{
    [SerializeField] private Material _explosionTexture;

    private Renderer render;

    private void Start()
    {
        render = GetComponent<Renderer>();
        render.enabled = true;
        render.sharedMaterial = _explosionTexture;
    }
}
