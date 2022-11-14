using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public static class AccessoryInformationUIViewUtility
    {
        public static void Setup(AccessoryInformationUIView passiveInformationUIView, Accessory accessory)
        {
            passiveInformationUIView.AccessoryName.text = UserData.current.equippedAccessoryInstanceId == accessory.instanceId
                ? $"[E] {accessory.Name}"
                : accessory.Name;
            passiveInformationUIView.DestroyAllPassiveSkillUIElements();
            for (var i = 0; i < accessory.passiveSkillIds.Count; i++)
            {
                var passiveSkillId = accessory.passiveSkillIds[i];
                var passiveSkill = MasterDataPassiveSkill.Instance.skills.Find(passiveSkillId.parameter);
                var passiveSkillUIElement = passiveInformationUIView.CreatePassiveSkillUIElement();
                passiveSkillUIElement.Index.SetText("{0}", i + 1);
                passiveSkillUIElement.NameText.text = passiveSkill.Name;
                passiveSkillUIElement.CreateRareEffect(passiveSkillId.rare);
            }
        }
    }
}
