using System.Collections.Generic;
using Cookie.UISystems;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class NotifyUIController
    {
        private NotifyUIView uiView;
        
        public void Setup(NotifyUIView prefab)
        {
            this.uiView = UIManager.Open(prefab);
            UIManager.Hidden(this.uiView);
        }

        public async UniTask Show(List<string> messages)
        {
            this.uiView.SetAsLastSibling();
            this.uiView.Show();
            
            foreach (var message in messages)
            {
                this.uiView.Message.text = message;
                await this.uiView.Button.OnClickAsync();
            }
            
            this.uiView.Hidden();
        }

        public async UniTask ShowUnlockContents()
        {
            var userData = UserData.current;
            var enemies = userData.notifyEnemies
                .Select(x =>
                {
                    var enemyStatus = MasterDataEnemyStatus.Instance.enemyStatusList.Find(y => y.id == x);
                    var fieldData = MasterDataFieldData.Instance.records.Find(y => y.id == enemyStatus.fieldId);
                    return string.Format(LocalizeString.Get("UI", "NotifyUnlockEnemy"), fieldData.Name, enemyStatus.Name);
                });
            var weaponGachas = userData.notifyWeaponGachas
                .Select(x =>
                {
                    var weaponGacha = MasterDataWeaponGacha.Instance.gachas.Find(y => y.id == x);
                    return string.Format(LocalizeString.Get("UI", "NotifyUnlockGacha"), weaponGacha.Name);
                });
            var armorGachas = userData.notifyArmorGachas
                .Select(x =>
                {
                    var armorGacha = MasterDataArmorGacha.Instance.gachas.Find(y => y.id == x);
                    return string.Format(LocalizeString.Get("UI", "NotifyUnlockGacha"), armorGacha.Name);
                });
            var accessoryGachas = userData.notifyAccessoryGachas
                .Select(x =>
                {
                    var accessoryGacha = MasterDataAccessoryGacha.Instance.gachas.Find(y => y.id == x);
                    return string.Format(LocalizeString.Get("UI", "NotifyUnlockGacha"), accessoryGacha.Name);
                });
            var messages = new List<string>();
            messages.AddRange(enemies);
            messages.AddRange(weaponGachas);
            messages.AddRange(armorGachas);
            messages.AddRange(accessoryGachas);
            
            userData.notifyEnemies.Clear();
            userData.notifyWeaponGachas.Clear();
            userData.notifyArmorGachas.Clear();
            userData.notifyAccessoryGachas.Clear();
            SaveData.SaveUserData(userData);

            await this.Show(messages);
        }
    }
}
