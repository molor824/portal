using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Portal _linkedPortal;
    [SerializeField] float _portalQuality = 0.5f;

    Matrix4x4 _portalMatrix;
    int _renderDepth;
    bool _showTex;
    RenderTexture _texture;
    Camera _mainCam;
    Camera _portalCam;
    MeshRenderer _linkedRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _mainCam = Camera.main;
        _portalCam = GetComponentInChildren<Camera>();
        _linkedRenderer = _linkedPortal.GetComponentInChildren<MeshRenderer>();
    }

    void LateUpdate()
    {
        if (!_mainCam.IsObjectVisible(_linkedRenderer)) return;

        if (_texture == null || _texture.width != Screen.width || _texture.height != Screen.height)
        {
            if (_texture != null) _texture.Release();
            _texture = new RenderTexture(
                (int)(Screen.width * _portalQuality),
                (int)(Screen.height * _portalQuality),
                0
            );
            _portalCam.targetTexture = _texture;
        }

        _portalCam.Render();

        _portalMatrix = transform.localToWorldMatrix * _linkedPortal.transform.worldToLocalMatrix;
        _portalCam.transform.position = _portalMatrix.MultiplyPoint(_mainCam.transform.position);
        _portalCam.transform.rotation = _portalMatrix.rotation * _mainCam.transform.rotation;

        _linkedRenderer.material.SetTexture("_Texture", Texture2D.blackTexture);
        _portalCam.Render();
        _linkedRenderer.material.SetTexture("_Texture", _texture);
    }
}
