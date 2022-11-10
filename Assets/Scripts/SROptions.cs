using System.Collections.Generic;
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

    private const string CategoryUser = "UserData";

    [Category(CategoryUser)]
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
        
        SaveData.SaveUserData(UserData.current);
    }

    [Category(CategoryUser)]
    public void AddMoney()
    {
        UserData.current.AddMoney(1000000);
        SaveData.SaveUserData(UserData.current);
    }

    public void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public async void ShowNotify()
    {
        Debug.Log("Start ShowNotify");
        await UIManager.NotifyUIController.Show(new List<string>{"Test1", "Test2"});
        Debug.Log("End ShowNotify");
    }

    public async void ShowPopup()
    {
        var result = await UIManager.PopupUIController.ShowAsync("Yes?");
        Debug.Log(result);
    }
}
