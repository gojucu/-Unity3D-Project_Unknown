//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//namespace RPG.CameraUI
//{
//    [RequireComponent(typeof(CameraRaycaster))]
//    public class CursorAffordance : MonoBehaviour
//    {
        
//        [SerializeField] Texture2D targetCursor = null;
//        [SerializeField] Texture2D unkownCursor = null;
//        [SerializeField] Vector2 cursorHotspot = new Vector2(0, 0);

//        [SerializeField] const int walkableLayerNumber = 9;
//        [SerializeField] const int enemyLayerNumber = 10;
//        // [SerializeField] int walkableLayerNumber = 11;

//        CameraRaycaster cameraRaycaster;

//        // Use this for initialization
//        void Start()
//        {
//            cameraRaycaster = GetComponent<CameraRaycaster>();
//            cameraRaycaster.notifyLayerChangeObservers += OnLayerChanged;
//        }

//        void OnLayerChanged(int newLayer)
//        {
//            switch (newLayer)
//            {
                
//                case enemyLayerNumber:
//                    Cursor.SetCursor(targetCursor, cursorHotspot, CursorMode.Auto);
//                    break;
//                default:
//                    Cursor.SetCursor(unkownCursor, cursorHotspot, CursorMode.Auto);

//                    return;
//            }
//        }
//    }
//}
