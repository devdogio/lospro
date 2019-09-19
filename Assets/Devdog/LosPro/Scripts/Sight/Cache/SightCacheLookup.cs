using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Devdog.LosPro
{
    public class SightCacheLookup
    {
        public ISightTarget target;
        protected Transform targetMeshParent;
        public int[] indices;
        public Mesh sharedMesh;
        public Vector3[] meshVertices;
        public Vector3[] selectedVertices;

        public int vertexCount
        {
            get { return indices.Length; }
        }

        public bool isSkinnedMesh
        {
            get { return skinnedMeshRenderer != null; }
        }

        public SkinnedMeshRenderer skinnedMeshRenderer { get; protected set; }
        //public Matrix4x4[] skinnedMeshMatrices { get; protected set; }
        public Transform[] skinnedMeshBones { get; protected set; }
        public Matrix4x4[] skinnedMeshBindPoses { get; protected set; }
        public BoneWeight[] skinnedMeshBoneWeights { get; protected set; }

        public SightCacheLookup(ISightTarget target, Transform meshParent, Mesh sharedMesh, int[] indices, SkinnedMeshRenderer skinnedMeshRenderer)
        {
            this.target = target;
            this.sharedMesh = sharedMesh;
            this.indices = indices;
            this.meshVertices = GetWithIndex(sharedMesh.vertices);
            this.selectedVertices = new Vector3[indices.Length];
            this.skinnedMeshRenderer = skinnedMeshRenderer;
            this.targetMeshParent = meshParent;

            if (isSkinnedMesh)
            {
                skinnedMeshBones = skinnedMeshRenderer.bones;
                skinnedMeshBindPoses = sharedMesh.bindposes;
                skinnedMeshBoneWeights = GetWithIndex(sharedMesh.boneWeights);
            }
            else
            {
                this.skinnedMeshBones = new Transform[0];
                this.skinnedMeshBindPoses = new Matrix4x4[0];
                this.skinnedMeshBoneWeights = new BoneWeight[0];
            }
        }

        private T[] GetWithIndex<T>(IList<T> list)
        {
            var l = new List<T>(list.Count);
            for (int i = 0; i < indices.Length; i++)
            {
                l.Add(list[indices[i]]);
            }

            return l.ToArray();
        }

        public void Clear()
        {
            indices = new int[indices.Length];
            selectedVertices = new Vector3[indices.Length];
        }
        
        public Vector3[] GetVertexPositions(int sampleCount)
        {
            UpdateVertexPositions(sampleCount);
            return selectedVertices;
        }

        public Vector3 GetVertexPosition(int localIndex)
        {
            UpdateVertexPosition(localIndex);
            return selectedVertices[localIndex];
        }

        public void UpdateVertexPositions(int sampleCount)
        {
            for (int i = 0; i < selectedVertices.Length; i++)
            {
                UpdateVertexPositions(i);
            }
        }

        public void UpdateVertexPosition(int localIndex)
        {
            selectedVertices[localIndex] = meshVertices[localIndex];

            if (isSkinnedMesh)
            {
                selectedVertices[localIndex] = GetSkinnedVertexWorldPosition(localIndex);
            }
            else
            {
                // To world position.
                selectedVertices[localIndex] = targetMeshParent.localToWorldMatrix.MultiplyPoint3x4(selectedVertices[localIndex]);
            }
        }

        private Vector3 GetSkinnedVertexWorldPosition(int localIndex)
        {
            var weight = skinnedMeshBoneWeights[localIndex];
//            var vertex = meshVertices[localIndex];

            var vertex = Vector3.zero;
            GetVertexWorldPositionBones(weight.weight0, weight.boneIndex0, ref vertex);
//            GetVertexWorldPositionBones(weight.weight1, weight.boneIndex1, ref a);
//            GetVertexWorldPositionBones(weight.weight2, weight.boneIndex2, ref a);
//            GetVertexWorldPositionBones(weight.weight3, weight.boneIndex3, ref a);

            return vertex;
        }


        private void GetVertexWorldPositionBones(float weight, int boneIndex, ref Vector3 vertex)
        {
            if (weight > 0.01f)
            {
                var localPos = skinnedMeshBindPoses[boneIndex].MultiplyPoint3x4(vertex) * weight;
                vertex = skinnedMeshBones[boneIndex].transform.localToWorldMatrix.MultiplyPoint3x4(localPos);
            }
        }
    }
}
