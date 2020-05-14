using Photon.Pun;

namespace Sailing.Online
{

    public class OnlineChangeLobbyScene : BaseNetworkObject
    {

        public void LeaveRoom()
        {

            PhotonNetwork.LeaveRoom();

        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();

            if (PhotonNetwork.OfflineMode)
            {
                SceneSwitch(SceneNameString.MainMenu);
            }
            else
            {
                SceneSwitch(SceneNameString.Lobby);
            }

        }

    }

}