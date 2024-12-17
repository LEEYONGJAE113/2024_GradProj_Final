using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Quest, Level, Kill, Gold, Time, HP, Equipment }
    public InfoType type;

    private string _levelText = "<color=#569CD6>int</color> <color=#88DCFE>Level</color> ";

    private Text _text;
    private Slider _slider;

    void Awake()
    {
        _text = GetComponent<Text>();
        _slider = GetComponent<Slider>();
    }

    string GetQuestName()
    {
        string name = null;
        switch (GameManager.instance.questManager.currentQuest)
        {
            case QuestManager.Quests.Kill:
                name = "디버깅하기";
                break;
            case QuestManager.Quests.Move:
                name = "산책하기";
                break;
            case QuestManager.Quests.Survive:
                name = "살아남기";
                break;
        }
        return name;
    }
    
    void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Quest:
                _text.text = string.Format("// TODO LIST\n// {0}\n// {1:F0} / {2:F0}", 
                GetQuestName(), 
                GameManager.instance.questManager.questProgress, 
                GameManager.instance.questManager.questGoal);
                break;
            case InfoType.Level:
                _text.text = _levelText + string.Format("= {0:F0};", GameManager.instance.inGameLevel);
                break;
            case InfoType.Kill:
                _text.text = string.Format("{0:F0}", GameManager.instance.inGameKill);
                break;
            case InfoType.Gold:
                _text.text = string.Format("{0:F0}", GameManager.instance.inGameGold);
                break;
            case InfoType.Time:
                int min = Mathf.FloorToInt(GameManager.instance.currentGameTime / 60);
                int sec = Mathf.FloorToInt(GameManager.instance.currentGameTime % 60);
                _text.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;
            case InfoType.HP:
                float curHP = GameManager.instance.inGameCurrentHp;
                float maxHP = GameManager.instance.playerMaxHP;
                _slider.value = curHP/ maxHP;
                break;
            case InfoType.Equipment:
                break;
        }
    }
}
