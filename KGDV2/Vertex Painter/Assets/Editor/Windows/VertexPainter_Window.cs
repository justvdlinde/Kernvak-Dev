using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class VertexPainter_Window : EditorWindow {
    #region Variables
    private GUIStyle boxStyle;
    private GUIStyle footerStyle;
    private GUIStyle onScreenGUIStyle;

    public bool isPainting = false;
    public bool brushLock = false;
    public bool paintingState = false;

    public Vector2 mousePos;
    public RaycastHit curHit;

    public float brushSize = .5f;
    public float brushOpacity = 1.0f;
    public float brushFalloff = 1.0f;

    public GameObject curGO;
    public GameObject lastGO;
    public Mesh curMesh;

    public Color foregroundColor;

    #endregion

    #region Main Method
    private void OnEnable() {
        SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
        SceneView.onSceneGUIDelegate += this.OnSceneGUI;
        GenerateStyles();
    }

    private void OnDestroy() {
        SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
    }

    private void Update() {
        if (isPainting) {
            Selection.activeGameObject = null;
        }
        else {
            curGO = null;
            lastGO = null;
            curMesh = null;
        }

        if (curHit.transform != null) {
            if (curHit.transform.gameObject != lastGO) {
                curGO = curHit.transform.gameObject;
                curMesh = VertexPainter_Utils.GetMesh(curGO);
                lastGO = curGO;

                if (curGO != null && curMesh != null)
                {
                    Debug.Log(curGO.name + " : " + curMesh);
                }
            }
        }
    }

    public static void LaunchVertexPainter() {
        var win = EditorWindow.GetWindow<VertexPainter_Window>(false, "Vertex Painter", true);
        win.GenerateStyles();
    }
    #endregion

    #region GUI Methods
    private void OnGUI() {
        //Header
        GUILayout.BeginHorizontal();
            GUILayout.Box("Vertex Painter& ", boxStyle, GUILayout.Height(110), GUILayout.ExpandWidth(true));
        GUILayout.EndHorizontal();

        //Body
        GUILayout.BeginVertical(boxStyle);
        GUILayout.Space(10);
        isPainting = (GUILayout.Toggle(isPainting, "Enable Painting (P)", GUI.skin.button, GUILayout.Height(50)));

        if(GUILayout.Button("Update Style", GUILayout.Height(35))) {
            GenerateStyles();
        }

        GUILayout.Space(10);

        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();

        //Footer
        GUILayout.Box("Copyright Just Industries", footerStyle, GUILayout.Height(20), GUILayout.ExpandWidth(true));

        //GUI Realtime Update
        Repaint();
    }

    private void OnSceneGUI(SceneView sceneView) {
        Handles.BeginGUI();
            GUILayout.BeginArea(new Rect(10, 10, 200, 200), boxStyle);
            GUILayout.Box("Brush Options", onScreenGUIStyle);

            GUILayout.Box("Brush Color", footerStyle);
            foregroundColor = EditorGUILayout.ColorField(foregroundColor);

            GUILayout.Box("Brush Size (CTRL)", footerStyle);
            brushSize = GUILayout.HorizontalSlider(brushSize, .1f, 10f);

            GUILayout.Box("Brush Opacity (SHIFT)", footerStyle);
            brushOpacity = GUILayout.HorizontalSlider(brushOpacity, 0f, 1f);

            GUILayout.EndArea();
        Handles.EndGUI();

        if (isPainting) {

            if (curHit.transform != null){ 
                Handles.color = new Color(foregroundColor.r, foregroundColor.g, foregroundColor.b, brushOpacity);
                Handles.DrawSolidDisc(curHit.point, curHit.normal, brushSize);

                Handles.color = Color.blue;
                Handles.DrawWireDisc(curHit.point, curHit.normal, brushSize);
                Handles.DrawWireDisc(curHit.point, curHit.normal, brushFalloff);
            }

            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
            Ray worldRay = HandleUtility.GUIPointToWorldRay(mousePos);

            if(!brushLock) { 
                if(Physics.Raycast(worldRay, out curHit, float.MaxValue)) {
                    if (paintingState) {
                    PaintVertexColor();
                    }
                }
            }
        }

        ProcessInputs();
        sceneView.Repaint();
    }
    #endregion

    #region TempPainter Method
    public void PaintVertexColor() {
        if(curMesh) {
            Vector3[] verts = curMesh.vertices;
            Color[] colors = new Color[0];
            Debug.Log(colors);
            if (curMesh.colors.Length > 0) {
                colors = curMesh.colors;
            }

            else {
                colors = new Color[verts.Length];
            }

            for (int i = 0; i < verts.Length; i++) { 
                Vector3 vertPos = curGO.transform.TransformPoint(verts[i]);
                float sqrMag = (vertPos - curHit.point).sqrMagnitude;

                if (sqrMag > brushSize) {
                    continue;
                }

                float falloff = VertexPainter_Utils.LinearFalloff(sqrMag, brushSize);
                falloff = Mathf.Pow(falloff, brushFalloff *3f);
                colors[i] = VertexPainter_Utils.VtxColorLerp(colors[i], foregroundColor, falloff);
            }

            curMesh.colors = colors;
        }

        else {
            Debug.LogWarning("No Mesh Detected");
        }
    }
    #endregion

    #region UtilityMethods
    private void ProcessInputs() {
        Event e = Event.current;
        mousePos = e.mousePosition;

        if (e.type == EventType.KeyDown) {
            if (e.isKey) {
                if (e.keyCode == KeyCode.P) {
                    isPainting = !isPainting;
                    if (!isPainting) {
                        Tools.current = Tool.View;
                    }
                    else {
                        Tools.current = Tool.None;
                    }
                }
            }
        }

        if (e.type == EventType.MouseUp)
        {
            brushLock = false;
            paintingState = false;
        }

        if (isPainting) {
            if (e.type == EventType.MouseDrag && e.control && e.button == 0 && !e.shift) {
                brushSize += e.delta.x * 0.01f;
                brushSize = Mathf.Clamp(brushSize, 0.1f, 10f);
                if (brushFalloff > brushSize) {
                    brushFalloff = brushSize;
                }
                brushLock = true;
            }

            if (e.type == EventType.MouseDrag && !e.control && e.button == 0 && e.shift)
            {
                brushOpacity += e.delta.x * 0.01f;
                brushOpacity = Mathf.Clamp01(brushOpacity);
                brushLock = true;
            }

            if (e.type == EventType.MouseDrag && e.control && e.button == 0 && e.shift)
            {
                brushFalloff += e.delta.x * 0.005f;
                brushFalloff = Mathf.Clamp(brushFalloff, 0.1f, brushSize);
                brushLock = true;
            }

            if (e.type == EventType.MouseDrag && !e.control && e.button == 0 && !e.shift && !e.alt)
            {
                paintingState = true;
            }
        }
    }

    private void GenerateStyles() {
        boxStyle = new GUIStyle(); 
        footerStyle = new GUIStyle();
        onScreenGUIStyle = new GUIStyle();

        boxStyle.normal.background = (Texture2D)Resources.Load("Textures/default_ui_bg");
        boxStyle.normal.textColor = (Color.black);
        boxStyle.border = new RectOffset(3, 3, 3, 3);
        boxStyle.margin = new RectOffset(10, 10, 10, 0);
        boxStyle.padding = new RectOffset(0, 0, 5, 0);
        boxStyle.fontSize = 100;
        boxStyle.alignment = TextAnchor.MiddleCenter;
        boxStyle.font = (Font)Resources.Load("Fonts/TequilaSunset");

        footerStyle.border = new RectOffset(3, 3, 3, 3);
        footerStyle.margin = new RectOffset(10, 10, 10, 10);
        footerStyle.alignment = TextAnchor.LowerCenter;

        onScreenGUIStyle.font = (Font)Resources.Load("Fonts/calibril");
        onScreenGUIStyle.fontSize = 20;
        onScreenGUIStyle.alignment = TextAnchor.MiddleCenter;
    }
    #endregion
}
