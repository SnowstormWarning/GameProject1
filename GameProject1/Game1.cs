using GameProject1.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

namespace GameProject1
{
    public enum GameState
    {
        BallHittable,
        Aiming,
        BallMoving
    }
    public class Game1 : Game
    {
        private static GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private StaticSprite _board;
        private MouseState _priorMouse;
        public static MouseState CurrentMouse;
        public static GameState State = GameState.BallHittable;
        public static Vector2 MousePosVector => new Vector2(CurrentMouse.X, CurrentMouse.Y);
        public static SpriteFont Font;
        public static float Friction = 1.5f;
        public static float ForceMod = 0.25f;
        public static float arrowForceMod = 4f;
        public string DisplayDebug = "";
        public static float Width => _graphics.GraphicsDevice.Viewport.Width;
        public static float Height => _graphics.GraphicsDevice.Viewport.Height;

        public bool MadeGoal = false;

        Goal goal;

        public List<ArrowField> Arrows = new List<ArrowField>();

        private bool _ballIsHittable = true;
        private bool _charging = false;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Window.Title = "Short-Course";
            _graphics.ApplyChanges();
            _board = new StaticSprite("BlankBoard");
            
            base.Initialize(); // Width = 800 Height = 480
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _board.LoadContent(Content);
            Ball.LoadContent(Content);
            Ring.LoadContent(Content);
            Font = Content.Load<SpriteFont>("arial");
            for(int row = 0; row*60 < _graphics.GraphicsDevice.Viewport.Width; row++)
            {
                for (int col = 0; col * 60 < _graphics.GraphicsDevice.Viewport.Height; col++)
                {
                    if(!(row == 0 && col == 0))
                    {
                        Random random = new Random();
                        int num = random.Next(1, 6);
                        switch (num)
                        {
                            case 1:
                                Arrows.Add(new ArrowField(new Vector2(60 * row, 60 * col), Directions.Up));
                                break;
                            case 2:
                                Arrows.Add(new ArrowField(new Vector2(60 * row, 60 * col), Directions.Down));
                                break;
                            case 5:
                                if (!MadeGoal)
                                {
                                    MadeGoal = true;
                                    goal = new Goal(new Vector2(60 * row + 15, 60 * col + 15));
                                }
                                break;
                        }
                    }
                }
                if (!MadeGoal)
                {
                    goal = new Goal(new Vector2(_graphics.GraphicsDevice.Viewport.Width - 32, _graphics.GraphicsDevice.Viewport.Height - 32));
                }
                goal.LoadContent(Content);
            }
            foreach (ArrowField arrow in Arrows)
            {
                arrow.Sprite.LoadContent(Content);
            }
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            CurrentMouse = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                Exit();
            switch (State) 
            {
                case GameState.BallHittable:
                    if (CurrentMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed &&
                        _priorMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
                    {
                        State = GameState.Aiming;
                    }
                    break;
                    case GameState.Aiming:
                    if (CurrentMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released &&
                        _priorMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    {
                        State = GameState.BallMoving;

                        Vector2 diff = PosTool.VectorSubtraction(Ball.Center, MousePosVector);
                        diff.Normalize();
                        diff *= Ring.Radius;
                        Ball.Velocity += ForceMod * diff;
                        Ball.Update(gameTime);
                    }
                    break;
                case GameState.BallMoving:
                    foreach (ArrowField arrow in Arrows)
                    {
                        if (arrow.IsCollision())
                        {
                            Ball.Velocity += arrow.GetDirectionVector() * arrowForceMod;
                        }
                    }
                    Vector2 direction = Ball.Velocity;
                    direction.Normalize();
                    Vector2 temp = Ball.Velocity - (direction * Friction);
                    if(!(temp.X > 0 && Ball.Velocity.X > 0 || temp.X < 0 && Ball.Velocity.X < 0))
                    {
                        temp =  new Vector2(0, temp.Y);
                    }
                    if (!(temp.Y > 0 && Ball.Velocity.Y > 0 || temp.Y < 0 && Ball.Velocity.Y < 0))
                    {
                        temp = new Vector2(temp.X, 0);
                    }
                    Ball.Velocity = temp;
                    if (goal.IsCollision()) Exit();
                    if(Ball.Velocity == Vector2.Zero) State = GameState.BallHittable; else Ball.Update(gameTime);
                    break;

            }
            if(Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space)) DisplayDebug = "" + Ball.Velocity.X + " , " + Ball.Velocity.Y + "RING RAD: " + Ring.Radius;
            _priorMouse = CurrentMouse;
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _board.Draw(gameTime,_spriteBatch,Color.ForestGreen);
            foreach(ArrowField arrow in Arrows)
            {
                arrow.Sprite.Draw(gameTime, _spriteBatch);
            }
            goal.Draw(gameTime, _spriteBatch);
            Ball.Draw(gameTime,_spriteBatch);
            if(State == GameState.Aiming)
            {
                Ring.Draw(gameTime, _spriteBatch);

            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}