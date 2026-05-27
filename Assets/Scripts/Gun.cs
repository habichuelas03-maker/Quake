using UnityEngine;
 
public class Gun : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Rotate rotateScript;
    [SerializeField]
    private GunData gunData;
    [SerializeField]
    private Transform bulletPivot;
    [SerializeField]
    private GameObject bulletPrefab;
    public void GrabGun(Transform gunPosition)
    {
        transform.SetParent(gunPosition);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        animator.Play("Grab", 0, 0f);
        rotateScript.canRotate = false;
        gameObject.GetComponent<Collider>().enabled = false;
     }
     public void Shoot()
    {
        float rayDistance = 1000f;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(rayDistance);
        }
        Vector3 direction = (targetPoint - transform.position).normalized;
        bulletPivot.forward = direction;
        GameObject bullet = Instantiate(bulletPrefab, bulletPivot.position, bulletPivot.rotation);
        bullet.transform.LookAt(targetPoint);
        animator.Play("Shoot", 0, 0f);
    }
}
 