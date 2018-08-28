using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.GameFramework.GameObjects;
using MonoGame.GameFramework.ScreenState;

namespace FM.Core.Match
{

    public class Player
        : DrawableObjectBase
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
        public override void Initialize()
        {
            _spriteFont = _screen.Load<SpriteFont>("playernumber");

            var random = _pitch.Rnd;
            var bounds = _pitch.Bounds;

            Location = new Vector2(random.Next(bounds.Left, bounds.Right), random.Next(bounds.Top, bounds.Bottom));

        }


        /// <inheritdoc />
        public override void Update(GameTime gameTime) { base.Update(gameTime); }

        /// <inheritdoc />
        public override void Draw(GameTime gameTime) { _spriteBatch.DrawString(_spriteFont, PlayerNumber, Location, _team.TeamColor * _screen.TransitionAlpha); }
    }

}
