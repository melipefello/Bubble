using TMPro;
using UnityEngine;

[RequireComponent(typeof(Bubble))]
class BubbleView : MonoBehaviour
{
    [SerializeField] Transform _graphicsRoot; //Removed Shapes module
    [SerializeField] TMP_Text _sizeText;

    Bubble _bubble;

    void Awake()
    {
        _bubble = GetComponent<Bubble>();
        _bubble.Initialized += OnInitialized;
        _bubble.Grew += UpdateSize;
    }

    void LateUpdate()
    {
        UpdateTransform();
    }

    void OnDestroy()
    {
        _bubble.Initialized -= OnInitialized;
        _bubble.Grew -= UpdateSize;
    }

    void OnInitialized()
    {
        UpdateSize();
        UpdateTransform();
    }

    void UpdateTransform()
    {
        transform.SetPositionAndRotation(_bubble.Position, _bubble.Rotation);
    }

    void UpdateSize()
    {
        _sizeText.SetText($"{_bubble.Size}");
        _graphicsRoot.localScale = Vector3.one * _bubble.Radius * 2;
    }
}