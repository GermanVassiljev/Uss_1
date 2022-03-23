using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uss_1
{
    class Snake : Figure
    {
        Direction direction;
        int score = 9;

        public Snake(Point tail, int length, Direction _direction)
        {
            direction = _direction;
            pList = new List<Point>();
            for (int i = 0; i < length; i++)
            {
                Point p = new Point(tail);
                p.Move(i, direction);
                pList.Add(p);
            }
        }

        internal void Move() // Добавление головы и удаление хвоста при движении
        {
            Point tail = pList.First();
            pList.Remove(tail);
            Point head = GetNextPoint();
            pList.Add(head);

            tail.Clear();
            head.Draw();
        }

        public Point GetNextPoint() // Увеличение змеи при взятии очка
        {
            Point head = pList.Last();
            Point nextPoint = new Point(head);
            nextPoint.Move(1, direction);
            return nextPoint;
        }

        internal bool IsHitTail()
        {
            var head = pList.Last();
            for (int i = 0; i < pList.Count - 2; i++)
            {
                if (head.IsHit(pList[i]))
                    return true;
            }
            return false;
        }

        public void HandleKey(ConsoleKey key) // Смотр клавишь управления и присваивание их direction
        {
            if (key == ConsoleKey.LeftArrow)
                direction = Direction.LEFT;
            else if (key == ConsoleKey.RightArrow)
                direction = Direction.RIGHT;
            else if (key == ConsoleKey.DownArrow)
                direction = Direction.DOWN;
            else if (key == ConsoleKey.UpArrow)
                direction = Direction.UP;
        }

        internal bool Eat(Point food)//Регистрация удара головы с едой
        {
            Point head = GetNextPoint();
            if (head.IsHit(food))
            {
                //Комментарии при достижении определённого счёта
                score++;
                Console.SetCursorPosition(70, 23);
                Console.WriteLine($"Score: {score}");
                food.sym = head.sym;
                pList.Add(food);
                if (score==10)
                {
                    Console.SetCursorPosition(35, 11);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Well done");
                }
                else if(score==20)
                {
                    Console.SetCursorPosition(35, 20);
                    Console.WriteLine("Cool!");
                }
                else if (score==50)
                {
                    Console.SetCursorPosition(35, 11);
                    Console.WriteLine("Rock&Roll");
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

