using System.Threading.Tasks;
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
        [SerializeField] private float dropForce;
        [SerializeField] private LayerMask dropLayer;
        private Interacting _interacting;

        private void Awake(){
            ParticleInitialize();
            _interacting = new Interacting(brickCollider, Axis.Vertical, DROP_CHECK_DISTANCE, dropLayer);
        }

        private void ParticleInitialize(){
            if (!crushParticles) return;

            crushParticles = Instantiate(crushParticles, this.transform);
            crushParticles.transform.position = transform.position;
            crushParticles.gameObject.SetActive(false);
        }

        public override void BrickHit(bool canCrush){
            if (!IsActive) return;

            this.DownHit();
            animator.SetTrigger("hit");

            if (bonusesCount > 0){
                base.BonusShow(canCrush);
                bonusesCount--;

                if (bonusesCount == 0){
                    IsActive = false;
                    primaryMesh.SetActive(false);
                    secondaryMesh.SetActive(true);
                }

            }
            else if (canCrush && brickCanBeCrushed){
                crushParticles.gameObject.SetActive(true);
                
                Crush(timeToDestroyBrickAfterHit);
            }
        }

        private void DownHit(){
            var interactions = _interacting.InteractionOverlap(Vector3.up);
            if (interactions.Length < 1) return;

            foreach (var obj in interactions){
                var activeObject = obj.transform.GetComponentInParent<ActiveInteractiveObject>();
                if (activeObject)
                    activeObject.DownHit(dropForce);
            }
        }

        private async void Crush(int destroyTime){
            //коллайдер склишком быстро исчезает и персонаж проскакивает через него, нет эффекта удара
            await Task.Delay(COLLIDER_DEACTIVATE_DELAY);
            brickCollider.enabled = false;
            primaryMesh.SetActive(false);

            await Task.Delay(destroyTime);
            Destroy(gameObject);
        }

        private void OnDrawGizmos(){
            _interacting?.OnDrawGizmos(Color.red);
        }
    }
}

