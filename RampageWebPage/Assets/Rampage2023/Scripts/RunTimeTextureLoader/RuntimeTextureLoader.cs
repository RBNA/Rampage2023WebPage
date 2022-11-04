using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RunTimeTextureLoader
{
    [RequireComponent(typeof(MeshRenderer))]
    public class RuntimeTextureLoader : RuntimeLoader
    {
        [SerializeField] private ExecutionMoment _executeOn = ExecutionMoment.Start;
        [SerializeField] private TextureType _textureType;
        [SerializeField] private List<string> _urls = new();

        private static readonly int MainTexture = Shader.PropertyToID("_MainTex");
        private static readonly int NormalMap = Shader.PropertyToID("_BumpMap");

        private Coroutine _downloadCoroutine;
        private MeshRenderer _mesh;
        private bool _processStarted = false;

        public void Awake()
        {
            _mesh = GetComponent<MeshRenderer>();
            _mesh.material.EnableKeyword("_NORMALMAP");

            if (!_processStarted && _executeOn == ExecutionMoment.Awake)
            {
                Load();
            }
        }

        public void Start()
        {
            if (!_processStarted && _executeOn == ExecutionMoment.Start)
            {
                Load();
            }
        }

        public void OnEnable()
        {
            if (!_processStarted && _executeOn == ExecutionMoment.Enable)
            {
                Load();
            }
        }

        private IEnumerator LoadLODTexturesAndApply(string[] urls, MeshRenderer mesh, TextureType textureType)
        {
            if (_downloadCoroutine != null)
            {
                StopCoroutine(_downloadCoroutine);
                _downloadCoroutine = null;
            }

            string fileName;
            int i = 0;
            for (i = 0; i < urls.Length; i++)
            {
                fileName = Path.GetFileName(urls[i]);

                switch (textureType)
                {
                    case TextureType.MainTexture:
                        Debug.Log(
                            $"LOD Texture: {fileName} loading.... It's the {(i + 1)}/{urls.Length} textures to be loaded.");
                        break;
                    case TextureType.NormalMap:
                        Debug.Log(
                            $"LOD NormalMap: {fileName} loading.... It's the {(i + 1)}/{urls.Length} normalMaps to be loaded.");
                        break;
                }

                yield return LoadImageFromUrl(urls[i], OnTextureDownloaded, OnDownloadError);
            }

            void OnTextureDownloaded(Texture2D texture)
            {
                int typeId;

                switch (textureType)
                {
                    case TextureType.MainTexture:
                        typeId = MainTexture;
                        break;
                    case TextureType.NormalMap:
                        typeId = NormalMap;
                        ConvertTextureToNormalMap(ref texture);
                        break;
                    default:
                        Debug.LogError("Tried to load a texture with an invalid type.");
                        return;
                }

                mesh.material.SetTexture(typeId, texture);
            }

            void OnDownloadError(string error)
            {
                Debug.Log(error);
            }
        }

        void ConvertTextureToNormalMap(ref Texture2D texture)
        {
            Texture2D convertedNormalMap = new Texture2D(texture.width, texture.height, texture.format, false, true);
            Graphics.CopyTexture(texture, convertedNormalMap);
            convertedNormalMap.Apply();

            texture = convertedNormalMap;
        }

        public void Load()
        {
            _processStarted = true;
            StartCoroutine(LoadLODTexturesAndApply(_urls.ToArray(), _mesh, _textureType));
        }
    }

    public enum TextureType
    {
        MainTexture = 0,
        NormalMap = 1
    }

    public enum ExecutionMoment
    {
        Awake = 1,
        Start = 2,
        Enable = 3
    }
}