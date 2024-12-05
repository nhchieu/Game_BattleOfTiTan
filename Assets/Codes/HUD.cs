using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp,Level,Kill,Time,Health }
    public InfoType type;

    Text myText;
    Slider mySlider;

    private void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (type) {
            case InfoType.Exp:
                float curExp=GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[Mathf.Min(GameManager.instance.level,GameManager.instance.nextExp.Length - 1)];
                mySlider.value = curExp/maxExp;
                break;
            case InfoType.Level:
                myText.text = "Level-"+GameManager.instance.level;
                break;
            case InfoType.Kill:
                myText.text=""+GameManager.instance.kill;
                break;
            case InfoType.Time:
                float time = GameManager.instance.gameTime;
                int min = Mathf.FloorToInt( time / 60);
                int sec = Mathf.FloorToInt(time % 60);
                myText.text=string.Format("{0:D2}:{1:D2}",min,sec);
                break;
            case InfoType.Health:
                float health=GameManager.instance.player.Health;
                float maxHealth=GameManager.instance.player.maxHealth;
                mySlider.value = health/maxHealth;
                break;
        }
    }
}
