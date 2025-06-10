using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public int count;
    public float damage;
    public float speed;

    float timer;
    Player player;
    Enemy enemy;
    private void Awake()
    {
        player = GameManager.instance.player;
        
    }
    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            case 1:
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Fire();  
                }
                break;
            case 2:
                timer += Time.deltaTime;
                if(timer > speed)
                {
                   timer=0f;
                   Fire1();
                }
                break;
            default:
                break;
        }

        
    }
    public void levelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
        {
            Batch();
        }
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }
    public void Init(ItemData data)
    {

        name = "Weapon " + data.itemId;
        transform.parent=player.transform;
        transform.localPosition=Vector3.zero;


        id=data.itemId;
        damage = data.baseDamage;
        count=data.baseCount;

        for (int i = 0; i < GameManager.instance.pool.prefabs.Length; i++) {
            if (data.projectile == GameManager.instance.pool.prefabs[i])
            {
                prefabId = i;
                break;
            }
        }

        switch (id)
        {
            case 0:
                speed = 200;
                Batch();
                break;
            case 1:
                speed = 0.5f;
                break;

            case 2:
                speed = 5f;
                break;  
            default:
                
                break;
        }
        player.BroadcastMessage("ApplyGear",SendMessageOptions.DontRequireReceiver);
    }
    void Batch()
    {
        for (int i = 0; i < count; i++)
        {
            Transform bullet;

            if (i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
            }

            bullet.parent = transform;
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;
            Vector3 rotVec = new Vector3(0f,0f,-1f) * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 4f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);
        }
    }
    public void Fire()
    {
        if (!player.scanner.nearestTarget)
        {
            return;
        }
        Vector3 targetPos = player.scanner.nearestTarget.position;
        Debug.Log("Target Position in weapon: " + targetPos);
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.Rotate(0, 0, 90f);
        bullet.localScale = new Vector3(1f, 1f, 0f);
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
        AudioManager.instance.sfx(0);

    }
    void Fire1()
    {
        if (!player.scanner.nearestTarget)
        {
            return;
        }
        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.Rotate(0, 0, 90f);
        Vector3 scale = new Vector3(5f, 5f, 5f);
        bullet.localScale=scale;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}
