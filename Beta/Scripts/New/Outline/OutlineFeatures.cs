using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class OutlineFeature : ScriptableRendererFeature
{
    [Serializable]
    public class RenderSettings
    {
        public Material OverrideMaterial = null;
        public int OverrideMaterialPassIndex = 0;
        [Space]
        public LayerMask LayerMask = 0;
    }

    [Serializable]
    public class BlurSettings
    {
        public Material BlurMaterial;
        public int DownSample = 1;
        public int PassesCount = 1;
    }

    [SerializeField] public RenderPassEvent _renderPassEvent;

    [SerializeField] private Material _outlineMaterial;
    [SerializeField] private string _renderTextureName;
    [SerializeField] private string _bluredTextureName;

    [SerializeField] private RenderSettings _renderSettings;
    [SerializeField] private BlurSettings _blurSettings;

    private RenderTargetHandle _bluredTexture;
    private RenderTargetHandle _renderTexture;

    private MyRenderObjectsPass _renderPass;
    private OutlinePass _outlinePass;

    public override void Create()
    {
        _renderTexture.Init(_renderTextureName);
        _bluredTexture.Init(_bluredTextureName);

        _renderPass = new MyRenderObjectsPass(_renderTexture, _renderSettings.LayerMask, _renderSettings.OverrideMaterial);
        _outlinePass = new OutlinePass(_outlineMaterial);

        _renderPass.renderPassEvent = _renderPassEvent;

        _outlinePass.renderPassEvent = _renderPassEvent;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        var depthTexture = renderer.cameraDepthTarget;
        _renderPass.SetDepthTexture(depthTexture);

        renderer.EnqueuePass(_renderPass);

        renderer.EnqueuePass(_outlinePass);
    }
}
