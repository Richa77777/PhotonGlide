using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class RecordLoader : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<TextMeshProUGUI>().text = "Record: " + PlayerPrefs.GetInt("Record", 0);
    }
}
