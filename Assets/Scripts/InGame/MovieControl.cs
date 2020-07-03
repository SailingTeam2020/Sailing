using UnityEngine;
using UnityEngine.Video;

public class MovieControl : MonoBehaviour
{
    public VideoClip videoClip;
    public GameObject screen;

    void Start()
    {
        var videoPlayer = screen.AddComponent<VideoPlayer>();   // videoPlayeコンポーネントの追加

        videoPlayer.source = VideoSource.VideoClip; // 動画ソースの設定
        videoPlayer.clip = videoClip;

        videoPlayer.isLooping = true;   // ループの設定
    }

    public void VPControl()
    {
        var videoPlayer = GetComponent<VideoPlayer>();

        if (!videoPlayer.isPlaying) // ボタンを押した時の処理
            videoPlayer.Play(); // 動画を再生する。
        else
            videoPlayer.Pause();    // 動画を一時停止する。
    }
}