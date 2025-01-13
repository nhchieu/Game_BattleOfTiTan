using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
        AudioManager.instance.ChanceVolume(0.01f);
        AudioManager.instance.sfx(2);
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
        AudioManager.instance.ChanceVolume(GameManager.instance.bgmBattleVolume);
        AudioManager.instance.sfx(2);
        AudioManager.instance.sfx(3);
    }

    public void Select(int i)
    {
        items[i].OnClick();
    }

    void Next()
    {
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        var indices = new System.Collections.Generic.List<int> { 0, 1, 2, 3, 4, 5 }; 
        Shuffle(indices);

        for (int i = 0; i < 3; i++)
        {
            var ranItem = items[indices[i]];

            if (ranItem.level == ranItem.Data.damages.Length)
            {
                items[4].gameObject.SetActive(true); 
            }
            else
            {
                ranItem.gameObject.SetActive(true); 
            }
        }
    }

    private void Shuffle(System.Collections.Generic.List<int> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
