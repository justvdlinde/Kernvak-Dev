using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class VertexPainter_Utils {
    public static Mesh GetMesh(GameObject go) {
        Mesh curMesh = null;

        if (go) {
            MeshFilter curFilter = go.GetComponent<MeshFilter>();
            SkinnedMeshRenderer curSkinned = go.GetComponent<SkinnedMeshRenderer>();

            if (curFilter && !curSkinned) {
                curMesh = curFilter.sharedMesh;
            }

            if (!curFilter && curSkinned) {
                curMesh = curSkinned.sharedMesh;
            }
        }
        return curMesh;
    }

    //Falloff Method

    public static float LinearFalloff(float distance, float brushRadius) {
        return Mathf.Clamp01(1 - distance / brushRadius);
    }

    //Lerp Method

    public static Color VtxColorLerp(Color colorA, Color colorB, float value) {
        if (value > 1f) {
            return colorB;
        }
        else if (value < 0f) {
            return colorA;
        }

        return new Color(colorA.r + (colorB.r - colorA.r) * value,
                         colorA.g + (colorB.g - colorA.g) * value,
                         colorA.b + (colorB.b - colorA.b) * value,
                         colorA.a + (colorB.a - colorA.a) * value);
    }

}
