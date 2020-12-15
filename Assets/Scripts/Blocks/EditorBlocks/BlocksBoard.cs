using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlocksBoard : MonoBehaviour
{
    public GameObject variablePanel;
    public GameObject variablePrefab;
    public TMP_InputField variableName;

    public GameObject messagesPanel;
    public GameObject messagesPrefab;
    public TMP_InputField messageName;

    public GameObject deleteButtonPrefab;

    public GameObject[] blocksPrefabs;
    public Transform startPosition;

    public float blockOffset;

    public CodeBoard codeBoard;

    public Transform contentHolder;

    List<GameObject> variables = new List<GameObject>();
    List<GameObject> messages =  new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnPostion = startPosition.position;
        foreach (var blockPrefab in blocksPrefabs)
        {
            GameObject blockSpawnerObject = Instantiate(blockPrefab, spawnPostion, transform.rotation, contentHolder);

            BlockSpawner blockSpawner = blockSpawnerObject.AddComponent<BlockSpawner>();
            Destroy(blockSpawnerObject.GetComponent<BlockEditor>());
            blockSpawner.codePanel = codeBoard;
            blockSpawner.blockPrefab = blockPrefab;
            spawnPostion -= transform.up * blockOffset;
        }

        variablePanel.transform.SetAsLastSibling();
        messagesPanel.transform.SetAsLastSibling();
    }

    public GameObject CreateBlock(BlockData blockData)
    {
        foreach (var blockPrefab in blocksPrefabs)
        {
            if (blockPrefab.name == blockData.blockType)
            {
                GameObject newBlock = Instantiate(blockPrefab, blockData.editorPosition, Quaternion.identity, codeBoard.gameObject.transform);
                BlockEditor blockEditor = newBlock.GetComponent<BlockEditor>();
                blockEditor.codePanel = codeBoard;
                if (blockEditor is CodeBlockEditor)
                {
                    CodeBlockEditor codeBlock = blockEditor as CodeBlockEditor;
                    if (codeBlock.variableAttachPoint && blockData.paramString != null && blockData.paramString != "")
                    {
                        GameObject variableObject = Instantiate(variablePrefab, newBlock.transform);
                        variableObject.GetComponentInChildren<TextMeshProUGUI>().text = blockData.paramString;
                        variableObject.GetComponent<BlockEditor>().codePanel = codeBoard;
                        blockEditor.AttachBlock(variableObject);
                    }
                }
                blockEditor.ImportData(blockData);
                return newBlock;
            }
        }
        return null;
    }

    public List<string> GetVariables()
    {
        List<string> result = new List<string>();
        foreach (var variable in variables)
        {
            result.Add(variable.GetComponentInChildren<TextMeshProUGUI>().text);
        }
        return result;
    }


    public void CreateVariable()
    {
        GameObject variableObject = Instantiate(variablePrefab, contentHolder);
        Destroy(variableObject.GetComponent<BlockEditor>());
        //variableObject.GetComponent<VariableEditor>().variableName = variableName.text;
        variableObject.GetComponentInChildren<TextMeshProUGUI>().text = variableName.text;

        BlockSpawner blockSpawner = variableObject.AddComponent<BlockSpawner>();
        blockSpawner.codePanel = codeBoard;
        blockSpawner.blockPrefab = variablePrefab;

        blockSpawner.transform.SetSiblingIndex(variablePanel.transform.GetSiblingIndex() + 1);

        //add delete button
        GameObject deleteButton = Instantiate(deleteButtonPrefab, blockSpawner.transform.position, Quaternion.identity, blockSpawner.transform);
        deleteButton.transform.localPosition += deleteButton.transform.right * 80f;
        string variableNameStr = variableName.text;
        deleteButton.GetComponent<Button>().onClick.AddListener(delegate { DeleteVariable(variableNameStr); });

        variables.Add(variableObject);

        codeBoard.RefreshBlockReferences();
    }

    public void DeleteVariable(string variableName)
    {
        foreach (var variable in variables)
        {
            if (variable.GetComponentInChildren<TextMeshProUGUI>().text == variableName)
            {
                variables.Remove(variable);
                Destroy(variable);
                codeBoard.RefreshBlockReferences();
                return;
            }
        }
    }

    public void EmptyVariables()
    {
        foreach (var variable in variables)
        {
            Destroy(variable);
        }
        variables.Clear();
        codeBoard.RefreshBlockReferences();
    }

    public List<string> GetMessages()
    {
        List<string> result = new List<string>();
        foreach (var message in messages)
        {
            result.Add(message.GetComponentInChildren<TextMeshProUGUI>().text);
        }
        return result;
    }

    public void CreateMessage()
    {
        GameObject variableObject = Instantiate(messagesPrefab, contentHolder);
        variableObject.GetComponentInChildren<TextMeshProUGUI>().text = messageName.text;

        //add delete button
        GameObject deleteButton = Instantiate(deleteButtonPrefab, variableObject.transform.position, Quaternion.identity, variableObject.transform);
        deleteButton.transform.localPosition += deleteButton.transform.right * 80f;
        string messageNameStr = messageName.text;
        deleteButton.GetComponent<Button>().onClick.AddListener(delegate { DeleteMessage(messageNameStr); });

        messages.Add(variableObject);

        codeBoard.RefreshBlockReferences();
    }

    public void DeleteMessage(string messageName)
    {
        foreach (var message in messages)
        {
            if (message.GetComponentInChildren<TextMeshProUGUI>().text == messageName)
            {
                messages.Remove(message);
                Destroy(message);
                codeBoard.RefreshBlockReferences();
                return;
            }
        }
    }

    public void EmptyMessages()
    {
        foreach (var message in messages)
        {
            Destroy(message);
        }
        messages.Clear();
        codeBoard.RefreshBlockReferences();
    }
}
