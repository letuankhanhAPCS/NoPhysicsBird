using UnityEngine;
using System.Collections;
using DG.Tweening;

public class LandControl : MonoBehaviour {

    private Sequence landSequence;
    public float halfHeight;

    private GameMain gameMain;
    private float birbRad;
	// Use this for initialization
	void Start ()
	{
		// land continue moving
        landSequence = DOTween.Sequence();

        landSequence.Append(transform.DOMoveX(transform.position.x - 0.48f, 0.5f).SetEase(Ease.Linear))
            .Append(transform.DOMoveX(transform.position.x, 0f).SetEase(Ease.Linear))
            .SetLoops(-1);
    }

	public void InitLandCollider()
	{
		gameMain = FindObjectOfType<GameMain>();
		print(gameMain.name);
		birbRad = gameMain.birbRadius;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!gameMain || !gameMain.gameStarted)
		{
			return;
		}
		if ((gameMain.bird.transform.position.y - birbRad) - (transform.position.y + halfHeight) <= 0)
		{
			gameMain.bird.GetComponent<BirdControl>().TouchLand();
		}
	}

    public void GameOver()
    {
        landSequence.Kill();
    }
}
