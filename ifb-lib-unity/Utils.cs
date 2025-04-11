using IFBLibUnity.Meshes;
using IFBOptLib.IFBGraphics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace IFBLibUnity
{
    public static class Utils
    {

        public static Mesh CloneUnityMesh(this Mesh mesh)
        {
            var inputMeshVerts = mesh.vertices;
            var inputMeshNormals = mesh.normals;
            var inputMeshUvs = mesh.uv;
            var inputMeshIndices = mesh.triangles;

            var newMesh = new Mesh();
            var vertices = new Vector3[inputMeshVerts.Length];
            var normals = new Vector3[inputMeshNormals.Length];
            var uvs = new Vector2[inputMeshUvs.Length];
            var indices = new int[inputMeshIndices.Length];

            Array.Copy(inputMeshVerts, vertices, inputMeshVerts.Length);
            Array.Copy(inputMeshNormals, normals, inputMeshNormals.Length);
            Array.Copy(inputMeshUvs, uvs, inputMeshUvs.Length);
            Array.Copy(inputMeshIndices, indices, inputMeshIndices.Length);

            newMesh.vertices = vertices;
            newMesh.normals = normals;
            newMesh.uv = uvs;
            newMesh.triangles = indices;

            return newMesh;
        }

        public static IEnumerable<float> SelectRawVerticesXYZ(this Vector3[] vertices)
        {
            foreach(var vert in vertices)
            {
                yield return vert.x;
                yield return vert.y;
                yield return vert.z;
            }
        }

        public static IFBMesh.MeshSimplMagEffortType ToEffortType(this EMeshSimplifyStrength simplifyStrength)
        {
            switch(simplifyStrength)
            {
                case EMeshSimplifyStrength.LOW:
                    {
                        return IFBConstants.MeshSimpl.LOW;
                    }
                case EMeshSimplifyStrength.MID:
                    {
                        return IFBConstants.MeshSimpl.MEDIUM;
                    }
                case EMeshSimplifyStrength.HIGH:
                {
                    return IFBConstants.MeshSimpl.HARD;
                }
                default:
                    {
                        throw new NotSupportedException();
                    }
            }
        }

        public static void GuardMeshBeforeNative(this FastUnityMesh fmesh)
        {

#if !DEBUG
            return;
#endif

            if(fmesh.Vertices == null)
            {
                throw new ArgumentNullException("FastUnityMesh.Vertices");
            }
            if (fmesh.Normals == null)
            {
                throw new ArgumentNullException("FastUnityMesh.Normals");
            }
            if (fmesh.Uvs == null)
            {
                throw new ArgumentNullException("FastUnityMesh.Uvs");
            }
            if (fmesh.Indices == null)
            {
                throw new ArgumentNullException("FastUnityMesh.Indices");
            }

            if (fmesh.Vertices.Length <= 0)
            {
                throw new ArgumentException("FastUnityMesh.Vertices.Length EMPTY");
            }
            if (fmesh.Normals.Length <= 0)
            {
                throw new ArgumentException("FastUnityMesh.Normals.Length EMPTY");
            }
            if (fmesh.Uvs.Length <= 0)
            {
                throw new ArgumentException("FastUnityMesh.Uvs.Length EMPTY");
            }
            if (fmesh.Indices.Length <= 0)
            {
                throw new ArgumentException("FastUnityMesh.Indices.Length EMPTY");
            }

            if (fmesh.Indices.Length % 3 != 0)
            {
                throw new ArgumentException("fmesh.Indices.Length % 3 != 0");
            }
        }


    }
}