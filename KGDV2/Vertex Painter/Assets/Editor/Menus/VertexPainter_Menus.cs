using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class VertexPainter_Menus : MonoBehaviour {
    [MenuItem("Tools/Vertex Painter")]
    private static void LaunchVertexPainter() {
        VertexPainter_Window.LaunchVertexPainter();
    }

}
