using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.GameFramework.GameObjects;
using MonoGame.GameFramework.ScreenState;

namespace FM.Core.Match
{

    public class Player
        : DrawableObjectBase,
          IMoveableObject
    {

        private readonly Pitch _pitch;
        private readonly GameScreenBase _screen;
        private readonly SpriteBatch _spriteBatch;
        private readonly Team _team;

       

        private SpriteFont _spriteFont;

        /// <inheritdoc />
        public Player(Game game,
                      SpriteBatch spriteBatch,
                      GameScreenBase screen,
                      Pitch pitch,
                      Team team)
            : base(game)
        {
            _spriteBatch = spriteBatch;
            _screen = screen;
            _pitch = pitch;
            _team = team;
        }

        public string PlayerNumber { get; set; }
        public Vector2 Location { get; private set; }

        /// <inheritdoc />
        public Vector2 Destination { get; private set;}

        /// <inheritdoc />
        public float Speed { get; private set; } = 50.0f;

        /// <inheritdoc />
        public void SetLocation(Vector2 location) { Location = location; }

        /// <inheritdoc />
        public void SetDestination(Vector2 destination) { Destination = destination; }

        /// <inheritdoc />
        public void SetSpeed(float speed) { Speed = speed; }

        /// <inheritdoc />
        public override void Initialize()
        {
            _spriteFont = _screen.Load<SpriteFont>("playernumber");

            var random = _pitch.Rnd;
            var bounds = _pitch.Bounds;

            SetLocation(new Vector2(random.Next(bounds.Left, bounds.Right), random.Next(bounds.Top, bounds.Bottom)));
            SetDestination( new Vector2(random.Next(bounds.Left, bounds.Right), random.Next(bounds.Top, bounds.Bottom)));

        }


        /// <inheritdoc />
        public override void Update(GameTime gameTime)
        {

            var direction = -(Location - Destination);
            direction.Normalize();

            Location = Location + (direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (Math.Abs(Location.X - Destination.X) < 1 && Math.Abs(Location.Y - Destination.Y) < 1)
            {
                var random = _pitch.Rnd;
                var bounds = _pitch.Bounds;

                Destination = new Vector2(random.Next(bounds.Left, bounds.Right), random.Next(bounds.Top, bounds.Bottom));
            };

        }

        /// <inheritdoc />
        public override void Draw(GameTime gameTime) { _spriteBatch.DrawString(_spriteFont, PlayerNumber, Location, _team.TeamColor * _screen.TransitionAlpha); }
    }

}
