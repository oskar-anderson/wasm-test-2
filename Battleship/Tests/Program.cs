using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Text.Json;
using Domain.Model;
using RogueSharp;
using Troschuetz.Random.Generators;
using IrrKlang;
using NUnit.Framework;
using System.Text.Json.Serialization;
using Domain.Model.Api;
using Game.Pack;
using SFML.Graphics;
using SFML.Window;
using Utils;

namespace Tests
{
    static class Program
    {
        static void Main(string[] args)
        {
            // RandomTimeTest();
            // loopingTest();
            // Pack(1);
            // SoundTest();
            // MultiArrayTest();
            // StackReversesOnSerializationTest();
            // ByteArrSerializationTest();
            // GetDrawAreaAsPicturePerformanceTest();
            // GetDrawPerformanceTest();
            // SaveGameDataAsGzip();
            // ReadGameDataFromGzip();
            GitTest();
        }

        private static GameData GetGameData()
        {
            var json = System.IO.File.ReadAllText(@$"{AppDomain.CurrentDomain.BaseDirectory}/SampleData/GameState1.json");
            GameViewDTO gameViewDTO = JsonSerializer.Deserialize<GameViewDTO>(json)!;
            GameData gameData = GameDataSerializable.ToGameModelSerializable(gameViewDTO.GameData);
            return gameData;
        }
        
        private static GameViewDTO_v2 GetGameViewDTO()
        {
            var json = System.IO.File.ReadAllText(@$"{AppDomain.CurrentDomain.BaseDirectory}/SampleData/GameState1.json");
            GameViewDTO_v2 gameViewDTO = JsonSerializer.Deserialize<GameViewDTO_v2>(json)!;
            return gameViewDTO;
        }

        private static void GetDrawAreaAsPicturePerformanceTest()
        {
            GameData gameData = GetGameData();
            int times = 100;
            var stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < times; i++)
            {
                SfmlApp.ConsoleDrawLogic.GetDrawAreaAsPicture(gameData);
            }

            stopwatch.Stop();
            Console.WriteLine($"Loop {nameof(SfmlApp.ConsoleDrawLogic.GetDrawAreaAsPicture)}({nameof(gameData)}) {times} Times: " + stopwatch.Elapsed);
        }

        private static void SaveGameDataAsGzip()
        {
            var gameData = GetGameViewDTO();
            string json = JsonSerializer.Serialize(gameData, new JsonSerializerOptions() { WriteIndented = true });
            var result = Compression.Compress(json);
            File.WriteAllTextAsync("gameDataAsGzip.txt", result);
        }
        
        private static void ReadGameDataFromGzip()
        {
            string input = File.ReadAllText($"gameDataAsGzip.txt");
            var result = Compression.Decompress(input);
            File.WriteAllTextAsync("gameDataAsJson.json", result);
        }

        private static void GetDrawPerformanceTest()
        {
            var gameData = GetGameData();
            var window = new RenderWindow(new VideoMode(480, 360), "Battleships");

            int times = 1000;
            window.SetFramerateLimit(0);
            var stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < times; i++)
            {
                SfmlApp.ConsoleDrawLogic.Draw(1, gameData, window);
            }
            window.Close();

            stopwatch.Stop();
            Console.WriteLine($"Loop {nameof(SfmlApp.ConsoleDrawLogic.Draw)}() {times} Times: " + stopwatch.Elapsed);

        }

        private static void ByteArrSerializationTest()
        {
            var list = new List<Byte>()
            {
                0,
                0,
                0,
                0
            };
            var result = System.Text.Json.JsonSerializer.Serialize(list.ToArray());
        }

        private static void RandomTimeTest()
        {
            int upLimit = 1000 * 1000;
            var stopwatch = Stopwatch.StartNew();
/*
            for (int i = 0; i < upLimit; i++)
            {
                var std = new ALFGenerator(i);
                std.NextInclusiveMaxValue();
            }
            stopwatch.Stop();
            Console.WriteLine("ALFGenerator         " + stopwatch.Elapsed);


            
            stopwatch.Restart();
            for (int i = 0; i < upLimit; i++)
            {
                var std = new MT19937Generator(i);
                std.NextInclusiveMaxValue();
            }
            stopwatch.Stop();
            Console.WriteLine("MT19937Generator     " + stopwatch.Elapsed);
            
           */ 
            stopwatch.Restart();
            for (int i = 0; i < upLimit; i++)
            {
                var std = new NR3Generator(i);
                std.NextInclusiveMaxValue();
            }
            stopwatch.Stop();
            Console.WriteLine("NR3Generator         " + stopwatch.Elapsed);
            
            stopwatch.Restart();
            for (int i = 0; i < upLimit; i++)
            {
                var std = new NR3Q1Generator(i);
                std.NextInclusiveMaxValue();
            }
            stopwatch.Stop();
            Console.WriteLine("NR3Q1Generator       " + stopwatch.Elapsed);
            
            
            stopwatch.Restart();
            for (int i = 0; i < upLimit; i++)
            {
                var std = new NR3Q2Generator(i);
                std.NextInclusiveMaxValue();
            }
            stopwatch.Stop();
            Console.WriteLine("NR3Q2Generator       " + stopwatch.Elapsed);
            
            stopwatch.Restart();
            for (int i = 0; i < upLimit; i++)
            {
                var std = new StandardGenerator(127);
                std.NextInclusiveMaxValue();
            }
            stopwatch.Stop();
            Console.WriteLine("StandardGenerator    " + stopwatch.Elapsed);
            
            stopwatch.Restart();
            for (int i = 0; i < upLimit; i++)
            {
                var std = new XorShift128Generator(i);
                std.NextInclusiveMaxValue();
            }
            stopwatch.Stop();
            Console.WriteLine("XorShift128Generator " + stopwatch.Elapsed);

            stopwatch.Restart();
            for (int i = 0; i < upLimit; i++)
            {
                var std = new Random(i);
                std.Next();
            }
            stopwatch.Stop();
            Console.WriteLine("System Random        " + stopwatch.Elapsed);
        }

        private static void loopingTest()
        {
            int upLimit = 1000 * 1000;
            var stopwatch = Stopwatch.StartNew();
            
            List<string> testList = new List<string>() { String.Concat(Enumerable.Repeat("T", 1000)) };
            stopwatch.Restart();
            for (int i = 0; i < upLimit; i++)
            {
                foreach (var s in testList.ToArray())
                {
                    
                }
            }
            stopwatch.Stop();
            Console.WriteLine("Loop with collection toArray  " + stopwatch.Elapsed);
            string[] testArray = new List<string>() { String.Concat(Enumerable.Repeat("T", 1000)) }.ToArray();
            stopwatch.Restart();
            for (int i = 0; i < upLimit; i++)
            {
                foreach (var s in testArray)
                {
                    
                }
            }
            stopwatch.Stop();
            Console.WriteLine("Loop with no collection toArray  " + stopwatch.Elapsed);
            
            
            stopwatch.Restart();
            for (int i = 0; i < upLimit; i++)
            {
                foreach (var (rectangle, idx1) in testArray.Select((value, idx) => (value, idx)))
                {
                    
                }
            }
            stopwatch.Stop();
            Console.WriteLine("LINQ tuple idx and item       " + stopwatch.Elapsed);
            
            
            stopwatch.Restart();
            for (int i = 0; i < upLimit; i++)
            {
                for (int j = 0; j < testArray.Length; j++)
                {
                    string s = testArray[j];
                }
            }
            stopwatch.Stop();
            Console.WriteLine("for with item from idx        " + stopwatch.Elapsed);
            
            stopwatch.Restart();
            for (int i = 0; i < upLimit; i++)
            {
                foreach (var s in testArray)
                {
                }
            }
            stopwatch.Stop();
            Console.WriteLine("foreach                       " + stopwatch.Elapsed);
        }
        
        private static List<Rectangle> Pack(int placementType)
        {
            List<Point> shipsSizesToPlace = new List<Point>()
            {
                new Point(5, 1),
                new Point(4, 1),
                new Point(3, 1),
                new Point(3, 1),
                new Point(3, 1),
                new Point(2, 1),
                new Point(2, 1),
                new Point(2, 1),
                new Point(2, 1),
            };
            List<Rectangle> packedRects;
            ShipPlacement.TryPackShip(shipsSizesToPlace, 10, 10, placementType, out packedRects);
            return packedRects;
        }

        private class SerializeMe
        {
            public Stack<int> Order { get; set; } = new Stack<int>();
        }

        private static void StackReversesOnSerializationTest()
        {
            List<int> items = new List<int>() { 1, 2, 3 };
            SerializeMe serializeMe = new SerializeMe();
            foreach (var item in items)
                serializeMe.Order.Push(item);
            string serialized = JsonSerializer.Serialize(serializeMe);
            SerializeMe deserialized = JsonSerializer.Deserialize<SerializeMe>(serialized) ?? throw new InvalidOperationException($"{serialized} cannot be deserialized!");
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine("deserialized: " + deserialized.Order.Pop() + "; original: " + serializeMe.Order.Pop());
            }
        }

        /// <summary>
        /// Doxygen generates descriptions for public functions
        /// </summary>
        public static void GitTest()
        {
            var executeProcess = (string command, string argument) =>
            {
                var process = Process.Start(command, argument);
                process.WaitForExit();
            };
            var currentTime = $"{DateTime.Now.ToString(CultureInfo.CreateSpecificCulture("en-GB"))}";
            executeProcess("git", @"add -A");
            executeProcess("git", $@"commit -m ""program executed at {currentTime}""");
            Console.WriteLine("test");
        }

        private static void SoundTest()
        {
            ISoundEngine engine = new ISoundEngine();
            engine.Play2D("../../../../../media/Test/getout.ogg", true);
            Console.Out.WriteLine("\nHello World");

            do
            {
                Console.Out.WriteLine("Press any key to play some sound, press 'q' to quit.");

                // play a single sound
                engine.Play2D("../../../../../media/Test/bell.wav");
            }
            while(_getch() != 'q');
        }
        
        // some simple function for reading keys from the console
        [System.Runtime.InteropServices.DllImport("msvcrt")]
        static extern int _getch();

        private static void MultiArrayTest()
        {
            // WTF is going on with jagged array dimensions?
            // Length is supposed to get the total number of elements in all the dimensions of the array,
            // but only gives current array element count. 
            // GetLength() throws exceptions in further dept
            int[][] testMap = new int[2][];
            testMap[0] = new[] {1, 2, 3};
            testMap[1] = new[] {1, 2, 3};
            Assert.AreEqual(2, testMap.Length);
            Assert.AreEqual(3, testMap[0].Length);
            Assert.AreEqual(2,testMap.GetLength(0));
            Assert.Throws<IndexOutOfRangeException>(() => testMap.GetLength(1));
        }

        #region Rider test
        private static Action Test { get; set; } = null!;

        private static void SetTest(Action? action = null)
        {
            // Rider IDE doesn't seem to highlight nullable to non-nullable assignment.
            Test = action;
        }
        #endregion
    }
}