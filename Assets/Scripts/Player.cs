using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Movement mov;
    private Shoot shoot;
    public int weaponId = 0;

    public GameObject skinObject;
    private SpriteRenderer skin;
    public Sprite[] spriteArray;
    public Sprite[] shootSpriteArray;

    void Start()
    {
        Camera.main.GetComponent<CameraMovement>().player = gameObject;

        skin = skinObject.GetComponent<SpriteRenderer>();
        mov = gameObject.GetComponent<Movement>();
        shoot = gameObject.GetComponent<Shoot>();

        skin.sprite = spriteArray[weaponId];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && shoot.shooting == false)
        {
            StartCoroutine(shootAnimation());
        }
    }

    IEnumerator shootAnimation()
    {
        skin.sprite = shootSpriteArray[weaponId];
        shoot.shoot();

        StartCoroutine(Camera.main.GetComponent<CameraMovement>().ShakeCamera(0.04f, 0.06f));

        yield return new WaitForSeconds(0.2f);
        skin.sprite = spriteArray[weaponId];
    }

    void FixedUpdate()
    {
        if (mov == null) return;

        mov.LookAtPos(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        mov.MoveByInput(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Weapon")
        {
            WeaponClass w = collision.gameObject.GetComponent<WeaponClass>();

            if(w.weaponId > weaponId)
            {
                weaponId = w.weaponId;
                shoot.damage = w.weaponDamage;
            }

            skin.sprite = spriteArray[weaponId];
        }
        else if (collision.gameObject.tag == "SafeZone" && GameController.instance.IsFlagTaken())
        {
            GameController.instance.levelUp();
        }
    }


}
