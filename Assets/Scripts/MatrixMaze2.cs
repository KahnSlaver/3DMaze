using System.Collections.Generic;
using UnityEngine;

public class MatrixMaze2 : MonoBehaviour
{   
    private static List<int[]> Path = new List<int[]>(); 
    private int[,,] height = new int[19,19,2];

    int valI,valJ;                                   //for BackTracking()
    private List<int> AvailSpaces = new List<int>(); //for NodeChecking()
    private int next;                                //for NodeChecking()


    private void Start() //Shit Works Don't touch
    {   
        InitialStatus();
        NodeChecking(0,0);
        HeightSetting();
        HeightChanging();
    }

    private void InitialStatus() //Shit Works don't touch
    {
        for (int i = 0; i < 19; i++)
        {
            for (int j = 0; j < 19; j++)
            {
                if(i%2==0&&j%2==0)
                {
                    height[i,j,1] = 3; //3 means unvisited cell and 4 means visited cell
                }
                else
                {
                    height[i,j,1] = 1; //1 means unbroken wall 2 means broken wall
                }
                height[i,j,0] = 1; //for debugging
            }
        }
    }

    private void NodeChecking(int i,int j) //would undergo recursion //Shit works don't touch
    {   
        height[i,j,1] = 4;
        Path.Add(new int[2]{i,j});
        AvailSpaces.Add(8);
        AvailSpaces.Add(2);
        AvailSpaces.Add(4);
        AvailSpaces.Add(6);
        if(( i== 0) || height[i-2,j,1]==4)
        {
            AvailSpaces.Remove(4);
        }
        if(( i==18) || height[i+2,j,1]==4)
        {
            AvailSpaces.Remove(6);
        }
        if(( j== 0) || height[i,j-2,1]==4)
        {
            AvailSpaces.Remove(2);
        }
        if(( j==18) || height[i,j+2,1]==4)
        {
            AvailSpaces.Remove(8);
        }
        
        if(AvailSpaces.Count==0)
        {   
            AvailSpaces.Clear();
            backtracking();
            if(Path.Count==0)
            {
                return;
            }
        }
        else
        {
            next=AvailSpaces[Random.Range(0,AvailSpaces.Count)];
            AvailSpaces.Clear();
            if(next == 8)
            {   
                height[i,j+1,1]=2;
                NodeChecking(i,j+2);
            }
            else if(next == 2)
            {
                height[i,j-1,1]=2;
                NodeChecking(i,j-2);
            }
            else if(next == 4)
            {
                height[i-1,j,1]=2;
                NodeChecking(i-2,j);
            }
            else
            {
                height[i+1,j,1]=2;
                NodeChecking(i+2,j);
            }
        }
    }

    private void backtracking() //Shit works don't touch
    {   
        Path.RemoveAt(Path.Count-1);
        if(Path.Count==0)
        {
            return;
        }
        else
        {   
            valI = Path[Path.Count-1][0];
            valJ = Path[Path.Count-1][1];
            Path.RemoveAt(Path.Count-1);
            NodeChecking(valI,valJ);
        }
    }
    private void HeightSetting() //FineTuneValues
    {
        for(int i=0;i<19;i++)
        {
            for(int j=0;j<19;j++)
            {
                if(height[i,j,1]==4) //Cell
                {   
                    if(i==0&&j==0)
                    {
                        height[i,j,0] = 1;//1;
                    }
                    else if(i==18&&j==18)
                    {
                        height[i,j,0] = 2;//1;
                    }
                    else
                    {
                        height[i,j,0] = 2;//Random.Range(1,3);
                    }
                }
                else if(height[i,j,1]==2) //BrokenWall
                {
                    height[i,j,0] = 2;
                }
                else if(height[i,j,1]==1) //UnbrokenWall
                {
                    height[i,j,0] = 5;//Random.Range(3,6);
                }
            }
        }
    }

    private void HeightChanging()
    {
        for(int i=0;i<19;i++)
        {
            for(int j=0;j<19;j++)
            {
                transform.GetChild(i).transform.GetChild(j).transform.localScale = new Vector3(1,height[i,j,0],1);
                if(height[i,j,1]==1)
                {
                    transform.GetChild(i).transform.GetChild(j).transform.GetChild(1).gameObject.tag = "Untagged";
                }
            }
        }
    }
}
