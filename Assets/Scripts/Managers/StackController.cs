using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StackController : MonoBehaviour
{
    public static StackController Instance { get; private set; }

    [Header("Stack Attributes")]
    [SerializeField] private GameObject stackPrefab;
    [SerializeField] private List<Material> materials;
    [SerializeField] private GameObject stackParent;

   
    private float stackLength = 2.7f;
    private List<GameObject> stacks = new List<GameObject>();

    private bool readyForSpawn = true;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        AssignChilds();
        //SpawnStack();
    }

    private void AssignChilds()
    {
        for (int i = 0; i < stackParent.transform.childCount; i++)
        {
            stacks.Add(stackParent.transform.GetChild(i).gameObject);
        }
    }
    
    public void SpawnStack()
    {
        if (readyForSpawn)
        {
            readyForSpawn=false;

            var zPos = stacks.Count * stackLength;
            GameObject stack = Instantiate(stackPrefab);
            stack.GetComponent<MeshRenderer>().material = materials[Random.Range(0, materials.Count)];
            stack.transform.SetParent(stackParent.transform);
            stack.transform.localPosition = new Vector3(0, -0.5007f, zPos);

            stack.transform.localScale = stacks[stacks.Count - 1].transform.localScale;
            stacks.Add(stack);

            stack.GetComponent<Stack>().StartMove();
        }
    }

    public void ResetState()
    {
        readyForSpawn = true;
    }
}


