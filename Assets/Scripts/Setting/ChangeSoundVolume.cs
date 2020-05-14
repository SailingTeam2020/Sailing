using UnityEngine;
using UnityEngine.UI;
using Sailing.SingletonObject;

namespace Sailing
{

    // スライダーによる音量調節を有効にします。各オブジェクトに対応するスライダーをアタッチしてください。
    public class ChangeSoundVolume : MonoBehaviour
    {
        [SerializeField] private GameObject bgmVolumeSlider = null;
        [SerializeField] private GameObject seVolumeSlider = null;

        private void Start()
        {
            bgmVolumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat(SoundManager.BGM_VOLUME_KEY, SoundManager.BGM_VOLUME_DEFULT);
            seVolumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat(SoundManager.SE_VOLUME_KEY, SoundManager.SE_VOLUME_DEFULT);
            bgmVolumeSlider.GetComponent<Slider>().onValueChanged.AddListener(ChangeBGMVolume);
            seVolumeSlider.GetComponent<Slider>().onValueChanged.AddListener(ChangeSEVolume);
        }

        private void ChangeBGMVolume(float value)
        {
            SoundManager.Instance.ChangeVolumeBGM(bgmVolumeSlider.GetComponent<Slider>().value);
        }

        private void ChangeSEVolume(float value)
        {
            SoundManager.Instance.ChangeVolumeSE(seVolumeSlider.GetComponent<Slider>().value);
            if (SoundManager.Instance.CheckPlaySE())
            {
                SoundManager.Instance.StopSE();
            }
            SoundManager.Instance.PlaySE("Goal");
        }
    }

}