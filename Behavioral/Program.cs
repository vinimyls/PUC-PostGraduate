// entendi a ideia de criar um command para cada comando enviado
// mas não consigo entender como criar comandos diferentes nesta linguagem
// a menos que utilizace uma factory de comandos para gerar tipos de comandos tiferentes
// acabei criando um acoplamento gigantesco entre cas classes de cada tipo de comando e a classe "Reciver"
// não acho que este seja o melhor caminho

using System;

namespace behavioral
{
    public interface ICommand
    {
        void Execute();
    }
    class CommandSearch : ICommand
    {
        private Receiver _command;
        private string _keywords;
        private string _destinetion;
        public CommandSearch(Receiver command, string keywords, string destinetion)
        {
            this._command = command;
            this._keywords = keywords;
            this._destinetion = destinetion;
        }
        public void Execute()
        {
            this._command.DoSomething("Command Search",this._keywords,this._destinetion);
        }   
    }
    class CommandUpload : ICommand
    {
        private Receiver _command;
        private string _filename;
        private byte[] _content;
        public CommandUpload(Receiver command, string filename, byte[] content)
        {
            this._command = command;
            this._filename = filename;
            this._content = content;
        }
        public void Execute()
        {
            this._command.DoSomething("Command Upload",this._filename, this._content);
        }   
    }
    class CommandExecute : ICommand
    {
        private Receiver _command;
        private string _script;
        public CommandExecute(Receiver command, string script)
        {
            this._command = command;
            this._script = script;
        }
        public void Execute()
        {
            this._command.DoSomething("Command Execute",this._script);
        }   
    }
    class CommandNeighbors : ICommand
    {
        private Receiver _command;
        private int _depth;
        private string _destinetion;
        public CommandNeighbors(Receiver command, int depth, string destinetion)
        {
            this._command = command;
            this._depth = depth;
            this._destinetion = destinetion;
        }
        public void Execute()
        {
            this._command.DoSomething("Command Neigbors",this._depth,this._destinetion);
            //--- Reenvia dados ---
            _depth--;
            if(_depth>0)
            {
                Console.WriteLine("neighbors"+ " , " +  _depth + " , "+ _destinetion );
            }
        }   
    }
    

    class Receiver
    {
        public void DoSomething(string command,string a, string b)
        {
            Console.WriteLine(command +" "+ a +" "+ b);
        }
        public void DoSomething(string command, string a, byte[] b)
        {
            Console.WriteLine(command +" "+ a +" "+ b);
        }
        public void DoSomething(string command, string a)
        {
            Console.WriteLine(command +" "+ a);
        }
        public void DoSomething(string command, int a, string b)
        {
            Console.WriteLine(command +" "+ a +" "+ b);
        }
    }
    class Invoker
    {
        private ICommand _onStart;
        public void SetOnStart(ICommand command)
        {
            this._onStart = command;
        }
        public void DoSomethingImportant()
        {
            if (this._onStart is ICommand)
            {
                this._onStart.Execute();
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Invoker invoker = new Invoker();

            Receiver search = new Receiver();
            invoker.SetOnStart(new CommandSearch(search, "music mp3", "100.22.11.25:8888"));
            invoker.DoSomethingImportant();

            Receiver upload = new Receiver();
            byte[] bytes = { 0, 0, 0, 25 };
            invoker.SetOnStart(new CommandUpload(upload, "music.mp3",bytes));
            invoker.DoSomethingImportant();

            Receiver execute = new Receiver();
            invoker.SetOnStart(new CommandExecute(execute, "music.sh"));
            invoker.DoSomethingImportant();

            Receiver neighbors = new Receiver();
            invoker.SetOnStart(new CommandNeighbors(neighbors, 2, "90.12.50.21:8975"));
            invoker.DoSomethingImportant();
        }
    }
}       