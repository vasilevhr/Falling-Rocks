using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


struct Rock
{
    public int positionX;
    public int positionY;
    public char rockChar;
    public ConsoleColor rockColor;
}

class Program
{
    static int gamefieldWidth = 70;
    static int windowWidth = 100;
    static int windowHeight = 30;
    static int playerPositionX = gamefieldWidth / 2;
    static int playerPositionY = windowHeight - 1;
    static int playerLives = 20;
    static int gameSpeed = 150;
    static int gameLevel = 1;
    static double points = 0;
    static Random randomGenerator = new Random();
    static List<Rock> rocksList = new List<Rock>();
    static char[] rocksCharArr = { '^', '*', '&', '%', '+', '@', '!', '$', '#', '.', ';', '-' };
    static ConsoleColor[] rocksColorArr = { ConsoleColor.Green, ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Magenta, ConsoleColor.Cyan };

    static void PrintOnPosition(int positionX, int positionY, char itemCharacter, ConsoleColor itemColor = ConsoleColor.Gray)
    {
        Console.ForegroundColor = itemColor;
        Console.SetCursorPosition(positionX, positionY);
        Console.Write(itemCharacter);
    }

    static void PrintTextInGameMenu(int positionX, int positionY, string menuText, ConsoleColor textColor = ConsoleColor.Gray)
    {
        Console.ForegroundColor = textColor;
        Console.SetCursorPosition(positionX, positionY);
        Console.Write(menuText);
    }

    static void DrawPlayer()
    {
        PrintOnPosition(playerPositionX - 1, playerPositionY, '(');
        PrintOnPosition(playerPositionX, playerPositionY, '0');
        PrintOnPosition(playerPositionX + 1, playerPositionY, ')');
    }

    static void SetConsoleSettings()
    {
        Console.BufferWidth = Console.WindowWidth = windowWidth;        
        Console.BufferHeight = Console.WindowHeight = windowHeight;
        Console.CursorVisible = false;
    }

    static void SetInitialPosition()
    {
        DrawPlayer();
    }

    static void DrawGameMenu()
    {
        for (int i = 0; i < windowHeight; i++)
        {
            PrintOnPosition(gamefieldWidth, i, '|');
        }
        PrintTextInGameMenu(gamefieldWidth + 1, 0, "-----------------------------");
        PrintTextInGameMenu(gamefieldWidth + 6, 1, "--GAME INFORMATION--");
        PrintTextInGameMenu(gamefieldWidth + 1, 2, "-----------------------------");
        PrintTextInGameMenu(gamefieldWidth + 11, 4, "Points:" + (int)points);
        PrintTextInGameMenu(gamefieldWidth + 11, 6, "Lives:" + playerLives);
        PrintTextInGameMenu(gamefieldWidth + 11, 8, "Level:" + gameLevel);
        //TODO: Finish the game information.
    }

    static void MovePlayerRight()
    {
        if (playerPositionX < gamefieldWidth - 2)
        {
            playerPositionX++;
        }
    }

    static void MovePlayerLeft()
    {
        if (playerPositionX > 1)
        {
            playerPositionX--;
        }
    }

    static void DrawRocks()
    {
        Rock rock = new Rock();
        rock.positionX = randomGenerator.Next(0, gamefieldWidth);
        rock.positionY = 0;
        rock.rockChar = rocksCharArr[randomGenerator.Next(0, rocksCharArr.Length)];
        rock.rockColor = rocksColorArr[randomGenerator.Next(0, rocksColorArr.Length)];
        rocksList.Add(rock);
        List<Rock> tempList = new List<Rock>();
        for (int i = 0; i < rocksList.Count; i++)
        {
            Rock newRock = new Rock();
            newRock.positionX = rocksList[i].positionX;
            newRock.positionY = rocksList[i].positionY + 1;
            newRock.rockChar = rocksList[i].rockChar;
            newRock.rockColor = rocksList[i].rockColor;
            tempList.Add(newRock);
        }
        rocksList = tempList;
        foreach (var item in rocksList)
        {
            if (item.positionY < windowHeight)
            {
                PrintOnPosition(item.positionX, item.positionY, item.rockChar, item.rockColor);
            }
            else
            {
                points+=0.01;
            }
            if ((item.positionX == playerPositionX) && (item.positionY == playerPositionY))
            {
                PrintOnPosition(playerPositionX - 1, playerPositionY, '(', ConsoleColor.Red);
                PrintOnPosition(playerPositionX, playerPositionY, 'X', ConsoleColor.Red);
                PrintOnPosition(playerPositionX + 1, playerPositionY, ')', ConsoleColor.Red);
                DrawGameMenu();
                Thread.Sleep(500);
                rocksList.Clear();
                playerLives--;
                break;
            }
        }
    }

    static void Main()
    {
        SetConsoleSettings();
        SetInitialPosition();
        while (playerLives>=0)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey();
                while (Console.KeyAvailable)
                {
                    Console.ReadKey();
                }

                if (pressedKey.Key == ConsoleKey.RightArrow)
                {
                    MovePlayerRight();
                }
                if (pressedKey.Key == ConsoleKey.LeftArrow)
                {
                    MovePlayerLeft();
                }
            }
            Console.Clear();
            DrawRocks();
            DrawGameMenu();
            DrawPlayer();
            Thread.Sleep(gameSpeed);
        }
        Console.Clear();
        PrintTextInGameMenu(windowWidth / 2-10, windowHeight / 2, "!!! GAME OVER !!!", ConsoleColor.Red);
        Console.SetCursorPosition(0, 0);
        Thread.Sleep(1000);
    }


}
