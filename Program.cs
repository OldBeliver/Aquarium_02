using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aquarium_02
{
    class Program
    {
        static void Main(string[] args)
        {
            int fishLimit = 15;
            Aquarium aquarium = new Aquarium(fishLimit);
            aquarium.KeepWatch();
        }
    }

    class Aquarium
    {
        private int _size;
        private List<Fish> _fishes;

        public Aquarium(int size)
        {
            _size = size;
            _fishes = new List<Fish>();
        }

        public void KeepWatch()
        {
            bool isKeep = true;

            while (isKeep)
            {
                ShowFishes();
                ShowMenu();

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "0":
                        SkipWeek();
                        MeetDeath();
                        break;
                    case "1":
                        AddFish();
                        break;
                    case "2":
                        RemoveFish();
                        break;
                    case "9":
                        isKeep = false;
                        break;
                    default:
                        Console.WriteLine($"Ошибка ввода команды.");
                        break;
                }

                Console.Write($"нажмите любую для продолжения ...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void ShowFishes()
        {
            string title = " рыбки в аквариуме ";

            if(_fishes.Count == 0)
            {
                title = " аквариум пустой ";
            }

            Console.WriteLine($"{title} {_fishes.Count}/{_size}");

            for (int i = 0; i < _fishes.Count; i++)
            {
                Console.Write($"{i + 1:d2}. ");
                _fishes[i].ViewFish();
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine($"-------------------");
            Console.WriteLine($"1. Добавить рыбку\n2. Отсадить рыбку\n0. Завершить ход\n9. Выйти");
            Console.Write($"\nВведите команду: ");
        }

        private void AddFish()
        {
            if (_fishes.Count >= _size)
            {
                Console.WriteLine($"Нельзя добавить рыбку. Это будет банка со шпротами, а не аквариум");
            }
            else
            {
                Console.WriteLine($"-------------------");
                Console.WriteLine($"Добавить в аквариум рыбку под номером:");
                Console.Write($"1. Склярия 2.Барбус 3.Гуппи : ");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        _fishes.Add(new AngelFish());
                        Console.WriteLine($"в аквариум запустили рыбку склярия");
                        break;
                    case "2":
                        _fishes.Add(new Barbus());
                        Console.WriteLine($"в аквариум запустили рыбку барбус");
                        break;
                    case "3":
                        _fishes.Add(new Guppy());
                        Console.WriteLine($"в аквариум запустили рыбку гуппи");
                        break;
                }
            }
        }

        private void RemoveFish()
        {   
            Console.Write($"Введите номер рыбки, которую нужно отсадить: ");

            if(int.TryParse(Console.ReadLine(), out int number))
            {
                if (number >= 1 && number <= _fishes.Count)
                {
                    Console.WriteLine($"рыбку {_fishes[number-1].Name} отсадили");
                    _fishes.RemoveAt(number - 1);
                }
            }
            else
            {
                Console.WriteLine($"ошибка выбора номера рыбки");
            }
        }

        private void SkipWeek()
        {
            for (int i = 0; i < _fishes.Count; i++)
            {
                _fishes[i].SkipWeek();
            }
        }

        private void MeetDeath()
        {
            for (int i = _fishes.Count; i > 0; i--)
            {
                if (_fishes[i - 1].IsDead())
                {
                    Console.WriteLine($"{_fishes[i - 1].Name} не будет больше радовать Вас");
                    _fishes.RemoveAt(i - 1);
                }
            }
        }
    }

    abstract class Fish
    {
        protected string Sort;
        protected string[] AllSorts;
        protected int LifeTime;
        protected int minLifeSpan;
        protected int maxLifeSpan;
        protected int Age;

        private static Random _rand = new Random();

        public string Name { get; protected set; }


        public Fish(string name)
        {
            Name = name;
            Age = 0;
        }

        public void ViewFish()
        {
            Console.WriteLine($"{Name} {Sort} возраст {Age}/{LifeTime} недель");
        }

        public void SkipWeek()
        {
            Age++;
        }

        public bool IsDead()
        {
            return Age > LifeTime;
        }


        protected int GetRandomIndex(int maxIndex)
        {
            return _rand.Next(maxIndex);
        }

        protected int GetRandomLifeTime(int minLifeSpan, int maxLifeSpan)
        {
            return _rand.Next(minLifeSpan, maxLifeSpan + 1);
        }
    }

    class AngelFish : Fish
    {
        public AngelFish() : base("скалярия")
        {
            minLifeSpan = 5;
            maxLifeSpan = 10;

            LoadFishSorts();
            int index = GetRandomIndex(AllSorts.Length);
            Sort = AllSorts[index];

            LifeTime = GetRandomLifeTime(minLifeSpan, maxLifeSpan);
        }

        private void LoadFishSorts()
        {
            AllSorts = new string[]
            {
                "дымчато-вуалевая",
                "золотисто-перламутровая",
                "золотая",
                "мраморная",
                "шлейфовая",
                "черная",
                "Кои"
            };
        }
    }

    class Barbus : Fish
    {
        public Barbus() : base("барбус")
        {
            minLifeSpan = 6;
            maxLifeSpan = 12;

            LoadFishSorts();
            int index = GetRandomIndex(AllSorts.Length);
            Sort = AllSorts[index];

            LifeTime = GetRandomLifeTime(minLifeSpan, maxLifeSpan);
        }

        private void LoadFishSorts()
        {
            AllSorts = new string[]
            {
                "суматранский",
                "мшистый",
                "огненный",
                "вишневый",
                "флуоресцентный"
            };
        }
    }

    class Guppy : Fish
    {
        public Guppy():base("гуппи")
        {
            minLifeSpan = 8;
            maxLifeSpan = 16;

            LoadFishSorts();
            int index = GetRandomIndex(AllSorts.Length);
            Sort = AllSorts[index];

            LifeTime = GetRandomLifeTime(minLifeSpan, maxLifeSpan);
        }

        private void LoadFishSorts()
        {
            AllSorts = new string[]
            {
                "вуалевая",
                "ковровая",
                "ленточная",
                "сетчатая",
                "смарагдовая"
            };  
        }
    }
}
