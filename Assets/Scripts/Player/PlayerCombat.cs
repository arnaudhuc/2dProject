using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
  private Animator anim;

  private Rigidbody2D rb;

  [SerializeField]
  private bool combatEnabled;

  [SerializeField]
  private float inputTimer, attack1Radius, attack1Damage;

  [SerializeField]
  private Transform attack1HitBoxPos;

  [SerializeField]
  private LayerMask whatIsDamageable;

  private bool gotInput, isAttacking, isFirstAttack;

  private float lastInputTime = Mathf.NegativeInfinity;

  private void Start()
  {
    anim = GetComponent<Animator>();
    anim.SetBool("canAttack", combatEnabled);
    rb = GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
    CheckCombatInput();
    CheckAttacks();
  }

  private void CheckCombatInput()
  {
    if (Input.GetButtonDown("Fire1"))
    {
      if (combatEnabled)
      {
        //Attempt combat
        gotInput = true;
        lastInputTime = Time.time;
      }
    }
  }

  private void CheckAttacks()
  {
    if (gotInput)
    {
      //Perform Attack1
      if (!isAttacking)
      {
        gotInput = false;
        isAttacking = true;
        isFirstAttack = !isFirstAttack;
        anim.SetBool("attack1", true);
        anim.SetBool("firstAttack", isFirstAttack);
        anim.SetBool("isAttacking", isAttacking);
      }
    }

    if (Time.time >= lastInputTime + inputTimer)
    {
      //Wait for new input
      gotInput = false;
    }
  }

  private void CheckAttackHitBox()
  {
    Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamageable);

    foreach (Collider2D collider in detectedObjects)
    {
      collider.transform.parent.SendMessage("Damage", attack1Damage);
      //Instantiate hit particle
    }
  }

  private void FinishAttack1()
  {
    isAttacking = false;
    anim.SetBool("isAttacking", isAttacking);
    anim.SetBool("attack1", false);
  }

  private void OnDrawGizmos()
  {
    Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
  }
}
