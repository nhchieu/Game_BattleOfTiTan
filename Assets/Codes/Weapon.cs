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


    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }
    private void Start()
    {
        Init();
    }
    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            case 1:
                //if (Input.GetMouseButtonDown(0))
                //{
                //    Debug.Log("hien thi");
                //    Fire();
                //}
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    if (Input.GetMouseButton(0))
                    {

                        Fire();

                    }
                }
                break;
            default:

                
                break;
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            levelUp(5,1);
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
    }
    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 200;
                Batch();
                break;
            
            default:
                speed = 0.3f;
                break;
        }
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
                bullet= GameManager.instance.pool.Get(prefabId).transform;
            }
            
            bullet.parent = transform;
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;
            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 3f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1,Vector3.zero);
        }
    }
    void Fire()
    {
        if (!player.scanner.nearestTarget)
        {
            return;
        }
        Vector3 targetPos=player.scanner.nearestTarget.position;
        Vector3 dir=targetPos-transform.position;
        dir = dir.normalized;

        Transform bullet= GameManager.instance.pool.Get(prefabId).transform;
        bullet.position=transform.position;
        bullet.Rotate(0, 0, 90f);
        bullet.rotation= Quaternion.FromToRotation(Vector3.up,dir);
        bullet.GetComponent<Bullet>().Init(damage,count,dir);

    }
    
}
