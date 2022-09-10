namespace Cookie
{
    /// <summary>
    /// ガチャシーンで利用するイベント
    /// </summary>
    public sealed class GachaEvent
    {
        /// <summary>
        /// 武器ガチャ実行をリクエストするメッセージ
        /// </summary>
        public class RequestWeaponGacha : Message<RequestWeaponGacha, int>
        {
            public int WeaponGachaId => this.Param1;
        }
        
        /// <summary>
        /// 防具ガチャ実行をリクエストするメッセージ
        /// </summary>
        public class RequestArmorGacha : Message<RequestArmorGacha, int>
        {
            public int ArmorGachaId => this.Param1;
        }
        
        /// <summary>
        /// アクセサリーガチャ実行をリクエストするメッセージ
        /// </summary>
        public class RequestAccessoryGacha : Message<RequestAccessoryGacha, int>
        {
            public int AccessoryGachaId => this.Param1;
        }
    }
}
