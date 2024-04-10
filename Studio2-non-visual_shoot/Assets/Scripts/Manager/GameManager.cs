using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    static GameManager _instance;
    private void Awake()
    {
        if (_instance !=  null)
        {
            Destroy(this);
        }
        _instance = this;
    }void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}