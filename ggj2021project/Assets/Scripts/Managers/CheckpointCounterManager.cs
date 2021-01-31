using UnityEngine;
using UnityEngine.UI;

public class CheckpointCounterManager : MonoBehaviour
{
    public GameObject CheckpointCounter;
    public Material[] CounterMaterials;

    public void SetCheckpoint(int checkpoint)
    {
        CheckpointCounter.GetComponent<Image>().material = CounterMaterials[checkpoint + 1];
    }
}
