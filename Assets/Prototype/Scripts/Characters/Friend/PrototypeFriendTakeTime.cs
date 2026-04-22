using UnityEngine;

public class PrototypeFriendTakeTime : MonoBehaviour
{
    private Animator animator;
    private PrototypeFriend friendScript;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        friendScript = GetComponentInParent<PrototypeFriend>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PrototypePlayerController player = collider.GetComponent<PrototypePlayerController>();

        if (player != null)
        {

            friendScript.CanMove = false;
            animator.SetBool("TakeTime", true);

            // reduz o tempo do jogador em 1 segundo
            PrototypeGameManager.Instance.DeductTime(1f);

        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        PrototypePlayerController player = collider.GetComponent<PrototypePlayerController>();

        if (player != null)
        {
            friendScript.CanMove = true;
            animator.SetBool("TakeTime", false);
        }
    }
}