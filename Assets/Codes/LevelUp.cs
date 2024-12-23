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
        rect.localScale= Vector3.one;//gan scale cua LevelUp bang (1,1,1)
        AudioManager.instance.sfx(2);
        AudioManager.instance.PauseMusic();
        GameManager.instance.Stop();
    }
    public void Hide()
    {
        rect.localScale = Vector3.zero;//gan scale cua LevelUp bang (0,0,0)
        GameManager.instance.Resume();
        AudioManager.instance.ResumeMusic();
        AudioManager.instance.sfx(3);
    }
    public void Select(int i)
    {
        items[i].OnClick();
    }
    void Next()
    {
        foreach (Item item in items) {
            item.gameObject.SetActive(false);
        }

        int[] ran = new int[3];
        while (true)
        {
            ran[0]=Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);//tra ve 1 so trong khoang tu 0 den so phan tu co trong LevelUp
            ran[2] = Random.Range(0, items.Length);

            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;
        }

        for (int i = 0; i < ran.Length; i++)
        {
            Item ranItem = items[ran[i]];//khai bao ranitem bang 1 phan tu ngau nhien trong LevelUp
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
}
