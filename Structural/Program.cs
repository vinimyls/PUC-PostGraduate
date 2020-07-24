using System;

namespace Structural
{
    public abstract class Component
    {
        public abstract string Operation();
    }
    class Text : Component
    {
        public string text;
        public Text ()
        {
            Console.WriteLine("Digite o texto:");
             text = Console.ReadLine();  
        }
        public override string Operation()
        {
            return  text;
        }
    }
    abstract class Decorator : Component
    {
        protected Component _component;

        public Decorator(Component component)
        {
            this._component = component;
        }

        public void SetComponent(Component component)
        {
            this._component = component;
        }

        // The Decorator delegates all work to the wrapped component.
        public override string Operation()
        {
            if (this._component != null)
            {
                return this._component.Operation();
            }
            else
            {
                return string.Empty;
            }
        }
    }
    class AddBlond : Decorator
    {
        public AddBlond(Component comp) : base(comp)
        {
        }

        public override string Operation()
        {            
            Console.WriteLine("Adicionar negrito? 1-Sim 2-Não");
            if(Console.ReadLine() == "1")
            {
                return $"<b>{base.Operation()}</b>";
            }
            return base.Operation();
            
        }
    }
    class AddItalics : Decorator
    {
        public AddItalics(Component comp) : base(comp)
        {
        }

        public override string Operation()
        {
            Console.WriteLine("Adicionar italico? 1-Sim 2-Não");
            if(Console.ReadLine() == "1")
            {
                return $"<i>{base.Operation()}</i>";
            }
            return base.Operation();
        }
    }
     class AddClass : Decorator
    {
        private string nomeClasse;
        public AddClass(Component comp) : base(comp)
        {
        }

        public override string Operation()
        {
            Console.WriteLine("Digite o nome da classe:");
            nomeClasse = Console.ReadLine();
            return $"<span class={nomeClasse}>{base.Operation()}</span>"; 

        }
    }
    public class Client
    {
        public void ClientCode(Component component)
        {
            Console.WriteLine("saida: " + component.Operation());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();

            var simple = new Text();
            
            AddClass classe = new AddClass(simple);
            AddBlond negrito = new AddBlond(classe);
            AddItalics italico = new AddItalics(negrito);
            client.ClientCode(italico);
        }
    }
}