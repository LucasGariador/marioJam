using UnityEngine;
using TMPro;

public class AmmoCountUI : MonoBehaviour
{
    private TMP_Text m_Text;
    void Start()
    {
        m_Text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Text.text = "" + GameManager.instance.GetAmmo();
    }
}
