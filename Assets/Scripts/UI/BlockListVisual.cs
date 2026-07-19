using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlockListVisual : MonoBehaviour
{
    [SerializeField] private List<Image> _blockImagesTemplate;
    [SerializeField] private TMP_Text _blockWeightText;

    private void OnEnable()
    {
        EventHandler.OnBlockChange += RefreshUI;
    }

    private void OnDisable()
    {
        EventHandler.OnBlockChange -= RefreshUI;
    }    

    private void RefreshUI(List<GameObject> blockPrefabList)
    {
        for (int i = 0; i < _blockImagesTemplate.Count; i++)
        {
            if (i < blockPrefabList.Count) //sprite renderer ganti ke image biasa klo sempet
            {
                _blockImagesTemplate[i].enabled = true;
                _blockImagesTemplate[i].sprite = blockPrefabList[i].GetComponentInChildren<SpriteRenderer>().sprite;
                _blockImagesTemplate[i].color = blockPrefabList[i].GetComponentInChildren<SpriteRenderer>().color;
            }
            else
            {
                _blockImagesTemplate[i].enabled = false;
            }
        }

        if (blockPrefabList.Count <= 0)
        {
            _blockWeightText.text = "";
        }
        else
        {
            _blockWeightText.text = $"Weight: {blockPrefabList[0]?.GetComponent<Rigidbody2D>()?.mass.ToString()}";
        }
    }
}
