using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class SkillManager : MonoBehaviour
{
    //Field
    [SerializeField] private Button skillbtn;
    [SerializeField] private GameObject fireAttack;
    private bool canFire = false;
    private float fireMoveSpeed = 17f;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] Transform startFirePos;
    [SerializeField] GameObject waypoint;
    [SerializeField] GameObject canFireImg;
    [SerializeField] GameObject cannotFireImg;
    private float intervalFire = 17f;
    private float timeFireCount;
    Vector3 cellPosCenter;
    // Start is called before the first frame update
    void Start()
    {
        skillbtn.GetComponent<Button>().onClick.AddListener(SetFire);
        timeFireCount = intervalFire + Time.deltaTime;
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.time>= timeFireCount)
        {
            if (canFire)
            {
                FireAttack();
            }
            //Set image of fire button
            canFireImg.SetActive(true);
            cannotFireImg.SetActive(false);
        }
        else
        {
            //Set image of fire button
            canFireImg.SetActive(false);
            cannotFireImg.SetActive(true);
        }
    }
    void SetFire()
    {
        if (canFire == false) canFire = true;
        else canFire = false;
    }
    void FireAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var cellPos = tilemap.WorldToCell(mousePos);
            var cellPosCentered = tilemap.GetCellCenterWorld(cellPos);
            cellPosCenter = cellPosCentered;
            StartFire();
            Invoke("StartFire", 0.2f);
            canFire = false;
            timeFireCount = Time.time + intervalFire;
        }
    }
    void StartFire()
    {
        GameObject fire = Instantiate(fireAttack, startFirePos.transform.position, Quaternion.identity);
        GameObject wp = Instantiate(waypoint, cellPosCenter, Quaternion.identity);
        fire.SetActive(true);
        wp.SetActive(true);

        //Rotate fire toward mouspos
        fire.GetComponent<Fire>().SetTarget(cellPosCenter);

        //Move fire toward mousepos
        Rigidbody2D rb = fire.GetComponent<Rigidbody2D>();
        Vector2 direction = (cellPosCenter - fire.transform.position).normalized;
        rb.velocity = direction * fireMoveSpeed;
    }

}
