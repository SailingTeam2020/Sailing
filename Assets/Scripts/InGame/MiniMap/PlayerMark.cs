using Photon.Pun;
using UnityEngine;

namespace Sailing
{

    public class PlayerMark : MonoBehaviour
    {

        [SerializeField]
        private MeshRenderer meshRenderer;
        [SerializeField]
        private Material mainPlayerMaterial;

        private PhotonView photonView;

        // Start is called before the first frame update
        void Start()
        {

            if (!photonView)
            {
                photonView = PhotonView.Get(transform.root);
            }

            if (photonView.IsMine)
            {
                meshRenderer.material = mainPlayerMaterial;
            }

        }

    }

}