using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using DG.Tweening;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    public List<GameObject> blockPrefabList = new();
    [SerializeField] private Vector3 _spawnTransformPos;
    [SerializeField] private float _spawnYPosition = 3.6f;
    [SerializeField] private GameObject _spawnBlockParent;
    private BlockController _spawnedBlock;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        SpawnBlock();
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;
        if (_spawnedBlock == null) return;

        MoveSpawnedBlock();

        if (Mouse.current.leftButton.wasReleasedThisFrame) DropSpawnedBlock();
    }

    public bool CheckListLength()
    {
        if (blockPrefabList.Count > 0) return true;
        else return false;
    }

    public void SpawnBlock()
    {
        if (_spawnedBlock != null) return;
        if (!CheckListLength()) return;

        GameObject _instantiatedBlock = Instantiate(blockPrefabList[0], _spawnTransformPos, Quaternion.identity);
        _instantiatedBlock.transform.parent = _spawnBlockParent.transform;

        _spawnedBlock = _instantiatedBlock.GetComponent<BlockController>();
        blockPrefabList.RemoveAt(0);
    }

    public void MoveSpawnedBlock()
    {
        Vector3 _blockPos = Mouse.current.position.ReadValue();
        _blockPos.z = 10;
        Vector2 _blockWorldPoint = Camera.main.ScreenToWorldPoint(_blockPos);
        _blockWorldPoint.y = _spawnYPosition;
        _spawnedBlock.MoveTo(_blockWorldPoint);
    }

    public void DropSpawnedBlock()
    {
        _spawnedBlock.DropBlock();
        _spawnedBlock = null;

        DOVirtual.DelayedCall(1f, () => SpawnBlock(), false);
    }
    
}
