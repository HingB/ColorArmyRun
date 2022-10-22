using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BrushColorChanger))]
public class Brush : MonoBehaviour, IDyeing
{
    private bool _iDyeing;
    public static event UnityAction<Brush> BrishRemoved;
    [field : SerializeField] public Transform Point1 { get; private set; }
    [field : SerializeField] public Transform Point2 { get; private set; }
    public BrushColorChanger BrushColorChanger { get; private set; }

    public void Die()
    {
        if (_iDyeing)
            return;
        BrishRemoved?.Invoke(this);
        this.transform.parent = transform.parent.parent;
        GetComponent<Collider>().isTrigger = false;
        var rigedbody = gameObject.AddComponent<Rigidbody>();
        rigedbody.mass = 1000;
        _iDyeing = true;
        BrushColorChanger.DisconnectTrailRender();
    }

    private void Awake()
    {
        BrushColorChanger = GetComponent<BrushColorChanger>();
    }
}
