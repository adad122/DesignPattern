using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgePatternApply
{
    public interface IEmitter
    {
        void Emit();
    }

    public class GatlingGun : IEmitter
    {
        public void Emit()
        {
            Console.WriteLine("GatlingGun:Fire");
        }
    }

    public class Sidewinder : IEmitter
    {
        public void Emit()
        {
            Console.WriteLine("Sidewinder:Fire");
        }
    }

    public class DfMissile : IEmitter
    {
        public void Emit()
        {
            Console.WriteLine("DfMissile:Fire");
        }
    }
    public abstract class Plane
    {
        protected IEmitter Emitter;
        public virtual void Shoot()
        {
            if (Emitter != null)
            {
                Emitter.Emit();
            }
        }

        public void SetEmitter(IEmitter emitter)
        {
            Emitter = emitter;
        }
    }

    public class F4FWildCat : Plane
    {
        public override void Shoot()
        {
            base.Shoot();
            Console.WriteLine("F4FWildCat:Shoot");
        }
    }

    public class Bf109 : Plane
    {
        public override void Shoot()
        {
            base.Shoot();
            Console.WriteLine("Bf109:Shoot");
        }
    }

    public class A6MZero : Plane
    {
        public override void Shoot()
        {
            base.Shoot();
            Console.WriteLine("A6MZero:Shoot");
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Plane f4FPlane = new F4FWildCat();
            f4FPlane.SetEmitter(new GatlingGun());
            f4FPlane.Shoot();
            f4FPlane.SetEmitter(new Sidewinder());
            f4FPlane.Shoot();

            Plane zeroPlane = new A6MZero();
            zeroPlane.SetEmitter(new GatlingGun());
            zeroPlane.Shoot();

            Plane bf109Plane = new Bf109();
            bf109Plane.SetEmitter(new DfMissile());
            bf109Plane.Shoot();
        }
    }
}
