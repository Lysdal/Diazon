using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    public Layer[] LayerPriorities = {
        Layer.Enemy,
        Layer.Walkable
    };

    [SerializeField]
    float distanceToBackground = 100f;
    Camera _viewCamera;

    RaycastHit _hit;
    public RaycastHit Hit
    {
        get { return _hit; }
    }

    Layer _layerHit;
    public Layer layerHit
    {
        get { return _layerHit; }
    }

    void Start()
    {
        _viewCamera = Camera.main;
    }

    void Update()
    {
        // Look for and return priority layer hit
        foreach (Layer layer in LayerPriorities)
        {
            var hit = RaycastForLayer(layer);
            if (hit.HasValue)
            {
                _hit = hit.Value;
                _layerHit = layer;
                return;
            }
        }

        // Otherwise return background hit
        _hit.distance = distanceToBackground;
        _layerHit = Layer.RaycastEndStop;
    }

    RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer; 
        Ray ray = _viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        Physics.Raycast(ray, out hit, distanceToBackground, layerMask);
        return hit;
    }
}
