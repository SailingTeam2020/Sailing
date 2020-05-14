/*
 * 長嶋
 */

using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Common;
using Sailing.Server;

namespace Sailing.Online
{

    public class LobbyManager : BaseNetworkObject
    {

        [SerializeField]
        private RoomFactory roomFactory;
        [SerializeField]
        private LobbyUIChanger lobbyUI;
        [SerializeField]
        private Text inRoomIDText;

        private const string DefaultPlayerName = "ゲストさん";

        private void Start()
        {

            FadeManager.FadeIn();

            if (PhotonNetwork.IsConnected)
            {
                ConnectionSuccessful();
                return;
            }

            if (!Connected())
            {
                Debug.LogError("サーバーへの接続に失敗しました");
                SceneSwitch(SceneNameString.MainMenu);
            }

        }

        #region Room

        #region ランダムマッチ

        /// <summary>
        /// @brief ランダムな部屋に参加する
        /// </summary>
        public void JoinRandomRoom()
        {

            PhotonNetwork.JoinRandomRoom();

        }

        #endregion

        #region 友達と遊ぶ

        /// <summary>
        /// @brief 新規の部屋を作成する
        /// </summary>
        public void CreateFriendRoom()
        {

            roomFactory.Create(roomFactory.CreateRandomRoomID(), roomFactory.CreateRoomOption(RoomSetting.MaxPlayerNum, false, true));

        }

        /// <summary>
        /// @brief 部屋に参加する
        /// </summary>
        public void JoinFriendRoom()
        {
            //参加ルームIDを取得する
            string id = inRoomIDText.text;

            //IDが正しくない場合、処理を終了する(InputFieldで数字のみに制限しているので文字数のみで判断)
            if (id.Length < RoomSetting.RoomIDLength)
            {
                Debug.LogWarning("IDが正しくありません");
                return;
            }

            //部屋が見つからなかったとき処理を終了する
            if (!PhotonNetwork.JoinRoom(id))
            {
                Debug.LogWarning("部屋が見つかりませんでした");
                return;
            }

        }

        #endregion

        #endregion

        public void LeftLobby()
        {

            Disconnect();
            SceneSwitch(SceneNameString.MainMenu);

        }

        /// <summary>
        /// @brief 接続成功したとき、ニックネームの設定とUIを初期化する
        /// </summary>
        private void ConnectionSuccessful()
        {

            SetNickName();
            lobbyUI.Init();

        }

        private void SetNickName()
        {

            UserData userData = gameObject.AddComponent<UserData>();

            if (userData.UserName == UserDataKey.UserName_Default || userData.UserName == null)
            {
                PhotonNetwork.LocalPlayer.NickName = DefaultPlayerName;
            }
            else
            {
                PhotonNetwork.LocalPlayer.NickName = userData.UserName;
            }

            Debug.Log("ニックネームを設定しました");

        }

        #region PhotonCallback

        /// <summary>
        /// @brief サーバーへ接続成功したとき用の処理を呼び出す
        /// </summary>
        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();

            ConnectionSuccessful();

        }

        /// <summary>
        /// @brief ランダムな部屋の参加に失敗したときの処理
        /// </summary>
        /// <param name="returnCode"></param>
        /// <param name="message"></param>
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            base.OnJoinRandomFailed(returnCode, message);

            roomFactory.Create();

        }

        /// <summary>
        /// @brief 部屋の入室に失敗したときの処理
        /// </summary>
        /// <param name="returnCode"></param>
        /// <param name="message"></param>
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            base.OnJoinRoomFailed(returnCode, message);

            Debug.LogError("部屋の入室に失敗しました");

        }

        /// <summary>
        /// @brief 自身が部屋に入ることに成功したらシーンを移動させる
        /// </summary>
        public override void OnJoinedRoom()
        {

            //一番初めに入った場合、カスタムプロパティにスタートまでの時間をセットする
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                //GameStartCountというプロパティに設定
                var properties = new ExitGames.Client.Photon.Hashtable
                {
                    { RoomPropertyKey.InRoomLimitTime, PhotonNetwork.Time }
                };
                PhotonNetwork.CurrentRoom.SetCustomProperties(properties);
            }

            Debug.Log("制限時間をセットしました");

            //メッセージ処理の実行を一時停止
            PhotonNetwork.IsMessageQueueRunning = false;

            //シーンを移動させる
            SceneSwitch(SceneNameString.MatchingRoom);

            Debug.Log("部屋の入室に成功しました");

        }

        #endregion

    }

}