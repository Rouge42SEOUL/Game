using UnityEngine;


namespace RougeEvent
{
    public abstract class Event : MonoBehaviour // namespace 
    {
        private bool _isDisplay = false;
        public string Name { get; protected set; } // Enum으로 교체

        public void UIControl()
        {
            if (_isDisplay == false)
                DisplayEvent();
            else
                CloseEvent();
        }

        protected abstract void BuildUI();

        private void Start()
        {
            CloseEvent();
        }

        private void DisplayEvent()
        {
            gameObject.SetActive(true);
            _isDisplay = true;
        }

        private void CloseEvent()
        {
            gameObject.SetActive(false);
            _isDisplay = false;
        }
    }
}