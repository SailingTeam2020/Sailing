/*
 * 
 * 長嶋
 * 
 */

using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Sailing.Online
{

    public class RoomFactory : BaseNetworkObject
    {

        //ルームID生成用
        private const string StrListNumber = "0123456789";

        public void Create()
        {

            Create(CreateRandomRoomID(), CreateRoomOption());

        }

        public void Create(string name, RoomOptions options)
        {

            PhotonNetwork.CreateRoom(name, options);

        }

        /// <summary>
        /// @brief ランダムなルームIDを生成する
        /// </summary>
        /// <returns>ランダムな部屋ID</returns>
        public string CreateRandomRoomID()
        {
            //使用文字が変わっていいように新しく変数を作成
            string list = StrListNumber;
            char[] id = new char[RoomSetting.RoomIDLength];

            for (int index = 0; index < id.Length; index++)
            {
                int rand = Random.Range(0, list.Length - 1);
                id[index] = list[rand];
            }

            //charをstringに変換させる
            string roomID = new string(id);

            Debug.Log("ルームID:" + roomID);

            return roomID;
        }

        /// <summary>
        /// @brief 部屋オプションを作成する
        /// </summary>
        /// <param name="mP">部屋に入れる最大人数</param>
        /// <param name="vis">部屋を公開するか</param>
        /// <param name="open">部屋に入れるかどうか</param>
        /// <returns>作成したオプション</returns>
        public RoomOptions CreateRoomOption(byte mP = RoomSetting.MaxPlayerNum, bool vis = true, bool open = true)
        {
            RoomOptions option = new RoomOptions
            {
                MaxPlayers = mP,
                IsVisible = vis,
                IsOpen = open
            };

            return option;
        }

    }

}
