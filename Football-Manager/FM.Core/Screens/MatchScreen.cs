using System;
using FM.Core.Match;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.GameFramework.ScreenState;

namespace FM.Core.Screens
{
    public class MatchScreen
        : GameScreenBase
    {

        private Texture2D _backgroundTexture;
        private Pitch _pitch;

        public MatchScreen()
        {
            TransitionInTime = TimeSpan.FromSeconds(10.0);
            TransitionOutTime = TimeSpan.FromSeconds(10.0);
        }

        /// <inheritdoc />
        public override void LoadContent()
        {

            _backgroundTexture = new Texture2D(ScreenManager.GraphicsDevice, 1, 1);
            _backgroundTexture.SetData(new[] {Color.DarkGreen});

            _pitch = new Pitch(ScreenManager.Game, ScreenManager.SpriteBatch, this);
            _pitch.Initialize();
        }

        /// <inheritdoc />
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {

            _pitch.Update(gameTime);

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        /// <inheritdoc />
        public override void Draw(GameTime gameTime)
        {

            var spriteBatch = ScreenManager.SpriteBatch;
            var viewport = ScreenManager.GraphicsDevice.Viewport;

            spriteBatch.Begin();
            
            spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, viewport.Width, viewport.Height), Color.DarkGreen * TransitionAlpha);
            _pitch.Draw(gameTime);

            spriteBatch.End();

        }

    }
}
