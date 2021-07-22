using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using TShockAPI;

namespace CSConnection
{
    public delegate void ReaderDelegate(ReaderArgs args);

    public class ReaderModule
    {
        public string Name { get; private set; }
        public ReaderDelegate Delegate { get; private set; }

        public ReaderModule(string name, ReaderDelegate deg)
        {
            Name = name;
            Delegate = deg;
        }

        public void Run(ReaderArgs args)
        {
            Delegate(args);
        }
    }

    public class ReaderArgs
    {
        public BinaryReader Reader;
        public int Index;
        private string _key;

        public string Key
        {
            get => _key;
        }

        public ReaderArgs(string key, BinaryReader read, int ind)
        {
            Reader = read;
            Index = ind;
            _key = key;
        }

        public TSPlayer Player
        {
            get => TShock.Players[Index];
        }
        public Player TPlayer
        {
            get => Main.player[Index];
        }
    }
}
