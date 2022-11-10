using System.Collections.Generic;
using Cookie.UISystems;
using Cysharp.Threading.Tasks;
using SerializableCollections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Localization.Settings;

namespace Cookie
{
    /// <summary>
    /// ブートシステム
    /// </summary>
    public static class BootSystem
    {
        public static UniTask IsReady { get; private set; }
            
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Setup()
        {
            IsReady = SetupInternal();
        }

        private static async UniTask SetupInternal()
        {
            await UniTask.WhenAll(
                SetupLocalizationAsync(),
                SetupMasterData(),
                SetupUISystem()
                );

            await SetupUserDataAsync();
            
            IsReady = UniTask.CompletedTask;
        }

        private static async UniTask SetupLocalizationAsync()
        {
            await LocalizationSettings.InitializationOperation.Task;
        }

        private static UniTask SetupUserDataAsync()
        {
            if (!SaveData.ContainsUserData())
            {
                UserData.current = new UserData();
                var masterDataInitialEquipment = MasterDataInitialUserData.Instance;
                UserData.current.weapons.Add(masterDataInitialEquipment.weapon);
                UserData.current.armors.Add(masterDataInitialEquipment.armor);
                UserData.current.accessories.Add(masterDataInitialEquipment.accessory);
                UserData.current.weaponCreatedNumber++;
                UserData.current.armorCreatedNumber++;
                UserData.current.accessoryCreatedNumber++;
                UserData.current.unlockEnemies = new List<int>(masterDataInitialEquipment.unlockEnemies);
                UserData.current.unlockWeaponGachas = new List<int>(masterDataInitialEquipment.unlockWeapons);
                UserData.current.unlockArmorGachas = new List<int>(masterDataInitialEquipment.unlockArmors);
                UserData.current.unlockAccessoryGachas = new List<int>(masterDataInitialEquipment.unlockAccessories);
                UserData.current.weaponGachaInvokeCounts = new IntIntSerializableDictionary();
                UserData.current.armorGachaInvokeCounts = new IntIntSerializableDictionary();
                UserData.current.accessoryGachaInvokeCounts = new IntIntSerializableDictionary();
                UserData.current.defeatedEnemies = new IntIntSerializableDictionary();
                SaveData.SaveUserData(UserData.current);
            }
            else
            {
                UserData.current = SaveData.LoadUserData();
            }
            return UniTask.CompletedTask;
        }

        private static UniTask SetupMasterData()
        {
            return UniTask.WhenAll(
                MasterDataWeaponGacha.LoadAsync("Assets/MasterData/MasterDataWeaponGacha.asset"),
                MasterDataArmorGacha.LoadAsync("Assets/MasterData/MasterDataArmorGacha.asset"),
                MasterDataAccessoryGacha.LoadAsync("Assets/MasterData/MasterDataAccessoryGacha.asset"),
                MasterDataActiveSkill.LoadAsync("Assets/MasterData/MasterDataActiveSkill.asset"),
                MasterDataPassiveSkill.LoadAsync("Assets/MasterData/MasterDataPassiveSkill.asset"),
                MasterDataInitialUserData.LoadAsync("Assets/MasterData/MasterDataInitialUserData.asset"),
                MasterDataEnemyStatus.LoadAsync("Assets/MasterData/MasterDataEnemyStatus.asset"),
                MasterDataFieldData.LoadAsync("Assets/MasterData/MasterDataFieldData.asset")
                );
        }

        private static UniTask SetupUISystem()
        {
            return UIManager.Setup();
        }
    }
}
