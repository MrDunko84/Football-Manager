using Microsoft.Xna.Framework;

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
        ///     The number of calls that have been made to the Update method
        /// </summary>
        public int UpdateCount { get; private set; }

        /// <summary>
        ///     A string that can be used to Tag the object for identification
        /// </summary>
        public string Tag { get; set; }


        /// <inheritdoc />
        public abstract override void Draw(GameTime gameTime);

        /// <inheritdoc />
        public override void Update(GameTime gameTime)
        {
            UpdateCount+= 1;
            base.Update(gameTime);
        }
    }
}
