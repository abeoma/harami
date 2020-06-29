using System.Collections;
using System;
using UnityEngine;
using System.Net.NetworkInformation;
using UnityEditor;

[Serializable]
public class Ballcontoller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int mode;

    private float changeSpeed = 0.01f;
    private float changeTime = 30.0f;


    [SerializeField]
    private GameObject ballObject = null;

    [SerializeField]
    private GameObject cameraObject = null;

    struct Ball
    {
        public GameObject ball;
        public Vector3 pos;
    }

    private Ball mainBall;


    private float ratio = 1.0f;
    private float time = 0;

    private float lineCenterSpeedRatio = 0.6f;
    private float lineCneterSwingRaito = 3.0f;

    private float lineCenterNear =8.0f;
    private float lineCenterFar = 40.0f;



    void Start()
    {
        mainBall.ball = ballObject;

    }

    // Update is called once per frame
    void Update()
    {
        //モード毎に処理を切り分け
        switch (mode) {
            case 0:
                LineAlone();
                break;
            case 1:
                SwingAlone();
                break;
            case 2:

                UpDownAlone();
                break;
            default:

                break;
        }

        Move();

        time += Time.deltaTime;

        if(time > changeTime)
        {
            ratio = 1.0f;
            time = 0;
            mode++;

            if(mode > 2)
            {
                mode = 0;
            }
        }
    }

    private void Move()
    {
        ratio -= changeSpeed * Time.deltaTime;

        if (ratio < 0) ratio = 0;

        ballObject.transform.position = mainBall.pos + (mainBall.ball.transform.position - mainBall.pos) * ratio;

    }

    private void LineAlone()
    {
        float radius = (lineCenterFar - lineCenterNear) / 2;
        Vector3 _pos = cameraObject.transform.position + cameraObject.transform.forward * (lineCenterNear + radius);
        Vector3 _vec = cameraObject.transform.forward * Mathf.Cos(time * lineCenterSpeedRatio) * radius;
        mainBall.pos = _pos + _vec;

    }

    private void SwingAlone()
    {
        //前後
        float radius = (lineCenterFar - lineCenterNear) / 2;
        Vector3 _pos = cameraObject.transform.position + cameraObject.transform.forward * (lineCenterNear + radius);
        Vector3 _vec = cameraObject.transform.forward * Mathf.Cos(time * lineCenterSpeedRatio) * radius;
        //距離が近いほど振れ幅が
        Vector3 _roll= cameraObject.transform.right * Mathf.Sin(time * lineCneterSwingRaito) * (radius *0.6f);
        mainBall.pos = _pos + _vec + _roll;
        //左右の揺れ


    }

    private void UpDownAlone()
    {
        //前後
        float radius = (lineCenterFar - lineCenterNear) / 2;
        Vector3 _pos = cameraObject.transform.position + cameraObject.transform.forward * (lineCenterNear + radius);
        Vector3 _vec = cameraObject.transform.forward * Mathf.Cos(time * lineCenterSpeedRatio) * radius;
        //距離が近いほど振れ幅が
        Vector3 _roll = cameraObject.transform.up * Mathf.Sin(time * lineCneterSwingRaito) * (radius * 0.4f);
        mainBall.pos = _pos + _vec + _roll;
        //左右の揺れ


    }
}
