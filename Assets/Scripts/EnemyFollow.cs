using UnityEngine;
using System.Collections;
 
public class EnemyFollow : Enemy
{
   [SerializeField]
   private float speed = 3f;
   [SerializeField]
   private float yPosition = 2f;
   [SerializeField]  
   private float pushForce =5f;
   private bool isFollowing = true;
   public override void OnEnable()
    {
        base.OnEnable();
        animator.Play("Appear", 0, 0f);
        isFollowing = true;
        SoundManager.instance.Play("monster_appear");
    }
    public override void TakeDamage()
    {
        SoundManager.instance.Play("monster_demage");
        if(!isFollowing) return;
        isFollowing = false;
        base.TakeDamage();
        StartCoroutine(StopAndFollow());
    }
    private IEnumerator StopAndFollow()
    {
        yield return null;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        isFollowing = true;
    }
    public override void Die()
    {
        SoundManager.instance.Play("monster_die");
        isFollowing = false;
        base.Die();
    }
    private void Update()
    {
        if (!isFollowing) return;
        Vector3 targetPosition = new Vector3(player.position.x, yPosition, player.position.z);
        transform.position = Vector3.MoveTowards(transform.position,
        targetPosition, speed * Time.deltaTime);
        transform.LookAt(targetPosition);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().PushBack(transform, pushForce);
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
 
 