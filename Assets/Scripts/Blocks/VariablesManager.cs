using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariablesManager : MonoBehaviour
{
    public Dictionary<string, IntVariable> variables = new Dictionary<string, IntVariable>();

    public static VariablesManager instance;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void InitVariables(string[] variablesArray)
    {
        foreach (var variableName in variablesArray)
        {
            IntVariable intVar = gameObject.AddComponent<IntVariable>();
            variables.Add(variableName, intVar);
        }
    }

    public IntVariable GetVariable(string variableName)
    {
        IntVariable var;
        variables.TryGetValue(variableName, out var);
        return var;
    }
}
