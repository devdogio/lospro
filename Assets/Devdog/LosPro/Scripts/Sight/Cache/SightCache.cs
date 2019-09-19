using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Devdog.LosPro
{
    public partial class SightCache : ISightCache
    {
        public SightConfiguration config { get; protected set; }

        protected readonly Dictionary<int, SightCacheLookup> vertexCache;


        public SightCache(SightConfiguration config)
        {
            this.config = config;

            vertexCache = new Dictionary<int, SightCacheLookup>(64);
        }


        public bool Contains(ISightTarget target)
        {
            return vertexCache.ContainsKey(target.gameObject.GetInstanceID());
        }

        public void GetFromCache(ISightTarget target, out SightCacheLookup cacheLookup)
        {
            if (vertexCache.ContainsKey(target.gameObject.GetInstanceID()))
            {
                cacheLookup = vertexCache[target.gameObject.GetInstanceID()];
                return;
            }

            throw new InvalidOperationException("Given object is not in cache, check if an object is in cache first by calling Contains");
        }

        public virtual bool GenerateCache(ISightTarget target)
        {
            var skinnedRenderer = target.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
            if (skinnedRenderer != null)
            {
                AddToCache(target, skinnedRenderer.transform, skinnedRenderer.sharedMesh, skinnedRenderer);
                return true;
            }

            var meshFilter = target.gameObject.GetComponentInChildren<MeshFilter>();
            if (meshFilter != null)
            {
                AddToCache(target, meshFilter.transform, meshFilter.sharedMesh, null);
                return true;
            }

            var spriteRender = target.gameObject.GetComponentInChildren<SpriteRenderer>();
            if (spriteRender != null)
            {
                var mesh = GenerateMeshFromSprite(spriteRender.sprite);
                AddToCache(target, spriteRender.transform, mesh, null);
                return true;
            }

            if (config.debug)
            {
                Debug.LogError("Spot object without a (Skinned) Mesh Renderer! - If the object you're trying to detect doesn't have a renderer use manual mode instead.", target.gameObject);
            }

            return false;
        }


        private Mesh GenerateMeshFromSprite(Sprite sprite)
        {
            Mesh mesh = new Mesh();
            mesh.MarkDynamic();

            if (sprite == null)
            {
                return mesh;
            }

            Vector3[] vertices = new Vector3[sprite.vertices.Length];
            int[] triangles = new int[sprite.triangles.Length];

            for (int i = 0; i <= sprite.triangles.Length - 1; i++)
            {
                triangles[i] = sprite.triangles[i];
            }

            for (int i = 0; i <= sprite.vertices.Length - 1; i++)
            {
                vertices[i] = sprite.vertices[i];
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateBounds();

            return mesh;
        }

        public void ClearCache()
        {
            vertexCache.Clear();
        }

        public void ClearFromCache(ISightTarget target)
        {
            if (vertexCache.ContainsKey(target.gameObject.GetInstanceID()))
            {
                vertexCache[target.gameObject.GetInstanceID()].Clear();
            }
        }

        protected virtual void AddToCache(ISightTarget target, Transform meshParent, Mesh mesh, SkinnedMeshRenderer skinnedMeshRenderer)
        {
            int instanceID = target.gameObject.GetInstanceID();

            //int sampleCount = config.sampleCount;
            int sampleCount = Mathf.Clamp(20, 0, mesh.vertexCount); // TODO: FIX -- When a sight viewer with 1 sample indexes first, one with a higher sample count won't re-index...
            var randomIndices = new int[sampleCount];
            for (int i = 0; i < sampleCount; i++)
            {
                int index = UnityEngine.Random.Range(0, mesh.vertexCount);
                randomIndices[i] = index;
            }

            if (vertexCache.ContainsKey(instanceID) == false)
            {
                if (config.debug)
                {
//                    Debug.Log("Added new object (" + target.name + ") to cache", target.gameObject);
                }

                vertexCache.Add(instanceID, new SightCacheLookup(target, meshParent, mesh, randomIndices, skinnedMeshRenderer));
                return;
            }

            if (config.debug)
            {
                Debug.Log("Re-building cache for " + target.name, target.gameObject);
            }

            vertexCache[instanceID] = new SightCacheLookup(target, meshParent, mesh, randomIndices, skinnedMeshRenderer);
        }
    }
}
