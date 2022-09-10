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
    public sealed class AccessoryGachaButtonController : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        [SerializeField]
        private TextMeshProUGUI text;

        [SerializeField]
        private int accessoryGachaId;

        private void Start()
        {
            var accessoryGacha = MasterDataAccessoryGacha.Instance.gachas.Find(this.accessoryGachaId);

            this.button.onClick.AddListener(() =>
            {
                GlobalMessagePipe.GetPublisher<GachaEvent.RequestAccessoryGacha>()
                    .Publish(GachaEvent.RequestAccessoryGacha.Get(this.accessoryGachaId));
            });

            this.text.text = accessoryGacha.Name;
        }
    }
}
