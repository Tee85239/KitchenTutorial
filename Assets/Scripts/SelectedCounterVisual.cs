using UnityEngine;

public class Selected : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    private BaseCounter baseCounter;
    [SerializeField]
    private GameObject[] selectedGameObjectArray;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChange += Instance_OnSelectedCounterChange;
    }

    private void Instance_OnSelectedCounterChange(object sender, Player.OnSelectedCounterChangeEventArgs e)
    {
        if (e.selectedCounter == baseCounter) {
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
        foreach (GameObject visualGameObject in selectedGameObjectArray)
        {
            visualGameObject.SetActive(true);
        }
    }
    private void Hide() 
    {
        foreach (GameObject visualGameObject in selectedGameObjectArray)
        {
            visualGameObject.SetActive(false);
        }
    }
}
