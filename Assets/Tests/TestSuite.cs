using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

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

        private static string jsonScene = @"{""environment"":""Classroom"",""saveData"":[{""id"":1,""parent"":0,""name"":""Box"",""model"":""Box"",""texture"":""bricks"",""color"":""BE00FFFF"",""position"":{""x"":-1.2934155464172363,""y"":1.686502456665039,""z"":1.5952855348587036},""rotation"":{""x"":0,""y"":0,""z"":0,""w"":1},""scale"":{""x"":0.19999998807907104,""y"":0.19999998807907104,""z"":0.19999998807907104},""isVisible"":1,""isInteractable"":false,""physics"":{""mass"":1,""drag"":0,""angularDrag"":0.05000000074505806},""text"":{""text"":"""",""size"":20,""color"":"""",""horizontalAligment"":0,""verticalAligment"":0},""code"":{""blocks"":[{""id"":382,""rootObject"":true,""blockType"":""ReceiveEvent"",""editorPosition"":{""x"":32.69476318359375,""y"":216.674072265625},""paramInt"":0,""paramString"":""m"",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[383,385,386,387]},{""id"":383,""rootObject"":false,""blockType"":""SetTextLook"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[384],""blocksIds"":[]},{""id"":384,""rootObject"":false,""blockType"":""StringVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":""u"",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":385,""rootObject"":false,""blockType"":""SetColorLook"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":""FFFFFFFF"",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":386,""rootObject"":false,""blockType"":""HideLook"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":387,""rootObject"":false,""blockType"":""DestroyControl"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":388,""rootObject"":true,""blockType"":""StartEvent"",""editorPosition"":{""x"":-248.08938598632812,""y"":236.2161865234375},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[389,390,392,394,396,402,410,416]},{""id"":389,""rootObject"":false,""blockType"":""ShowLook"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":390,""rootObject"":false,""blockType"":""PointsTowardMotion"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":2,""childBlocksIds"":[],""attachedBlocksIds"":[391],""blocksIds"":[]},{""id"":391,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":1,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":392,""rootObject"":false,""blockType"":""WaitControl"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[393],""blocksIds"":[]},{""id"":393,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":1,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":394,""rootObject"":false,""blockType"":""MoveMotion"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":2,""childBlocksIds"":[],""attachedBlocksIds"":[395],""blocksIds"":[]},{""id"":395,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":1,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":396,""rootObject"":false,""blockType"":""SetVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":""n"",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[397],""blocksIds"":[]},{""id"":397,""rootObject"":false,""blockType"":""AddVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[398,399],""blocksIds"":[]},{""id"":398,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":2,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":399,""rootObject"":false,""blockType"":""SubtractVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[400,401],""blocksIds"":[]},{""id"":400,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":1,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":401,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":1,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":402,""rootObject"":false,""blockType"":""IfControl"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[408],""attachedBlocksIds"":[403],""blocksIds"":[]},{""id"":403,""rootObject"":false,""blockType"":""EqualsCondition"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[404,405],""blocksIds"":[]},{""id"":404,""rootObject"":false,""blockType"":""Variable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":""n"",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":405,""rootObject"":false,""blockType"":""MultiplyVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[406,407],""blocksIds"":[]},{""id"":406,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":2,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":407,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":1,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":408,""rootObject"":false,""blockType"":""SetVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":""n"",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[409],""blocksIds"":[]},{""id"":409,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":1,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":410,""rootObject"":false,""blockType"":""SetVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":""n"",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[411],""blocksIds"":[]},{""id"":411,""rootObject"":false,""blockType"":""MultiplyVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[412,413],""blocksIds"":[]},{""id"":412,""rootObject"":false,""blockType"":""Variable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":""n"",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":413,""rootObject"":false,""blockType"":""DivideVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[414,415],""blocksIds"":[]},{""id"":414,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":1,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":415,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":1,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":416,""rootObject"":false,""blockType"":""BroadcastEvent"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":""m"",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":417,""rootObject"":true,""blockType"":""OnEnterEvent"",""editorPosition"":{""x"":35.50836181640625,""y"":-24.58392906188965},""paramInt"":0,""paramString"":""trigger"",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[418]},{""id"":418,""rootObject"":false,""blockType"":""SetVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":""n"",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[419],""blocksIds"":[]},{""id"":419,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":1,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":420,""rootObject"":true,""blockType"":""OnExitEvent"",""editorPosition"":{""x"":79.71929931640625,""y"":-243.4313201904297},""paramInt"":0,""paramString"":""trigger"",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[421]},{""id"":421,""rootObject"":false,""blockType"":""SetVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":""n"",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[422],""blocksIds"":[]},{""id"":422,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":1,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]}]},""tag"":""a""},{""id"":3,""parent"":1,""name"":""Sphere"",""model"":""Sphere"",""texture"":"""",""color"":"""",""position"":{""x"":-1.3174158334732056,""y"":2.1405038833618164,""z"":1.5952856540679932},""rotation"":{""x"":0,""y"":0,""z"":0,""w"":1},""scale"":{""x"":0.9999998807907104,""y"":0.9999998807907104,""z"":0.9999998807907104},""isVisible"":1,""isInteractable"":false,""physics"":{""mass"":1,""drag"":0,""angularDrag"":0.05000000074505806},""text"":{""text"":"""",""size"":20,""color"":"""",""horizontalAligment"":0,""verticalAligment"":0},""code"":{""blocks"":[]},""tag"":""""},{""id"":2,""parent"":0,""name"":""Banana_pill"",""model"":""Banana_pill"",""texture"":"""",""color"":"""",""position"":{""x"":0.9476202130317688,""y"":1.5817348957061768,""z"":1.600000023841858},""rotation"":{""x"":-0.7071068286895752,""y"":0,""z"":0,""w"":0.7071067094802856},""scale"":{""x"":0.13386249542236328,""y"":0.13386249542236328,""z"":0.13386249542236328},""isVisible"":1,""isInteractable"":false,""physics"":{""mass"":1,""drag"":0,""angularDrag"":0.05000000074505806},""text"":{""text"":"""",""size"":20,""color"":"""",""horizontalAligment"":0,""verticalAligment"":0},""code"":{""blocks"":[{""id"":423,""rootObject"":true,""blockType"":""InteractEvent"",""editorPosition"":{""x"":-137.13877868652344,""y"":124.59197235107422},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[424,428]},{""id"":424,""rootObject"":false,""blockType"":""IfControl"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[425],""blocksIds"":[]},{""id"":425,""rootObject"":false,""blockType"":""GreaterCondition"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[426,427],""blocksIds"":[]},{""id"":426,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":1,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":427,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":1,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":428,""rootObject"":false,""blockType"":""IfControl"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[429],""blocksIds"":[]},{""id"":429,""rootObject"":false,""blockType"":""LessCondition"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":0,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[430,431],""blocksIds"":[]},{""id"":430,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":1,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]},{""id"":431,""rootObject"":false,""blockType"":""IntVariable"",""editorPosition"":{""x"":0,""y"":0},""paramInt"":1,""paramString"":"""",""objectReference"":0,""childBlocksIds"":[],""attachedBlocksIds"":[],""blocksIds"":[]}]},""tag"":""""},{""id"":1,""parent"":0,""name"":""Empty"",""model"":""Empty"",""texture"":"""",""color"":"""",""position"":{""x"":-0.24140864610671997,""y"":1.5781581401824951,""z"":1.5725278854370117},""rotation"":{""x"":0,""y"":0,""z"":0,""w"":1},""scale"":{""x"":0.037275705486536026,""y"":0.037275709211826324,""z"":0.037275705486536026},""isVisible"":1,""isInteractable"":false,""physics"":{""mass"":1,""drag"":0,""angularDrag"":0.05000000074505806},""text"":{""text"":"""",""size"":20,""color"":"""",""horizontalAligment"":0,""verticalAligment"":0},""code"":{""blocks"":[]},""tag"":""trigger""}],""globalData"":{""variables"":[""n""],""messages"":[""m""]}}";

        [UnityTest]
        public IEnumerator SaveLoadScene()
        {
            AssetsLoader.appUrl = "https://app.scioxr.com/";

            yield return new WaitForSeconds(1.0f);

            SceneManager.LoadScene("Main");

            yield return new WaitForSeconds(1.0f);

            AppManager.instance.currentSceneName = "unitTestScene";

            using (var writer = new StreamWriter(File.Open(AppManager.instance.GetScenePath(), FileMode.Create)))
            {
                writer.Write(jsonScene);
            }

            SceneManager.LoadScene("Editor");

            yield return new WaitForSeconds(1.0f);

            ScioXRSceneManager.instance.SaveScene(AppManager.instance.GetScenePath());

            yield return new WaitForSeconds(1.0f);

            //Open Main menu panel
            EditorButtonsListener buttonListener = GameObject.FindObjectOfType<EditorButtonsListener>();
            buttonListener.toggleCanvas.Toggle();

            yield return new WaitForSeconds(0.2f);
            XRTabGroup mainMenuTabs = buttonListener.toggleCanvas.panel as XRTabGroup;
            mainMenuTabs.SelectTab(0);

            yield return new WaitForSeconds(1.5f);

            ModelSelecter basicModelSelector = GameObject.FindObjectOfType<ModelSelecter>();
            basicModelSelector.CreateBasicModel();

            yield return new WaitForSeconds(0.5f);

            buttonListener.toggleCanvas.Toggle();

            mainMenuTabs.SelectTab(1);

            yield return new WaitForSeconds(1.5f);

            ModelSelecter modelSelector = GameObject.FindObjectOfType<ModelSelecter>();
            modelSelector.CreateModel();

            yield return new WaitForSeconds(0.5f);

            buttonListener.toggleCanvas.Toggle();

            mainMenuTabs.SelectTab(2);

            yield return new WaitForSeconds(0.1f);

            //Select environment tab
            mainMenuTabs.tabs[2].GetComponentInChildren<XRTabGroup>().SelectTab(0);

            yield return new WaitForSeconds(4.0f);

            mainMenuTabs.tabs[2].GetComponentInChildren<XRTabGroup>().SelectTab(1);

            yield return new WaitForSeconds(0.2f);

            mainMenuTabs.tabs[2].GetComponentInChildren<XRTabGroup>().SelectTab(2);

            yield return new WaitForSeconds(0.2f);

            mainMenuTabs.SelectTab(3);

            //EditorManager.instance.NextTransformMode();

            yield return new WaitForSeconds(0.5f);

            buttonListener.toggleCanvas.Toggle();

            GameObject firstObject = GameObject.Find("Box");

            //Open properties with controller
            WebXRInteractor controller = GameObject.FindObjectOfType<WebXRInteractor>();
            EditorManager.instance.NextTransformMode();
            EditorManager.instance.SetTransformMode(TransformMode.PROPERTIES);
            IWebXRInteractable editorInteractable = firstObject.GetComponent<IWebXRInteractable>();
            editorInteractable.OnSecondaryGrab(controller);
            yield return new WaitForSeconds(0.1f);
            editorInteractable.OnSecondaryUngrab(controller);


            //EditorManager.instance.ToggleProperties(firstObject);

            EditorManager.instance.selectedObject = firstObject;

            yield return new WaitForSeconds(0.5f);

            XRTabGroup propertiesTabs = EditorManager.instance.propertiesMenu.panel as XRTabGroup;

            propertiesTabs.SelectTab(0);

            yield return new WaitForSeconds(0.2f);

            VRKeyboardInput keyboardInput = GameObject.FindObjectOfType<VRKeyboardInput>();
            keyboardInput.OnSelect(null);
            keyboardInput.OnDeselect(null);

            propertiesTabs.tabs[0].GetComponentInChildren<XRTabGroup>().SelectTab(1);

            yield return new WaitForSeconds(0.2f);

            propertiesTabs.tabs[0].GetComponentInChildren<XRTabGroup>().SelectTab(2);

            yield return new WaitForSeconds(0.2f);

            propertiesTabs.SelectTab(1);

            yield return new WaitForSeconds(0.5f);

            propertiesTabs.SelectTab(2);

            yield return new WaitForSeconds(0.5f);

            PointerEventData eventData = new PointerEventData(EventSystem.current);

            //test spawner block drag
            BlockSpawner blockSpawner = GameObject.Find("VariableReference(Clone)").GetComponent<BlockSpawner>();
            blockSpawner.OnBeginDrag(eventData);
            blockSpawner.OnDrag(eventData);
            blockSpawner.OnEndDrag(eventData);

            yield return new WaitForSeconds(0.1f);

            //Test block drag
            BlockEditor blockEditor = GameObject.FindObjectOfType<BlockEditor>();
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

            keyboardInput = GameObject.FindObjectOfType<VRKeyboardInput>();
            keyboardInput.OnSelect(null);
            keyboardInput.OnDeselect(null);
            keyboardInput.CloseKeybaord();

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

            yield return new WaitForSeconds(3.0f);

            GameObject.FindObjectOfType<InteractEvent>().Trigger();

            WebXRInteract interactObject = GameObject.FindObjectOfType<WebXRInteract>();
            interactObject.OnGrab(null);
            interactObject.OnUngrab(null);
            interactObject.OnSecondaryGrab(null);
            interactObject.OnSecondaryUngrab(null);
            interactObject.OnTouch(null);
            interactObject.OnUntouch(null);
            interactObject.OnPointerClick(null);

            PlayerMenu playerMenu = GameObject.FindObjectOfType<PlayerMenu>(true);
            playerMenu.Show();
            playerMenu.PlayExperience();

            yield return new WaitForSeconds(0.5f);

            playerMenu = GameObject.FindObjectOfType<PlayerMenu>(true);
            playerMenu.Show();
            playerMenu.EditExperience();

            yield return new WaitForSeconds(0.5f);

            SceneManager.LoadScene("Player");

            yield return new WaitForSeconds(0.5f);

            playerMenu = GameObject.FindObjectOfType<PlayerMenu>(true);
            playerMenu.Show();
            playerMenu.GoToMainMenu();

            yield return new WaitForSeconds(4.0f);

            yield return null;
        }

        [UnityTest]
        public IEnumerator AssetLoaderTest()
        {
            yield return AssetsLoader.GetEnvironmentList(result => { });
            yield return AssetsLoader.GetBasicModelsList(result => { });
            AssetsLoader.GetBasicModelsList();
            yield return AssetsLoader.GetModelsList(result => { });
            yield return AssetsLoader.GetTexturesList(result => { });
            yield return AssetsLoader.GetModelsFromUrl("https://app.scioxr.com/StreamingAssets/Models/files.txt", result => { });
            yield return AssetsLoader.LoadTextureFromUrl("https://app.scioxr.com/StreamingAssets/Textures/bricks.png", result => { });
        }

        [UnityTest]
        public IEnumerator MainMenuTest()
        {
            SceneManager.LoadScene("Main");

            yield return new WaitForSeconds(0.5f);

            ChooseExperienceMenu menu = GameObject.FindObjectOfType<ChooseExperienceMenu>();

            menu.NewExperience();

            yield return new WaitForSeconds(0.5f);

            AppManager.instance.currentSceneName = "sceneToDelete";

            ScioXRSceneManager.instance.SaveScene(AppManager.instance.GetScenePath());

            SceneManager.LoadScene("Main");

            yield return new WaitForSeconds(0.5f);

            menu = GameObject.FindObjectOfType<ChooseExperienceMenu>();
            menu.selectedExperience = "sceneToDelete";
            menu.EditExperience();

            yield return new WaitForSeconds(0.5f);

            SceneManager.LoadScene("Main");

            yield return new WaitForSeconds(0.5f);

            menu = GameObject.FindObjectOfType<ChooseExperienceMenu>();
            menu.selectedExperience = "sceneToDelete";
            menu.PlayExperience();

            yield return new WaitForSeconds(0.5f);

            SceneManager.LoadScene("Main");

            yield return new WaitForSeconds(0.5f);

            menu = GameObject.FindObjectOfType<ChooseExperienceMenu>();
            menu.selectedExperience = "sceneToDelete";
            menu.ShowExperienceInfo("sceneToDelete");
            menu.DeleteExperience();

            menu.PlayGame("K8");

            yield return new WaitForSeconds(0.5f);

            yield return null;
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

        [UnityTest]
        public IEnumerator SaveLoadSceneTest3D()
        {
            AssetsLoader.appUrl = "https://app.scioxr.com/";
            SceneManager.LoadScene("Editor");

            yield return new WaitForSeconds(1.0f);

            PlatformLoader.instance.currentPlatfrom = PlatformLoader.ScioXRPlatforms.Flat3D;
            PlatformLoader.instance.Init();

            yield return new WaitForSeconds(1.0f);

            //Open Main menu panel
            EditorButtonsListener buttonListener = GameObject.FindObjectOfType<EditorButtonsListener>();
            buttonListener.toggleCanvas.Toggle();

            yield return new WaitForSeconds(0.2f);
            XRTabGroup mainMenuTabs = buttonListener.toggleCanvas.panel as XRTabGroup;
            mainMenuTabs.SelectTab(0);

            yield return new WaitForSeconds(1.5f);

            ModelSelecter basicModelSelector = GameObject.FindObjectOfType<ModelSelecter>();
            basicModelSelector.CreateBasicModel();

            yield return new WaitForSeconds(0.5f);

            EditorTransform3D editorObject = GameObject.FindObjectOfType<EditorTransform3D>();
            
            IDragHandler editorDrag = editorObject.GetComponent<IDragHandler>();
            PointerEventData poitnerData = new PointerEventData(EventSystem.current);
            EditorManager.mode = TransformMode.MOVE_XY;
            editorDrag.OnDrag(poitnerData);
            EditorManager.mode = TransformMode.MOVE_XZ;
            editorDrag.OnDrag(poitnerData);
            EditorManager.mode = TransformMode.ROTATE_XY;
            editorDrag.OnDrag(poitnerData);
            EditorManager.mode = TransformMode.ROTATE_XZ;
            editorDrag.OnDrag(poitnerData);
            EditorManager.mode = TransformMode.SCALE;
            editorDrag.OnDrag(poitnerData);

            editorObject.OnPointerEnter(poitnerData);
            editorObject.OnPointerExit(poitnerData);

            editorObject.GetComponent<IBeginDragHandler>().OnBeginDrag(poitnerData);
            editorObject.GetComponent<IEndDragHandler>().OnEndDrag(poitnerData);

            EditorManager.mode = TransformMode.SET_PARENT;
            EditorManager.instance.selectedObject = editorObject.gameObject;
            editorObject.GetComponent<IPointerClickHandler>().OnPointerClick(poitnerData);
            EditorManager.mode = TransformMode.PROPERTIES;
            editorObject.GetComponent<IPointerClickHandler>().OnPointerClick(poitnerData);

            yield return new WaitForSeconds(0.5f);

            MouseLook mouseLook = GameObject.FindObjectOfType<MouseLook>();
            mouseLook.DoMouseMove();
            mouseLook.axes = MouseLook.RotationAxes.MouseX;
            mouseLook.DoMouseMove();
            mouseLook.axes = MouseLook.RotationAxes.MouseY;
            mouseLook.DoMouseMove();

            yield return new WaitForSeconds(0.5f);

            //test properties
            Saveable saveable = GameObject.FindObjectOfType<Saveable>();
            PropertiesPanel properties = EditorManager.instance.propertiesMenu.GetComponentInChildren<PropertiesPanel>();
            properties.SetObject(saveable.gameObject);

            EditorManager.instance.keyboard.textInput = properties.tagInputField;
            EditorManager.instance.keyboard.HandleUpdate("TestTag");
            EditorManager.instance.keyboard.HandleSubmit("TestTag");
            EditorManager.instance.keyboard.HandleCancel();

            EditorManager.instance.keyboard.textInput = null;
            EditorManager.instance.keyboard.textInputTMP = GameObject.FindObjectOfType<TMP_InputField>(true);
            EditorManager.instance.keyboard.HandleUpdate("TestTag");
            EditorManager.instance.keyboard.HandleSubmit("TestTag");
            EditorManager.instance.keyboard.HandleCancel();

            EditorManager.instance.keyboard.UpdatePosition();

            properties.SetInteractable(false);
            properties.SaveState();
            properties.DuplicateButton(saveable.gameObject);

            properties.tagInputField.GetComponentInChildren<Text>().text = "TestTag";
            properties.AddTagToList();
            properties.AddTagToList();
            properties.EnterSetParentMode();

            properties.DestroyButton();
            properties.CloseButton();


            AppManager.instance.currentSceneName = "unitTestScene3D";

            ScioXRSceneManager.instance.SaveScene(AppManager.instance.GetScenePath());
            
            SceneManager.LoadScene("Player");

            yield return new WaitForSeconds(1.0f);

            InteractFlat3D interactObject = GameObject.FindObjectOfType<InteractFlat3D>();
            interactObject.OnPointerClick(null);
        }
    }
}
