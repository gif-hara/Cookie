using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EnemyInformationUIView : UIView
    {
        [SerializeField]
        private TextMeshProUGUI enemyName;
        
        [SerializeField]
        private TextMeshProUGUI hitPoint;

        [SerializeField]
        private TextMeshProUGUI speed;

        [SerializeField]
        private TextMeshProUGUI physicalStrength;

        [SerializeField]
        private TextMeshProUGUI magicStrength;

        [SerializeField]
        private TextMeshProUGUI physicalDefense;

        [SerializeField]
        private TextMeshProUGUI magicDefense;

        [SerializeField]
        private TextMeshProUGUI money;

        [SerializeField]
        private ActiveSkillUIElement activeSkillUIElementPrefab;

        [SerializeField]
        private Transform activeSkillUIElementParent;

        private readonly List<ActiveSkillUIElement> activeSkillUIElements = new ();

        public void Setup(EnemyStatus enemyStatus)
        {
            this.enemyName.text = enemyStatus.Name;
            this.hitPoint.SetText("{0}", enemyStatus.hitPoint);
            this.speed.SetText("{0}", enemyStatus.speed);
            this.physicalStrength.SetText("{0}", enemyStatus.physicalStrength);
            this.magicStrength.SetText("{0}", enemyStatus.magicStrength);
            this.physicalDefense.SetText("{0}", enemyStatus.physicalDefense);
            this.magicDefense.SetText("{0}", enemyStatus.magicDefense);
            this.money.SetText("{0}", enemyStatus.money);

            foreach (var activeSkillUIElement in this.activeSkillUIElements)
            {
                Destroy(activeSkillUIElement.gameObject);
            }
            this.activeSkillUIElements.Clear();
            for (var i = 0; i < enemyStatus.activeSkills.Count; i++)
            {
                var activeSkillId = enemyStatus.activeSkills[i];
                var activeSkill = MasterDataActiveSkill.Instance.skills.Find(x => x.id == activeSkillId);
                Assert.IsNotNull(activeSkill, $"{activeSkillId}");
                var element = Instantiate(this.activeSkillUIElementPrefab, this.activeSkillUIElementParent);
                this.activeSkillUIElements.Add(element);
                element.Index.SetText("{0}", i + 1);
                element.NameText.text = activeSkill.Name;
            }
        }

        public void SetActive(bool isActive)
        {
            this.gameObject.SetActive(isActive);
        }
    }
}
