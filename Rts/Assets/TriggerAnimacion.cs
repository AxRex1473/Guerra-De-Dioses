using UnityEngine;

public class DestinationArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Animator animator = other.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Animator animator = other.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("isWalking", true);
        }
    }
}
