using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEditor;

public class BirdControl : MonoBehaviour {

	public int rotateRate = 10;
	public float upSpeed = 10;
    public GameObject scoreMgr;

    public AudioClip jumpUp;
    public AudioClip hit;
    public AudioClip score;

    public bool inGame = false;

	private bool dead = false;
	private bool landed = false;

	private float curSpeed = 0;
	public float jumpTimer;
	public float jumpStrength;
	private float jumpTimeLeft;
	private bool jumping;
	private GameMain gameMain;
	
    // Use this for initialization
    void Start () {
        float birdOffset = 0.05f;
        float birdTime = 0.3f;
        float birdStartY = transform.position.y;

        gameMain = FindObjectOfType<GameMain>();
    }
	
	// Update is called once per frame
	void Update () {
		if (dead)
		{
			if (Input.GetButtonDown("Fire1"))
			{
				Restart();
			}
		}
		
		if (jumpTimeLeft <= 0 && gameMain)
		{
			if (jumping)
			{
				jumping = false;
				//ResetSpeed();
			}
				
			ApplyGravity();
		}
		else if(gameMain)
		{
			print("sound");
			JumpUp();
		}
		
        if (!inGame)
        {
            return;
        }
        //birdSequence.Kill();

		if (!dead)
		{
			if (Input.GetButtonDown("Fire1"))
			{
				jumping = true;
				UpdateSpeed();
				jumpTimeLeft = jumpTimer;
			}
		}

		if (!landed)
		{
			float rotate = Mathf.Min(Mathf.Max(-90, curSpeed * rotateRate + 60), 30);
			
			transform.rotation = Quaternion.Euler(0f, 0f, rotate);
		}
	}

	void UpdateSpeed()
	{
		curSpeed = jumpStrength;
	}
	
	void ApplyGravity()
	{
		if (!gameMain.gameStarted || landed) return;
		transform.Translate(new Vector2(0, curSpeed) * Time.deltaTime, Space.World);
		curSpeed -= Time.deltaTime * 9.8f;
	}

	public void TouchTube()
	{
		if (!dead)
		{
			GameObject[] objs = GameObject.FindGameObjectsWithTag("movable");
			foreach (GameObject g in objs)
			{
				g.BroadcastMessage("GameOver");
			}

			GetComponent<Animator>().SetTrigger("die");
			AudioSource.PlayClipAtPoint(hit, Vector3.zero);
		}
	}

	public void TouchLand()
	{
		if (!dead)
		{
			GameObject[] objs = GameObject.FindGameObjectsWithTag("movable");
			foreach (GameObject g in objs)
			{
				g.BroadcastMessage("GameOver");
			}

			GetComponent<Animator>().SetTrigger("die");
			AudioSource.PlayClipAtPoint(hit, Vector3.zero);
		}
		
		landed = true;
	}

	public void Score()
	{
		scoreMgr.GetComponent<ScoreMgr>().AddScore();
		AudioSource.PlayClipAtPoint(score, Vector3.zero);
	}

	public void JumpUp()
    {
	    transform.Translate(new Vector2(0, curSpeed) * Time.deltaTime, Space.World);
	    jumpTimeLeft -= Time.deltaTime;
	    AudioSource.PlayClipAtPoint(jumpUp, Vector3.zero);
    }

	void Restart()
	{
		gameMain.gameoverPic.SetActive(false);
		gameMain.gameStarted = false;
		Application.LoadLevel(0);
	}
	
	public void GameOver()
	{
		dead = true;
		gameMain.gameoverPic.SetActive(true);
	}
}
