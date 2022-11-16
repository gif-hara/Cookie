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
        [MenuItem("HK/Cookie/Download MasterData/WeaponGacha")]
        private static async void DownloadMasterDataWeaponGacha()
        {
            var items = await UniTask.WhenAll(
                DownloadFromSpreadSheet("WeaponGachaSpec"),
                DownloadFromSpreadSheet("WeaponGachaPhysicalStrength"),
                DownloadFromSpreadSheet("WeaponGachaMagicStrength"),
                DownloadFromSpreadSheet("WeaponGachaSkillNumber"),
                DownloadFromSpreadSheet("WeaponGachaActiveSkill"),
                DownloadFromSpreadSheet("WeaponGachaCriticalRate")
                );
            var weaponGachaSpecJson = JsonUtility.FromJson<WeaponGachaSpec.Json>(items.Item1);
            var weaponGachaPhysicalStrengthJson = JsonUtility.FromJson<WeaponGachaPhysicalStrength.Json>(items.Item2);
            var weaponGachaMagicStrengthJson = JsonUtility.FromJson<WeaponGachaMagicStrength.Json>(items.Item3);
            var weaponGachaSkillNumberJson = JsonUtility.FromJson<WeaponGachaSkillNumber.Json>(items.Item4);
            var weaponGachaActiveSkillJson = JsonUtility.FromJson<WeaponGachaActiveSkill.Json>(items.Item5);
            var weaponGachaCriticalRateJson = JsonUtility.FromJson<WeaponGachaCriticalRate.Json>(items.Item6);
            
            var masterDataWeaponGacha = AssetDatabase.LoadAssetAtPath<MasterDataWeaponGacha>("Assets/MasterData/MasterDataWeaponGacha.asset");
            masterDataWeaponGacha.gachas.Clear();
            masterDataWeaponGacha.gachas.AddRange(
                weaponGachaSpecJson.elements.Select(x =>
                {
                    return new WeaponGacha
                    {
                        id = x.id,
                        nameKey = new LocalizedString("Gacha", x.nameKey),
                        money = x.money,
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
                        criticalRates = new List<InstanceRangeParameterWithWeight>
                            (
                            weaponGachaCriticalRateJson.elements
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
            EditorUtility.SetDirty(masterDataWeaponGacha);
            AssetDatabase.SaveAssets();
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
                        money = x.money,
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
            EditorUtility.SetDirty(masterDataArmorGacha);
            AssetDatabase.SaveAssets();
        }

        [MenuItem("HK/Cookie/Download MasterData/AccessoryGacha")]
        private static async void DownloadMasterDataAccessoryGacha()
        {
            var items = await UniTask.WhenAll(
                DownloadFromSpreadSheet("AccessoryGachaSpec"),
                DownloadFromSpreadSheet("AccessoryGachaSkillNumber"),
                DownloadFromSpreadSheet("AccessoryGachaPassiveSkill")
                );
            var accessoryGachaSpecJson = JsonUtility.FromJson<AccessoryGachaSpec.Json>(items.Item1);
            var accessoryGachaSkillNumberJson = JsonUtility.FromJson<AccessoryGachaSkillNumber.Json>(items.Item2);
            var accessoryGachaPassiveSkillJson = JsonUtility.FromJson<AccessoryGachaPassiveSkill.Json>(items.Item3);
            
            var masterDataAccessoryGacha = AssetDatabase.LoadAssetAtPath<MasterDataAccessoryGacha>("Assets/MasterData/MasterDataAccessoryGacha.asset");
            masterDataAccessoryGacha.gachas.Clear();
            masterDataAccessoryGacha.gachas.AddRange(
                accessoryGachaSpecJson.elements.Select(x =>
                {
                    return new AccessoryGacha()
                    {
                        id = x.id,
                        nameKey = new LocalizedString("Gacha", x.nameKey),
                        money = x.money,
                        skillNumbers = new List<InstanceRangeParameterWithWeight>
                            (
                            accessoryGachaSkillNumberJson.elements
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
                        passiveSkillIds = new List<InstanceParameterWithWeight>
                            (
                            accessoryGachaPassiveSkillJson.elements
                                .Where(y => y.gachaId == x.id)
                                .Select(y => new InstanceParameterWithWeight()
                                {
                                    value = new InstanceParameter()
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
            EditorUtility.SetDirty(masterDataAccessoryGacha);
            AssetDatabase.SaveAssets();
        }

        [MenuItem("HK/Cookie/Download MasterData/EnemyStatus")]
        private static async void DownloadMasterDataEnemyStatus()
        {
            var items = await UniTask.WhenAll(
                DownloadFromSpreadSheet("EnemySpec"),
                DownloadFromSpreadSheet("EnemyActiveSkill"),
                DownloadFromSpreadSheet("DefeatEnemyUnlock")
                );

            var enemySpecJson = JsonUtility.FromJson<EnemySpec.Json>(items.Item1);
            var enemyActiveSkillJson = JsonUtility.FromJson<EnemyActiveSkill.Json>(items.Item2);
            var defeatEnemyUnlockJson = JsonUtility.FromJson<DefeatEnemyUnlock.Json>(items.Item3);
            var masterDataEnemyStatus = AssetDatabase.LoadAssetAtPath<MasterDataEnemyStatus>("Assets/MasterData/MasterDataEnemyStatus.asset");
            masterDataEnemyStatus.enemyStatusList.Clear();
            masterDataEnemyStatus.enemyStatusList.AddRange(
                enemySpecJson.elements.Select(x => new EnemyStatus
                {
                    id = x.id,
                    nameKey = new LocalizedString("Enemy", x.nameKey),
                    hitPoint = x.hitPoint,
                    physicalStrength = x.physicalStrength,
                    magicStrength = x.magicStrength,
                    physicalDefense = x.physicalDefense,
                    magicDefense = x.magicDefense,
                    speed = x.speed,
                    money = x.money,
                    fieldId = x.fieldId,
                    spriteId = x.id,
                    appearanceEffectId = x.appearanceEffectId,
                    diedAnimationId = x.diedAnimationId,
                    playerLevel = x.playerLevel,
                    activeSkills = enemyActiveSkillJson.elements
                        .Where(enemyActiveSkill => enemyActiveSkill.enemyId == x.id)
                        .Select(enemyActiveSkill => enemyActiveSkill.activeSkillId)
                        .ToList(),
                    defeatEnemyUnlocks = defeatEnemyUnlockJson.elements
                        .Where(defeatEnemyUnlock => defeatEnemyUnlock.enemyId == x.id)
                        .ToList()
                }));
            EditorUtility.SetDirty(masterDataEnemyStatus);
            AssetDatabase.SaveAssets();
        }

        [MenuItem("HK/Cookie/Download MasterData/ActiveSkill")]
        private static async void DownloadMasterDataActiveSkill()
        {
            var items = await UniTask.WhenAll(
                DownloadFromSpreadSheet("ActiveSkillSpec"),
                DownloadFromSpreadSheet("ActiveSkillAttribute")
                );

            var activeSkillSpecJson = JsonUtility.FromJson<ActiveSkillSpec.Json>(items.Item1);
            var activeSkillAttributeJson = JsonUtility.FromJson<ActiveSkillAttribute.Json>(items.Item2);
            var masterDataActiveSkill = AssetDatabase.LoadAssetAtPath<MasterDataActiveSkill>("Assets/MasterData/MasterDataActiveSkill.asset");
            masterDataActiveSkill.skills.Clear();
            masterDataActiveSkill.skills.AddRange(
                activeSkillSpecJson.elements.Select(x => new ActiveSkill
                {
                    id = x.id,
                    name = new LocalizedString("Skill", x.nameKey),
                    attachMax = x.attachMax,
                    attributes = activeSkillAttributeJson.elements
                        .Where(a => a.activeSkillId == x.id)
                        .Select(a => new Attribute
                        {
                            name = a.key,
                            value = a.value
                        })
                        .ToList()
                }));
            EditorUtility.SetDirty(masterDataActiveSkill);
            AssetDatabase.SaveAssets();
        }

        [MenuItem("HK/Cookie/Download MasterData/PassiveSkill")]
        private static async void DownloadMasterDataPassiveSkill()
        {
            var items = await UniTask.WhenAll(
                DownloadFromSpreadSheet("PassiveSkillSpec"),
                DownloadFromSpreadSheet("PassiveSkillAttribute")
                );

            var passiveSkillSpecJson = JsonUtility.FromJson<PassiveSkillSpec.Json>(items.Item1);
            var passiveSkillAttributeJson = JsonUtility.FromJson<PassiveSkillAttribute.Json>(items.Item2);
            var masterDataPassiveSkill = AssetDatabase.LoadAssetAtPath<MasterDataPassiveSkill>("Assets/MasterData/MasterDataPassiveSkill.asset");
            masterDataPassiveSkill.skills.Clear();
            masterDataPassiveSkill.skills.AddRange(
                passiveSkillSpecJson.elements.Select(x => new PassiveSkill
                {
                    id = x.id,
                    name = new LocalizedString("Skill", x.nameKey),
                    attachMax = x.attachMax,
                    attributes = passiveSkillAttributeJson.elements
                        .Where(a => a.passiveSkillId == x.id)
                        .Select(a => new Attribute
                        {
                            name = a.key,
                            value = a.value
                        })
                        .ToList()
                }));
            EditorUtility.SetDirty(masterDataPassiveSkill);
            AssetDatabase.SaveAssets();
        }

        [MenuItem("HK/Cookie/Download MasterData/FieldData")]
        private static async void DownloadMasterDataFieldData()
        {
            var fieldSpecText = await DownloadFromSpreadSheet("FieldSpec");
            var fieldSpecJson = JsonUtility.FromJson<FieldSpec.Json>(fieldSpecText);
            var masterDataFieldData = AssetDatabase.LoadAssetAtPath<MasterDataFieldData>("Assets/MasterData/MasterDataFieldData.asset");
            masterDataFieldData.records.Clear();
            masterDataFieldData.records.AddRange(
                fieldSpecJson.elements.Select(x => new FieldData()
                {
                    id = x.id,
                    nameKey = new LocalizedString("Field", x.nameKey)
                }));
            EditorUtility.SetDirty(masterDataFieldData);
            AssetDatabase.SaveAssets();
        }

        [MenuItem("HK/Cookie/Download MasterData/All")]
        private static void DownloadAll()
        {
            DownloadMasterDataWeaponGacha();
            DownloadMasterDataArmorGacha();
            DownloadMasterDataAccessoryGacha();
            DownloadMasterDataEnemyStatus();
            DownloadMasterDataActiveSkill();
            DownloadMasterDataPassiveSkill();
            DownloadMasterDataFieldData();
        }

        private static async UniTask<string> DownloadFromSpreadSheet(string sheetName)
        {
            var sheetUrl = await File.ReadAllTextAsync("masterdata_sheet_url.txt");
            var bearer = await File.ReadAllTextAsync("bearer.txt");
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
