using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private AudioSO audioSO;

    public static SoundManager Instance { get; private set; }
   
   

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Recipemanager.Instance.OnRecipeSuccess += RecipeManager_OnRecipeSuccess;
        Recipemanager.Instance.OnRecipeFailed += RecipeManager_OnRecipeFailed;
        CuttingCounter.onAnyCut += CuttingCounter_onAnyCut;
        Player.Instance.objectGetSound += Instance_objectGetSound;
        BaseCounter.OnAnyObjectPlaced += BaseCounter_OnAnyObjectPlaced;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlaced(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioSO.objectDrop, baseCounter.transform.position);
    }

    private void Instance_objectGetSound(object sender, System.EventArgs e)
    {
       PlaySound(audioSO.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_onAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioSO.chop, cuttingCounter.transform.position);
    }

    private void RecipeManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
       PlaySound(audioSO.deliveryfail, deliveryCounter.transform.position);
    }

    private void RecipeManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        PlaySound(audioSO.deliverysuccess, Camera.main.transform.position);
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioSO.deliveryfail, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 15f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);

    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 15f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);

    }

    public void PlayPlayerFootsteps(Vector3 pos, float volume)
    {
        PlaySound(audioSO.footsteps, pos);
    }


}
