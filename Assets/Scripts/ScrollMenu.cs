using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollMenu : MonoBehaviour
{
    [SerializeField]
    private Transform spawn;
    [SerializeField]
    private GameObject listItemPrefab;
    [SerializeField]
    private RectTransform content;
    [SerializeField]
    private float padding;
    private float height;

    public void GenerateListFromList()
    {
        Clear();
        height = listItemPrefab.GetComponent<RectTransform>().sizeDelta.y;

        content.sizeDelta = new Vector2(content.sizeDelta.x, 6 * (height + padding) + padding*2);
        for (int i = 0; i < 6; i++)
        {
            float spawnY = i * (height + padding);
            //newSpawn Position
            Vector3 pos = new Vector3(0, -spawnY, spawn.position.z);
            //instantiate item
            GameObject SpawnedItem = Instantiate(listItemPrefab, pos, spawn.rotation);
            //setParent
            SpawnedItem.transform.SetParent(spawn, false);
        }
    }

    public void Clear()
    {

        foreach (Transform t in spawn.GetComponentsInChildren<Transform>())
        {
            if (t != spawn)
            {
                Destroy(t.gameObject);
            }
        }
    }

    public void GenerateStats(PlayerStats stats)
    {
        Clear();
        height = listItemPrefab.GetComponent<RectTransform>().sizeDelta.y;
        Attribute[] attributes = stats.GetAttributesAsArray();

        content.sizeDelta = new Vector2(content.sizeDelta.x, attributes.Length * (height + padding) + padding * 2);

        for (int i = 0; i < attributes.Length; i++)
        {
            float spawnY = i * (height + padding);
            //newSpawn Position
            Vector3 pos = new Vector3(0, -spawnY, spawn.position.z);
            //instantiate item
            GameObject SpawnedItem = Instantiate(listItemPrefab, pos, spawn.rotation);
            //setParent
            SpawnedItem.transform.SetParent(spawn, false);

            SpawnedItem.GetComponent<StatScrollMenuItem>().SetInformation(attributes[i].name, attributes[i].DisplayFinalValue());

        }
    }

    public void GenerateModules(ModuleInventory module)
    {
        Clear();
        height = listItemPrefab.GetComponent<RectTransform>().sizeDelta.y;
        List<Module> modules = module.GetModules();

        content.sizeDelta = new Vector2(content.sizeDelta.x, modules.Count * (height + padding) + padding * 2);

        for (int i = 0; i < modules.Count; i++)
        {
            float spawnY = i * (height + padding);
            //newSpawn Position
            Vector3 pos = new Vector3(0, -spawnY, spawn.position.z);
            //instantiate item
            GameObject SpawnedItem = Instantiate(listItemPrefab, pos, spawn.rotation);
            //setParent
            SpawnedItem.transform.SetParent(spawn, false);

            string bonuses = "";
            foreach (Bonus b in modules[i].bonuses)
            {
                bonuses += b.attributeName + " - " + b.value + "\n";
            }

            SpawnedItem.GetComponent<ModuleScrollMenuItem>().SetInformation(modules[i].name, bonuses, modules[i].sprite);
        }
    }
}
