using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

class Program
{
    class MovingObj
    {
        private static Random rand = new Random(); // Static으로 변경
        public int x = 2;
        public int y;;

        public MovingObj()
        {
            y = rand.Next(1, 5);
        }

        public void Update()
        {
            Console.SetCursorPosition(x, y);
            Console.Write('　'); // 이전 위치 지우기
            x += 1;
            Console.SetCursorPosition(x, y);
            Console.Write('⑦'); // 새로운 위치 그리기
        }
    }

    static void Main()
    {
        Stopwatch timer = new Stopwatch();
        timer.Start();

        List<MovingObj> movable = new List<MovingObj>();
        int energy = 2;

        // 초기 화면 출력
        Console.Clear();
        Console.WriteLine("┌──────────────────────────────────────────────────┐  |>");
        Console.WriteLine("│                                                  │ _|___  ");
        Console.WriteLine("│                                                  │/     |");
        Console.WriteLine("│                                                  │|_____| ");
        Console.WriteLine("│                                                  │ _| |_  ");
        Console.WriteLine("│                                                  │|_____|");
        Console.WriteLine("└──────────────────────────────────────────────────┘");
        Console.WriteLine("에너지 잔량");
        Console.WriteLine("\n\n1. 전사 소환(3개 소모) \n2. 탱커 소환(5개 소모)");

        while (true)
        {
            if (timer.ElapsedMilliseconds >= 500) // 조건 수정
            {
                timer.Restart();

                foreach (var obj in movable)
                {
                    obj.Update();
                }

                energy++;
                Console.SetCursorPosition(0, 8);
                Console.Write("에너지: ");
                Console.Write(new string('■', energy / 2));
            }

            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(intercept: true); // 키 입력 인터셉트
                if (key.Key == ConsoleKey.Spacebar && energy >= 6)
                {
                    energy -= 6;
                    movable.Add(new MovingObj());
                }
            }

            Thread.Sleep(1); // CPU 사용량 최적화
        }
    }
}