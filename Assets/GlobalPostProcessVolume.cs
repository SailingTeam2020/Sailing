using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Sailing.Online
{

    public class GlobalPostProcessVolume : MonoBehaviour
    {
        private PostProcessVolume volume;
        private Bloom bloom;
        private ShipMove shipMove;
        private float speed;

        // Start is called before the first frame update
        void Start()
        {
            volume = GetComponent<PostProcessVolume>();
            //設定を取得する
            bloom = volume.profile.GetSetting<Bloom>();
            shipMove = GameObject.Find("Ship").GetComponent<ShipMove>();

        }

        // Update is called once per frame
        void Update()
        {

            speed = shipMove.MoveSpeed;
            //Debug.Log("発光数"+bloom.intensity.value);
            ; if (speed >= 18)
            {
                bloom.intensity.value -= 0.2f;

                if (bloom.intensity.value <= 0)
                {
                    bloom.intensity.value = 0f;
                }

            }
            else
            {
                bloom.intensity.value += 2f;

                if (bloom.intensity.value >= 2)
                {
                    bloom.intensity.value = 2f;
                }

            }
        }
    }
}