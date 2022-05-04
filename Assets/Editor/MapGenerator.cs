using UnityEngine;
using UnityEditor;

//Данный скрипт не доработан, корректно работает только с квадратными полями
public class MapGenerator : EditorWindow
{
    private int _lines;
    private int _drawedLines;

    private int _columns;
    private int _drawedColumns;

    private GUIStyle _roadStyle;
    private GUIStyle _borderStyle;
    private GUIStyle _startStyle;

    private bool _isMapDrawed;

    private bool[,] _matrix;

    private GameObject[,] _map;

    [SerializeField] public GameObject _borderPrefab;
    [SerializeField] private GameObject _roadPrefab;

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
            _isMapDrawed = false;

        if (GUILayout.Button("Draw Map"))
        {
            _isMapDrawed = true;

            _drawedColumns = _columns;
            _drawedLines = _lines;

            _matrix = new bool[_columns, _lines];
            _map = new GameObject[_lines, _columns];
        }

        if (_isMapDrawed && _matrix != null)
        {
            EditorGUILayout.Space(10);

            for (int i = 0; i < _columns; i++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int j = 0; j < _lines; j++)
                {
                    GUIStyle currentButtonStyle;


                    if (_matrix[i, j] == true)
                        currentButtonStyle = _borderStyle;
                    else
                        currentButtonStyle = _roadStyle;

                    if (GUILayout.Button($"{i} {j}", currentButtonStyle))
                        _matrix[i, j] = !_matrix[i, j];
                }
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Generate Map"))
                GenerateMap();
        }
    }

    private void GenerateMap()
    {
        var mapObject = new GameObject("NewGeneratedMap");

        for (int i = 0; i < _matrix.GetLength(0); i++)
        {
            var line = new GameObject($"Line {i}");
            line.transform.parent = mapObject.transform;

            for (int j = 0; j < _matrix.GetLength(1); j++)
            {
                GameObject newBlock;

                if (_matrix[i, j] == true)
                    newBlock = Instantiate(_borderPrefab, new Vector3(j, _borderPrefab.transform.position.y, -i), Quaternion.identity, line.transform);
                else
                    newBlock = Instantiate(_roadPrefab, new Vector3(j, _roadPrefab.transform.position.y, -i), Quaternion.identity, line.transform);

                _map[j, i] = newBlock;
            }
        }

        FillMapBlocks();

        //SceneView.lastActiveSceneView.FrameSelected();

        Debug.Log("Map was generated");
    }

    public void FillMapBlocks()
    {
        for (int i = 0; i < _map.GetLength(0); i++)
        {
            for (int j = 0; j < _map.GetLength(1); j++)
            {
                if (_map[i, j].TryGetComponent(out RoadBlock roadBlock))
                {
                    if (i > 0 && j > 0 && i < _matrix.GetLength(0) - 1 && j < _matrix.GetLength(1) - 1)
                    {
                        var neighbors = new Neighbors();

                        if (_map[i + 1, j].TryGetComponent(out RoadBlock rightNeighbor))
                            neighbors.RightNeighbor = rightNeighbor;

                        if (_map[i - 1, j].TryGetComponent(out RoadBlock leftNeighbor))
                            neighbors.LeftNeighbor = leftNeighbor;

                        if (_map[i, j - 1].TryGetComponent(out RoadBlock upNeighbor))
                            neighbors.UpNeighbor = upNeighbor;

                        if (_map[i, j + 1].TryGetComponent(out RoadBlock downNeighbor))
                            neighbors.DownNeighbor = downNeighbor;

                        roadBlock.SetNeighbors(neighbors);
                    }
                }
            }
        }
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
