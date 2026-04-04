using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Player player;
    private float footStepTimer;
    private float footStepTimeMax = .1f;
    

    private void Awake()
    {
        player = GetComponent<Player>();
       
    }
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        footStepTimer -= Time.deltaTime;


        if (player.IsWalking())
        {
            if (footStepTimer < 0f)
            {
                float volume = 5f;
                footStepTimer = footStepTimeMax;
                SoundManager.Instance.PlayPlayerFootsteps(player.transform.position, volume);
            }
        }
    }
}
