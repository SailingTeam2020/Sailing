/*
 * 
 * 長嶋
 * カスタムプロパティ用定数をまとめたクラス
 * 
 * カスタムプロパティを使用する際は、このクラスに定数としてまとめること（入力ミスを防ぐため）
 * 
 */

namespace Sailing.Online
{

    /// <summary>
    /// @brief カスタムプロパティ用の定数をまとめたクラス
    /// </summary>
    public class RoomPropertyKey
    {

        //文字列分だけデータサイズが増えるため、なるべく短くする
        //細かい違いは定数名で区別させる
        public const string PlayerLoadLevel = "LoadLv";
        public const string InRoomLimitTime = "RoomLim";
        public const string PlayerGoalRank = "Rank";

    }

    public class PlayerPropertyKey
    {

        public const string PlayerObject = "pObj";

    }

}
