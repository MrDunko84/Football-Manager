﻿using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace MonoGame.GameFramework.ScreenState
{

    /// <summary>
    ///     Describes the screen transition state
    /// </summary>
    public enum ScreenState
    {
        In,
        Active,
        Out,
        Hidden
    }

    public enum ScreenStateDirection
    {
        TransitionIn = -1,
        TransitionOut
    }

    /// <summary>
    ///     Base to all GameScreens with the game
    /// </summary>
    public abstract class GameScreenBase
    {

        private GestureType _enableGestures = GestureType.None;


        private bool _otherScreenHasFocus;

        /// <summary>
        ///     Determine if the this screen is an overlay screen,
        ///     this will leave current screen transitioned in
        /// </summary>
        public bool IsOverlay { get; set; } = false;

        /// <summary>
        ///     Indication how long the screen takes to
        ///     transition in when it's activated
        /// </summary>
        public TimeSpan TransitionInTime { get; set; } = TimeSpan.Zero;

        /// <summary>
        ///     Indications how long the screen takes to
        ///     transition out when it's activated
        /// </summary>
        public TimeSpan TransitionOutTime { get; set; } = TimeSpan.Zero;

        /// <summary>
        ///     Gets the current position of the screen transition,
        ///     from 0 (fully active, no transition) to 1 (transitioned fully out)
        /// </summary>
        public float TransitionPosition { get; set; } = 1.0f;

        /// <summary>
        ///     Gets the current alpha of the screen transition,
        ///     from 1 (fully active, no transition) to 0 (transitioned fully out)
        /// </summary>
        public float TransitionAlpha => 1.0f - TransitionPosition;

        /// <summary>
        ///     Gets the current screen transition state
        /// </summary>
        public ScreenState ScreenState { get; set; } = ScreenState.In;

        /// <summary>
        ///     Determine if the screen is exiting, transitioning out
        /// </summary>
        public bool IsExiting { get; protected internal set; }

        /// <summary>
        ///     Determine if the screen is current active
        /// </summary>
        public bool IsActive => !_otherScreenHasFocus &&
                                (ScreenState == ScreenState.In ||
                                 ScreenState == ScreenState.Active);

        /// <summary>
        ///     Gets the screen manager that this screen belongs to
        /// </summary>
        public ScreenManager ScreenManager { get; internal set; }

        public GestureType EnableGestures
        {
            get => _enableGestures;
            protected set
            {
                _enableGestures = value;
                if (ScreenState == ScreenState.Active) TouchPanel.EnabledGestures = value;
            }
        }

        /// <summary>
        ///     Determines whether the screen will be recorded into the screen manager's state
        /// </summary>
        public bool IsSerialisable { get; protected set; } = true;


        /// <summary>
        ///     Load graphics content for the screen
        /// </summary>
        public virtual void LoadContent() { }

        /// <summary>
        ///     Unload graphics content for the screen
        /// </summary>
        public virtual void UnloadContent() { }

        /// <summary>
        ///     Allows the screen to run logic, has transition logic built in
        /// </summary>
        public virtual void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            _otherScreenHasFocus = otherScreenHasFocus;

            if (IsExiting)
            {
                ScreenState = ScreenState.Out;
                if (!UpdateTransition(gameTime, TransitionOutTime, ScreenStateDirection.TransitionOut)) ScreenManager.RemoveScreen(this);

            }
            else if (coveredByOtherScreen)
            {
                ScreenState = UpdateTransition(gameTime, TransitionOutTime, ScreenStateDirection.TransitionOut) ? ScreenState.Out : ScreenState.Hidden;
            }
            else
            {
                ScreenState = UpdateTransition(gameTime, TransitionInTime, ScreenStateDirection.TransitionIn) ? ScreenState.In : ScreenState.Active;
            }

        }

        /// <summary>
        ///     Update the screen transition position
        /// </summary>
        private bool UpdateTransition(GameTime gameTime, TimeSpan time, ScreenStateDirection direction)
        {

            // Determine how much we should move by
            var transitionDelta = time == TimeSpan.Zero ?
                1.0f :
                (float) (gameTime.ElapsedGameTime.TotalMilliseconds /
                         time.TotalMilliseconds);

            // Update the transition position
            TransitionPosition += transitionDelta * (int) direction;

            // Determine if we are at the end of the transition
            if ((direction >= 0 || !(TransitionPosition <= 0)) &&
                (direction <= 0 || !(TransitionPosition >= 1))) return true;

            TransitionPosition = MathHelper.Clamp(TransitionPosition, 0f, 1f);

            return false;

        }

        /// <summary>
        ///     This is called when the screen should draw itself
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Draw(GameTime gameTime) { }

        /// <summary>
        ///     Tells the screen to serilise it's state into the passed in stream
        /// </summary>
        /// <param name="stream"></param>
        public virtual void Serialise(Stream stream) { }

        /// <summary>
        ///     Tells the screen to deserialise it's state into the passed in stream
        /// </summary>
        /// <param name="stream"></param>
        public virtual void Deserialise(Stream stream) { }


        /// <summary>
        ///     Tells the screen to exit
        /// </summary>
        public void ExitScreen()
        {
            if (TransitionOutTime == TimeSpan.Zero)
                ScreenManager.RemoveScreen(this);
            else
                IsExiting = true;
        }

        /// <summary>
        ///     Helper method to load assets from the screen managers content
        /// </summary>
        /// <typeparam name="T">Represents the type of asset</typeparam>
        /// <param name="assetName">Asset name, relative to the root directory</param>
        /// <returns></returns>
        public T Load<T>(string assetName) { return ScreenManager.Game.Content.Load<T>(assetName); }
    }

}
