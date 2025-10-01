using UnityEngine;
using UnityEngine.UI;

public class HomeButtonController : MonoBehaviour
{
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => {
            AudioManager.Instance.PlaySound(AudioManager.Sound.Click);
            ScenesManager.Instance.LoadMainScene();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
