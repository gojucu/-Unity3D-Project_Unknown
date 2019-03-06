using System.Collections;
using UnityEngine;
using RPG.CameraUI; //todo consider re-wiring


namespace RPG.Characters {
    public class PlayerControl : MonoBehaviour {
        
       
        Character character;

        SpecialAbilities abilities;
        WeaponSystem weaponSystem;

        [SerializeField] GameObject projectileToUse;
        [SerializeField] GameObject projectileSocket;
        [SerializeField] Vector3 aimOffset = new Vector3(0, 1f, 0);

        EnemyAI enemyAI = null;

        void Start()
        {
            enemyAI = FindObjectOfType<EnemyAI>();
            character = GetComponent<Character>();
            abilities = GetComponent<SpecialAbilities>();
            weaponSystem = GetComponent<WeaponSystem>();

            RegisterForMouseEvents();

        }
        private void RegisterForMouseEvents()
        {
            var cameraRaycaster = FindObjectOfType<CameraRaycaster>();
            cameraRaycaster.onMouseOverEnemy += OnMouseOverEnemy;
            cameraRaycaster.onMouseOverPotentiallyWalkable += OnMouseOverPotentiallyWalkable;
        }
        

        void Update()
        {
            ScanForAbilityKeyDown();
        }

        void ScanForAbilityKeyDown()
        {
            for (int keyIndex = 1; keyIndex < abilities.GetNumberOfAbilities(); keyIndex++)
            {
                if (Input.GetKeyDown(keyIndex.ToString()))
                {
                    abilities.AttemptSpecialAbility(keyIndex);
                }
            }
        }


        void OnMouseOverPotentiallyWalkable(Vector3 destination)
        {
            if (Input.GetMouseButton(0))
            {
                weaponSystem.StopAttacking();
                character.SetDestination(destination);
            }

        }

        bool IsTargetInRange(GameObject target)
        {
            float distanceToTarget = (target.transform.position - transform.position).magnitude;
            return distanceToTarget <= weaponSystem.GetCurrentWeapon().GetMaxAttackRange();
        }


        void OnMouseOverEnemy(EnemyAI enemy)
        {
            
            if (Input.GetMouseButton(0) && IsTargetInRange(enemy.gameObject))
            {
                weaponSystem.AttackTarget(enemy.gameObject);
            }
            else if (Input.GetMouseButton(0) && !IsTargetInRange(enemy.gameObject))
            {
                StartCoroutine(MoveAndAttack(enemy));
            }
            else if (Input.GetMouseButtonDown(1) )
            {
                abilities.AttemptSpecialAbility(0, enemy.gameObject);
                FireProjectile();
            }
        }

        void FireProjectile()
        {
            GameObject newProjectile;
            Projectile projectileComponent;
            SpawnProjectile(out newProjectile, out projectileComponent);

            Vector3 unitVectorToPlayer = (enemyAI.transform.position + aimOffset - projectileSocket.transform.position).normalized;
            float projectileSpeed = projectileComponent.GetDefaultLaunchSpeed();
            newProjectile.GetComponent<Rigidbody>().velocity = unitVectorToPlayer * projectileSpeed;
        }
        private void SpawnProjectile(out GameObject newProjectile, out Projectile projectileComponent)
        {
            newProjectile = Instantiate(projectileToUse, projectileSocket.transform.position, Quaternion.identity);
            projectileComponent = newProjectile.GetComponent<Projectile>();
            projectileComponent.SetShooter(gameObject);
        }

        IEnumerator MoveToTarget(GameObject target)
        {
            character.SetDestination(target.transform.position);
            while (!IsTargetInRange(target))
            {
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForEndOfFrame();
        }
        IEnumerator MoveAndAttack(EnemyAI enemy)
        {
            yield return StartCoroutine(MoveToTarget(enemy.gameObject));
            weaponSystem.AttackTarget(enemy.gameObject);
        }

        //IEnumerator MoveAndPowerAttack(EnemyAI enemy)
        //{
        //    yield return StartCoroutine(MoveToTarget(enemy.gameObject));
        //    abilities.AttemptSpecialAbility(0, enemy.gameObject);
        //}


    }
}