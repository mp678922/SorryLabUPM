using UnityEngine;
namespace SorryLab.Editor.Example {
    public class AssetBundleBuilderHandlerExample : AssetBundleBuilderHandler {
        /*
        打包assetBundle前的處理辦法，
        例如把材質球拿掉節省資源。
        不會影響原始Prefab。
        */
        public override void Handle(GameObject gameObject) {
            //Clipboard.Write(gameObject.name);
            //由於建置AssetBundle會清除Log，故使用此方式留存資訊
        }
    }

}
