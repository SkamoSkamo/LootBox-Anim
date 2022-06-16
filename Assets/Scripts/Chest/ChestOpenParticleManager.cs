using System;
using UnityEngine;

namespace Chest
{
    public class ChestOpenParticleManager : MonoBehaviour
    {
        [SerializeField] private ParticleSystem defaultChestOpenParticleSystem;
        [SerializeField] private ParticleSystem defaultExplodeParticleSystem;
        [SerializeField] private ParticleSystem rareChestOpenParticleSystem;
        [SerializeField] private ParticleSystem rareChestExplodeParticleSystem;
        [SerializeField] private ParticleSystem defaultRewardParticle;
        [SerializeField] private ParticleSystem rareRewardParticle;

        private static ChestOpenParticleManager instance;

        public static ChestOpenParticleManager GetInstance()
        {
            return instance;
        }

        private void Awake()
        {
            instance = this;
        }

        public void PlayDefaultParticle() => defaultChestOpenParticleSystem.Play(true);
        public void PlayDefaultExplodeParticle() => defaultExplodeParticleSystem.Play(true);   
        
        public void PlayRareOpenParticle() => rareChestOpenParticleSystem.Play(true);
        public void PlayRareExplodeParticle() => rareChestExplodeParticleSystem.Play(true);

        public void PlayDefaultRewardParticle() => defaultRewardParticle.Play(true);
        public void PlayRareRewardParticle() => rareRewardParticle.Play(true);
    }
}
