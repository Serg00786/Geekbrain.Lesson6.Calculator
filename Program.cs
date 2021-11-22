using Autofac;
using Autofac.Core;
using System;
using System.Reflection;
using test;
namespace test
{
    
   public class Program
    {
        
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Введите первое число");
                string FirstValue = Console.ReadLine();
                Console.WriteLine("Введите знак операции");
                string Sign = Console.ReadLine();
                Console.WriteLine("Введите второе число");
                string SecondValue = Console.ReadLine();

                UserData user = new UserData(FirstValue, Sign, SecondValue);

                var builder = new ContainerBuilder();
                builder.RegisterType<ContainerCreator>().OnActivating(e => e.Instance.CreateContainer(user));
                IContainer container = builder.Build();
                container.Resolve<ContainerCreator>();
                Console.ReadKey();
            }

        }
    }
    interface IOperation
    {
        void Operation(double a, double b);
    }
    class Sum : IOperation
    {
        public void Operation(double a, double b)
        {
            Console.WriteLine(a + b);
        }
    }

    class Minus : IOperation
    {
        public void Operation(double a, double b)
        {
            Console.WriteLine(a - b);
        }
    }

    class Multiply : IOperation
    {
        public void Operation(double a, double b)
        {
            Console.WriteLine(a * b);
        }
    }

    class Divide : IOperation
    {
        public void Operation(double a, double b)
        {
            if (b == 0) throw new DivideByZeroException();
            Console.WriteLine(a / b);
        }
    }

    class BusinessLogic
    {
        public void DoOperation(IOperation Operations, UserData userData)
        {
            Operations.Operation(userData.FirstValue, userData.SecondValue);
        }
    }
    interface IOtherOperation
    {
        void Operation(double a, double b);
    }
    class Interpolation : IOtherOperation
    {
        public void Operation( double a, double b)
        {
            Console.WriteLine(Math.Pow(a,b));
        }
    }
    class InterpolationAdapter : IOperation
    {
        Interpolation interpolation;
        public InterpolationAdapter(Interpolation inter)
        {
            interpolation = inter;
        }

        public void Operation(double a, double b)
        {
            interpolation.Operation(a,b);
        }
    }
    public class ContainerCreator
    {
        public interface IContainerCreator
        {
            void CreateContainer(UserData user);
        }
        private static IContainer Container { get; set; }
        public void CreateContainer(UserData user)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Sum>().OnActivating(e => e.Instance.Operation(user.FirstValue, user.SecondValue));
            builder.RegisterType<Minus>().OnActivating(e => e.Instance.Operation(user.FirstValue, user.SecondValue));
            builder.RegisterType<Multiply>().OnActivating(e => e.Instance.Operation(user.FirstValue, user.SecondValue));
            builder.RegisterType<Divide>().OnActivating(e => e.Instance.Operation(user.FirstValue, user.SecondValue));
            builder.RegisterType<Interpolation>().OnActivating(e => e.Instance.Operation(user.FirstValue, user.SecondValue));
            Container = builder.Build();

            switch (user.OperationSign)
            {
                case "+":
                    Container.Resolve<Sum>();
                    break;
                case "-":
                    Container.Resolve<Minus>();
                    break;
                case "*":
                    Container.Resolve<Multiply>();
                    break;
                case "/":
                    Container.Resolve<Divide>();
                    break;
                case ">>":
                    Container.Resolve<Interpolation>();
                    break;
            }

        }
    }
}
