﻿using System;

namespace HometaskDeckCards.Scripts
{
    public class UserInput
    {
        const int MinCountName = 2;
        const string MassengInputName = "Введите имя игрока:";
        const string MassengErrorInputName = "Имя игрока не может быть пустым и должно быть длиннее: ";
        const string MassengInputCountCard = "Ведите количество карт которое вы хотите дать игроку.";
        const string MassengErrorInputCountCard = "Либо вы не указали сколько карт дать игроку, либо ввели неверное значение, количество карт нудно вводить цифрами и оно не может быть отрицательным.";

        public string PlayerName { get; private set; }
        public int PlayerCountCards { get; private set; }

        public void ReadUserInput()
        {
            do
            {
                Console.WriteLine(MassengInputName);
                PlayerName = Console.ReadLine();

                if (PlayerName.Length < MinCountName)               
                    Console.WriteLine($"{MassengErrorInputName}{MinCountName}");               
            } 
            while (PlayerName.Length < MinCountName);

            uint tempPlayerCountCards;

            while (PlayerCountCards <= 0)
            {
                Console.WriteLine(MassengInputCountCard);

                PlayerCountCards = uint.TryParse(Console.ReadLine(), out tempPlayerCountCards) ? (int)tempPlayerCountCards : 0;

                if (PlayerCountCards == 0)
                    Console.WriteLine(MassengErrorInputCountCard);
            }
        }
    }
}