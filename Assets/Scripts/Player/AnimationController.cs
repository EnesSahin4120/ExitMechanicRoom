using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private ThrowController throwController;

    private void OnEnable()
    {
        DoorController.onOpenedDoor += SetWalking;
        ThrowController.onThrowed += SetThrowing;
    }

    private void OnDisable()
    {
        DoorController.onOpenedDoor -= SetWalking;
        ThrowController.onThrowed -= SetThrowing;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //Animation Event
    public void Throw()
    {
        Vector3 _target = throwController.aim.transform.position; 
        Vector3 _origin = throwController.glass.transform.position;

        throwController.glass.transform.SetParent(null);
        throwController.glass.isKinematic = false;
        throwController.glass.velocity = throwController.CalculateVelocity(_target, _origin, 1f);
    }

    private void SetWalking()
    {
        animator.SetTrigger("Walk");
    }

    private void SetThrowing()
    {
        animator.SetTrigger("Throw");
    }
}
