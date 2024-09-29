using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class Trash : MonoBehaviour
{
    private Rigidbody2D thisObject;
    private GameStates state;
    public Types type;
    private bool drag;
    bool once;
    void Start()
    {
        thisObject = gameObject.GetComponent<Rigidbody2D>();
        state = GameStates.Start;
        thisObject.gravityScale = 0.2f;
        once = false;
    }

    void Update()
    {


        if (drag && !once)
        {
            Move();
        }
        if (!drag && !once)
        {
            MoveToBin();
        }

    }
    void OnMouseDown()
    {
        drag = true;
    }
    void OnMouseUp()
    {
        drag = false;
    }
    void Move()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

        thisObject.transform.position = new Vector3(mousePosition.x, thisObject.transform.position.y, thisObject.transform.position.z);
        Camera.main.GetComponent<DisplayBin>().type=type;
    }
    void MoveToBin()
    {
        thisObject.gravityScale = 48f*Time.deltaTime;
    }
    public GameStates SetState
    {
        get { return state; }
        set { state = value; }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!once)
        {
            if (collision.transform.tag == "Parent")
            {
                collision.transform.DOShakePosition(0.05f, 0.2f, 3, 90, false, true, ShakeRandomnessMode.Full);
                gameObject.transform.DOScale(0, 0.3f).OnComplete(() => { thisObject.constraints = RigidbodyConstraints2D.FreezeAll; gameObject.transform.SetParent(collision.transform); });
                if (collision.transform.GetComponent<Bin>().newType == type && Camera.main.GetComponent<Enviroment>().slider.value >= 0 && Camera.main.GetComponent<Enviroment>().slider.value != 1)
                {
                    Camera.main.GetComponent<Enviroment>().slider.value -= 0.03f;
                }
                else if (collision.transform.GetComponent<Bin>().newType != type)
                {
                    if (Camera.main.GetComponent<Enviroment>().slider.value < 1)
                    {
                        Camera.main.GetComponent<Enviroment>().slider.value += 0.06f;
                    }
                }
                else if (Camera.main.GetComponent<Enviroment>().slider.value == 1)
                {
                    Camera.main.GetComponent<Enviroment>().slider.value = 0;
                }

            }
            else
            {
                if (collision.transform.tag == "Untagged")
                {
                    gameObject.transform.SetParent(collision.transform);
                    if (Camera.main.GetComponent<Enviroment>().slider.value < 1)
                    {
                        Camera.main.GetComponent<Enviroment>().slider.value += 0.06f;
                    }
                }

            }
            Camera.main.GetComponent<GameManager>().Collision.Play();
            transform.DOShakePosition(0.1f, 0.2f, 0, 90, false, true, ShakeRandomnessMode.Full);
            thisObject.constraints = RigidbodyConstraints2D.FreezeAll;
            once = true;
        }
    }


}
