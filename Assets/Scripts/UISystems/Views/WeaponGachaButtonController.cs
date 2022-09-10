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
    public sealed class WeaponGachaButtonController : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        [SerializeField]
        private TextMeshProUGUI text;

        [SerializeField]
        private int weaponGachaId;

        private void Start()
        {
            var weaponGacha = MasterDataWeaponGacha.Instance.gachas.Find(this.weaponGachaId);

            this.button.onClick.AddListener(() =>
            {
                GlobalMessagePipe.GetPublisher<GachaEvent.RequestWeaponGacha>()
                    .Publish(GachaEvent.RequestWeaponGacha.Get(this.weaponGachaId));
            });

            this.text.text = weaponGacha.Name;
        }
    }
}
