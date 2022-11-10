using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using MessagePipe;
using SerializableCollections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Localization;

namespace Cookie
{
    /// <summary>
    /// ユーザーデータ
    /// </summary>
    [Serializable]
    public sealed class UserData
    {
        [NonSerialized]
        public static UserData current;

        public List<Weapon> weapons = new();

        public List<Armor> armors = new();

        public List<Accessory> accessories = new();

        [SerializeField]
        private int money;
        public int Money => this.money;

        public int weaponCreatedNumber = 0;

        public int armorCreatedNumber = 0;

        public int accessoryCreatedNumber = 0;

        public int equippedWeaponInstanceId;

        public int equippedArmorInstanceId;
        
        public int equippedAccessoryInstanceId;

        public List<int> unlockEnemies = new();

        public List<int> notifyEnemies = new();

        public List<int> unlockWeaponGachas = new();

        public List<int> notifyWeaponGachas = new();

        public List<int> unlockArmorGachas = new();

        public List<int> notifyArmorGachas = new();

        public List<int> unlockAccessoryGachas = new();

        public List<int> notifyAccessoryGachas = new();

        /// <summary>
        /// 各武器ガチャを実行した回数
        /// </summary>
        public IntIntSerializableDictionary weaponGachaInvokeCounts;

        /// <summary>
        /// 各防具ガチャを実行した回数
        /// </summary>
        public IntIntSerializableDictionary armorGachaInvokeCounts;

        /// <summary>
        /// 各アクセサリーガチャを実行した回数
        /// </summary>
        public IntIntSerializableDictionary accessoryGachaInvokeCounts;

        /// <summary>
        /// 倒した敵の数
        /// </summary>
        public IntIntSerializableDictionary defeatedEnemies;

        public BattleSpeedType battleSpeedType;

        public Weapon EquippedWeapon => this.weapons.Find(x => x.instanceId == this.equippedWeaponInstanceId);
        
        public Armor EquippedArmor => this.armors.Find(x => x.instanceId == this.equippedArmorInstanceId);

        public Accessory EquippedAccessory => this.accessories.Find(x => x.instanceId == this.equippedAccessoryInstanceId);

        public ActorStatusBuilder ToActorStatusBuilder()
        {
            var weapon = this.EquippedWeapon;
            var armor = this.EquippedArmor;
            return new ActorStatusBuilder
            {
                nameKey = new LocalizedString("UI", "Player"),
                hitPoint = armor.hitPoint,
                physicalStrength = weapon.physicalStrength.parameter,
                magicStrength = weapon.magicStrength.parameter,
                criticalRate = weapon.criticalRate.parameter,
                physicalDefense = armor.physicalDefense,
                magicDefense = armor.magicDefense,
                speed = armor.speed,
                activeSkillIds = weapon.activeSkillIds.Select(x => x.parameter).ToList()
            };
        }

        public void UnlockEnemy(int unlockId)
        {
            if (this.unlockEnemies.Contains(unlockId))
            {
                return;
            }
            
            this.unlockEnemies.Add(unlockId);
            this.notifyEnemies.Add(unlockId);
        }

        public void UnlockWeaponGacha(int unlockId)
        {
            if (this.unlockWeaponGachas.Contains(unlockId))
            {
                return;
            }
            
            this.unlockWeaponGachas.Add(unlockId);
            this.notifyWeaponGachas.Add(unlockId);
        }

        public void UnlockArmorGacha(int unlockId)
        {
            if (this.unlockArmorGachas.Contains(unlockId))
            {
                return;
            }
            
            this.unlockArmorGachas.Add(unlockId);
            this.notifyArmorGachas.Add(unlockId);
        }

        public void UnlockAccessoryGacha(int unlockId)
        {
            if (this.unlockAccessoryGachas.Contains(unlockId))
            {
                return;
            }
            
            this.unlockAccessoryGachas.Add(unlockId);
            this.notifyAccessoryGachas.Add(unlockId);
        }

        public void AddMoney(int value)
        {
            this.money += value;
            Assert.IsTrue(this.money >= 0);
            GlobalMessagePipe.GetPublisher<UserDataEvent.UpdatedMoney>()
                .Publish(UserDataEvent.UpdatedMoney.Get());
        }

        public bool IsPossessionMoney(int value)
        {
            return this.money >= value;
        }
    }
}
