using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goods : MonoBehaviour
{
    [SerializeField] public Text moneyText;
    [SerializeField] public Text headText;

    public static int money;

    public static void ArmyDrop()
    {
        int drop = Random.Range(10, 21);    // 10에서 20 사이 무작위 정수
        money += drop;
    }

    public static void CaoRenDrop()
    {
        money += 200;
        bossHead.caoRen += 1;
    }

    public void CaoHongDrop()
    {
        money += 200;
        bossHead.caoHong += 1;
    }
    public void ZhangLiaoDrop()
    {
        money += 1000;
        bossHead.zhangLiao += 1;
    }
    public void XiahouDunDrop()
    {
        money += 2000;
        bossHead.xiahouDun += 1;
    }
    public void XiahouYuanDrop()
    {
        money += 4000;
        bossHead.xiahouYuan += 1;
    }
    public void CaoCaoDrop()
    {
        money += 10000;
        bossHead.caoCao += 1;
    }

    void Update()
    {
        moneyText.text = ":" + money;
        headText.text = "      :" + bossHead.caoRen + "      :" + bossHead.caoHong + "       :" +
        bossHead.zhangLiao + "       :" + bossHead.xiahouDun + "       :" + bossHead.xiahouYuan + "       :" + bossHead.caoCao;
    }
}

public class bossHead
{
    public static int caoRen = 0;
    public static int caoHong = 0;
    public static int zhangLiao = 0;
    public static int xiahouDun = 0;
    public static int xiahouYuan = 0;
    public static int caoCao = 0;
}
