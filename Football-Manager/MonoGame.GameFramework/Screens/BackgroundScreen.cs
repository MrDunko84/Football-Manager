using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.GameFramework.ScreenState;

namespace MonoGame.GameFramework.Screens
{

    public class BackgroundScreen
        : GameScreenBase
    {

        private Texture2D _backgroundTexture;

        public BackgroundScreen()
        {
            TransitionInTime = TimeSpan.FromSeconds(0.0);
            TransitionOutTime = TimeSpan.FromSeconds(0.5);
        }

        /// <inheritdoc />
        public override void LoadContent()
        {
            _backgroundTexture = new Texture2D(ScreenManager.GraphicsDevice, 1, 1);
            _backgroundTexture.SetData(new[] {Color.Black});
        }

        /// <inheritdoc />
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen) { base.Update(gameTime, otherScreenHasFocus, false); }

        /// <inheritdoc />
        public override void Draw(GameTime gameTime)
        {

            var spriteBatch = ScreenManager.SpriteBatch;
            var viewport = ScreenManager.GraphicsDevice.Viewport;

            spriteBatch.Begin();

            spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, viewport.Width, viewport.Height), new Color(Color.Black, TransitionAlpha));

            spriteBatch.End();

        }
    }

}
