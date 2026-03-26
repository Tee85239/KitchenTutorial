using UnityEngine;

public class ContainerVisual : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private ContainerCounter containerCounter;
  private Animator animator;

    private const string OPEN_CLOSED = "OpenClose";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.onPlayerGrabObject += ContainerCounter_OnPlayerGrabbedObject;
              
    }

    private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSED);
    }


}
