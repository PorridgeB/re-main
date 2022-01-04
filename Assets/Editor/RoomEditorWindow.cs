using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RoomEditorWindow : EditorWindow
{
    public class Palette
    {
        public string Name;
        public List<Tile> Tiles;
        public int SelectedTileIndex = 0;
        public Vector2 ScrollPosition = new Vector2();

        public Tile SelectedTile => Tiles[SelectedTileIndex];
    }

    private const string DefaultTilesetPath = "Tileset";
    private const int paletteColumns = 4;
    private const float cellSelectionOutlineWidth = 4;
    private readonly Color cellSelectionOutlineColor = new Color(0.3f, 0.4f, 0.9f);
    private readonly Color cellSelectionBackgroundColor = new Color(0.5f, 0.6f, 0.9f, 0.1f);
    private readonly Color cellSelectionFontColor = new Color(0.1f, 0.1f, 0.2f);
    private readonly Vector2 cellSize = new Vector2(1, 1);
    private const string FloorPrefix = "Floor_";
    private const string WallPrefix = "Wall_";
    private const string WallTrimPrefix = "WallTrim_";

    // Rect fill tool
    private Vector2Int rectFillFrom = new Vector2Int();
    private bool rectFillDragging = false;

    public Palette SelectedPalette => palettes[selectedPalette];
    public Tile SelectedTile => SelectedPalette.SelectedTile;
    private List<Palette> palettes = new List<Palette>();
    private int selectedPalette = 0;
    private int selectedTool = 0;

    [MenuItem("Window/Room Editor")]
    private static void ShowWindow()
    {
        GetWindow(typeof(RoomEditorWindow), false, "Room Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Tool");
        selectedTool = GUILayout.Toolbar(selectedTool, new string[] { "None", "Paint", "Fill Rect" });

        GUILayout.Space(8f);

        GUILayout.Label("Palette");
        selectedPalette = GUILayout.Toolbar(selectedPalette, palettes.Select(x => x.Name).ToArray());

        var palette = palettes[selectedPalette];
        palette.ScrollPosition = EditorGUILayout.BeginScrollView(palette.ScrollPosition, false, true);
        palette.SelectedTileIndex = GUILayout.SelectionGrid(palette.SelectedTileIndex, palette.Tiles.Select(x => x.Preview).ToArray(), paletteColumns);
        EditorGUILayout.EndScrollView();

        GUILayout.Space(8f);

        GUILayout.Label("Room Options");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Move To Origin"))
        {
            var room = GetRoom();
            Undo.RecordObject(room, "Moved To Origin");
            room.MoveToOrigin();
            room.Rebuild();
        }

        if (GUILayout.Button("Fill Outline"))
        {
            var room = GetRoom();
            Undo.RecordObject(room, "Filled Outline");
            room.FillOutline(SelectedTile);
            room.Rebuild();
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(8f);
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        if (selectedTool == 0)
        {
            return;
        }

        var mouseCell = GetMouseCell();

        if (Event.current.type == EventType.Layout)
        {
            HandleUtility.AddDefaultControl(0);
        }

        switch (selectedTool)
        {
            case 1: // Paint
                DrawCellSelection(mouseCell);

                if ((Event.current.type == EventType.MouseDown || Event.current.type == EventType.MouseDrag) && Event.current.button == 0)
                {
                    var room = GetRoom();

                    if (Event.current.control)
                    {
                        room.RemoveTile(mouseCell);
                    }
                    else
                    {
                        Undo.RecordObject(room, "Placed Tile");
                        room.PlaceTile(mouseCell, SelectedTile);
                    }

                    room.Rebuild();
                }

                break;
            case 2: // Rect Fill
                if (Event.current.button == 0)
                {
                    switch (Event.current.type)
                    {
                        case EventType.MouseDown:
                            rectFillFrom = mouseCell;
                            break;
                        case EventType.MouseDrag:
                            rectFillDragging = true;
                            break;
                        case EventType.MouseUp:
                            rectFillDragging = false;

                            var rect = RectFromCells(rectFillFrom, mouseCell);

                            var room = GetRoom();

                            if (Event.current.control)
                            {
                                room.RemoveRect(rect);
                            }
                            else
                            {
                                Undo.RecordObject(room, "Placed Tiles");
                                room.FillRect(rect, SelectedTile);
                            }
                            
                            room.Rebuild();

                            break;
                    }
                }

                if (rectFillDragging)
                {
                    DrawRectSelection(rectFillFrom, mouseCell);
                }
                else
                {
                    DrawCellSelection(mouseCell);
                }

                break;
        }

        sceneView.Repaint();
    }

    private Vector2Int GetMouseCell()
    {
        var guiRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        var mousePosition = guiRay.origin - guiRay.direction * (guiRay.origin.y / guiRay.direction.y);
        var cell = new Vector2Int(Mathf.FloorToInt(mousePosition.x / cellSize.x), Mathf.FloorToInt(mousePosition.z / cellSize.y));

        return cell;
    }

    private void DrawCellSelection(Vector2Int cell)
    {
        DrawRectSelection(new RectInt(cell, Vector2Int.one));
    }

    private RectInt RectFromCells(Vector2Int fromCell, Vector2Int toCell)
    {
        var min = new Vector2Int(Mathf.Min(fromCell.x, toCell.x), Mathf.Min(fromCell.y, toCell.y));
        var max = new Vector2Int(Mathf.Max(fromCell.x, toCell.x) + 1, Mathf.Max(fromCell.y, toCell.y) + 1);

        var rect = new RectInt();
        rect.SetMinMax(min, max);

        return rect;
    }

    private void DrawRectSelection(Vector2Int fromCell, Vector2Int toCell)
    {
        DrawRectSelection(RectFromCells(fromCell, toCell));
    }

    private void DrawRectSelection(RectInt rect)
    {
        var fromPosition = new Vector3(rect.xMin * cellSize.x, 0, rect.yMin * cellSize.y);
        var toPosition = new Vector3(rect.xMax * cellSize.x, 0, rect.yMax * cellSize.y);

        var topLeft = fromPosition + new Vector3(0, 0, rect.height * cellSize.y);
        var topRight = fromPosition + new Vector3(rect.width * cellSize.x, 0, rect.height * cellSize.y);
        var bottomLeft = fromPosition;
        var bottomRight = fromPosition + new Vector3(rect.width * cellSize.x, 0, 0);

        if (rect.width != 1 || rect.height != 1)
        {
            var style = new GUIStyle { normal = new GUIStyleState { textColor = cellSelectionFontColor } };
            Handles.Label((fromPosition + toPosition) * 0.5f, $"{rect.width}x{rect.height}", style);
        }

        Handles.color = cellSelectionBackgroundColor;
        Handles.DrawAAConvexPolygon(topLeft, topRight, bottomRight, bottomLeft);
        Handles.color = cellSelectionOutlineColor;
        Handles.DrawAAPolyLine(cellSelectionOutlineWidth, topLeft, topRight, bottomRight, bottomLeft, topLeft);
    }

    private void RefreshPalettes()
    {
        var tileset = Resources.LoadAll<Sprite>(DefaultTilesetPath);
        
        var wallTrimSprites = new List<Sprite>(tileset.Where(x => x.name.StartsWith(WallTrimPrefix)));
        var floorSprites = new List<Sprite>(tileset.Where(x => x.name.StartsWith(FloorPrefix)));
        var wallSprites = new List<Sprite>(tileset.Where(x => x.name.StartsWith(WallPrefix)));

        var floorPalette = new Palette { Name = "Floors", Tiles = floorSprites.Select(x => (Tile)new FloorTile { Sprite = x }).ToList() };
        var wallPalette = new Palette { Name = "Walls", Tiles = wallSprites.Select(x => (Tile)new WallTile { Sprite = x, WallTrim = wallTrimSprites }).ToList() };
        var pitPalette = new Palette { Name = "Pits", Tiles = new List<Tile>() { new PitTile() } };

        palettes = new List<Palette>() { floorPalette, wallPalette, pitPalette };
    }

    private RoomMesh GetRoom()
    {
        var room = FindObjectOfType<RoomMesh>();

        // Create the Room GameObject if one doesn't exist
        if (room == null)
        {
            var roomObject = new GameObject("Room");
            roomObject.tag = "Room";
            room = roomObject.AddComponent<RoomMesh>();
        }

        return room;
    }

    private void OnFocus()
    {
        SceneView.duringSceneGui -= this.OnSceneGUI;
        SceneView.duringSceneGui += this.OnSceneGUI;

        Undo.undoRedoPerformed += OnUndoRedo;

        RefreshPalettes();
    }

    private void OnDestroy()
    {
        SceneView.duringSceneGui -= this.OnSceneGUI;

        Undo.undoRedoPerformed -= OnUndoRedo;
    }

    private void OnUndoRedo()
    {
        var room = GetRoom();
        room.Rebuild();
    }
}