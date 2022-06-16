using System;
using Chest;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private ChestSelectionManager chestParent;
        [SerializeField] private Button defaultChestButton;
        [SerializeField] private Button rareChestButton;
        [SerializeField] private Button openChestButton;

        [SerializeField] private GameObject rewardPlaceholder;

        private bool chestOpen = false;
        private void Awake()
        {
            Assert.IsNotNull(chestParent, "Missing reference to ChestSelectionManager");
            Application.targetFrameRate = 120;
        }

        private void OnEnable()
        {
            defaultChestButton.onClick.AddListener(OpenDefaultChest);
            rareChestButton.onClick.AddListener(OpenRareChest);
            openChestButton.onClick.AddListener(OpenChest);
        }

        private void OnDisable()
        {
            defaultChestButton.onClick.RemoveListener(OpenDefaultChest);
            rareChestButton.onClick.RemoveListener(OpenRareChest);
            openChestButton.onClick.RemoveListener(OpenChest);
        }

        private void OpenDefaultChest()
        {
            chestParent.DropDefaultChest();   
        }

        private void OpenRareChest()
        {
            chestParent.DropRareChest();
        }

        private void OpenChest()
        {
            if (!chestOpen)
            {
                chestParent.OpenChest();
                HideButtons();
                openChestButton.interactable = false;
                
                DOTween.Sequence()
                    .AppendInterval(3.3f)
                    .OnComplete(() =>
                    {
                        rewardPlaceholder.SetActive(true);
                        chestOpen = true;
                        openChestButton.interactable = true;
                    });
            }
            else
            {
                openChestButton.interactable = false;
                rewardPlaceholder.SetActive(false);
                
                DOTween.Sequence()
                    .AppendInterval(1f)
                    .OnComplete(() => openChestButton.interactable = true);
                chestParent.ResetChestSelection();
                ShowButtons();
                chestOpen = false;
            }
            
        }

        private void HideButtons()
        {
            defaultChestButton.interactable = false;
            rareChestButton.interactable = false;
            DOTween.Sequence()
                .Append(defaultChestButton.transform.DOMoveX(-150f, .3f))
                .Append(rareChestButton.transform.DOMoveX(-150f, .3f));
        }

        private void ShowButtons()
        {
            defaultChestButton.interactable = true;
            rareChestButton.interactable = true;
            DOTween.Sequence()
                .Append(defaultChestButton.transform.DOMoveX(150f, .3f))
                .Append(rareChestButton.transform.DOMoveX(150f, .3f));
        }
    
    }
}
