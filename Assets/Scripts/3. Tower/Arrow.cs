using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Arrow : MonoBehaviour
{
    private Action<GameObject> onRelease;
    private float damage;
    private List<Type> effectList;
    private GameObject targetAiming;
    public float Damage { get => damage; set => damage = value; }
    public GameObject TargetAiming { get => targetAiming; set => targetAiming = value; }
    public Action<GameObject> OnRelease { get => onRelease; set => onRelease = value; }
    public List<Type> EffectList { get => effectList; set => effectList = value; }

    // Start is called before the first frame update
    void Start()
    {
        //transform.eulerAngles = new(0, 0, Mathf.Atan2((TargetAiming.transform.position - transform.position).y, (TargetAiming.transform.position - transform.position).x) * Mathf.Rad2Deg);
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetAiming.GetComponent<AEnemy>().HP <= 0)
        {
            OnRelease(gameObject);
            return;
        }
        Vector2 direction = TargetAiming.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.position += 10f * Time.deltaTime * (TargetAiming.transform.position - transform.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            //Debug.Log(collision);
            OnRelease(gameObject);
        }
        if (collision.CompareTag("Minions"))
        {
            collision.GetComponent<AEnemy>().ReceiveDamage(Damage);
            SoundManagerDetail.PlaySound("Arrow_Hit");
            List<Type> types = new List<Type> { typeof(Burn) };
            AEffect.Active(collision.gameObject, types, 2);
            OnRelease(gameObject);
        }
    }
}