using System;
using System.Collections.Generic;
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

        public Weapon EquippedWeapon => this.weapons.Find(x => x.instanceId == this.equippedWeaponInstanceId);
        
        public Armor EquippedArmor => this.armors.Find(x => x.instanceId == this.equippedArmorInstanceId);

        public Accessory EquippedAccessory => this.accessories.Find(x => x.instanceId == this.equippedAccessoryInstanceId);
    }
}
