using UnityEngine;

public class Hand : MonoBehaviour
{
    
    public SpriteRenderer spriter;

    SpriteRenderer player;

    Vector3 rightPos = new Vector3(0.3f, -0.17f, 0);
    Vector3 rightPosReverse = new Vector3(-0.17f, -0.17f, 0);

    private void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    private void LateUpdate()
    {
        bool isReverse=player.flipX;
        transform.localPosition=isReverse ? rightPosReverse : rightPos;
        spriter.flipX = isReverse;
        spriter.sortingOrder = isReverse ? 6 : 4;

    }
}
