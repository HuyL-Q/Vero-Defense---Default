using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillArrow : MonoBehaviour
{
    GameObject Enemies;
    Vector3 target;
    Vector3 basePos;
    float _moveSpeed = 30;
    float damage;

    public float Damage { get => damage; set => damage = value; }

    // Start is called before the first frame update
    void Start()
    {
        Enemies = GameObject.Find("Enemy");
        Destroy(gameObject, 3);
        basePos = transform.position;
        Vector3? t = getTargetEnemy();
        if (t == null)
        {
            Destroy(gameObject);
        }
        else
        {
            target = t ?? new Vector3(1, 1, 1);
        }
        Vector2 direction = (target + (target - basePos) * 5) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target + (target - basePos) * 5, _moveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Minions"))
        {
            collision.gameObject.GetComponent<AEnemy>().ReceiveDamage(Damage);
            AEffect.Active(collision.gameObject, new List<Type> { typeof(Burn) }, 5);
        }
    }
    Vector3? getTargetEnemy(int? i = null)
    {
        int j = i ?? 0;
        //if (ChildCountActive(Enemies.transform) == 0)
        //    return null;
        Debug.Log(i);
        if (i >= Enemies.transform.childCount)
            return null;
        return Enemies.transform.GetChild(j).gameObject.activeSelf ? Enemies.transform.GetChild(j).transform.position : getTargetEnemy(++j);
    }
    public static int ChildCountActive(Transform t)
    {
        int k = 0;
        foreach (Transform c in t)
        {
            if (c.gameObject.activeSelf)
                k++;
        }
        return k;
    }
}
