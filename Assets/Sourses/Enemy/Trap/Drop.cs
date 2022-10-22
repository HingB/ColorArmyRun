using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Drop : MonoBehaviour
{
    [SerializeField] private int[] _layerBlackList;
    [SerializeField] private float _shootForse = 900;
    [SerializeField] private ParticleSystem _waterBlop;
    [SerializeField] private ParticleSystem _waterBoom;

    private Rigidbody _rigidbody;
    private Transform _particleParent;

    public abstract void OnStickmanTouch(Stickman stickman);

    public void Move()
    {
        _rigidbody.AddRelativeForce(Vector3.forward * Time.deltaTime * _shootForse);
    }

    public void Init(Vector3 shootDirection, Transform particleParent)
    {
        _rigidbody = GetComponent<Rigidbody>();
        _particleParent = particleParent;
        _rigidbody.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!ExistInBlackList(other.gameObject.layer))
        {
            Destroy(gameObject);
            CreateBlop();
            CreateBoom();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy stickman))
        {
            OnStickmanTouch(stickman);
            CreateBoom();
            Destroy(gameObject);
        }

        if (!ExistInBlackList(collision.gameObject.layer))
        {
            Destroy(gameObject);
            CreateBlop();
        }
    }

    private bool ExistInBlackList(int id)
    {
        return _layerBlackList.Contains(id);
    }

    private void CreateBlop()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit))
            Instantiate(_waterBlop, hit.point, Quaternion.identity, _particleParent).Play();
    }

    private void CreateBoom()
    {
        Instantiate(_waterBoom, transform.position, Quaternion.identity, _particleParent).Play();
    }

    private void Update()
    {
        Move();
    }
}
