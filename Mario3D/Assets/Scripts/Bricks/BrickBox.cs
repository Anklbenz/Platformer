using System.Threading.Tasks;
using Character.States;
using Enemy;
using Enums;
using UnityEngine;

namespace Bricks
{
    public sealed class BrickBox : Brick
    {
        private const int COLLIDER_DEACTIVATE_DELAY = 5;
        private const float DROP_CHECK_DISTANCE = 0.1f;

        [SerializeField] private ParticleSystem crushParticles;
        [SerializeField] private Animator animator;
        [SerializeField] private int timeToDestroyBrickAfterHit;
        [SerializeField] private bool brickCanBeCrushed = false;
        [SerializeField] private LayerMask dropLayer;
        private Interactor _interactor;

        private void Awake(){
            ParticleInitialize();
            _interactor = new Interactor(brickCollider, Axis.Vertical, DROP_CHECK_DISTANCE, dropLayer);
        }

        private void ParticleInitialize(){
            if (!crushParticles) return;

            crushParticles = Instantiate(crushParticles, this.transform);
            crushParticles.transform.position = transform.position;
            crushParticles.gameObject.SetActive(false);
        }

        public override void BrickHit(StateSystem state){
            if (!IsActive) return;

            this.DownHit();
            animator.SetTrigger("hit");

            if (bonusesCount > 0){
                base.BonusShow(state);
                bonusesCount--;

                if (bonusesCount == 0){
                    IsActive = false;
                    primaryMesh.SetActive(false);
                    secondaryMesh.SetActive(true);
                }

            }
            else if (state.Data.CanCrush && brickCanBeCrushed){
                crushParticles.gameObject.SetActive(true);
              //  Destroy(gameObject, timeToDestroyBrickAfterHit);
              Crush(timeToDestroyBrickAfterHit);
            }
        }

        private void DownHit(){
            var interactions = _interactor.InteractionOverlap(Vector3.up);
            if (interactions.Length < 1) return;

            foreach (var obj in interactions){
                var activeObject = obj.transform.GetComponentInParent<ActiveInteractiveObject>();
                if (activeObject)
                    activeObject.DownHit();
            }
        }

        private async void Crush(int destroyTime){
            await Task.Delay(COLLIDER_DEACTIVATE_DELAY);
            brickCollider.enabled = false;
            primaryMesh.SetActive(false);
            
            await Task.Delay(destroyTime);
            Destroy(gameObject);
        }

        private void OnDrawGizmos(){
            _interactor?.OnDrawGizmos(Color.red);
        }
    }
}

