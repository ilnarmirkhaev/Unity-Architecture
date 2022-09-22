using System;
using CodeBase.Services;
using CodeBase.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;

        public BoxCollider Collider;

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _saveLoadService.SaveProgress();
            Debug.Log("Progress Saved.");
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if (!Collider) return;

            var t = transform;
            Matrix4x4 rotationMatrix = Matrix4x4.TRS(t.position, t.rotation, t.lossyScale);
            Gizmos.matrix = rotationMatrix;
            Gizmos.color = new Color32(30, 200, 30, 130);
            Gizmos.DrawCube(Collider.center, Collider.size);
        }
    }
}