namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GlobalEvent
    {
        /// <summary>
        /// シーン切り替え前のイベント
        /// </summary>
        public class WillChangeScene : Message<WillChangeScene>
        {
        }

        /// <summary>
        /// シーン切り替え後のイベント
        /// </summary>
        public class ChangedScene : Message<ChangedScene>
        {
        }
    }
}
