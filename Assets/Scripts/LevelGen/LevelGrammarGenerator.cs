using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrammarGenerator : MonoBehaviour
{
    /// <summary>
    /// Here is a reference for the characters and what they represent because i will inevitably forget
    /// 
    /// f = finish. This is always the last room and there can only be one.
    /// 
    /// Common
    /// =======
    /// e = enemy room
    /// o = obstacle (trap or door or whatever)
    /// h = hallway (basically empty room, maybe some resources)
    /// 
    /// Dead Ends
    /// =========
    /// t = treasure (modules or resources)
    /// n = NPC (has an special npc room)
    /// b = boss (has more enemies or a miniboss)
    /// a = artifact (has an artifact)
    /// 
    /// Special
    /// ========
    /// | = locked
    /// > = contains key
    /// ? = concealed
    /// 
    /// </summary>


    [SerializeField]
    private int roomCount;
    private int maxBranchDepth;
    private int branchCount;
    private int levelDepth;
    [SerializeField]
    private List<char> essentialRooms;
    [SerializeField]
    private List<char> deadEnds;
    [SerializeField]
    private List<char> commonRooms;

    void Awake()
    {
        levelDepth = roomCount/4;
        maxBranchDepth = 3;
        branchCount = Random.Range(3, roomCount - levelDepth-1);
    }


    public string GetGenerationTemplate()
    {
        int roomsRemaining = roomCount;
        int branchMin = 0;
        string levelTemplate = "";
        for (int i = 0; i < levelDepth; i++)
        {
            roomsRemaining--;
            levelTemplate += commonRooms[Random.Range(0, commonRooms.Count - 1)];
        }
        for (int i = 0; i < branchCount; i++)
        {
            if (i+1 == branchCount)
            {
                branchMin = roomsRemaining;
            }
            string branch = CreateBranch(Random.Range(branchMin, roomsRemaining - branchCount + 1));
            roomsRemaining -= branch.Length - 2;
            int pos = Random.Range(1, levelTemplate.Length - 1);
            if (levelTemplate[pos + 1] == '(') pos++;
            if (levelTemplate[pos - 1] == ')') pos--;
            levelTemplate = levelTemplate.Insert(pos, branch);
        }
        return levelTemplate + "f";
    }

    private string CreateBranch(int branchSize)
    {
        string branch = "(";
        for (int i = 1; i < branchSize; i++)
        {
            branch += commonRooms[Random.Range(0, commonRooms.Count - 1)];
        }
        branch += deadEnds[Random.Range(0, deadEnds.Count - 1)];
        branch += ")";
        return branch;
    }
}
