using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class MapTest : MonoBehaviour
{
    // The prefab used for a stage node in the map
    public GameObject StagePrefab;

    public GameObject Container;

    public List<Biome> Biomes;

    public Map Map;

    private int SpacingX = 70;

    private int SpacingY = 70;

    // Start is called before the first frame update
    void Start()
    {
        MapGenerator mapGen = new MapGenerator();

        mapGen.Biomes = Biomes;

        Map = mapGen.Generate();

        // Create UI

        int x = 0;
        int y = 0;

        foreach (var quadrant in Map.Quadrants)
        {
            foreach (var layer in quadrant.Stages)
            {
                foreach (var stage in layer)
                {
                    var stageGameObject = CreateStage(stage, new Vector2(x * SpacingX, y * SpacingY));

                    //foreach (var connection in stage.Next)
                    //{
                    //    GameObject go = new GameObject();

                    //    var lineRenderer = go.AddComponent<UILineRenderer>();

                    //    lineRenderer.Points = new Vector2[] { new Vector2(0, 0), new Vector2(10, 10) };
                    //    go.transform.parent = stageGameObject.transform;
                    //}

                    y++;
                }

                y = 0;
                x++;
            }

            x += 2;
        }
    }

    GameObject CreateStage(Stage stageInfo, Vector2 position)
    {
        var stage = Instantiate(StagePrefab, Container.transform);

        var stageTransform = stage.GetComponent<RectTransform>();
        stageTransform.anchoredPosition = position;

        var stageDisplay = stage.GetComponent<StageDisplay>();

        stageDisplay.Stage = stageInfo;

        return stage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
