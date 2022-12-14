using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheat : MonoBehaviour
{
    [SerializeField] private GameObject _blockOfWheatPrefab;
    
    private GameObject[] _partsOfWheat;
    
    private MeshCollider _meshCollider;
    private MeshRenderer _meshRenderer;
    private Material _material;
    
    private float _height;
    
    private float _scytheAnimTimer;
    private float _animEps = 0.2f;

    [SerializeField] private float _growInterval;
    private float _growTimer;
    
    
    private FirstClickHandler _firstClickHandler;

    private void Awake()
    {
        _partsOfWheat = new GameObject[3];
        
        _meshRenderer = GetComponent<MeshRenderer>();
        _material = _meshRenderer.material;
        _meshCollider = GetComponent<MeshCollider>();
        _firstClickHandler = GetComponent<FirstClickHandler>();
        
        _height = _meshCollider.bounds.max.y - _meshCollider.bounds.min.y;
    }

    private void Start()
    {
        Slice();
    }

    private void Slice()
    {
        Material[] materials = {_material, _material};
        
        Vector3 slicedPosition = new Vector3(transform.position.x, transform.position.y + _height * (2f / 3f),
            transform.position.z);
        List<GameObject> slicedObjects = gameObject.Slice(slicedPosition, _material);
        _partsOfWheat[0] = slicedObjects[0];
        Destroy(slicedObjects[1]);
        
        slicedPosition = new Vector3(transform.position.x, transform.position.y + _height * (1f / 3f),
            transform.position.z);
        slicedObjects = slicedObjects[1].Slice(slicedPosition, _material);

        _partsOfWheat[1] = slicedObjects[0];
        _partsOfWheat[2] = slicedObjects[1];
        
        
        foreach (var part in _partsOfWheat)
        {
            part.GetComponent<MeshRenderer>().materials = materials;
            part.AddComponent<PartOfWheat>();
            part.transform.position = transform.position;
            part.transform.SetParent(transform);
        }

        _meshRenderer.enabled = false;
    }

    private void Update()
    {
        _scytheAnimTimer += Time.deltaTime;
        _growTimer += Time.deltaTime;
        

        if (_growTimer >= _growInterval)
        {
            for (int i = _partsOfWheat.Length - 1; i >= 0; i--)
            {
                if (!_partsOfWheat[i].activeSelf)
                {
                    _partsOfWheat[i].SetActive(true);
                    _growTimer = 0;
                    
                    if (i == _partsOfWheat.Length - 1)
                    {
                        _meshCollider.enabled = true;
                    }
                    break;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Scythe>() && other.transform.root.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("SplashAttack"))
        {
            if (_scytheAnimTimer > other.transform.root.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + _animEps)
            {
                _scytheAnimTimer = 0;
                for (int i = 0; i <= _partsOfWheat.Length - 1; i++)
                {
                    if (_partsOfWheat[i].activeSelf)
                    {
                        
                        _partsOfWheat[i].SetActive(false);
                        Invoke(nameof(SpawnBlockOfWheat),0.1f);
                        _growTimer = 0;

                        if (i == _partsOfWheat.Length - 1)
                        {
                            _meshCollider.enabled = false;
                        }
                        
                        if(_firstClickHandler!=null && !_firstClickHandler.isCursorDeleted)
                            _firstClickHandler.DeleteCursor();
                        break;
                    }
                }
            }
            
        }
    }

    private void SpawnBlockOfWheat()
    {
        Instantiate(_blockOfWheatPrefab, transform.position, Quaternion.identity);
    }
}
