using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWater : MonoBehaviour
{
    public List<GameObject> molecules = new List<GameObject>();

    public GameObject moleculeModel;

    public GameObject winMark;
    public GameObject infoText;

    public GameObject createButton;

    public Cage cage;

    private void Start()
    {
        winMark.SetActive(false);
    }

    public void Create()
    {
        int numH = 0;
        int numO = 0;
        int numOther = 0;
        foreach (var molecule in molecules)
        {
            if (molecule.tag == "molecule_h")
            {
                numH++;
            } else if (molecule.tag == "molecule_o")
            {
                numO++;
            } else if (molecule.tag == "molecule_other")
            {
                numOther++;
            }
        }
        Debug.Log("H: " + numH + ", O: " + numO + ", Other: " + numOther);

        foreach (var molecule in molecules)
        {
            Destroy(molecule);
        }
        molecules.Clear();

        if (numH == 2 && numO == 1 && numOther == 0)
        {
            Destroy(createButton.GetComponent<TouchButton>());
            moleculeModel.SetActive(true);
            Invoke("ShowWater", 2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "molecule_h" || other.gameObject.tag == "molecule_o" || other.gameObject.tag == "molecule_other")
        {
            molecules.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        molecules.Remove(other.gameObject);
    } 

    public void ShowWater()
    {
        infoText.SetActive(false);
        winMark.SetActive(true);
        moleculeModel.SetActive(false);
        GetComponent<MeshRenderer>().enabled = true;
        cage.UnlockLight();
    }
}
