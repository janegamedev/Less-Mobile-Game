using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public LayerMask selectionMask;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity,  selectionMask);
        
        if(hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
        }
    }
}
