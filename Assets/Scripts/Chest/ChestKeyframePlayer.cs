using UnityEngine;

namespace Chest
{
    public class ChestKeyframePlayer : MonoBehaviour
    {
        [SerializeField] private CameraChestTweener cct;
        private ChestOpenParticleManager chestParticleManager;

        private void Start()
        {
            chestParticleManager = ChestOpenParticleManager.GetInstance();
        }
        
        public void PlayDefaultOpenParticle() => chestParticleManager.PlayDefaultParticle();
        public void PlayDefaultExplodeParticle() => chestParticleManager.PlayDefaultExplodeParticle();
        public void PlayDefaultRewardParticle() => chestParticleManager.PlayDefaultRewardParticle();

        public void PlayRareOpenParticle() => chestParticleManager.PlayRareOpenParticle();
        public void PlayRareExplodeParticle() => chestParticleManager.PlayRareExplodeParticle();
        public void PlayRareRewardParticle() => chestParticleManager.PlayRareRewardParticle();

        public void ShakeCamera()
        {
            if(cct != null)
                cct.ShakeCam(.3f, 3f, 1.5f);
        }
    }
}
