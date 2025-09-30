using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => {
            AudioManager.Instance.PlaySound(AudioManager.Sound.Click);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
