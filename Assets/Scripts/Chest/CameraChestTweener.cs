using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions;

namespace Chest
{
    public class CameraChestTweener : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private List<Transform> cameraPositions;
        [SerializeField] private Material skyboxMaterial;
        private static readonly int Rotation = Shader.PropertyToID("_Rotation");

        private void Start()
        {
            Assert.IsNotNull(cameraTransform, "Camera needs to be hooked up to tweener: " + this.gameObject);
            if(cameraPositions.Count < 1) Debug.LogWarning("camera positions list is empty!");
        }

        public void DoCameraTween()
        {
            DOTween.Sequence(
                cameraTransform.DOMove(cameraPositions[1].position, 1.2f, false).SetEase(Ease.OutBounce))
                .Join(skyboxMaterial.DOFloat(360f,Rotation, 1.2f))
                .AppendInterval(1.6f)
                .OnComplete(() =>
                {
                    cameraTransform.DORotate(new Vector3(-30f, 0f, 0f), .6f).SetEase(Ease.InElastic);
                    skyboxMaterial.SetFloat(Rotation, 0f);
                });

        }

        public void ResetCameraPosition()
        {
            DOTween.Sequence(
                cameraTransform.DOMove(cameraPositions[0].position, .6f))
                .Append(cameraTransform.DORotate(new Vector3(7f, 0f,0f), .6f));
        }

        public void ShakeCam(float delay, float intensity, float duration = 0.8f)
        {
            DOTween.Sequence()
                .AppendInterval(delay)
                .OnComplete( () => cameraTransform.DOShakeRotation(duration, intensity, 5, 10f));
        }
    }
}
