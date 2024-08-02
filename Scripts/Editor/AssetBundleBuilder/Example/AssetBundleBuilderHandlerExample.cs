using UnityEngine;
namespace SorryLab.Editor.Example {
    public class AssetBundleBuilderHandlerExample : AssetBundleBuilderHandler {
        /*
        打包assetBundle前的處理辦法，
        例如把材質球拿掉節省資源。
        不會影響原始Prefab。
        */
        public override void Handle(GameObject gameObject) {
            //Debug.Log(gameObject.name);
        }
    }

}
