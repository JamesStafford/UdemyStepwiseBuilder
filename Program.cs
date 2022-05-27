using System;

namespace StepWiseBuilder
{
    public enum CarType
    {
        Sedan,
        Crossover
    }
    
    public class Car
    {
        public CarType Type;
        public int WheelSize;
    }

    public interface ISpecifyCarType
    {
        ISpecifyWheelSize OfType(CarType type);
    }

    public interface ISpecifyWheelSize
    {
        IBuildCar WithWheel(int size);
    }

    public interface IBuildCar
    {
        public Car Build();
    }

    public class CarBuilder
    {
        private class StepWiseCarBuilder : ISpecifyCarType, ISpecifyWheelSize, IBuildCar
        {
            private readonly Car _car = new Car();
            
            public ISpecifyWheelSize OfType(CarType type)
            {
                _car.Type = type;
                return this;
            }

            public IBuildCar WithWheel(int size)
            {
                if (_car.Type == CarType.Crossover && size < 17 || size > 20)
                {
                    throw new ArgumentException($"Wrong size of wheel for {_car.Type}");
                }

                if (_car.Type == CarType.Sedan && size < 15 || size > 17)
                {
                    throw new ArgumentException($"Wrong size of wheel for {_car.Type}");
                }

                _car.WheelSize = size;
                return this;
            }

            public Car Build()
            {
                return _car;
            }
        }
        
        public static ISpecifyCarType Create()
        {
            return new StepWiseCarBuilder();
        }
    }
    
    class Program
    {
        static void Main()
        {
            var car = CarBuilder
                .Create()
                .OfType(CarType.Sedan)
                .WithWheel(16)
                .Build();
        }
    }
}