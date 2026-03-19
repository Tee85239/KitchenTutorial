using UnityEngine;

public class Selected : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    private ClearCounter clearCounter;
    [SerializeField]
    private GameObject selectedGameObject;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChange += Instance_OnSelectedCounterChange;
    }

    private void Instance_OnSelectedCounterChange(object sender, Player.OnSelectedCounterChangeEventArgs e)
    {
        if (e.selectedCounter == clearCounter) {
            Show();
        }
        else
        {
            Hide();
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    private void Show()
    {
        selectedGameObject.SetActive(true);
    }
    private void Hide() 
    {
        selectedGameObject.SetActive(false);
    }
}
