using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SorryLab.Easing;
using SorryLab;
namespace SorryLab.Expansion {
    static class TransformExpansion {
        public static void LookAt2D(this Transform self, Transform target) {
            self.LookAt2D(target.position);
        }

        public static void LookAt2D(this Transform self, Vector2 target) {
            Vector2 dir = (Vector2)self.position - target;
            float rot_z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            self.rotation = Quaternion.Euler(0f, 0f, rot_z);
        }

        public static void SetPositionX(this Transform self, float x) {
            Vector3 pos = self.position;
            pos.x = x;
            self.position = pos;
        }
        public static void SetPositionY(this Transform self, float y) {
            Vector3 pos = self.position;
            pos.y = y;
            self.position = pos;
        }
        public static void SetPositionZ(this Transform self, float z) {
            Vector3 pos = self.position;
            pos.z = z;
            self.position = pos;
        }

        public static Coroutine Move(this Transform self, Vector3 offest, float time, EaseType easeType = EaseType.Linear, Space relativeTo = Space.World, bool usingRealTime = false) {
            return StaticCoroutine.StartCoroutine(MoveAsync(self, offest, time, easeType, relativeTo, usingRealTime));
        }

        public static Coroutine MoveTo(this Transform self, Vector3 target, float time, EaseType easeType = EaseType.Linear, Space relativeTo = Space.World, bool usingRealTime = false) {
            Vector2 offest = target - self.position;
            return StaticCoroutine.StartCoroutine(MoveAsync(self, offest, time, easeType, relativeTo, usingRealTime));
        }

        static IEnumerator MoveAsync(Transform transform, Vector3 offest, float time, EaseType easeType, Space relativeTo, bool usingRealTime) {
            if (time > 0f) {
                float lastFrameProgress = 0f;
                for (float i = 0f; i < time; i += usingRealTime ? Time.unscaledDeltaTime : Time.deltaTime) {
                    float progress = i / time;
                    Vector3 movePerFrame = Ease.Lerp(Vector3.zero, offest, progress, easeType) - Ease.Lerp(Vector3.zero, offest, lastFrameProgress, easeType);
                    transform.Translate(movePerFrame, relativeTo);
                    lastFrameProgress = progress;
                    yield return null;
                }
                Vector3 endFrameMove = Ease.Lerp(Vector3.zero, offest, 1f, easeType) - Ease.Lerp(Vector3.zero, offest, lastFrameProgress, easeType);
                transform.Translate(endFrameMove, relativeTo);
            } else {
                transform.Translate(offest, relativeTo);
            }
        }
    }
}