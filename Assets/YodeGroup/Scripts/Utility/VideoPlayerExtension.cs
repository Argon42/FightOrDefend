using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;


public class VideoPlayerExtension : MonoBehaviour
{
    [SerializeField] private UrlType urlType;
    [SerializeField] private string url;

    [SerializeField] private UnityEvent onLoopPointReached;
    [SerializeField] private UnityEvent onPrepareCompleted;
    [SerializeField] private UnityEvent onFrameReady;
    [SerializeField] private UnityEvent onFrameDropped;
    [SerializeField] private UnityEvent onStarted;

    private VideoPlayer _videoPlayer;

    public VideoPlayer VideoPlayer
    {
        get
        {
            if (_videoPlayer is null)
                _videoPlayer = GetComponent<VideoPlayer>();
            return _videoPlayer;
        }
    }

    private void Awake()
    {
        SetupUrl();
        VideoPlayer.started += VideoPlayerStarted;
        VideoPlayer.frameDropped += VideoPlayerFrameDropped;
        VideoPlayer.frameReady += VideoPlayerFrameReady;
        VideoPlayer.prepareCompleted += VideoPlayerPrepareCompleted;
        VideoPlayer.loopPointReached += VideoPlayerLoopPointReached;
    }

    private void OnDestroy()
    {
        VideoPlayer.started -= VideoPlayerStarted;
        VideoPlayer.frameDropped -= VideoPlayerFrameDropped;
        VideoPlayer.frameReady -= VideoPlayerFrameReady;
        VideoPlayer.prepareCompleted -= VideoPlayerPrepareCompleted;
        VideoPlayer.loopPointReached -= VideoPlayerLoopPointReached;
    }

    private void VideoPlayerLoopPointReached(VideoPlayer source) => onLoopPointReached?.Invoke();
    private void VideoPlayerPrepareCompleted(VideoPlayer source) => onPrepareCompleted?.Invoke();
    private void VideoPlayerFrameReady(VideoPlayer source, long frameidx) => onFrameReady?.Invoke();
    private void VideoPlayerFrameDropped(VideoPlayer source) => onFrameDropped?.Invoke();
    private void VideoPlayerStarted(VideoPlayer source) => onStarted?.Invoke();


    private void SetupUrl()
    {
        if (urlType == UrlType.None)
            return;
        if (urlType == UrlType.Absolute)
            VideoPlayer.url = url;
        if (urlType == UrlType.Local)
            VideoPlayer.url = $"{Application.dataPath}/{url}";
        if (urlType == UrlType.StreamingAssets)
            VideoPlayer.url = $"{Application.streamingAssetsPath}/{url}";
        if (urlType == UrlType.Resources)
            VideoPlayer.clip = Resources.Load<VideoClip>(url);
    }


    private enum UrlType
    {
        None,
        Absolute,
        Local,
        StreamingAssets,
        Resources
    }
}