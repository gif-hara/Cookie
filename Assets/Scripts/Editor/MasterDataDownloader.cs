using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Localization;
using UnityEngine.Networking;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public static class MasterDataDownloader
    {
        [MenuItem("HK/Cookie/Download MasterData/Player")]
        private static async void DownloadMasterDataPlayerStatus()
        {
            var text = await DownloadFromSpreadSheet("Player");
            var json = JsonUtility.FromJson<PlayerStatus.Json>(text);
            var masterDataPlayerStatus = AssetDatabase.LoadAssetAtPath<MasterDataPlayerStatus>("Assets/MasterData/MasterDataPlayerStatus.asset");
            masterDataPlayerStatus.playerStatusList.Clear();
            masterDataPlayerStatus.playerStatusList.AddRange(json.elements);
            EditorUtility.SetDirty(masterDataPlayerStatus);
            AssetDatabase.SaveAssets();
        }

        [MenuItem("HK/Cookie/Download MasterData/WeaponGacha")]
        private static async void DownloadMasterDataWeaponGacha()
        {
            var items = await UniTask.WhenAll(
                DownloadFromSpreadSheet("WeaponGachaSpec"),
                DownloadFromSpreadSheet("WeaponGachaPhysicalStrength"),
                DownloadFromSpreadSheet("WeaponGachaMagicStrength"),
                DownloadFromSpreadSheet("WeaponGachaSkillNumber"),
                DownloadFromSpreadSheet("WeaponGachaActiveSkill")
                );
            var weaponGachaSpecJson = JsonUtility.FromJson<WeaponGachaSpec.Json>(items.Item1);
            var weaponGachaPhysicalStrengthJson = JsonUtility.FromJson<WeaponGachaPhysicalStrength.Json>(items.Item2);
            var weaponGachaMagicStrengthJson = JsonUtility.FromJson<WeaponGachaMagicStrength.Json>(items.Item3);
            var weaponGachaSkillNumberJson = JsonUtility.FromJson<WeaponGachaSkillNumber.Json>(items.Item4);
            var weaponGachaActiveSkillJson = JsonUtility.FromJson<WeaponGachaActiveSkill.Json>(items.Item5);
            
            var masterDataWeaponGacha = AssetDatabase.LoadAssetAtPath<MasterDataWeaponGacha>("Assets/MasterData/MasterDataWeaponGacha.asset");
            masterDataWeaponGacha.gachas.Clear();
            masterDataWeaponGacha.gachas.AddRange(
                weaponGachaSpecJson.elements.Select(x =>
                {
                    return new WeaponGacha
                    {
                        id = x.id,
                        nameKey = new LocalizedString("Gacha", x.nameKey),
                        physicalStrengths = new List<InstanceRangeParameterWithWeight>
                            (
                            weaponGachaPhysicalStrengthJson.elements
                                .Where(y => y.gachaId == x.id)
                                .Select(y => new InstanceRangeParameterWithWeight
                                {
                                    value = new InstanceRangeParameter
                                    {
                                        min = y.min,
                                        max = y.max,
                                        rare = y.rare,
                                    },
                                    weight = y.weight
                                })
                            ),
                        magicStrengths = new List<InstanceRangeParameterWithWeight>
                            (
                            weaponGachaMagicStrengthJson.elements
                                .Where(y => y.gachaId == x.id)
                                .Select(y => new InstanceRangeParameterWithWeight
                                {
                                    value = new InstanceRangeParameter
                                    {
                                        min = y.min,
                                        max = y.max,
                                        rare = y.rare,
                                    },
                                    weight = y.weight
                                })
                            ),
                        skillNumbers = new List<InstanceRangeParameterWithWeight>
                            (
                            weaponGachaSkillNumberJson.elements
                                .Where(y => y.gachaId == x.id)
                                .Select(y => new InstanceRangeParameterWithWeight
                                {
                                    value = new InstanceRangeParameter
                                    {
                                        min = y.min,
                                        max = y.max,
                                        rare = y.rare,
                                    },
                                    weight = y.weight
                                })
                            ),
                        activeSkillIds = new List<InstanceParameterWithWeight>
                            (
                            weaponGachaActiveSkillJson.elements
                                .Where(y => y.gachaId == x.id)
                                .Select(y => new InstanceParameterWithWeight
                                {
                                    value = new InstanceParameter
                                    {
                                        parameter = y.value,
                                        rare = y.rare,
                                    },
                                    weight = y.weight
                                })
                            ),
                    };
                })
                );
        }

        [MenuItem("HK/Cookie/Download MasterData/ArmorGacha")]
        private static async void DownloadMasterDataArmorGacha()
        {
            var items = await UniTask.WhenAll(
                DownloadFromSpreadSheet("ArmorGachaSpec"),
                DownloadFromSpreadSheet("ArmorGachaPhysicalDefense"),
                DownloadFromSpreadSheet("ArmorGachaMagicDefense"),
                DownloadFromSpreadSheet("ArmorGachaHitPoint"),
                DownloadFromSpreadSheet("ArmorGachaSpeed")
                );
            var armorGachaSpecJson = JsonUtility.FromJson<ArmorGachaSpec.Json>(items.Item1);
            var armorGachaPhysicalDefenseJson = JsonUtility.FromJson<ArmorGachaPhysicalDefense.Json>(items.Item2);
            var armorGachaMagicDefenseJson = JsonUtility.FromJson<ArmorGachaMagicDefense.Json>(items.Item3);
            var armorGachaHitPointJson = JsonUtility.FromJson<ArmorGachaHitPoint.Json>(items.Item4);
            var armorGachaSpeedJson = JsonUtility.FromJson<ArmorGachaSpeed.Json>(items.Item5);
            
            var masterDataArmorGacha = AssetDatabase.LoadAssetAtPath<MasterDataArmorGacha>("Assets/MasterData/MasterDataArmorGacha.asset");
            masterDataArmorGacha.gachas.Clear();
            masterDataArmorGacha.gachas.AddRange(
                armorGachaSpecJson.elements.Select(x =>
                {
                    return new ArmorGacha
                    {
                        id = x.id,
                        nameKey = new LocalizedString("Gacha", x.nameKey),
                        physicalDefenses = new List<InstanceRangeParameterWithWeight>
                            (
                            armorGachaPhysicalDefenseJson.elements
                                .Where(y => y.gachaId == x.id)
                                .Select(y => new InstanceRangeParameterWithWeight
                                {
                                    value = new InstanceRangeParameter
                                    {
                                        min = y.min,
                                        max = y.max,
                                        rare = y.rare,
                                    },
                                    weight = y.weight
                                })
                            ),
                        magicDefenses = new List<InstanceRangeParameterWithWeight>
                            (
                            armorGachaMagicDefenseJson.elements
                                .Where(y => y.gachaId == x.id)
                                .Select(y => new InstanceRangeParameterWithWeight
                                {
                                    value = new InstanceRangeParameter
                                    {
                                        min = y.min,
                                        max = y.max,
                                        rare = y.rare,
                                    },
                                    weight = y.weight
                                })
                            ),
                        hitPoints = new List<InstanceRangeParameterWithWeight>
                            (
                            armorGachaHitPointJson.elements
                                .Where(y => y.gachaId == x.id)
                                .Select(y => new InstanceRangeParameterWithWeight
                                {
                                    value = new InstanceRangeParameter
                                    {
                                        min = y.min,
                                        max = y.max,
                                        rare = y.rare,
                                    },
                                    weight = y.weight
                                })
                            ),
                        speeds = new List<InstanceRangeParameterWithWeight>
                            (
                            armorGachaSpeedJson.elements
                                .Where(y => y.gachaId == x.id)
                                .Select(y => new InstanceRangeParameterWithWeight
                                {
                                    value = new InstanceRangeParameter
                                    {
                                        min = y.min,
                                        max = y.max,
                                        rare = y.rare,
                                    },
                                    weight = y.weight
                                })
                            ),
                    };
                })
                );
        }
        
        private static async UniTask<string> DownloadFromSpreadSheet(string sheetName)
        {
            var sheetUrl = File.ReadAllText("masterdata_sheet_url.txt");
            var bearer = File.ReadAllText("bearer.txt");
            var request = UnityWebRequest.Get($"{sheetUrl}?mode={sheetName}");
            request.SetRequestHeader("Authorization", $"Bearer {bearer}");
            var operation = request.SendWebRequest();
            while (!operation.isDone)
            {
                await UniTask.Delay(100);
            }

            var result = operation.webRequest.downloadHandler.text;
            Debug.Log(result);

            return result;
        }
    }
}
