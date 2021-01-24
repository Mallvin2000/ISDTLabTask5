using System.Collections;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public float fireWait = 0.1f;
    public GameObject projectilePrefab = null;

    private Weapon weapon = null;

    private Coroutine firingRoutine = null;
    private WaitForSeconds wait = null;
    
    public void Setup(Weapon weapon)
    {
        this.weapon = weapon;
    }

    private void Awake()
    {
        wait = new WaitForSeconds(fireWait);
    }

    public void StartFiring()
    {
        firingRoutine = StartCoroutine(FiringSequence());
    }

    // Fire projectile with recoil every 0.1s
    private IEnumerator FiringSequence()
    {
        while (gameObject.activeSelf)
        {
            CreateProjectile();
            weapon.ApplyRecoil();
            yield return wait;
        }
    }

    private void CreateProjectile()
    {
        // Can improve this with pooling
        GameObject projectileObject = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch();
    }

    public void StopFiring()
    {
        StopCoroutine(firingRoutine);
    }

    public void firecartridge() 
    {
        CreateProjectile();
        weapon.ApplyRecoil();
    }
}
