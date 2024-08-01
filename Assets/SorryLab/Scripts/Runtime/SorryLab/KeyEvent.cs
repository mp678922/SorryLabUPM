using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace SorryLab {
    public class KeyEvent {

        public KeyCode m_keyCode;

        public Func<bool> condition;
        public Action onGetKeyDown;
        public Action onGetKeyKeep;
        public Action onGetKeyUp;
        public Action<Vector2> onGetMouseDrag;

        public KeyCode[] m_combinationKey;

        bool m_isKeyDown = false;
        bool m_dragging = false;
        Vector2 dragStartPoint;

        Graphic m_graphic;

        Collider2D m_collider2D;
        Camera m_camera;

        StaticCoroutine m_updateEvent;

        public KeyEvent(KeyCode key, params KeyCode[] combinationKey) {
            m_keyCode = key;
            m_combinationKey = combinationKey;
            m_updateEvent = StaticCoroutine.UpdateInvoke(Update);
        }

        //對應UGUI，在ui範圍內才會運作
        public KeyEvent SetActionGraphic(Graphic graphic) {
            m_collider2D = null;
            m_graphic = graphic;
            return this;
        }

        //對應Collider2D，在碰撞範圍內才會運作
        public KeyEvent SetActionCollider2D(Collider2D collider2D, Camera camera = null) {
            m_graphic = null;
            m_collider2D = collider2D;
            m_camera = camera;
            return this;
        }

        public KeyEvent SetEvents(Action onKeyDown, Action onKeyKeep = null, Action onKeyUp = null, Action<Vector2> onKeyDrag = null) {
            this.onGetKeyDown = onKeyDown;
            this.onGetKeyKeep = onKeyKeep;
            this.onGetKeyUp = onKeyUp;
            this.onGetMouseDrag = onKeyDrag;
            return this;
        }

        public void Update() {

            bool isGetCombinationKey = true;

            if (m_combinationKey.Length > 0) {
                for (int i = 0; i < m_combinationKey.Length; i++) {
                    if (!Input.GetKey(m_combinationKey[i])) {
                        isGetCombinationKey = false;
                        break;
                    }
                }
            }

            if (m_isKeyDown && Input.GetKeyUp(m_keyCode)) {
                if (onGetKeyUp != null) { onGetKeyUp(); }
                m_dragging = false;
            }

            if (m_isKeyDown && Input.GetKey(m_keyCode)) {
                if (onGetKeyKeep != null) { onGetKeyKeep(); }
                if (m_dragging) {
                    if (onGetMouseDrag != null) {
                        onGetMouseDrag((Vector2)Input.mousePosition - dragStartPoint);
                    }
                }
            }

            if (isGetCombinationKey && Input.GetKeyDown(m_keyCode)) {

                //若有設定graphic，滑鼠沒有在graphic裡則不執行
                if (m_graphic != null && !IsPointInRectTransform(Input.mousePosition, m_graphic.rectTransform)) {
                    return;
                }

                //若有設定collider2D，滑鼠沒有在collider2D裡則不執行
                if (m_collider2D != null) {
                    Camera camera = (m_camera != null) ? m_camera : Camera.main;
                    if (!m_collider2D.OverlapPoint(camera.ScreenToWorldPoint(Input.mousePosition))) {
                        return;
                    }
                }

                //所有設定條件，則會判斷阻止
                if (condition != null) {
                    if (!condition()) {
                        return;
                    }
                }

                if (onGetKeyDown != null) { onGetKeyDown(); }
                m_isKeyDown = m_dragging = true;
                dragStartPoint = Input.mousePosition;
            }

        }

        public void Kill() {
            if (m_updateEvent != null) {
                m_updateEvent.Kill();
            }
        }

        static bool IsPointInRectTransform(Vector2 point, RectTransform rectTransform) {

            point = point - new Vector2(Screen.width / 2, Screen.height / 2);

            Rect rect = rectTransform.rect;

            float leftSide = rectTransform.anchoredPosition.x - rect.width / 2;
            float rightSide = rectTransform.anchoredPosition.x + rect.width / 2;
            float topSide = rectTransform.anchoredPosition.y + rect.height / 2;
            float bottomSide = rectTransform.anchoredPosition.y - rect.height / 2;

            if (point.x >= leftSide &&
                point.x <= rightSide &&
                point.y >= bottomSide &&
                point.y <= topSide) {
                return true;
            }
            return false;
        }

    }
}
