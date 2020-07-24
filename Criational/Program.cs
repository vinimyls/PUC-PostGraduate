// usei o exemplo de pool em c# consegui criar um pool de conexões,
// mas não sei como chamar os objetos separados


using System;
using System.Collections.Concurrent;

namespace Criatinal
{
    public class ObjectPool<T>
    {
        private readonly ConcurrentBag<T> _objects;
        private readonly Func<T> _objectGenerator;

        public ObjectPool(Func<T> objectGenerator)
        {
            _objectGenerator = objectGenerator ?? throw new ArgumentNullException(nameof(objectGenerator));
            _objects = new ConcurrentBag<T>();
        }

        public T Get() => _objects.TryTake(out T item) ? item : _objectGenerator();

        public void Return(T item) => _objects.Add(item);
    }

    class Program
    {
        static void Main(string[] args)
        {
 
            var pool = new ObjectPool<Connection>(() => new Connection());

                var example = pool.Get();
                try
                {
                    Console.CursorLeft = 0;
                    Console.WriteLine("Host: "+$"{example.GetHost()}");
                    Console.WriteLine("Port: "+$"{example.GetPort()}");
                    Console.WriteLine("User: "+$"{example.GetUser()}");
                    Console.WriteLine("Password: "+$"{example.GetPassword()}");
                    Console.WriteLine("Data Base: "+$"{example.GetDbName()}");
                }
                finally
                {
                    pool.Return(example);
                }
        }
        

    }

    class Connection
    {
         private string host;
         private string port;
         private string user;
         private string password;
         private string dbName;
        public Connection()
        {
            Console.WriteLine("Host: ");
            host = Console.ReadLine();
            Console.WriteLine("Port: ");
            port = Console.ReadLine();
            Console.WriteLine("User: ");
            user = Console.ReadLine();
            Console.WriteLine("Password: ");
            password = Console.ReadLine();
            Console.WriteLine("Data Base: ");
            dbName =Console.ReadLine();
        }
        public string GetHost()
        {
               return host;
        }
        public string GetPort()
        {
               return port;
        }
        public string GetUser()
        {
               return user;
        }
        public string GetPassword()
        {
               return password;
        }
        public string GetDbName()
        {
               return dbName;
        }
    }
}