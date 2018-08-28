﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using MonoGame.GameFramework.GameObjects;

namespace MonoGame.GameFramework.ScreenState
{

    public class ScreenManager
        : DrawableObjectBase
    {

        private readonly List<GameScreenBase> _screens = new List<GameScreenBase>();
        private readonly List<GameScreenBase> _screensToUpdate = new List<GameScreenBase>();

        private Texture2D _blankTexture;

        private bool _isInitialised;


        /// <inheritdoc />
        /// <summary>
        ///     Constructs a new screen manager component
        /// </summary>
        public ScreenManager(Game game)
            : base(game)
        {
            TouchPanel.EnabledGestures = GestureType.None;
        }

        public SpriteBatch SpriteBatch { get; private set; }

        public bool TraceEnabled { get; set; } = false;


        /// <inheritdoc />
        /// <summary>
        ///     Initialise the screen manager
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            _isInitialised = true;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Loads the graphics content
        /// </summary>
        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            _screens?.ForEach(x => x.LoadContent());
        }

        /// <inheritdoc />
        /// <summary>
        ///     Unload the graphics content
        /// </summary>
        protected override void UnloadContent() { _screens?.ForEach(x => x.UnloadContent()); }

        /// <inheritdoc />
        /// <summary>
        ///     Allows each screen to run logic
        /// </summary>
        public override void Update(GameTime gameTime)
        {

            _screensToUpdate.Clear();
            _screensToUpdate.AddRange(_screens);

            var otherScreenHasFocus = !Game.IsActive;
            var coveredByOtherScreen = false;

            while (_screensToUpdate.Any())
            {
                var topMostScreenIndex = _screensToUpdate.Count - 1;
                var screen = _screensToUpdate[topMostScreenIndex];

                _screensToUpdate.RemoveAt(topMostScreenIndex);

                // Update the screen
                screen.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

                if (screen.ScreenState != ScreenState.In &&
                    screen.ScreenState != ScreenState.Active) continue;

                if (!otherScreenHasFocus) otherScreenHasFocus = true;

                if (!screen.IsOverlay) coveredByOtherScreen = true;

            }

            // Trace screens
            if (TraceEnabled) TraceScreens();


        }

        /// <summary>
        ///     Prints out a list of all the screen, for debugging
        /// </summary>
        private void TraceScreens() { Console.WriteLine(string.Join(", ", _screens.Select(x => x.GetType().Name))); }

        /// <inheritdoc />
        /// <summary>
        ///     Tells each screen to draw itself
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            _screens.ForEach(x =>
            {
                if (x.ScreenState == ScreenState.Hidden) return;
                x.Draw(gameTime);
            });
        }

        /// <summary>
        ///     Adds a new screen to the screen manager
        /// </summary>
        public void AddScreen(GameScreenBase screen)
        {
            screen.ScreenManager = this;
            screen.IsExiting = false;

            if (_isInitialised) screen.LoadContent();

            _screens.Add(screen);

            TouchPanel.EnabledGestures = screen.EnableGestures;

        }

        /// <summary>
        ///     Removes a screen from the screen manager
        /// </summary>
        public void RemoveScreen(GameScreenBase screen)
        {
            if (_isInitialised) screen.UnloadContent();

            _screens.Remove(screen);
            _screensToUpdate.Remove(screen);

            if (_screens.Any()) TouchPanel.EnabledGestures = _screens.Last().EnableGestures;
        }

        public GameScreenBase[] GetScreens() { return _screens.ToArray(); }
    }

}
