using Cysharp.Threading.Tasks;
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
                SetupMasterData()
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
                var masterDataInitialEquipment = MasterDataInitialEquipment.Instance;
                UserData.current.weapons.Add(masterDataInitialEquipment.weapon);
                UserData.current.armors.Add(masterDataInitialEquipment.armor);
                UserData.current.accessories.Add(masterDataInitialEquipment.accessory);
                UserData.current.weaponCreatedNumber++;
                UserData.current.armorCreatedNumber++;
                UserData.current.accessoryCreatedNumber++;
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
                MasterDataInitialEquipment.LoadAsync("Assets/MasterData/MasterDataInitialEquipment.asset"),
                MasterDataPlayerStatus.LoadAsync("Assets/MasterData/MasterDataPlayerStatus.asset")
                );
        }
    }
}
