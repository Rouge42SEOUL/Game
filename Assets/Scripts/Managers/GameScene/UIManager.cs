using UnityEngine;

namespace GameScene {
	public partial class UIManager : MonoBehaviour {
		private EventUI[] _events;
		private GameObject _eventUI;
		public GoldUI goldUI;

		private void Start() 
		{
			_events = FindObjectsOfType<EventUI>();
			_eventUI = GameObject.Find("EventSelectionWindow");
			goldUI = FindObjectOfType<GoldUI>();
			_eventUI.gameObject.SetActive(false);
		}
		public void OptionUIControl()
		{
			_eventUI.gameObject.SetActive(!_eventUI.gameObject.activeSelf);
		}
	}
	
	public partial class UIManager // singleton
	{
		private static UIManager _instance;
		public static UIManager Instance
		{
			get
			{
				if (_instance == null)
				{
					UIManager obj = FindObjectOfType<UIManager>();
					if (obj != null)
					{
						_instance = obj;
					}
					else
					{
						UIManager newObj = new GameObject().AddComponent<UIManager>();
						_instance = newObj;
					}
				}
				return _instance;
			}
		}
		private void Awake()
		{
			UIManager[] obj = FindObjectsOfType<UIManager>();
			if (obj.Length != 1)
			{
				Destroy(gameObject);
				return;
			}
		}
	}
}
