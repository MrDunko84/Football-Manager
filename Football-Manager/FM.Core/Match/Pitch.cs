using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.GameFramework.GameObjects;
using MonoGame.GameFramework.ScreenState;

namespace FM.Core.Match
{
    public class Pitch
        : DrawableObjectBase
    {

        private Rectangle _bounds;
        private readonly SpriteBatch _spriteBatch;
        private readonly GameScreenBase _screen;

        /// <inheritdoc />
        public Pitch(Game game, 
                     SpriteBatch spriteBatch,
                     GameScreenBase screen)
            : base(game)
        {
            _spriteBatch = spriteBatch;
            _screen = screen;
        }


        /// <inheritdoc />
        public override void Initialize()
        {
            _bounds = new Rectangle(0, 0, 80, 120);
        }

        /// <inheritdoc />
        public override void Draw(GameTime gameTime)
        {
            DrawBorder(_spriteBatch, _bounds, Color.White * _screen.TransitionAlpha, 2);
        }
    }
}
