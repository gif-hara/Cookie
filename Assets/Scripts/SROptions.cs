using System.ComponentModel;
using Cookie;
using Cookie.UISystems;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// 
/// </summary>
public partial class SROptions
{
    private const string CategoryDefault = "Default";

    public void UnlockAll()
    {
        foreach (var enemyStatus in MasterDataEnemyStatus.Instance.enemyStatusList)
        {
            UserData.current.UnlockEnemy(enemyStatus.id);
        }
        foreach (var gacha in MasterDataWeaponGacha.Instance.gachas)
        {
            UserData.current.UnlockWeaponGacha(gacha.id);
        }
        foreach (var gacha in MasterDataArmorGacha.Instance.gachas)
        {
            UserData.current.UnlockArmorGacha(gacha.id);
        }
        foreach (var gacha in MasterDataAccessoryGacha.Instance.gachas)
        {
            UserData.current.UnlockAccessoryGacha(gacha.id);
        }
    }

    public void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public async void ShowNotify()
    {
        Debug.Log("Start ShowNotify");
        await UIManager.NotifyUIController.Show(new(){"Test1", "Test2"});
        Debug.Log("End ShowNotify");
    }
}
