using System;
using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Logic;
using CodeBase.Services;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        public HeroAnimator HeroAnimator;
        public CharacterController CharacterController;

        private IInputService _input;
        
        private static int _layerMask;
        private Collider[] _hits = new Collider[3];
        private Stats _stats;

        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();

            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Update()
        {
            if (_input.AttackButtonUp() && !HeroAnimator.IsAttacking)
                HeroAnimator.PlayAttack();
        }

        public void OnAttack()
        {
            for (var i = 0; i < Hit(); i++)
            {
                PhysicsDebug.DrawDebug(StartPoint() + transform.forward, _stats.DamageRadius, 1f);
                _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_stats.Damage);
            }
        }

        public void LoadProgress(PlayerProgress progress) => 
            _stats = progress.HeroStats;

        private int Hit() =>
            Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _stats.DamageRadius, _hits, _layerMask);

        private Vector3 StartPoint()
        {
            var position = transform.position;
            return new Vector3(position.x, CharacterController.center.y / 2, position.z);
        }
    }
}