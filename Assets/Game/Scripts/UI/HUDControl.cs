using TMPro;
using UnityEngine;

public class HUDControl : MonoBehaviour
{
    public GameObject Root;
    public GameObject Target;
    public TextMeshProUGUI TypeText;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI HpCurrentText;
    public TextMeshProUGUI HpMaxText;
    public TextMeshProUGUI AmmoText;
    public TextMeshProUGUI CountText;
    public GameObject InteractionObject;
    public GameObject ConsumeObject;

    // Update is called once per frame
    void Update()
    {
        if (IsVisible())
        {
            Root.SetActive(true);
        }
        else
        {
            Root.SetActive(false);
        }
    }

    bool IsVisible()
    {
        if(Target == null || GameProcess.Instance.GameState != GameProcess.GameProcessState.Ingame)
        {
            return false;
        }
        return true;
    }

    public void UpdateType()
    {
        TypeText.text = Target.GetComponent<GameCharacter>().CharacterInfo.IsSpy ? "SPY" : "HUMAN";
    }
    public void UpdateNameTag()
    {
        UpdateName(Target.GetComponent<GameCharacter>().PlayerName);
        UpdateHp(Target.GetComponent<GameCharacter>().CurrentStatus.HP_NOW, Target.GetComponent<GameCharacter>().CurrentStatus.HP_MAX);
    }

    void UpdateHp(int curHp, int maxHp)
    {
        HpCurrentText.text = curHp.ToString();
        HpMaxText.text = maxHp.ToString();
    }

    void UpdateName(string name)
    {
        NameText.text = name;
    }

    public void UpdateAmmo()
    {
        AmmoText.text = Target.GetComponent<GameCharacter>().CurrentStatus.Ammo.ToString();
    }
    
    public void UpdateCount(int count)
    {
        int min = count / 60;
        int sec = count % 60;
        CountText.text = string.Format("{0:00}:{1:00}", min, sec);
    }
}
