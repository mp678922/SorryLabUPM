using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SorryLab.Spring;
namespace SorryLab.Spring.Demo {
    public class SpringDemo : MonoBehaviour {

        public RectTransform target;
        public SpringVector2 sv2;
        void Start() {
            sv2 = new SpringVector2(target.anchoredPosition, Input.mousePosition, 0.1f, Mathf.PI * 2f);
        }
        void Update() {
            target.anchoredPosition = sv2.Update(Input.mousePosition);
        }

    }
}