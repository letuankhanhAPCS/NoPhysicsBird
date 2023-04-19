using UnityEngine;
using System.Collections;

public class PipeMove : MonoBehaviour {

	public float moveSpeed;
	public float gapHeight;
	public float gapWidth;
	private bool isGameOver;
	
	
	private GameMain gameMain;
	private BirdControl birdControl;
	private Vector3 birb;
	private float xBound;
	private float yBound;
	public float yDiff;
	private bool isScored;

	// Use this for initialization
	void Start ()
	{
		gameMain = FindObjectOfType<GameMain>();
		birdControl = gameMain.bird.GetComponent<BirdControl>();
		xBound = gapWidth + gameMain.birbRadius;
		yBound = gapHeight - gameMain.birbRadius;
	}

	// Update is called once per frame
	void Update () {
		if (!isGameOver)
		{
			transform.Translate( new Vector2(moveSpeed, 0) * Time.deltaTime);
		}

		birb = gameMain.bird.transform.position;
		if (birb.x > transform.position.x - xBound && birb.x < transform.position.x + xBound)
		{
			if (birb.x >= transform.position.x && !isScored)
			{
				isScored = true;
				birdControl.Score();
			}
			
			if (birb.y - yDiff > transform.position.y + yBound || birb.y - yDiff < transform.position.y - yBound)
			{
				if (isGameOver) return;
				print(birb.y + " " + (transform.position.y + " " + yBound));
				birdControl.TouchTube();
			}
		}

		if (transform.position.x <= -0.4) 
		{
			Destroy(gameObject);
		}
	}

	public void GameOver()
	{
		isGameOver = true;
	}
}
