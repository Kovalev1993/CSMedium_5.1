using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _12_Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            var unitBuilder = new UnitBuilder().WithMagicalArmor().Flying().Create();
        }
    }

    class UnitBuilder
    {
        public GameObject CreatingObject { get; private set; }

        public UnitBuilder()
        {
            CreatingObject = new GameObject();
        }

        public ArmoredUnitBuilder WithPhysicalArmor()
        {
            CreatingObject.Components.Add(new Health(new PhysicalArmor()));
            return new ArmoredUnitBuilder(CreatingObject);
        }

        public ArmoredUnitBuilder WithMagicalArmor()
        {
            CreatingObject.Components.Add(new Health(new MagicArmor()));
            return new ArmoredUnitBuilder(CreatingObject);
        }

        public virtual GameObject Create()
        {
            return CreatingObject;
        }
    }

    class ArmoredUnitBuilder
    {
        public GameObject CreatingObject { get; private set; }

        public ArmoredUnitBuilder(GameObject creatingObject)
        {
            CreatingObject = CreatingObject;
        }

        public FinalUnitBuilder MovingOnPath()
        {
            CreatingObject.Components.Add(new PathMovement());
            return new FinalUnitBuilder(CreatingObject);
        }

        public FinalUnitBuilder Flying()
        {
            CreatingObject.Components.Add(new FlyMovement());
            return new FinalUnitBuilder(CreatingObject);
        }

        public GameObject Create()
        {
            return CreatingObject;
        }
    }
    
    class FinalUnitBuilder
    {
        public GameObject CreatingObject { get; private set; }
        public IArmor Armor { get; private set; }
        public Movement Movement { get; private set; }

        public FinalUnitBuilder(GameObject creatingObject)
        {
            CreatingObject = creatingObject;
        }

        public GameObject Create()
        {
            return CreatingObject;
        }
    }

    class GameObject
    {
        public List<IComponent> Components;
    }

    interface IComponent
    {
        void Start();
        void Update();
    }

    class Health : IComponent
    {
        public IArmor Armor { get; private set; }

        public Health(IArmor armor)
        {
            Armor = armor;
        }

        public void Start() { }

        public void Update() { }
    }

    interface IArmor
    {
        int ProcessDamage(int damage);
    }

    class MagicArmor : IArmor
    {
        public int ProcessDamage(int damage)
        {
            return (int)(damage / 3);
        }
    }

    class PhysicalArmor : IArmor
    {
        public int ProcessDamage(int damage)
        {
            return (int)(damage / 2);
        }
    }

    abstract class Movement : IComponent
    {
        public void Start() { }

        public void Update() { }

        public abstract void Move();
    }

    class PathMovement : Movement
    {
        public override void Move() { }
    }

    class FlyMovement : Movement
    {
        public override void Move() { }
    }
}