using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
namespace SorryLab {
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TMP_LinkHandler : MonoBehaviour, IPointerClickHandler {
        TMP_Text text => GetComponent<TMP_Text>();
        Dictionary<string, Action> _actions = new Dictionary<string, Action>();
        public void OnPointerClick(PointerEventData eventData) {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, eventData.position, eventData.pressEventCamera);
            if (linkIndex != -1) {
                TMP_LinkInfo linkInfo = text.textInfo.linkInfo[linkIndex];
                string action = linkInfo.GetLinkID();
                if (_actions.ContainsKey(action)) {
                    _actions[action].Invoke();
                }
            }
        }
        public void AddLinkAction(string actionName, Action action) {
            _actions[actionName] = action;
        }
        public void ClearActions() {
            _actions.Clear();
        }
    }
}