using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    private Canvas Canvas;

    // Start is called before the first frame update
    void Start()
    {

        Canvas = GetComponent<Canvas>();

    }

    // Update is called once per frame
    void Update()
    {

        Canvas.enabled = GameManager.Paused;


    }

}
