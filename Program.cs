
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Uss_1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создание окна
            Console.SetBufferSize(800, 250);

            // Рисовка стен по позиции
            Walls walls = new Walls(80, 25);
            walls.Draw();

            // Создание силуета змейки
            Point p = new Point(4, 5, '*');
            Snake snake = new Snake(p, 4, Direction.RIGHT);
            snake.Draw();
            
            // Отрисовка еды
            FoodCreator foodCreator = new FoodCreator(80, 25, '$');
            Point food = foodCreator.CreateFood();
            food.Draw();


            // бесконечный цикл с условиями столкновенния змеи с чем либо
            while (true)
            {
                if (walls.IsHit(snake) || snake.IsHitTail())
                {
                    break;
                }
                if (snake.Eat(food))
                {
                    food = foodCreator.CreateFood();
                    food.Draw();
                }
                else
                {
                    snake.Move();
                }

                Thread.Sleep(100);
                if (Console.KeyAvailable) // Условия управления змейкой
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.HandleKey(key.Key);
                }
            }
            WriteGameOver(); //Конец игры
            Console.ReadLine();
        }

        static void WriteGameOver() // Подробности конца игры
        {
            int xOffset = 25;
            int yOffset = 8;
            Console.ForegroundColor = ConsoleColor.Red; // Цвет написанного
            Console.SetCursorPosition(xOffset, yOffset++);
            WriteText("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=", xOffset, yOffset++);
            WriteText("     G A M E   O V E R", xOffset + 1, yOffset++);
            yOffset++;
            WriteText("Creator: German Vassiljev", xOffset + 6, yOffset++);
            WriteText("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=", xOffset, yOffset++);
        }

        static void WriteText(String text, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine(text);
        }
    }
}