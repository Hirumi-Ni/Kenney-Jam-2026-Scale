using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    public List<GameObject> blockPrefabList = new();
    [SerializeField] private Vector3 _spawnTransformPos;
    [SerializeField] private float _spawnYPosition = 3.6f;
    [SerializeField] private GameObject _spawnBlockParent;
    private GameObject _instantiatedBlock;
    private BlockController _spawnedBlock;
    private Tween _spawnDelayTween;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        SpawnBlock();
        EventHandler.WhenBlockChange(blockPrefabList);
    }

    private void OnDestroy()
    {
        _spawnDelayTween?.Kill();    
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;
        if (_spawnedBlock == null) return;

        MoveSpawnedBlock();

        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (Mouse.current.leftButton.wasReleasedThisFrame) DropSpawnedBlock();
    }

    public bool CheckListLength()
    {
        if (blockPrefabList.Count > 0) return true;
        else return false;
    }

    public GameObject GetCurrentBlock()
    {
        return blockPrefabList[0];
    }

    public void SpawnBlock()
    {
        if (_spawnedBlock != null) return;
        if (!CheckListLength()) return;

        _instantiatedBlock = Instantiate(blockPrefabList[0], _spawnTransformPos, Quaternion.identity);
        _instantiatedBlock.transform.parent = _spawnBlockParent.transform;

        _spawnedBlock = _instantiatedBlock.GetComponent<BlockController>();
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
        if (_spawnedBlock.CheckOverlapBlock()) { Debug.Log("Block is overlapping"); return; }

        blockPrefabList.RemoveAt(0);
        _spawnedBlock.DropBlock();
        _spawnedBlock = null;

        EventHandler.WhenBlockChange(blockPrefabList);

        _spawnDelayTween = DOVirtual.DelayedCall(1f, SpawnBlock, false);
    }
    
    public void SwapBlock()
    {
        if (blockPrefabList.Count <= 1) return;
        if (_spawnedBlock == null) return;

        Destroy(_instantiatedBlock);
        _spawnedBlock = null;

        GameObject temp = blockPrefabList[0];
        blockPrefabList[0] = blockPrefabList[1];
        blockPrefabList[1] = temp;

        EventHandler.WhenBlockChange(blockPrefabList);
        SpawnBlock();
    }
}
