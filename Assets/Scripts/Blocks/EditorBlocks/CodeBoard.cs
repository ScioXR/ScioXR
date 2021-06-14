using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CodeBoard : MonoBehaviour
{
    public BlocksBoard blockBoard;

    public bool shouldSave;

    public void UpdateHighlights(GameObject dragObject)
    {
        BlockEditor.BlockGroup group = dragObject.GetComponent<BlockEditor>().blockGroup;
        if (group == BlockEditor.BlockGroup.VARIABLE)
        {
            AttachPoint[] attachPoints = gameObject.GetComponentsInChildren<VariableAttachPoint>();
            foreach (var attachPoint in attachPoints)
            {
                bool inside = attachPoint.CheckIsInside(dragObject);
                attachPoint.UpdateHighlight(inside);
            }
        }

        if (group == BlockEditor.BlockGroup.CONDITION)
        {
            AttachPoint[] attachPoints = gameObject.GetComponentsInChildren<ConditionAttachPoint>();
            foreach (var attachPoint in attachPoints)
            {
                bool inside = attachPoint.CheckIsInside(dragObject);
                attachPoint.UpdateHighlight(inside);
            }
        }

        if (group == BlockEditor.BlockGroup.BLOCK)
        {
            if (dragObject.GetComponent<CodeBlockEditor>().previousAttachPoint) {
                AttachPoint[] attachPointsBelow = gameObject.GetComponentsInChildren<BelowAttachPoint>();
                foreach (var attachPoint in attachPointsBelow)
                {
                    bool inside = attachPoint.CheckIsInside(dragObject);
                    bool changedHighlight = attachPoint.UpdateHighlight(inside);

                    if (changedHighlight)
                    {
                        //if its in inside block we need to resize
                        if (attachPoint.GetComponent<CodeBlockEditor>().First().parentBlock)
                        {
                            float hoverObjectHeight = dragObject.GetComponent<CodeBlockEditor>().CalculateSize();
                            attachPoint.GetComponent<CodeBlockEditor>().First().parentBlock.GetComponent<CodeBlockEditor>().Resize(inside, hoverObjectHeight);
                        }
                    }
                }

                AttachPoint[] attachPointsInside = gameObject.GetComponentsInChildren<InsideAttachPoint>();
                foreach (var attachPoint in attachPointsInside)
                {
                    bool inside = attachPoint.CheckIsInside(dragObject);
                    bool changedHighlight = attachPoint.UpdateHighlight(inside);

                    if (changedHighlight)
                    {
                        float hoverObjectHeight = dragObject.GetComponent<CodeBlockEditor>().CalculateSize();
                        attachPoint.GetComponent<CodeBlockEditor>().Resize(inside, hoverObjectHeight);
                    }
                }
            }

            if (dragObject.GetComponent<CodeBlockEditor>().nextAttachPoint)
            {
                CodeBlockEditor lastDrag = dragObject.GetComponent<CodeBlockEditor>().Last();
                AttachPoint[] attachPointsAbove = gameObject.GetComponentsInChildren<AboveAttachPoint>();
                foreach (var attachPoint in attachPointsAbove)
                {
                    bool inside = attachPoint.CheckIsInside(lastDrag.gameObject);
                    attachPoint.UpdateHighlight(inside);
                }
            }
        }
    }

    public void DropBlock(BlockEditor block)
    {
        GameObject dragObject = block.gameObject;
        BlockEditor.BlockGroup group = dragObject.GetComponent<BlockEditor>().blockGroup;
        if (group == BlockEditor.BlockGroup.VARIABLE)
        {
            AttachPoint[] attachPoints = gameObject.GetComponentsInChildren<VariableAttachPoint>();
            foreach (var attachPoint in attachPoints)
            {
                if (attachPoint.CheckIsInside(dragObject))
                {
                    attachPoint.GetComponent<BlockEditor>().AttachBlock(dragObject, attachPoint);
                    break;
                }
            }
        }

        if (group == BlockEditor.BlockGroup.CONDITION)
        {
            AttachPoint[] attachPoints = gameObject.GetComponentsInChildren<ConditionAttachPoint>();
            foreach (var attachPoint in attachPoints)
            {
                if (attachPoint.CheckIsInside(dragObject))
                {
                    attachPoint.GetComponent<BlockEditor>().AttachBlock(dragObject, attachPoint);
                    break;
                }
            }
        }

        if (group == BlockEditor.BlockGroup.BLOCK)
        {
            AttachPoint[] attachPointsBelow = gameObject.GetComponentsInChildren<BelowAttachPoint>();
            foreach (var attachPoint in attachPointsBelow)
            {
                if (attachPoint.CheckIsInside(dragObject))
                {
                    attachPoint.GetComponent<BlockEditor>().AttachBlock(dragObject);
                    break;
                }
            }

            CodeBlockEditor lastDrag = dragObject.GetComponent<CodeBlockEditor>().Last();
            AttachPoint[] attachPointsAbove = gameObject.GetComponentsInChildren<AboveAttachPoint>();
            foreach (var attachPoint in attachPointsAbove)
            {
                if (attachPoint.CheckIsInside(lastDrag.gameObject))
                {
                    lastDrag.AttachBlock(attachPoint.gameObject);
                }
            }

            AttachPoint[] attachPointsInside = gameObject.GetComponentsInChildren<InsideAttachPoint>();
            foreach (var attachPoint in attachPointsInside)
            {
                if (attachPoint.CheckIsInside(dragObject))
                {
                    attachPoint.GetComponent<BlockEditor>().AttachChildBlock(dragObject);
                }
            }
        }
    }

    public static bool IsInRect(GameObject sourceObject, RectTransform destinationObject)
    {
        return destinationObject.Overlaps(sourceObject.GetComponent<RectTransform>());
    }

    public void RefreshBlockReferences()
    {
        BlockEditor[] blocks = GetComponentsInChildren<BlockEditor>();
        foreach (var block in blocks)
        {
            block.RefreshReferences();
        }
    }

    public string CodeToJSON()
    {
        CodeData codeData = SaveCode();

        string jsonCode = JsonUtility.ToJson(codeData);

        //Cleaning of unnecesary json code that are default params
        jsonCode = jsonCode.Replace("\"editorPosition\":{\"x\":0.0,\"y\":0.0},", "");
        jsonCode = jsonCode.Replace("\"paramInt\":0,", "");
        jsonCode = jsonCode.Replace("\"paramString\":\"\",", "");

        return jsonCode;
    }

    public CodeData SaveCode()
    {
        CodeData codeData = new CodeData();
        List<BlockData> blocksList = new List<BlockData>();

        //Debug.Log("Exporting to JSON");
        BlockEditor[] blocks = GetComponentsInChildren<BlockEditor>();
        foreach (BlockEditor block in blocks)
        {
            if (!block.IsAttached())
            {
                BlockData blockData = new BlockData();
                blockData.editorPosition = new Vector2(block.transform.localPosition.x, block.transform.localPosition.y);
                block.ExportData(ref blockData);
                blocksList.Add(blockData);
            }
        }
        codeData.blocks = blocksList.ToArray();

        return codeData;
    }

    public void CodeFromJSON(string json)
    {
        CodeData dataJson = JsonUtility.FromJson<CodeData>(json);
        LoadCode(dataJson);
    }

    public void LoadCode(CodeData data)
    {
        shouldSave = true;

        //clean all code
        blockBoard.EmptyVariables();
        blockBoard.EmptyMessages();
        BlockEditor[] blocks = GetComponentsInChildren<BlockEditor>();
        foreach (var block in blocks)
        {
            if (!block.IsAttached())
            {
                Destroy(block.gameObject);
            }
        }

        foreach (var variableString in EditorManager.instance.globalData.variables)
        {
            blockBoard.variableName.text = variableString;
            blockBoard.CreateVariable();
        }

        foreach (var messageString in EditorManager.instance.globalData.messages)
        {
            blockBoard.messageName.text = messageString;
            blockBoard.CreateMessage();
        }

        if (data != null && data.blocks != null)
        {
            foreach (var rootBlockData in data.blocks)
            {
                ParseBlock(rootBlockData);
                /*GameObject rootBlock = blockBoard.CreateBlock(rootBlockData);
                rootBlock.transform.localPosition = rootBlockData.editorPosition;
                rootBlock.transform.localRotation = Quaternion.identity;
                foreach (var childBlockData in rootBlockData.blocks)
                {
                    GameObject childBlock = blockBoard.CreateBlock(childBlockData);
                    childBlock.transform.localRotation = Quaternion.identity;
                    rootBlock.GetComponent<BlockEditor>().AttachBlock(childBlock);
                    rootBlock = childBlock;
                }*/
            }
        }
    }

    private GameObject ParseBlock(BlockData blockData)
    {
        GameObject rootBlock = blockBoard.CreateBlock(blockData);
        if (rootBlock)
        {
            rootBlock.transform.localPosition = blockData.editorPosition;
            rootBlock.transform.localRotation = Quaternion.identity;
            if (blockData.blocks != null)
            {
                GameObject nextBlockIterator = rootBlock;
                foreach (var childBlockData in blockData.blocks)
                {
                    GameObject nextBlock = ParseBlock(childBlockData);
                    //GameObject childBlock = blockBoard.CreateBlock(childBlockData);
                    // childBlock.transform.localRotation = Quaternion.identity;
                    nextBlockIterator.GetComponent<BlockEditor>().AttachBlock(nextBlock);
                    nextBlockIterator = nextBlock;
                }
            }
            if (blockData.childBlocks != null)
            {
                GameObject nextBlockIterator = null;
                foreach (var childBlockData in blockData.childBlocks)
                {
                    GameObject nextBlock = ParseBlock(childBlockData);
                    if (!nextBlockIterator)
                    {
                        rootBlock.GetComponent<BlockEditor>().AttachChildBlock(nextBlock);
                    }
                    else
                    {
                        nextBlockIterator.GetComponent<BlockEditor>().AttachBlock(nextBlock);
                    }
                    nextBlockIterator = nextBlock;
                }
            }
            if (blockData.attachedBlocks != null)
            {
                for (int i = 0; i < blockData.attachedBlocks.Length; i++)
                {
                    BlockData childBlockData = blockData.attachedBlocks[i];
                    GameObject nextBlock = ParseBlock(childBlockData);
                    if (nextBlock)
                    {
                        BlockEditor.BlockGroup group = nextBlock.GetComponent<BlockEditor>().blockGroup;
                        if (group == BlockEditor.BlockGroup.CONDITION)
                        {
                            rootBlock.GetComponent<BlockEditor>().AttachBlock(nextBlock, rootBlock.GetComponent<CodeBlockEditor>().conditionalAttachPoint);
                        } else if (group == BlockEditor.BlockGroup.VARIABLE)
                        {
                            rootBlock.GetComponent<BlockEditor>().AttachBlock(nextBlock, rootBlock.GetComponent<CodeBlockEditor>().variableAttachPoints[i]);
                        }
                    }
                }
            }
        }
        return rootBlock;
    }

    public void SaveTest()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "editor_test.json");
        using (var writer = new StreamWriter(File.Open(filePath, FileMode.Create)))
        {
            writer.Write(CodeToJSON());
            Debug.Log("SaveTest: " + filePath);
        }
    }

    public void LoadTest()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "editor_test.json");
        if (File.Exists(filePath))
        {
            string data = File.ReadAllText(filePath);
            // Debug.Log("Data: " + data);           
            try
            {
                CodeFromJSON(data);
            }
            catch (FormatException fex)
            {
                //Invalid json format
                Debug.LogError("FormatException: " + fex);
            }
            catch (Exception ex)
            {
                //some other exception
                Debug.LogError("Exception: " + ex);
            }
        }
        else
        {
            Debug.LogError("There is no SaveData!");
        }
    }
}
