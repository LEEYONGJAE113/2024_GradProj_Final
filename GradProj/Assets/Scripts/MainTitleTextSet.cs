using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTitleTextSet : MonoBehaviour
{
    private string[] _title;
    private Text _text;
    
    void Awake()
    {
        _text = GetComponentInChildren<Text>();
        _title = new string[2];
        _title[0] = "<color=#569CD6>int</color>"
                    + "<color=#F5F591> main</color>"
                    + "<color=#B95AA5>()\n{\n</color>"
                    + "<color=#F5F591>  printf</color>"
                    + "<color=#569CD6>(</color>"
                    + "<color=#CD9178>\"Hello, Error!\\n\"</color>"
                    + "<color=#569CD6>)</color><color=#FFFFFF>;\n</color>"
                    + "<color=#B95AA5>}</color>";
        _title[1] = "<color=#A400C0>print</color>"
                    + "<color=#FFFFFF>(</color>"
                    + "<color=#00B50D>\'Hello, Error!\'</color>"
                    + "<color=#FFFFFF>)</color>";

        _text.text = _title[0];
    }

    public void OnClick()
    {
        int idx = Random.Range(0, _title.Length);
        _text.text = _title[idx];
    }
}
