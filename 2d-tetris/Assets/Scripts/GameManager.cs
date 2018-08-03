using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject[] blockTypes;

    private Random random;
    private int columns = 10;
    private int rows = 30;
    private int blockSize = 32;

    private GameObject currentBlock;
    private bool blockMoving;
    private int numSeconds = 3;

    void Awake()
    {
        // Initializing singleton to manage game state
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        random = new Random();
        InitializeGame();

        // Testing
        SpawnBlock();
    }

    // Update is called once per frame
    void Update()
    {
        if (!blockMoving)
        {
            StartCoroutine(MoveBlockDown());
        }

        HandlePlayerInput();
    }

    private void InitializeGame()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                //Instantiate(Block_I, )
            }
        }
    }

    private void SpawnBlock()
    {
        int index = Random.Range(0, blockTypes.Length);

        currentBlock = Instantiate(blockTypes[index], new Vector3(4.5f, 7.5f), Quaternion.identity);
    }

    private void HandlePlayerInput()
    {
        Vector3 currentPosition = currentBlock.transform.position;

        if (Input.GetKeyDown(KeyCode.A))
        {
            currentBlock.transform.position = new Vector3(currentPosition.x - 1, currentPosition.y);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            currentBlock.transform.position = new Vector3(currentPosition.x + 1, currentPosition.y);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            currentBlock.transform.Rotate(new Vector3(0, 0, -90));
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            currentBlock.transform.Rotate(new Vector3(0, 0, 90));
        }
    }

    IEnumerator MoveBlockDown()
    {
        blockMoving = true;
        yield return new WaitForSeconds(numSeconds);

        currentBlock.transform.position = new Vector3(currentBlock.transform.position.x, currentBlock.transform.position.y - 1);

        blockMoving = false;
    }
}
