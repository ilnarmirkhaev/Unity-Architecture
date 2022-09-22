using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class Aggro : MonoBehaviour
    {
        public TriggerObserver TriggerObserver;
        public Follow Follow;

        public float Cooldown = 2f;
        private Coroutine _aggroFalloffCoroutine;
        private bool _hasAggroTarget;

        private void Start()
        {
            TriggerObserver.TriggerEnter += TriggerEnter;
            TriggerObserver.TriggerExit += TriggerExit;

            SwitchFollowOff();
        }

        private void TriggerEnter(Collider obj)
        {
            if (_hasAggroTarget) return;
            _hasAggroTarget = true;
                
            StopAggroFalloffCoroutine();
                
            SwitchFollowOn();
        }

        private void TriggerExit(Collider obj)
        {
            if (!_hasAggroTarget) return;
            _hasAggroTarget = false;
              
            _aggroFalloffCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
        }

        private IEnumerator SwitchFollowOffAfterCooldown()
        {
            yield return new WaitForSeconds(Cooldown);
            
            SwitchFollowOff();
        }

        private void StopAggroFalloffCoroutine()
        {
            if (_aggroFalloffCoroutine == null) return;
            
            StopCoroutine(_aggroFalloffCoroutine);
            _aggroFalloffCoroutine = null;
        }

        private void SwitchFollowOn() =>
            Follow.enabled = true;
        
        private void SwitchFollowOff() =>
            Follow.enabled = false;
    }
}