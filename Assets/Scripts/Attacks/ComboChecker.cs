using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Timers;

public class ComboChecker : MonoBehaviour
{
    [SerializeField] PlayerAttack playerAttack;
    [SerializeField] Timer timer;
    [SerializeField] TextMeshProUGUI comboText;
    [SerializeField] float MeleeAttackDamage;
    [SerializeField] float HeavyAttackDamage;
    [SerializeField] float ArrowUpAttackDamage;
    [SerializeField] float ArrowDownAttackDamage;
    [SerializeField] float MoveDownAttackDamage;
    private float comboCooldown;
    private float curtime;

    HashSet<string> combos;
    string curCombo;
    bool registered;

    private float cooldown;
    private float attack_timer;

    [SerializeField] PlayerAnimation playerAnimation;
    enum ATK_BUTTONS { NONE, LEFT_CLICK, RIGHT_CLICK, MOVE_UP, MOVE_DOWN, MOVE_LEFT, MOVE_RIGHT, ARROW_UP, ARROW_DOWN }
    void Start()
    {
        curCombo = "";
        combos = new HashSet<string> { "01", "20", "60", "30" };
        registered = false;
        curtime = 0;
        comboCooldown = timer.BPM_Timer;
        //StartCoroutine(MyFixedUpd());
    }

    private void Update()
    {
        if (PauseMenu.isGamePaused)
        {
            return;
        }
        curtime += Time.deltaTime;
        if (timer.CurTick)
        {
            var btn = ParseKey();
            if (btn != ATK_BUTTONS.NONE)
            {
                curCombo += (int)(btn - 1);
                //Debug.Log(curCombo);
                var possibles = combos.Where(x => curCombo.Contains(x));
                if (possibles.Count() > 0 && curtime > comboCooldown)
                {
                    curCombo = possibles.First();
                    PerformCombo(curCombo);
                    curCombo = "";
                    curtime = 0;
                }
                else
                {
                    if (curCombo.Length > 0 && curCombo.Last() == '0')
                    {
                        playerAttack.Attack(MeleeAttackDamage, false);
                    }
                    if (curCombo.Length > 0 && curCombo.Last() == '1')
                    {
                        playerAttack.RangeAttack(false);
                    }
                }
            }
        }
        else
        {
            curCombo = "";
            var btn = ParseKey();
            if (btn != ATK_BUTTONS.NONE)
            {
                curCombo += (int)(btn - 1);
            }
            if (curCombo.Length > 0 && curCombo.Last() == '0')
            {
                playerAttack.Attack(MeleeAttackDamage, false);
            }
            if (curCombo.Length > 0 && curCombo.Last() == '1')
            {
                playerAttack.RangeAttack(false);
            }
        }
    }
    ATK_BUTTONS ParseKey()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.K)) return ATK_BUTTONS.LEFT_CLICK;
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown("l")) return ATK_BUTTONS.RIGHT_CLICK;
        if (Input.GetKeyDown("space")) return ATK_BUTTONS.MOVE_UP;
        if (Input.GetKeyDown("s")) return ATK_BUTTONS.MOVE_DOWN; 
        if (Input.GetKeyDown("a")) return ATK_BUTTONS.MOVE_LEFT;
        if (Input.GetKeyDown("d")) return ATK_BUTTONS.MOVE_RIGHT;
        if (Input.GetKeyDown("w")) return ATK_BUTTONS.ARROW_UP;
        return ATK_BUTTONS.NONE;
    }

    IEnumerator ShowText(string text)
    {
        comboText.text = text;

        comboText.faceColor = new Color32(255, 255, 255, 255);

        yield return new WaitForSeconds(1);

        comboText.faceColor = new Color32(255, 255, 255, 0);
    }

    void PerformCombo(string cmb)
    {
        switch (cmb)
        {
            case "01":
                StartCoroutine(ShowText("Long range attack!"));
                playerAttack.RangeCombo();
                break;
            case "20":
                StartCoroutine(ShowText("Move down attack!"));
                playerAttack.ComboAttack23(MoveDownAttackDamage);
                playerAnimation.ComboPerformed("PlayerMoveDownCombo");
                break;
            case "60":
                StartCoroutine(ShowText("Arrow up attack!"));
                playerAttack.ComboAttack60(ArrowUpAttackDamage);
                playerAnimation.ComboPerformed("PlayerArrowUpCombo");
                break;
            case "30":
                playerAttack.ComboAttack30(ArrowDownAttackDamage);
                StartCoroutine(ShowText("Arrow down attack!"));
                playerAnimation.ComboPerformed("PlayerArrowDownCombo");
                break;

        }
    }
}
