using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator m_Animator;
   private const string IS_WALKING = "IsWalking";
    [SerializeField]
    private Player m_Player;
    private void Awake()
    {
       m_Animator = GetComponent<Animator>();
       
    }
    private void Update()
    {
        m_Animator.SetBool(IS_WALKING, m_Player.IsWalking());
    }
}
