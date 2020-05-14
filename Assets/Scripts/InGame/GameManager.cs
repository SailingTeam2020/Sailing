/*
 * 
 * 長嶋
 * Photonのデモを参考に作成
 * 
 */

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Sailing.Online;
using Common;

namespace Sailing
{
    public class GameManager : BaseNetworkObject
    {

        [SerializeField]
        private Text waitingText;

        public void Awake()
        {

            if (!PhotonNetwork.InRoom)
            {
                TestSoloPlay();
            }

            //メッセージ処理の実行を再開する
            PhotonNetwork.IsMessageQueueRunning = true;

        }

        public override void OnEnable()
        {
            base.OnEnable();

            //CountdownTimer.OnCountdownTimerHasExpired += OnCountdownTimerIsExpired;

        }


        // Use this for initialization
        private void Start()
        {

            FadeManager.FadeIn();

            //waitingText.text = "他の人を待っています";

            /*Hashtable props = new Hashtable { { RoomPropertyKey.PlayerLoadLevel, true } };
            PhotonNetwork.LocalPlayer.SetCustomProperties(props);*/

        }

        public override void OnDisable()
        {
            base.OnDisable();

            //CountdownTimer.OnCountdownTimerHasExpired -= OnCountdownTimerIsExpired;
        }

        /// <summary>
        /// @brief ゲームを開始して、プレイヤーの動作をアクティブ化する
        /// </summary>
        public void StartRace()
        {

            Debug.Log("ゲームがスタートしました");

        }

        /// <summary>
        /// @brief ゲームルームを抜ける
        /// </summary>
        public void ExitGameRoom()
        {

            PhotonNetwork.LeaveRoom();

        }

        public void TestSoloPlay()
        {

            PhotonNetwork.OfflineMode = true;
            PhotonNetwork.JoinRoom("Offline Mode");

        }

        /// <summary>
        /// @brief ルームを抜けた場合の処理
        /// </summary>
        public override void OnLeftRoom()
        {
            base.OnLeftRoom();

            //シーンを移動させる
            //SceneSwitch(Editor.SceneNameString.OnlineLobbyScene);

        }

        /// <summary>
        /// @brief 各プレイヤーの読み込みレベルプロパティを更新する
        /// </summary>
        /// <param name="targetPlayer"></param>
        /// <param name="changedProps"></param>
        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            if (changedProps.ContainsKey(RoomPropertyKey.PlayerLoadLevel))
            {
                CheckEndOfGame();
                return;
            }

            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }

            if (!changedProps.ContainsKey(RoomPropertyKey.PlayerLoadLevel))
            {
                return;
            }

            if (!CheckAllPlayerLoadedLevel())
            {
                return;
            }

            Hashtable props = new Hashtable { { CountdownTimer.CountdownStartTime, (float)PhotonNetwork.Time } };
            PhotonNetwork.CurrentRoom.SetCustomProperties(props);

        }

        /// <summary>
        /// @brief 各プレイヤーのシーンロード状態を確認する
        /// </summary>
        /// <returns>全プレイヤーが読み込みを終了していた場合true、そうでない場合false</returns>
        private bool CheckAllPlayerLoadedLevel()
        {
            foreach (Player p in PhotonNetwork.PlayerList)
            {
                object playerLoadedLevel;

                if (p.CustomProperties.TryGetValue(RoomPropertyKey.PlayerLoadLevel, out playerLoadedLevel))
                {
                    //読み込みが完了していたら次のプレイヤーの読み込み状態を確認する
                    if ((bool)playerLoadedLevel)
                    {
                        continue;
                    }
                }

                Debug.Log("読み込みは終わっていません");

                return false;
            }

            Debug.Log("読み込みが終わりました");

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnCountdownTimerIsExpired()
        {

            StartRace();

        }

        private IEnumerator EndOfGame()
        {

            float timer = 5.0f;

            while (timer > 0.0f)
            {

                waitingText.text = string.Format("Returning to login screen in {0} seconds.", timer.ToString("n2"));

                yield return new WaitForEndOfFrame();

                timer -= Time.deltaTime;
            }

            PhotonNetwork.LeaveRoom();

        }

        private void CheckEndOfGame()
        {

            if (PhotonNetwork.IsMasterClient)
            {
                StopAllCoroutines();
            }


            //StartCoroutine(EndOfGame());

        }

    }

}