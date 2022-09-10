using System;
using MessagePipe;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ArmorGachaButtonController : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        [SerializeField]
        private TextMeshProUGUI text;

        [SerializeField]
        private int armorGachaId;

        private void Start()
        {
            var armorGacha = MasterDataArmorGacha.Instance.gachas.Find(this.armorGachaId);

            this.button.onClick.AddListener(() =>
            {
                GlobalMessagePipe.GetPublisher<GachaEvent.RequestArmorGacha>()
                    .Publish(GachaEvent.RequestArmorGacha.Get(this.armorGachaId));
            });

            this.text.text = armorGacha.Name;
        }
    }
}
