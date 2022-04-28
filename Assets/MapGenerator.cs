using UnityEngine;
using UnityEditor;

public class MapGenerator : EditorWindow
{
    private int _lines;
    private int _drawedLines;

    private int _columns;
    private int _drawedColumns;

    private GUIStyle _roadStyle;
    private GUIStyle _borderStyle;
    private GUIStyle _startStyle;

    private bool _mapDrawed;

    private bool[,] _map;

    [SerializeField] public GameObject _borderPrefab;
    [SerializeField] private GameObject _roadPrefab;

    [SerializeField] private MapGeneratorPrefabsConfig _prefabs; 

    [MenuItem("Window/MapGenerator")]
    static void ShowWindow()
    {
        MapGenerator window = (MapGenerator)EditorWindow.GetWindow(typeof(MapGenerator));
        window.Show();

    }

    private void OnEnable()
    {
        _roadStyle = CreateGUIStyle(Color.white);
        _borderStyle = CreateGUIStyle(Color.green);

        Repaint();
    }

    void OnGUI()
    {

        _columns = EditorGUILayout.IntSlider("Columns", _columns, 3, 10);
        _lines = EditorGUILayout.IntSlider("Lines", _lines, 3, 10);

        if (_drawedColumns != _columns || _drawedLines != _lines)
            _mapDrawed = false;

        if (GUILayout.Button("Draw Map"))
        {
            _mapDrawed = true;

            _drawedColumns = _columns;
            _drawedLines = _lines;

            _map = new bool[_columns, _lines];
        }

        if (_mapDrawed && _map != null)
        {
            EditorGUILayout.Space(10);

            for (int i = 0; i < _columns; i++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int j = 0; j < _lines; j++)
                {
                    GUIStyle currentButtonStyle;


                    if (_map[i, j] == true)
                        currentButtonStyle = _borderStyle;
                    else
                        currentButtonStyle = _roadStyle;

                    if (GUILayout.Button($"{i} {j}", currentButtonStyle))
                        _map[i, j] = !_map[i, j];
                }
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Generate Map"))
                GenerateMap();
        }
    }

    private void GenerateMap()
    {
        var map = new GameObject("NewGeneratedMap");

        for (int i = 0; i < _map.GetLength(0); i++)
        {
            for (int j = 0; j < _map.GetLength(1); j++)
            {
                if (_map[i, j] == true)
                    Instantiate(_borderPrefab, new Vector3(j, _borderPrefab.transform.position.y, -i), Quaternion.identity, map.transform);
                else
                    Instantiate(_roadPrefab, new Vector3(j, _roadPrefab.transform.position.y, -i), Quaternion.identity, map.transform);
            }
        }

        Debug.Log("Map was generated");

    } 

    private GUIStyle CreateGUIStyle(Color color)
    {
        GUIStyle style = new GUIStyle();
        style.normal.background = MakeBackgroundTexture(10, 10, color);
        style.active.background = MakeBackgroundTexture(10, 10, color * 0.8f);

        style.alignment = TextAnchor.MiddleCenter;

        style.fixedHeight = 40;
        style.fixedWidth = 40;

        return style;
    }

    private Texture2D MakeBackgroundTexture(int width, int height, Color color)
    {
        Color[] pixels = new Color[width * height];

        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = color;
        }

        Texture2D backgroundTexture = new Texture2D(width, height);

        backgroundTexture.SetPixels(pixels);
        backgroundTexture.Apply();

        return backgroundTexture;
    }
}
