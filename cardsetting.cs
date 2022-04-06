using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class Record
{
    public int CardNumber { get; set; }
    public int Cost { get; set; }
    public string Name { get; set; }
    public int Atkdef { get; set; }
    public int Atknumber { get; set; }
    public int Special { get; set; }
    public int Spnumber { get; set; }
}
public class Mydeck
{
    public int CardNumber { get; set; }
    public int CardPosition { get; set; }
    public int Cardused { get; set; }

}
public class cardsetting : MonoBehaviour
{
    public GameObject card;
    public List<Record> records;
    public List<Mydeck> deck;
    public string[] lines;
    public int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 1;
        lines =
           System.IO.File.ReadAllLines(@"C:\Users\620-13\My project\Assets\deck.txt");
        foreach (var line in lines)
        {
            Console.WriteLine(line);
        }
        records = new List<Record>();
        foreach (var line in lines)
        {
            string[] splitData = line.Split(',');
            records.Add(
              new Record
              {
                  CardNumber = int.Parse(splitData[0]),
                  Cost = int.Parse(splitData[1]),
                  Name = splitData[2],
                  Atkdef = int.Parse(splitData[3]),
                  Atknumber = int.Parse(splitData[4]),
                  Special = int.Parse(splitData[5]),
                  Spnumber = int.Parse(splitData[6])
              });
        }
        deck = new List<Mydeck>();
        for (int i = 0; i < 10; i++)
        {
            deck.Add(new Mydeck { CardNumber = 0, CardPosition = 0, Cardused = 0 });
            if (i < 5)
                deck[i].CardNumber = 1;
            else
                deck[i].CardNumber = 2;
        }
        for (int i = 0; i < 10; i++)
        {
            System.Random random = new System.Random(Guid.NewGuid().GetHashCode());
            int rnd = random.Next(0, i);
            int temp = deck[i].CardNumber;
            deck[i].CardNumber = deck[rnd].CardNumber;
            deck[rnd].CardNumber = temp;
        }
        for (int i = 0; i < 10; i++)
        {
            Debug.Log(i.ToString() + "." + deck[i].CardNumber);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(card, new Vector3(-8, -4, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
        }
    }
}
