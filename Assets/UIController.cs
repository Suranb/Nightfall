using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{

  private Button _openButton;
  private Button _closeButton;
  private VisualElement _bottomContainer;

  // Start is called before the first frame update
  void Start()
  {

    var root = GetComponent<UIDocument>().rootVisualElement;
    _bottomContainer = root.Q<VisualElement>("Bottom_Container");
    _openButton = root.Q<Button>("Button_Open");
    _closeButton = root.Q<Button>("Button_Close");

  }

  // Update is called once per frame
  void Update()
  {

  }
}
