using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using SerializableCollections;
using UnityEngine;
using UnityEngine.Assertions;

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

        public int weaponCreatedNumber = 0;

        public int armorCreatedNumber = 0;

        public int accessoryCreatedNumber = 0;

        public int equippedWeaponInstanceId;

        public int equippedArmorInstanceId;
        
        public int equippedAccessoryInstanceId;

        public List<int> unlockEnemies = new();

        /// <summary>
        /// 倒した敵の数
        /// </summary>
        public IntIntSerializableDictionary defeatedEnemies;

        public Weapon EquippedWeapon => this.weapons.Find(x => x.instanceId == this.equippedWeaponInstanceId);
        
        public Armor EquippedArmor => this.armors.Find(x => x.instanceId == this.equippedArmorInstanceId);

        public Accessory EquippedAccessory => this.accessories.Find(x => x.instanceId == this.equippedAccessoryInstanceId);

        public ActorStatusBuilder ToActorStatusBuilder()
        {
            var weapon = this.EquippedWeapon;
            var armor = this.EquippedArmor;
            return new ActorStatusBuilder
            {
                hitPoint = armor.hitPoint,
                physicalStrength = weapon.physicalStrength.parameter,
                magicStrength = weapon.magicStrength.parameter,
                physicalDefense = armor.physicalDefense,
                magicDefense = armor.magicDefense,
                speed = armor.speed,
                activeSkillIds = weapon.activeSkillIds.Select(x => x.parameter).ToList()
            };
        }
    }
}
