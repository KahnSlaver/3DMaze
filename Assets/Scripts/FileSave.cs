using System.IO;
using UnityEngine;

public class FileSave : MonoBehaviour
{
    [SerializeField] float hertz = 0.05f;
    Vector3 startingPosition, Velocity, Position;

    float prevTime;

    string filename = "";

    void Start()
    {   
        prevTime = Time.time;
        startingPosition = transform.position;
        filename = Application.dataPath + "/test.csv";
        
        TextWriter tw = new StreamWriter( filename , false);
        tw.WriteLine("Time, PositionX ,PositionY,VelocityX,VelocityY");
        tw.Close();
    }

    void Update()
    {
        Position = transform.position - startingPosition;
        Velocity = GetComponent<Rigidbody>().velocity;
        WriteCSV();
        
        

    }

    private void WriteCSV()
    {
        if (Time.time-prevTime>=hertz)
        {
            prevTime = Time.time;
            TextWriter tw = new StreamWriter( filename , true);
            tw.WriteLine(Time.time + "," + Position.x + "," + Position.y);
            tw.Close();
        }
    }
}
