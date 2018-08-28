using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.GameFramework.GameObjects
{

    /// <summary>
    ///     Forms the base for all drawable objects
    /// </summary>
    public abstract class DrawableObjectBase
        : DrawableGameComponent
    {

        /// <inheritdoc />
        protected DrawableObjectBase(Game game)
            : base(game)
        {
        }

        /// <summary>
        ///     A string that can be used to Tag the object for identification
        /// </summary>
        public string Tag { get; set; }


        /// <inheritdoc />
        public abstract override void Draw(GameTime gameTime);

        /// <summary>
        ///     Helper method to draw the outline of a rectangle
        /// </summary>
        public void DrawBorder(SpriteBatch spritebatch, Rectangle rectangle, Color color, int borderWidth)
        {

            var texture = new Texture2D(Game.GraphicsDevice, 1, 1);
            texture.SetData(new[] {color});

            spritebatch.Draw(texture, new Rectangle(rectangle.Left, rectangle.Top, borderWidth, rectangle.Height), color);
            spritebatch.Draw(texture, new Rectangle(rectangle.Right, rectangle.Top, borderWidth, rectangle.Height), color);
            spritebatch.Draw(texture, new Rectangle(rectangle.Left + borderWidth, rectangle.Top, rectangle.Width - borderWidth, borderWidth), color);
            spritebatch.Draw(texture, new Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width + borderWidth, borderWidth), color);

        }
    }

}
