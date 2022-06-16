using UnityEngine;
using DG.Tweening;
using UnityEngine.Assertions;

namespace Chest
{
    [RequireComponent(typeof(CameraChestTweener))]
    public class ChestSelectionManager : MonoBehaviour
    {
        
        [SerializeField] private GameObject defaultChest;
        [SerializeField] private GameObject rareChest;
        [SerializeField] private Animator chestAnimator;
        
        [Header("Transition Materials")] 
        [SerializeField] private Material defaultChestMaterial;
        [SerializeField] private Material rareChestMaterial;

        private CameraChestTweener cct;

        private const string MATERIAL_PROPERTY_NAME = "_DissolveAmount";
        private static readonly int DissolveAmount = Shader.PropertyToID(MATERIAL_PROPERTY_NAME);

        private void Start()
        {
            defaultChest.SetActive(false);
            rareChest.SetActive(false);
            cct = GetComponent<CameraChestTweener>();
            DropDefaultChest();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                cct.ResetCameraPosition();
        }

        public void DropDefaultChest()
        {
            if(defaultChest.activeInHierarchy) return;
            
            defaultChestMaterial.SetFloat(DissolveAmount, 0f);
            
            chestAnimator.SetTrigger("DropDefaultChest");
            
            defaultChest.transform.position = new Vector3(0f, 10f, 0f); // resolves annoying visual glitch where the chest is visible below the dissolving one
            defaultChest.SetActive(true);

            cct.ShakeCam(.45f, 1.65f);
            
            DOTween.Sequence(
                rareChestMaterial.DOFloat(1f,DissolveAmount, 1f)
                    .OnComplete( () => rareChest.SetActive(false)));
            
        }

        public void DropRareChest()
        {
            if(rareChest.activeInHierarchy) return;
            
            rareChestMaterial.SetFloat(DissolveAmount, 0f);
            
            cct.ShakeCam(.45f, 2.22f);
            
            chestAnimator.SetTrigger("DropRareChest");

            rareChest.transform.position = new Vector3(0f, 10f, 0f);
            rareChest.SetActive(true);
            
            DOTween.Sequence(
                defaultChestMaterial.DOFloat(1f,DissolveAmount, 1f)
                    .OnComplete( () => defaultChest.SetActive(false)));
        }

        public void OpenChest()
        {
            if(defaultChest.activeInHierarchy) chestAnimator.SetTrigger("OpenDefaultChest");
            else chestAnimator.SetTrigger("OpenRareChest");
            
            cct.DoCameraTween();
        }

        public void ResetChestSelection()
        {
            defaultChest.SetActive(false);
            rareChest.SetActive(false);
            cct.ResetCameraPosition();
            DropDefaultChest();
        }
    }
}
