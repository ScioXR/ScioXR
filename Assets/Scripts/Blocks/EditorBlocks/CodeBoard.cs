using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CodeBoard : MonoBehaviour
{
    public BlocksBoard blockBoard;

    public bool shouldSave;

    //public List<string> variables = new List<string>();
    //public List<string> messages = new List<string>();

    public void UpdateHighlights(GameObject dragObject)
    {
        if (dragObject.GetComponent<VariableEditor>())
        {
            AttachPoint[] attachPoints = gameObject.GetComponentsInChildren<VariableAttachPoint>();
            foreach (var attachPoint in attachPoints)
            {
                attachPoint.CheckIsInside(dragObject);
            }
        }

        if (dragObject.GetComponent<CodeBlockEditor>())
        {
            if (dragObject.GetComponent<CodeBlockEditor>().previousAttachPoint) {
                AttachPoint[] attachPointsBelow = gameObject.GetComponentsInChildren<BelowAttachPoint>();
                foreach (var attachPoint in attachPointsBelow)
                {
                    attachPoint.CheckIsInside(dragObject);
                }
            }

            if (dragObject.GetComponent<CodeBlockEditor>().nextAttachPoint)
            {
                CodeBlockEditor lastDrag = dragObject.GetComponent<CodeBlockEditor>().Last();
                AttachPoint[] attachPointsAbove = gameObject.GetComponentsInChildren<AboveAttachPoint>();
                foreach (var attachPoint in attachPointsAbove)
                {
                    attachPoint.CheckIsInside(lastDrag.gameObject);
                }
            }
        }
    }

    public void DropBlock(BlockEditor block)
    {
        GameObject dragObject = block.gameObject;

        if (dragObject.GetComponent<VariableEditor>())
        {
            AttachPoint[] attachPoints = gameObject.GetComponentsInChildren<VariableAttachPoint>();
            foreach (var attachPoint in attachPoints)
            {
                if (attachPoint.CheckIsInside(dragObject))
                {
                    attachPoint.GetComponent<BlockEditor>().AttachBlock(dragObject);
                    break;
                }
            }
        }

        if (dragObject.GetComponent<CodeBlockEditor>())
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
        }
    }

    public static bool IsInRect(GameObject sourceObject, RectTransform destinationObject)
    {
        Vector3 diff = sourceObject.transform.position - destinationObject.position;
        return destinationObject.rect.Contains(diff);
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

                if (block is CodeBlockEditor)
                {
                    CodeBlockEditor codeBlock = (block as CodeBlockEditor).nextBlock;
                    List<BlockData> attachedBlocksList = new List<BlockData>();
                    while (codeBlock != null)
                    {
                        BlockData attachedBlockData = new BlockData();
                        codeBlock.ExportData(ref attachedBlockData);
                        attachedBlocksList.Add(attachedBlockData);

                        codeBlock = codeBlock.nextBlock;
                    }
                    blockData.blocks = attachedBlocksList.ToArray();
                }

                blocksList.Add(blockData);
            }
        }
        codeData.blocks = blocksList.ToArray();
        codeData.variables = blockBoard.GetVariables().ToArray();
        codeData.messages = blockBoard.GetMessages().ToArray();

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
        blockBoard.EmptyVariables();
        BlockEditor[] blocks = GetComponentsInChildren<BlockEditor>();
        foreach (var block in blocks)
        {
            if (!block.IsAttached())
            {
                Destroy(block.gameObject);
            }
        }

        if (data != null)
        {
            foreach (var variableString in data.variables)
            {
                blockBoard.variableName.text = variableString;
                blockBoard.CreateVariable();
            }

            foreach (var messageString in data.messages)
            {
                blockBoard.messageName.text = messageString;
                blockBoard.CreateMessage();
            }

            foreach (var rootBlockData in data.blocks)
            {
                GameObject rootBlock = blockBoard.CreateBlock(rootBlockData);
                rootBlock.transform.localPosition = rootBlockData.editorPosition;
                foreach (var childBlockData in rootBlockData.blocks)
                {
                    GameObject childBlock = blockBoard.CreateBlock(childBlockData);
                    rootBlock.GetComponent<BlockEditor>().AttachBlock(childBlock);
                    rootBlock = childBlock;
                }
            }
        }
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
