using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestSuite
    {
        /*[UnityTest]
        public IEnumerator ConversionIntBoolArray()
        {
            bool[] testArray = { true, false, false, true, false };
            int converted = Util.BoolArrayToInt(testArray);
            bool[] finalArray = Util.IntToBoolArray(converted, testArray.Length);

            Assert.AreEqual(testArray, finalArray);

            return null;
        }*/

        private static string jsonScene = @"{""environment"":""Classroom"",""saveData"":[{""id"":1,""parent"":0,""name"":""Box"",""model"":""Box"",""texture"":""bricks"",""color"":""BE00FFFF"",""position"":{""x"":-1.2934155464172363,""y"":1.686502456665039,""z"":1.5952855348587036},""rotation"":{""x"":0,""y"":0,""z"":0,""w"":1},""scale"":{""x"":0.19999998807907104,""y"":0.19999998807907104,""z"":0.19999998807907104},""isVisible"":1,""isInteractable"":false,""physics"":{""mass"":1,""drag"":0,""angularDrag"":0.05000000074505806},""text"":{""text"":"""",""size"":20,""color"":"""",""horizontalAligment"":0,""verticalAligment"":0},""code"":{""blocks"":[{""id"":168,""rootObject"":true,""blockType"":""ReceiveEvent"",""editorPosition"":{""x"":32.69476318359375,""y"":216.674072265625},""paramInt"":0,""paramString"":""m"",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[169,171,172,173]},{""id"":169,""rootObject"":false,""blockType"":""SetTextLook"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[170],""blocksIds"":[]},{""id"":170,""rootObject"":false,""blockType"":""StringVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":""u"",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":171,""rootObject"":false,""blockType"":""SetColorLook"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":""FFFFFFFF"",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":172,""rootObject"":false,""blockType"":""HideLook"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":173,""rootObject"":false,""blockType"":""DestroyControl"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":174,""rootObject"":true,""blockType"":""StartEvent"",""editorPosition"":{""x"":-248.08938598632812,""y"":236.2161865234375},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[175,176,178,180,182,188,196,202]},{""id"":175,""rootObject"":false,""blockType"":""ShowLook"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":176,""rootObject"":false,""blockType"":""PointsTowardMotion"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":2,""childBlocksIds"":[],""attachedBlocksIds"":[177],""blocksIds"":[]},{""id"":177,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":1,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":178,""rootObject"":false,""blockType"":""WaitControl"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[179],""blocksIds"":[]},{""id"":179,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":1,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":180,""rootObject"":false,""blockType"":""MoveMotion"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":2,""childBlocksIds"":[],""attachedBlocksIds"":[181],""blocksIds"":[]},{""id"":181,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":1,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":182,""rootObject"":false,""blockType"":""SetVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":""n"",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[183],""blocksIds"":[]},{""id"":183,""rootObject"":false,""blockType"":""AddVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[184,185],""blocksIds"":[]},{""id"":184,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":2,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":185,""rootObject"":false,""blockType"":""SubtractVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[186,187],""blocksIds"":[]},{""id"":186,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":1,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":187,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":1,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":188,""rootObject"":false,""blockType"":""IfControl"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[194],""attachedBlocksIds"":[189],""blocksIds"":[]},{""id"":189,""rootObject"":false,""blockType"":""EqualsCondition"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[190,191],""blocksIds"":[]},{""id"":190,""rootObject"":false,""blockType"":""Variable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":""n"",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":191,""rootObject"":false,""blockType"":""MultiplyVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[192,193],""blocksIds"":[]},{""id"":192,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":2,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":193,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":1,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":194,""rootObject"":false,""blockType"":""SetVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":""n"",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[195],""blocksIds"":[]},{""id"":195,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":1,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":196,""rootObject"":false,""blockType"":""SetVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":""n"",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[197],""blocksIds"":[]},{""id"":197,""rootObject"":false,""blockType"":""MultiplyVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[198,199],""blocksIds"":[]},{""id"":198,""rootObject"":false,""blockType"":""Variable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":""n"",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":199,""rootObject"":false,""blockType"":""DivideVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[200,201],""blocksIds"":[]},{""id"":200,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":1,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":201,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":1,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":202,""rootObject"":false,""blockType"":""BroadcastEvent"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":""m"",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":203,""rootObject"":true,""blockType"":""OnEnterEvent"",""editorPosition"":{""x"":35.50836181640625,""y"":-24.58392906188965},""paramInt"":0,""paramString"":""trigger"",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[204]},{""id"":204,""rootObject"":false,""blockType"":""SetVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":""n"",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[205],""blocksIds"":[]},{""id"":205,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":1,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":206,""rootObject"":true,""blockType"":""OnExitEvent"",""editorPosition"":{""x"":79.71929931640625,""y"":-243.4313201904297},""paramInt"":0,""paramString"":""trigger"",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[207]},{""id"":207,""rootObject"":false,""blockType"":""SetVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":""n"",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[208],""blocksIds"":[]},{""id"":208,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":1,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]}]},""tag"":""a""},{""id"":3,""parent"":1,""name"":""Sphere"",""model"":""Sphere"",""texture"":"""",""color"":"""",""position"":{""x"":-1.3174158334732056,""y"":2.1405038833618164,""z"":1.5952856540679932},""rotation"":{""x"":0,""y"":0,""z"":0,""w"":1},""scale"":{""x"":0.9999998807907104,""y"":0.9999998807907104,""z"":0.9999998807907104},""isVisible"":1,""isInteractable"":false,""physics"":{""mass"":1,""drag"":0,""angularDrag"":0.05000000074505806},""text"":{""text"":"""",""size"":20,""color"":"""",""horizontalAligment"":0,""verticalAligment"":0},""code"":{""blocks"":[]},""tag"":""""},{""id"":2,""parent"":0,""name"":""Banana_pill"",""model"":""Banana_pill"",""texture"":"""",""color"":"""",""position"":{""x"":0.9476202130317688,""y"":1.5817348957061768,""z"":1.600000023841858},""rotation"":{""x"":-0.7071068286895752,""y"":0,""z"":0,""w"":0.7071067094802856},""scale"":{""x"":0.13386249542236328,""y"":0.13386249542236328,""z"":0.13386249542236328},""isVisible"":1,""isInteractable"":false,""physics"":{""mass"":1,""drag"":0,""angularDrag"":0.05000000074505806},""text"":{""text"":"""",""size"":20,""color"":"""",""horizontalAligment"":0,""verticalAligment"":0},""code"":{""blocks"":[]},""tag"":""""},{""id"":1,""parent"":0,""name"":""Empty"",""model"":""Empty"",""texture"":"""",""color"":"""",""position"":{""x"":-0.24140864610671997,""y"":1.5781581401824951,""z"":1.5725278854370117},""rotation"":{""x"":0,""y"":0,""z"":0,""w"":1},""scale"":{""x"":0.037275705486536026,""y"":0.037275709211826324,""z"":0.037275705486536026},""isVisible"":1,""isInteractable"":false,""physics"":{""mass"":1,""drag"":0,""angularDrag"":0.05000000074505806},""text"":{""text"":"""",""size"":20,""color"":"""",""horizontalAligment"":0,""verticalAligment"":0},""code"":{""blocks"":[]},""tag"":""trigger""}],""globalData"":{""variables"":[""n""],""messages"":[""m""]}}";

        [UnityTest]
        public IEnumerator SaveLoadScene()
        {
            SceneManager.LoadScene("Main");

            yield return new WaitForSeconds(1.0f);

            AppManager.instance.currentSceneName = "unitTestScene";

            using (var writer = new StreamWriter(File.Open(AppManager.instance.GetScenePath(), FileMode.Create)))
            {
                writer.Write(jsonScene);
            }

            yield return new WaitForSeconds(1.0f);

            SceneManager.LoadScene("Editor");

            yield return new WaitForSeconds(1.0f);

            ScioXRSceneManager.instance.SaveScene(AppManager.instance.GetScenePath());

            yield return new WaitForSeconds(1.0f);

            EditorButtonsListener buttonListener = GameObject.FindObjectOfType<EditorButtonsListener>();
            buttonListener.toggleCanvas.Toggle();

            yield return new WaitForSeconds(0.2f);
            XRTabGroup mainMenuTabs = buttonListener.toggleCanvas.panel as XRTabGroup;
            mainMenuTabs.SelectTab(0);

            yield return new WaitForSeconds(0.5f);

            mainMenuTabs.SelectTab(1);

            yield return new WaitForSeconds(0.5f);

            mainMenuTabs.SelectTab(2);

            yield return new WaitForSeconds(0.1f);

            //Select environment tab
            mainMenuTabs.tabs[2].GetComponentInChildren<XRTabGroup>().SelectTab(0);

            //EditorManager.instance.NextTransformMode();

            yield return new WaitForSeconds(0.5f);

            buttonListener.toggleCanvas.Toggle();

            GameObject firstObject = GameObject.Find("Box");

            //Open properties with controller
            WebXRInteractor controller = GameObject.FindObjectOfType<WebXRInteractor>();
            EditorManager.instance.NextTransformMode();
            IWebXRInteractable editorInteractable = firstObject.GetComponent<IWebXRInteractable>();
            editorInteractable.OnSecondaryGrab(controller);
            yield return new WaitForSeconds(0.1f);
            editorInteractable.OnSecondaryUngrab(controller);


            //EditorManager.instance.ToggleProperties(firstObject);

            EditorManager.instance.selectedObject = firstObject;

            yield return new WaitForSeconds(0.5f);

            XRTabGroup propertiesTabs = EditorManager.instance.propertiesMenu.panel as XRTabGroup;

            propertiesTabs.SelectTab(1);

            yield return new WaitForSeconds(0.5f);

            propertiesTabs.SelectTab(2);

            yield return new WaitForSeconds(0.5f);

            //Test block drag
            BlockEditor blockEditor = GameObject.FindObjectOfType<BlockEditor>();
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            blockEditor.OnBeginDrag(eventData);
            blockEditor.OnDrag(eventData);
            blockEditor.OnEndDrag(eventData);

            yield return new WaitForSeconds(0.1f);

            BlockEditor[] allblocks = GameObject.FindObjectsOfType<BlockEditor>();
            BlockEditor conditionBlock = null;
            foreach (var item in allblocks)
            {
                if (item.blockGroup == BlockEditor.BlockGroup.CONDITION)
                {
                    conditionBlock = item;
                    break;
                }
            }
            conditionBlock.OnBeginDrag(eventData);
            conditionBlock.OnDrag(eventData);
            conditionBlock.OnEndDrag(eventData);

            yield return new WaitForSeconds(0.1f);

            VariableEditor variableEditor = GameObject.FindObjectOfType<VariableEditor>();
            variableEditor.OnBeginDrag(eventData);
            variableEditor.OnDrag(eventData);
            variableEditor.OnEndDrag(eventData);

            yield return new WaitForSeconds(0.1f);

            //Test creating and deleting of variables
            BlocksBoard blocksBoard = GameObject.FindObjectOfType<BlocksBoard>();
            blocksBoard.variableName.text = "testVariable";
            blocksBoard.CreateVariable();
            blocksBoard.DeleteVariable("testVariable");

            //Test creating and deleting of messages
            blocksBoard.messageName.text = "testMessage";
            blocksBoard.CreateMessage();
            blocksBoard.DeleteMessage("testMessage");

            //Test Json serialization
            CodeBoard codeBoard = GameObject.FindObjectOfType<CodeBoard>();
            codeBoard.SaveTest();
            codeBoard.LoadTest();

            EditorManager.instance.ToggleProperties(firstObject);

            yield return new WaitForSeconds(0.2f);

            //test WebXRInteractor


            controller.gameObject.transform.position = firstObject.transform.position;
            yield return new WaitForSeconds(0.2f);

            //controller.controller.triggerPressed.wasPressedThisFrame = true;

            controller.gameObject.transform.position = firstObject.transform.position + controller.gameObject.transform.forward;

            yield return new WaitForSeconds(0.5f);

            //Test controller in editor
            EditorSettings.instance.enableSnap = true;
            EditorSettings.instance.snapMoveStep = 0.1f;
            EditorSettings.instance.snapRotateStepX = 90f;
            EditorSettings.instance.snapRotateStepY = 90f;
            EditorSettings.instance.snapRotateStepZ = 90f;
            EditorSettings.instance.snapScaleStep = 0.1f;


            EditorManager.instance.NextTransformMode();
            EditorManager.instance.NextTransformMode();
            EditorManager.instance.NextTransformMode();
            EditorManager.instance.NextTransformMode();
            EditorManager.instance.NextTransformMode();

            editorInteractable.OnGrab(controller);
            yield return new WaitForSeconds(0.1f);
            EditorSettings.instance.enableSnap = false;
            yield return new WaitForSeconds(0.1f);
            editorInteractable.OnUngrab(controller);

            editorInteractable.OnSecondaryGrab(controller);
            EditorSettings.instance.enableSnap = true;

            yield return new WaitForSeconds(0.1f);

            WebXREditorInteractable editorInteractableObject = editorInteractable as WebXREditorInteractable;
            WebXREditorPivot[] gizmos = editorInteractableObject.gizmoScale.GetComponentsInChildren<WebXREditorPivot>();
            foreach (var gizmo in gizmos)
            {
                editorInteractable.OnSecondaryUngrab(controller);
                editorInteractableObject.SelectGizmo(gizmo);
                editorInteractable.OnSecondaryGrab(controller);
                editorInteractableObject.SelectGizmo(gizmo);
                yield return new WaitForSeconds(0.1f);
            }

            editorInteractable.OnSecondaryUngrab(controller);

            editorInteractable.OnTouch(controller);
            yield return new WaitForSeconds(0.5f);
            editorInteractable.OnUntouch(controller);

            //Clone test
            EditorManager.instance.NextTransformMode();

            editorInteractable.OnSecondaryGrab(controller);
            yield return new WaitForSeconds(0.1f);
            editorInteractable.OnSecondaryUngrab(controller);

            //Set parent test
            EditorManager.mode = TransformMode.SET_PARENT;

            EditorManager.instance.selectedObject = GameObject.Find("Banana_pill");
            editorInteractable.OnSecondaryGrab(controller);
            yield return new WaitForSeconds(0.1f);
            editorInteractable.OnSecondaryUngrab(controller);

            yield return new WaitForSeconds(0.1f);

            EditorManager.instance.selectedObject = firstObject;
            editorInteractable.OnSecondaryGrab(controller);
            yield return new WaitForSeconds(0.1f);
            editorInteractable.OnSecondaryUngrab(controller);

            //Test loading and play
            SceneManager.LoadScene("Player");

            yield return new WaitForSeconds(4.0f);

            yield return null;
        }

        [UnityTest]
        public IEnumerator AssetLoaderTest()
        {
            yield return AssetsLoader.GetEnvironmentList(result => { });
            yield return AssetsLoader.GetBasicModelsList(result => { });
            yield return AssetsLoader.GetBasicModelsList(result => { });
            yield return AssetsLoader.GetModelsList(result => { });
            yield return AssetsLoader.GetTexturesList(result => { });
            yield return AssetsLoader.GetModelsFromUrl("https://app.scioxr.com/StreamingAssets/Models/files.txt", result => { });
        }

        [UnityTest]
        public IEnumerator RectExtensionTest()
        {
            SceneManager.LoadScene("Main");

            yield return new WaitForSeconds(1.0f);

            RectTransform t1 = GameObject.Find("K6").GetComponent<RectTransform>();
            RectTransform t2 = GameObject.Find("K7").GetComponent<RectTransform>();

            t1.Overlaps(t2);
            t1.Overlaps(t2, true);
            t1.WorldRect();
            yield return null;
        }
    }
}
