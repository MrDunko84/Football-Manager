using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FM.Core.Match
{
    public interface IMoveableObject
    {
        Vector2 Location { get; }
        Vector2 Destination { get; }
        float Speed { get; }

        void SetLocation(Vector2 location);
        void SetDestination(Vector2 destination);
        void SetSpeed(float speed);
    }
}
