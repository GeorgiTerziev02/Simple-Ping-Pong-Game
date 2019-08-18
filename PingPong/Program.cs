using System;
using System.Threading;

namespace PingPong
{
    class Program
    {
        //State
        static int firstPlayerPadSize = 4;
        static int secondPlayerPadSize = 4;
        static int ballPositionX = 0;
        static int ballPositionY = 0;
        static int firstPlayerPosition = 0;
        static int secondPlayerPosition = 0;

        static bool ballDirectionUp = true;
        static bool ballDirectionRight = true;

        static int firstPlayerScore = 0;
        static int secondPlayerScore = 0;
        static int difficulty = 75;

        static Random randomGenerator = new Random();

        static void Main(string[] args)
        {
            RemoveScrollBars();
            SetInitialPositions();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();

                    if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        MoveFirstPlayerUp();
                    }

                    if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        MoveFirstPlayerDown();
                    }
                }
                SecondPlayerAIMove();
                MoveBall();

                Console.Clear();
                DrawFirstPlayer();
                DrawSecondPlayer();
                DrawBall();
                PrintResult();

                Thread.Sleep(41);
            }
        }

        static void MoveBall()
        {
            if (ballPositionY == 0)
            {
                ballDirectionUp = false;
            }

            if (ballPositionY == Console.WindowHeight - 1)
            {
                ballDirectionUp = true;
            }

            if (ballPositionX == Console.WindowWidth - 1)
            {
                SetBallAtTheMiddleOfTheGameField();
                ballDirectionRight = false;
                ballDirectionUp = true;
                firstPlayerScore++;
                IncreaseDifficulty();

                string winner = "First player wins!";
                Console.SetCursorPosition(Console.WindowWidth / 2 - winner.Length / 2, Console.WindowHeight / 2);
                Console.WriteLine(winner);
                Console.ReadKey();
            }

            if (ballPositionX == 0)
            {
                SetBallAtTheMiddleOfTheGameField();
                ballDirectionRight = true;
                ballDirectionUp = true;
                secondPlayerScore++;

                string winner = "Second player wins!";
                Console.SetCursorPosition(Console.WindowWidth / 2 - winner.Length / 2, Console.WindowHeight / 2);
                Console.WriteLine(winner);
                Console.ReadKey();
            }

            if (ballPositionX < 3)
            {
                if (ballPositionY >= firstPlayerPosition
                    && ballPositionY < firstPlayerPosition + firstPlayerPadSize)
                {
                    ballDirectionRight = true;
                }
            }

            if (ballPositionX >= Console.WindowWidth - 4)
            {
                if (ballPositionY >= secondPlayerPosition
                    && ballPositionY < secondPlayerPosition + secondPlayerPadSize)
                {
                    ballDirectionRight = false;
                }
            }

            if (ballDirectionUp)
            {
                ballPositionY--;
            }
            else
            {
                ballPositionY++;
            }

            if (ballDirectionRight)
            {
                ballPositionX++;
            }
            else
            {
                ballPositionX--;
            }
        }

        static void IncreaseDifficulty()
        {
            if (difficulty < 99)
            {
                difficulty++;
            }
        }

        static void SecondPlayerAIMove()
        {
            int randomNumber = randomGenerator.Next(1, 101);

            //if (randomNumber == 0)
            //{
            //    MoveSecondPlayerUp();
            //}
            //if (randomNumber == 1)
            //{
            //    MoveSecondPlayerDown();
            //}

            if (randomNumber <= difficulty)
            {
                if (ballDirectionUp)
                {
                    MoveSecondPlayerUp();
                }
                else
                {
                    MoveSecondPlayerDown();
                }
            }

        }

        static void MoveSecondPlayerDown()
        {
            if (secondPlayerPosition + secondPlayerPadSize < Console.WindowHeight)
            {
                secondPlayerPosition++;
            }
        }

        static void MoveSecondPlayerUp()
        {
            if (secondPlayerPosition > 0)
            {
                secondPlayerPosition--;
            }
        }

        static void MoveFirstPlayerDown()
        {
            if (firstPlayerPosition + firstPlayerPadSize < Console.WindowHeight)
            {
                firstPlayerPosition++;
            }
        }

        static void MoveFirstPlayerUp()
        {
            if (firstPlayerPosition > 0)
            {
                firstPlayerPosition--;
            }
        }

        static void RemoveScrollBars()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
        }

        static void SetInitialPositions()
        {
            firstPlayerPosition = Console.WindowHeight / 2 - firstPlayerPadSize / 2;
            secondPlayerPosition = Console.WindowHeight / 2 - secondPlayerPadSize / 2;
            SetBallAtTheMiddleOfTheGameField();
        }

        static void SetBallAtTheMiddleOfTheGameField()
        {
            ballPositionX = Console.WindowWidth / 2;
            ballPositionY = Console.WindowHeight / 2;
        }

        static void DrawFirstPlayer()
        {
            int firstPlayerCol = 0;

            for (int i = firstPlayerPosition; i < firstPlayerPosition + firstPlayerPadSize; i++)
            {
                PrintAtPosition(firstPlayerCol, i, '|');
                PrintAtPosition(firstPlayerCol + 1, i, '|');
            }
        }

        static void DrawSecondPlayer()
        {
            int secondPlayerCol = Console.WindowWidth - 1;

            for (int i = secondPlayerPosition; i < secondPlayerPosition + secondPlayerPadSize; i++)
            {
                PrintAtPosition(secondPlayerCol, i, '|');
                PrintAtPosition(secondPlayerCol - 1, i, '|');
            }
        }

        static void DrawBall()
        {
            PrintAtPosition(ballPositionX, ballPositionY, '@');
        }

        static void PrintResult()
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 1, 0);
            Console.Write($"{firstPlayerScore} - {secondPlayerScore}");
        }

        static void PrintAtPosition(int x, int y, char symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }
    }
}
