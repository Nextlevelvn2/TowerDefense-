using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarCollect : MonoBehaviour
{
    [SerializeField] Text starText;
    private float starMoveSpeed = 5f;
    private bool canMove = false;
    // Start is called before the first frame update
    void Start()
    {
    }
    void CollectStar()
    {
        canMove = true;
        Debug.Log(canMove);
    }
    private void Update()
    {
        DetectStarClick();
        if (canMove == true)
        {
            MoveStar();
        }
    }
    //Move star when being clicked
    void MoveStar()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(2.2f,5f,0f), Time.deltaTime * starMoveSpeed);
        Invoke("UpdateStarUI", 2f);
    }
    void DetectStarClick()
    {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(ray, out hit)&&hit.collider.gameObject == gameObject)
            {
                canMove = true;
            }
        }
    }
    //Remove star and update starUI
    private void UpdateStarUI()
    {
            gameObject.SetActive(false);
        CurrencyManager.Instance.stars.Enqueue(gameObject);
    }
    private void OnDisable()
    {
        CurrencyManager.Instance.GainStar(1);
    }

}
