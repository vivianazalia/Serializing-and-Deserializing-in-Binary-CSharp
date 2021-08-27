using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serialize
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            Serializer serializer = new Serializer();

            int choiceNumber;
            Console.WriteLine("Choose the action!");
            Console.WriteLine("1. Serialize Data");
            Console.WriteLine("2. Deserialize Data");
            Console.Write("Input a number : ");
            choiceNumber = Convert.ToInt32(Console.ReadLine());

            Console.Write("Input name of file : ");
            string path = Console.ReadLine();

            if (choiceNumber == 1)
            {
                player.InputData();
                serializer.BinarySerialize(player, path);
            }
            else if (choiceNumber == 2)
            {
                if (File.Exists(path))
                {
                    serializer.BinaryDeserialize(path);
                }
                else
                {
                    Console.WriteLine("File doesn't exist");
                }
            }
        }
    }

    [Serializable]
    class Player
    {
        public Player() { }
       
        public string name { get; private set; }
        public int level { get; private set; }
        public int score { get; private set; }

        public void InputData()
        {
            Console.WriteLine("Input Player Data");
            Console.Write("Name : ");
            name = Console.ReadLine();
            Console.Write("Level : ");
            level = Convert.ToInt32(Console.ReadLine());
            Console.Write("Score : ");
            score = Convert.ToInt32(Console.ReadLine());
        }
    }

    class Serializer
    {
        public void BinarySerialize(Player data, string path)
        {
            FileStream fileStream;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            fileStream = File.Create(path);
            binaryFormatter.Serialize(fileStream, data);
            fileStream.Close();
            Console.WriteLine("Serialize success");
        }

        public void BinaryDeserialize(string path)
        {
            Player playerData = null;
            FileStream fileStream;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            if (File.Exists(path))
            {
                fileStream = File.OpenRead(path);
                playerData = (Player)binaryFormatter.Deserialize(fileStream);
            }
            Console.WriteLine("Name : " + playerData.name + " || Level : " + playerData.level + " || Score : " + playerData.score);
        }
    }
}
