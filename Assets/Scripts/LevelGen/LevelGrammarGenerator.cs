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
    /// t - trap room (enemy room but the player is locked in until all enemies are defeated)
    /// o = obstacle (trap or door or whatever)
    /// h = hallway (basically empty room, maybe some resources)
    /// 
    /// Dead Ends
    /// =========
    /// r = reward (modules or resources)
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
    private List<char> deadEnds;
    [SerializeField]
    private List<char> commonRooms;
    [SerializeField]
    private List<char> buildingBlocks;
    [SerializeField]
    private List<string> branches = new List<string>();
    [SerializeField]
    private int challengeRating;
    [SerializeField]
    private int rewardRating;

    private int challengeCount = 0;
    private int rewardCount = 0;
    void Awake()
    {
        levelDepth = 5;
        maxBranchDepth = 3;
        branchCount = 10;
        challengeRating = (int)Mathf.Floor(roomCount / 3);
        rewardRating = (int)Mathf.Floor(roomCount / 4);
    }

    private void ResetAll()
    {
        branches.Clear();
    }

    public string GetGenerationTemplate()
    {
        ResetAll();
        int roomsRemaining = roomCount - 2;
        int branchMin = 2;
        string levelTemplate = "-";
        for (int i = 1; i < levelDepth - 1; i++)
        {
            roomsRemaining--;
            levelTemplate += buildingBlocks[Random.Range(0, 1)];
        }
        for (int i = 0; i < branchCount; i++)
        {
            if (roomsRemaining <= 1)
            {
                break;
            }
            string branch = CreateBranch(Random.Range(branchMin, Mathf.Min(roomsRemaining, levelDepth)));
            roomsRemaining -= branch.Length - 2;
            branches.Add(branch);

        }
        for (int i = 0; i < branches.Count; i++)
        {
            CombineBranches();
        }
        
        PlaceFinish();
        levelTemplate += deadEnds[Random.Range(0, deadEnds.Count)];
        levelTemplate = PlaceBranches(levelTemplate);
        return levelTemplate;
    }

    private string PlaceBranches(string level)
    {
        foreach(string branch in branches)
        {
            int pos = level.LastIndexOf(')') + 2;
            if (pos < 0) pos = 1;
            level = level.Insert(pos, branch);
        }
        return level;
    }

    private void PlaceFinish()
    {
        string branch = branches[branches.Count-1];
        branches[branches.Count - 1] = branch.Insert(branch.IndexOf(')') , "f");
    }

    private void CombineBranches()
    {
        for (int i = 0; i < branches.Count; i++)
        {
            if (branches.Count < levelDepth - 1) return;
            int braces = 0;
            foreach (char c in branches[i])
            {
                if (c == ')' || c == '(') braces++;
            }
            if (braces <= branches[i].Length/2)
            {
                string nestedBranch = GetRandomBranch(i);
                branches.Remove(nestedBranch);
                branches[i] = branches[i].Insert(Random.Range(1, branches[i].Length-2), nestedBranch);
            }
        }
    }

    private string GetRandomBranch(int excludingIndex)
    {
        return branches[Random.Range(excludingIndex, branches.Count - 1)];
    }

    private string CreateBranch(int branchSize)
    {
        branchSize = Mathf.Clamp(branchSize, 0, levelDepth);
        string branch = "(";
        for (int i = 1; i < branchSize; i++)
        {
            char r = buildingBlocks[Random.Range(0, 2)];
            if (challengeCount < challengeRating)
            {
                challengeCount++;
                r = buildingBlocks[2];
                Debug.Log(r);
            }
            branch += r;
        }
        char room = buildingBlocks[3];
        if (rewardCount < rewardRating)
        {
            Debug.Log("adding reward");
            rewardCount++;
            room = buildingBlocks[4]; 
        }
        branch += room;
        branch += ")";
        return branch;
    }

    public string FillTemplate(string template)
    {
        for (int i = 0; i < template.Length; i++)
        {
            switch (template[i])
            {
                case '-':
                    if (i == 0) break;
                    char room = commonRooms[1];
                    template = ReplaceAt(template, i, room);
                    break;
                case '*':
                    template = ReplaceAt(template, i, commonRooms[Random.Range(2,4)]);
                    break;
                case '#':
                    template = ReplaceAt(template, i, deadEnds[Random.Range(1, deadEnds.Count)]);
                    break;
                case '$':
                    template = ReplaceAt(template, i, deadEnds[0]);
                    break;
                case '!':
                    template = ReplaceAt(template, i, commonRooms[0]);
                    break;
                case '(':
                case ')':
                    break;

            }

        }

        return template;
    }
    private string ReplaceAt(string s, int index, char newChar)
    {
        char[] chars = s.ToCharArray();
        chars[index] = newChar;
        string newString = "";
        foreach (char c in chars)
        {
            newString += c;
        }
        return newString;
    }
}
