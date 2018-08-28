using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.GameFramework.GameObjects;
using MonoGame.GameFramework.ScreenState;

namespace FM.Core.Match
{

    public class Pitch
        : DrawableObjectBase
    {
        private readonly GameScreenBase _screen;
        private readonly SpriteBatch _spriteBatch;


        private Team _awayTeam;
        private Team _homeTeam;

        
        /// <inheritdoc />
        public Pitch(Game game,
                     SpriteBatch spriteBatch,
                     GameScreenBase screen)
            : base(game)
        {
            _spriteBatch = spriteBatch;
            _screen = screen;

            Rnd = new Random((int) DateTime.Now.Ticks);
            Scale = 3;
        }

        public Rectangle Bounds { get; private set; }

        public Random Rnd { get; }

        public int Scale { get; }

        /// <inheritdoc />
        public override void Initialize()
        {
            Bounds = new Rectangle(300, 50, 80 * Scale, 120 * Scale);

            _homeTeam = new Team(Game, _spriteBatch, _screen, this, Color.Red);
            _awayTeam = new Team(Game, _spriteBatch, _screen, this, Color.White);


            _homeTeam.AddPlayer("2");
            _homeTeam.AddPlayer("3");
            _homeTeam.AddPlayer("4");
            _homeTeam.AddPlayer("5");

            _homeTeam.AddPlayer("6");
            _homeTeam.AddPlayer("7");
            _homeTeam.AddPlayer("8");
            _homeTeam.AddPlayer("9");

            _homeTeam.AddPlayer("10");
            _homeTeam.AddPlayer("11");


            _awayTeam.AddPlayer("2");
            _awayTeam.AddPlayer("3");
            _awayTeam.AddPlayer("4");
            _awayTeam.AddPlayer("5");

            _awayTeam.AddPlayer("6");
            _awayTeam.AddPlayer("7");
            _awayTeam.AddPlayer("8");
            _awayTeam.AddPlayer("9");

            _awayTeam.AddPlayer("10");
            _awayTeam.AddPlayer("11");

            _homeTeam.Initialize();
            _awayTeam.Initialize();

        }


        /// <inheritdoc />
        public override void Update(GameTime gameTime)
        {
            _homeTeam.Update(gameTime);
            _awayTeam.Update(gameTime);
        }


        /// <inheritdoc />
        public override void Draw(GameTime gameTime)
        {
            DrawBorder(_spriteBatch, Bounds, Color.White * _screen.TransitionAlpha, 2);

            _homeTeam.Draw(gameTime);
            _awayTeam.Draw(gameTime);
        }
    }

}
