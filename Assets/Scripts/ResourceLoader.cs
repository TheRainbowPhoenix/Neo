using System;
using System.Collections.Generic;
using UnityEngine;

using Phoebe.Neo.Base;

namespace Phoebe.Neo {
    public class ResourceLoader
    {
        private static ResourceLoader instance;

        private Dictionary<ResourceType, string> ResourcePath;

        private Dictionary<ResourceType, Type> ResourceTypeDic;

        public static ResourceLoader Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ResourceLoader();
                }
                return instance;
            }
        }

        public ResourceLoader()
        {
            ResourcePath = new Dictionary<ResourceType, string>
            {
                {
                    ResourceType.UIPrefab,
                    "UIPrefab/"
                },
                {
                    ResourceType.UISprite,
                    "UISprite/"
                },
                {
                    ResourceType.Sprite,
                    "Sprite/"
                },
                {
                    ResourceType.Prefab,
                    "Prefab/"
                },
                {
                    ResourceType.Audio,
                    "Audio/"
                },
                {
                    ResourceType.Effect,
                    "Effect/"
                },
                {
                    ResourceType.TextAsset,
                    "TextAsset/"
                }
            };
            ResourceTypeDic = new Dictionary<ResourceType, Type>
            {
                {
                    ResourceType.UISprite,
                    typeof(Sprite)
                },
                {
                    ResourceType.Sprite,
                    typeof(Sprite)
                },
                {
                    ResourceType.Prefab,
                    typeof(GameObject)
                },
                {
                    ResourceType.UIPrefab,
                    typeof(GameObject)
                },
                {
                    ResourceType.Audio,
                    typeof(AudioClip)
                },
                {
                    ResourceType.Effect,
                    typeof(GameObject)
                },
                {
                    ResourceType.TextAsset,
                    typeof(TextAsset)
                }
            };
        }

        public object Load(ResourceType type, string name)
        {
            // Debug.Log("Load "+ ResourcePath[type] + name);
            UnityEngine.Object @object = Resources.Load(ResourcePath[type] + name, ResourceTypeDic[type]);
            if ((object)@object == null)
            {
                Debug.LogError("ResourceLoader:Can't find object with name:'" + name + "',please check your ResourceType and Name is true.");
            }
            return @object;
        }

        public void ReleaseResource()
        {
            Resources.UnloadUnusedAssets();
        }
    }
}