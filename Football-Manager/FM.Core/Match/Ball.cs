using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame.GameFramework.GameObjects;

namespace FM.Core.Match
{
    public class Ball
        : DrawableObjectBase,
          IMoveableObject
    {
        /// <inheritdoc />
        public Ball(Game game)
            : base(game)
        {
        }

        /// <inheritdoc />
        public Vector2 Location { get; }

        /// <inheritdoc />
        public Vector2 Destination { get; }

        /// <inheritdoc />
        public float Speed { get; }

        /// <inheritdoc />
        public void SetLocation(Vector2 location) { throw new NotImplementedException(); }

        /// <inheritdoc />
        public void SetDestination(Vector2 destination) { throw new NotImplementedException(); }

        /// <inheritdoc />
        public void SetSpeed(float speed) { throw new NotImplementedException(); }


        /// <inheritdoc />
        public override void Draw(GameTime gameTime) { throw new NotImplementedException(); }


    }
}
