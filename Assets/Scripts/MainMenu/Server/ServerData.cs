/*
 * 
 * 長嶋
 * 
 *更新者   ：足立拓海 
 *更新日   ：2021/01/18
 *更新内容 ：LoginUserとGetUserDataを追加
 *
 */

namespace Sailing.Server
{

    public class ServerData
    {

        public const string GetRanking = "http://ydasailing.php.xdomain.jp/GetRanking.php";
        public const string RegisterRanking = "http://ydasailing.php.xdomain.jp/RegisterRecord.php";
        public const string RegisterUserData = "http://ydasailing.php.xdomain.jp/RegisterUserData.php";
        public const string LoginUser = "http://ydasailing.php.xdomain.jp/LoginUserData.php";
        public const string GetUserData = "http://ydasailing.php.xdomain.jp/GetUserData.php";


        public const int MaxWaitTime = 15;

    }

}