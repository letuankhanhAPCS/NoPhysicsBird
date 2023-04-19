using UnityEngine;
using System.Collections;

public class StartMain : MonoBehaviour {

    public GameObject bird;
    public GameObject land;
    public GameObject back_ground;

    public void OnPressStart()
    {
        Application.LoadLevel(1);
    }
}
