using UnityEngine;

public class LevelManager : MonoBehaviour
{
	[SerializeField] private GameUI gameUI;
	[SerializeField] private Transform parentPanel;
	[SerializeField] private GameObject level;
    [SerializeField] private GameObject[] levelBlocks, obstacle1Blocks, obstacle2Blocks;
	[SerializeField] private LayerMask layerMask;
	private int blockIndex, obstacle1Index, obstacle2Index;
	private float rayDistance = 448f;
	private bool rayHit;
	private Vector2 currentPosition;
	private GameObject currentLevelBlock;

	private void Start(){
		currentPosition = new Vector2(0, 1000);
		GenerateLevel();
		currentLevelBlock = levelBlocks[blockIndex];
	}

    private void Update(){
		// set raycast to current level block.
		LevelRaycast(currentLevelBlock.transform.position);

		// check it the player pass the level.
		if(rayHit){
			rayHit = false;
			// increment socre if passed.
			gameUI.GameScore();
			// update the position
			currentPosition = new Vector2(currentPosition.x, currentPosition.y + (112 * 3));
			// Create new level.
			GenerateLevel();
			currentLevelBlock = levelBlocks[blockIndex];
		}
    }

	private void LevelRaycast(Vector2 currentLevelBlock){
		// set the position of raycast.
        Vector2 origin = new Vector2(currentLevelBlock.x - 56, currentLevelBlock.y);
        Vector2 direction = Vector2.right;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayDistance, layerMask);

		// Check if raycast hit.
        if (hit.collider != null)
        {
            Debug.Log("Hit");
			rayHit = true;
        }

        Debug.DrawLine(origin, origin + direction * rayDistance, Color.red);
	}

	GameObject[] GetAllBlocks(GameObject parent)
    {
		// count all the blocks.
        int blockCount = parent.transform.childCount;

        GameObject[] blocks = new GameObject[blockCount];

		// initalize all the child block from the prefab.
        for (int i = 0; i < blockCount; i++)
        {
            blocks[i] = parent.transform.GetChild(i).gameObject;
			blocks[i].SetActive(true);
        }

        return blocks;
    }

	// hide all the block in level.
	private void ActiveBlock(GameObject[] obstacleBlocks){
		foreach(GameObject block in obstacleBlocks){
			block.SetActive(false);
		}
	}

	private void GenerateLevel(){
		// instanticate the level block and obstacles.
		GameObject currentLevel = Instantiate(level, parentPanel);
		GameObject obstacle1 = Instantiate(level, parentPanel);
		GameObject obstacle2 = Instantiate(level, parentPanel);

		// Set position.
		currentLevel.transform.localPosition = currentPosition;

		currentPosition = new Vector2(currentPosition.x, currentPosition.y + (112 * 3));
		obstacle1.transform.localPosition = currentPosition;

		currentPosition = new Vector2(currentPosition.x, currentPosition.y + (112 * 4));
		obstacle2.transform.localPosition = currentPosition;

		levelBlocks = GetAllBlocks(currentLevel);
		obstacle1Blocks = GetAllBlocks(obstacle1);
		obstacle2Blocks = GetAllBlocks(obstacle2);

		blockIndex = Random.Range(2, levelBlocks.Length - 5);
		Debug.Log("random value is" + blockIndex);

		// hide blocks to create path.
		for(int i = blockIndex; i < blockIndex + 4; i++){
			levelBlocks[i].SetActive(false);
			// Debug.Log("working");
		}

		// enable blocks as obstacles.
		ActiveBlock(obstacle1Blocks);
		obstacle1Index = Random.Range(blockIndex, blockIndex + 4);
		obstacle1Blocks[obstacle1Index].SetActive(true);

		ActiveBlock(obstacle2Blocks);
		obstacle2Index = Random.Range(blockIndex, blockIndex + 4);
		obstacle2Blocks[obstacle2Index].SetActive(true);
	}
}