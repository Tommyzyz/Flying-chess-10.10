using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 飞行棋_10._10
{
    class Program
    {
        //地图数组(空□)(幸运轮盘1◎) (地雷2☆) (暂停3▲) (时空隧道4卍)
        public static int[] Map = new int[100];
        //玩家位置数组(玩家A，玩家B)
        public static int[] Player = new int[2];
        //玩家姓名
        public static string[] PlayerName=new string[2];

        static void Main(string[] args)
        {
            MenuShow();
            #region 输入姓名
            Console.WriteLine("请输入玩家A姓名：");
            PlayerName[0]= Console.ReadLine();
            while (PlayerName[0]=="")
            {
                Console.WriteLine("输入错误，请输入玩家A姓名：");
                PlayerName[0] = Console.ReadLine();
            }
            Console.WriteLine("请输入玩家B姓名：");
            PlayerName[1] = Console.ReadLine();
            while (PlayerName[1] == ""&&PlayerName[1]==PlayerName[0])
            {
                Console.WriteLine("输入错误，请输入玩家B姓名：");
                PlayerName[1] = Console.ReadLine();
            }
            #endregion
            Console.Clear();
            MenuShow();
            Console.WriteLine("{0}的位置用A表示,{1}的位置用B表示", PlayerName[0], PlayerName[1]);
            Console.WriteLine("当AB在同一位置时用\"<>\"表示");
            InitialMap();
            DrawMap();
            while(Player[0]<99 && Player[1]<99)
            {
                PlayGame(0);
                if (Player[0] >= 99)
                {
                    Console.WriteLine("玩家{0}赢了！",PlayerName[0]);
                    Console.ReadKey();
                }
                PlayGame(1);
                if (Player[1] >= 99)
                {
                    Console.WriteLine("玩家{0}赢了！", PlayerName[1]);
                    Console.ReadKey();



                }
            }
            Console.ReadKey();
        }

        /// <summary>
        /// 显示标题
        /// </summary>
        public static void MenuShow()
        {
            Console.WriteLine("***************************");
            Console.WriteLine("***************************");
            Console.WriteLine("**********飞行棋***********");
            Console.WriteLine("***************************");
            Console.WriteLine("***************************");
            Console.WriteLine("***************************");


        }

        /// <summary>
        /// 初始化地图
        /// </summary>
        public static void InitialMap()
        {
            int[] luckyturn = { 6, 23, 40, 55, 69, 83 };//幸运轮盘1 ◎    
            int[] landMine = { 5, 13, 17, 33, 38, 50, 64, 80, 94 };//地雷2 ☆
            int[] pause = { 9, 27, 60, 93 };//暂停3▲
            int[] timeTunnel = { 20, 25, 45, 63, 72, 88, 90 };//时空隧道4 卍
            for (int i = 0; i < luckyturn.Length; i++)
            {
                Map[luckyturn[i]] = 1;
            }
            for (int i = 0; i < timeTunnel.Length; i++)
            {
                Map[timeTunnel[i]] = 4;
            }
            for (int i = 0; i < landMine.Length; i++)
            {
                Map[landMine[i]] = 2;
            }
            for (int i = 0; i < pause.Length; i++)
            {
                Map[pause[i]] = 3;
            }

        }

        /// <summary>
        /// 显示出地图
        /// </summary>
        public static void DrawMap()
        {
            Console.WriteLine("图例：幸运轮盘◎，地雷☆，暂停▲，时空隧道卍");
            for (int i = 0; i < 30; i++)
            {
                Console.Write(DrawStringMap(i));
            }
            Console.WriteLine();

            for (int i = 30; i < 35; i++)
            {
                
                for (int j = 0; j < 29; j++)
                {
                    Console.Write("　");
                }
                Console.Write(DrawStringMap(i));
                Console.WriteLine();
            }

            for (int i=64;i>34;i--)
            {
                Console.Write(DrawStringMap(i));
            }
            Console.WriteLine();

            for (int i=65;i<70;i++)
            {
                Console.Write(DrawStringMap(i));
                Console.WriteLine();
            }

            for (int i=70;i<100;i++)
            {
                Console.Write(DrawStringMap(i));
            }
            Console.WriteLine();
        }

        /// <summary>
        /// 画出关卡数对应图形
        /// </summary>
        /// <param name="i">当前关卡下标</param>
        /// <returns>当前关卡图形</returns>
        public static string DrawStringMap(int i)
        {
            string str="";
            if (Player[0] == Player[1] && Player[0] == i)
            {
                str = "<>";
            }
            else if (Player[0] == i)
            {
                str = "Ａ";
            }
            else if (Player[1] == i)
            {
                str = "Ｂ";
            }
            else
            {
                switch (Map[i])
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.White;
                        str = "□";
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        str = "◎";
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Red;
                        str = "☆";
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        str = "▲";
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        str = "卍";
                        break;
                }
            }
            return str;
        }

        /// <summary>
        /// 玩游戏
        /// </summary>
        public static void PlayGame(int playerNum)
        {
            Random r = new Random();
            int rNumber = r.Next(1, 7);
            Console.WriteLine("{0}请按任意键掷骰子：", PlayerName[playerNum]);
            Console.ReadKey(true);
            Console.WriteLine("{0}掷出了：{1}", PlayerName[playerNum],rNumber);
            Player[playerNum] += rNumber;
            PlayerFix();
            Console.ReadKey(true);
            Console.WriteLine("{0}请按任意键开始行动：", PlayerName[playerNum]);
            Console.ReadKey(true);
            Console.WriteLine("{0}前进{1}格", PlayerName[playerNum],rNumber);
            Console.ReadKey(true);
            if (Player[playerNum] == Player[1-playerNum])
            {
                Console.WriteLine("{0}踩到了{1}，{1}退6格", PlayerName[playerNum], PlayerName[1- playerNum]);
                Player[1- playerNum] -= 6;
                PlayerFix();
                Console.ReadKey(true);
            }
            else
            {
                switch (Map[Player[playerNum]])
                {
                    case 0:
                        Console.WriteLine("{0}踩到了方块", PlayerName[playerNum]);
                        Console.ReadKey(true);
                        break;
                    case 1:
                        Console.WriteLine("{0}踩到了幸运轮盘，1-交换位置，2-轰炸玩家，请选择：", PlayerName[playerNum]);
                        string input = Console.ReadLine();
                        while (true)
                        {
                            if (input == "1")
                            {
                                Console.WriteLine("{0}选择与{1}交换位置", PlayerName[playerNum], PlayerName[1- playerNum]);
                                Console.ReadKey(true);
                                int temp;
                                temp = Player[playerNum];
                                Player[playerNum] = Player[1- playerNum];
                                Player[1- playerNum] = temp;
                                PlayerFix();
                                break;
                            }
                            else if (input == "2")
                            {
                                Console.WriteLine("{0}选择轰炸{1}，{1}退6格", PlayerName[playerNum], PlayerName[1- playerNum]);
                                Console.ReadKey(true);
                                Player[1- playerNum] -= 6;
                                PlayerFix();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("输入错误，1-交换位置，2-轰炸玩家，请选择：");
                                input = Console.ReadLine();
                            }
                        }
                        break;
                    case 2:
                        Console.WriteLine("{0}踩到了地雷,退6格", PlayerName[playerNum]);
                        Player[playerNum] -= 6;
                        PlayerFix();
                        Console.ReadKey(true);
                        break;
                    case 3:
                        Console.WriteLine("{0}踩到了暂停,暂停1回合", PlayerName[playerNum]);
                        Console.ReadKey(true);
                        PlayGame(1 - playerNum);
                        break;
                    case 4:
                        Console.WriteLine("{0}踩到了时空隧道，前进10格", PlayerName[playerNum]);
                        Player[playerNum] += 10;
                        PlayerFix();
                        Console.ReadKey(true);
                        break;
                }
            }
            Console.Clear();
            DrawMap();

        }

        /// <summary>
        /// 玩家坐标修正
        /// </summary>
        public static void PlayerFix()
        {
            if(Player[0] < 0)
            {
                Player[0] = 0;
            }
            if (Player[0] > 100)
            {
                Player[0] = 99;
            }
            if (Player[1] < 0)
            {
                Player[1] = 0;
            }
            if (Player[1] > 100)
            {
                Player[1] = 99;
            }
        }
    }
}
