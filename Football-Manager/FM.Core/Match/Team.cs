using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.GameFramework.GameObjects;
using MonoGame.GameFramework.ScreenState;

namespace FM.Core.Match
{

    public class Team
        : DrawableObjectBase
    {
        private readonly Pitch _pitch;
        private readonly GameScreenBase _screen;

        private readonly SpriteBatch _spriteBatch;

        /// <inheritdoc />
        public Team(Game game,
                    SpriteBatch spriteBatch,
                    GameScreenBase screen,
                    Pitch pitch,
                    Color color)
            : base(game)
        {
            Players = new List<Player>();

            _spriteBatch = spriteBatch;
            _screen = screen;
            _pitch = pitch;
            TeamColor = color;
        }

        public List<Player> Players { get; }
        public Color TeamColor { get; }

        public void AddPlayer(string playerNumber) { Players.Add(new Player(Game, _spriteBatch, _screen, _pitch, this) {PlayerNumber = playerNumber}); }

        /// <inheritdoc />
        public override void Initialize() { Players?.ForEach(x => x.Initialize()); }

        /// <inheritdoc />
        public override void Update(GameTime gameTime) { Players?.ForEach(x => x.Update(gameTime)); }

        /// <inheritdoc />
        public override void Draw(GameTime gameTime) { Players?.ForEach(x => x.Draw(gameTime)); }
    }

}
