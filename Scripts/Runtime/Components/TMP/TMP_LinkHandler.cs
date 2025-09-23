using System;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
namespace SorryLab {
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TMP_LinkHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
        TMP_Text _text => GetComponent<TMP_Text>();
        Dictionary<string, Action> _clickEvents = new Dictionary<string, Action>();
        Dictionary<string, bool> _isEnter = new Dictionary<string, bool>();
        Dictionary<string, Action> _enterEvents = new Dictionary<string, Action>();
        Dictionary<string, Action> _exitEvents = new Dictionary<string, Action>();

        public void OnPointerClick(PointerEventData eventData) {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(_text, eventData.position, eventData.pressEventCamera);
            if (linkIndex != -1) {
                TMP_LinkInfo linkInfo = _text.textInfo.linkInfo[linkIndex];
                string action = linkInfo.GetLinkID();
                if (_clickEvents.ContainsKey(action)) {
                    _clickEvents[action].Invoke();
                }
            }
        }
        public void OnPointerEnter(PointerEventData eventData) {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(_text, eventData.position, eventData.pressEventCamera);
            if (linkIndex != -1) {
                TMP_LinkInfo linkInfo = _text.textInfo.linkInfo[linkIndex];
                string action = linkInfo.GetLinkID();
                if (!_isEnter.ContainsKey(action)) { _isEnter[action] = false; }
                if (!_isEnter[action] && _enterEvents.ContainsKey(action)) {
                    _enterEvents[action].Invoke();
                }
                _isEnter[action] = true;
            }
        }
        public void OnPointerExit(PointerEventData eventData) {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(_text, eventData.position, eventData.pressEventCamera);
            if (linkIndex != -1) {
                TMP_LinkInfo linkInfo = _text.textInfo.linkInfo[linkIndex];
                string action = linkInfo.GetLinkID();
                if (!_isEnter.ContainsKey(action)) { _isEnter[action] = false; }
                if (_isEnter[action] && _enterEvents.ContainsKey(action)) {
                    _exitEvents[action].Invoke();
                }
                _isEnter[action] = false;
            }
        }
        void OnDisable() {
            foreach (var i in _isEnter) {
                if (i.Value) {
                    _exitEvents[i.Key].Invoke();
                    _isEnter[i.Key] = false;
                }
            }
        }
        public void AddClickListener(string actionName, Action action) {
            _clickEvents[actionName] = action;
        }
        public void AddEnterListener(string actionName, Action action) {
            _enterEvents[actionName] = action;
        }
        public void AddExitListener(string actionName, Action action) {
            _exitEvents[actionName] = action;
        }
        public void ClearAllEvents() {
            _clickEvents.Clear();
            _enterEvents.Clear();
            _exitEvents.Clear();
            _isEnter.Clear();
        }
    }
}