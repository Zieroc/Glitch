using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Glitch;
using Glitch.BattleSystem;
using Glitch.Character;
using Glitch.Graphics;

namespace AppAdventure
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        #region Variables
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        Player dev;
        Sprite[] devSprites;
        Enemy newsApp;
        Sprite[] newsAppSprites;
        Enemy mailApp;
        Sprite[] mailAppSprites;
        Enemy storeApp;
        Sprite[] storeAppSprites;
        Texture2D[] battleMenuButtons = new Texture2D[4];
        Texture2D tileHub;
        int battles = 3;

        Texture2D creditsButton;
        Texture2D playButton;

        Texture2D background;
        Texture2D credits;
        Texture2D story;
        Texture2D howTo;
        Texture2D hudTile;
        Texture2D gameOver;
        Texture2D winScreen;

        Texture2D versus1;
        Texture2D versus2;
        Texture2D versus3;

        float timer;
        float interval;
        #endregion

        #region Constructor
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.IsFullScreen = true;

            _graphics.PreferredBackBufferHeight = 768;
            _graphics.PreferredBackBufferWidth = 1366;

            Content.RootDirectory = "Assets";
        }
        #endregion

        #region Initialize
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = true;
            GlobalVar.mouseReleased = true;
            GlobalVar.battleTurn = 1;

            GlobalVar.clientBoundsWidth = this.Window.ClientBounds.Width;
            GlobalVar.clientBoundsHeight = this.Window.ClientBounds.Height;

            GlobalVar.content = Content;
            GlobalVar.state = GlobalVar.GameState.Menu;

            devSprites = new Sprite[2];
            newsAppSprites = new Sprite[2];
            mailAppSprites = new Sprite[2];
            storeAppSprites = new Sprite[2];

            base.Initialize();
        }
        #endregion

        #region Load/Unload
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            devSprites[0] = new Sprite(GlobalVar.content.Load<Texture2D>(@"Characters\Van Neumman"), 100, 100, 1, true, true, 10f);
            devSprites[1] = new Sprite(GlobalVar.content.Load<Texture2D>(@"Characters\NeumannReels"), 100, 100, 7, true, false, 75f);
            dev = new Player(new Vector2(260, 425), 40, devSprites, true, Content.Load<Texture2D>(@"Characters\Glitch"));

            newsAppSprites[0] = new Sprite(GlobalVar.content.Load<Texture2D>(@"Characters\PixelGirlReel"), 100, 100, 4, false, false, .1f);
            newsAppSprites[1] = new Sprite(GlobalVar.content.Load<Texture2D>(@"Characters\PixelGirlReel"), 100, 100, 4, true, false, 75f);
            newsApp = new Enemy(new Vector2(890, 100), 50, true, "NewsApp", newsAppSprites, Content.Load<Texture2D>(@"Characters\newsTile"));

            mailAppSprites[0] = new Sprite(Content.Load<Texture2D>(@"Characters\Visio The Stud"));
            mailAppSprites[1] = new Sprite(GlobalVar.content.Load<Texture2D>(@"Characters\VisioReel"), 100, 100, 4, true, false, 75f);
            mailApp = new Enemy(new Vector2(890, 100), 30, true, "MailApp", mailAppSprites, Content.Load<Texture2D>(@"Characters\messageTile"));

            storeAppSprites[0] = new Sprite(Content.Load<Texture2D>(@"Characters\Pixel Suit"));
            storeAppSprites[1] = new Sprite(GlobalVar.content.Load<Texture2D>(@"Characters\PixelSuitReel"), 100, 100, 6, true, false, 75f);
            storeApp = new Enemy(new Vector2(890, 100), 60, true, "WindowStore", storeAppSprites, Content.Load<Texture2D>(@"Characters\storeTile"));

            hudTile = Content.Load<Texture2D>(@"hud 2");
            battleMenuButtons[0] = Content.Load<Texture2D>(@"Buttons\innovate");
            battleMenuButtons[1] = Content.Load<Texture2D>(@"Buttons\goodCode");
            battleMenuButtons[2] = Content.Load<Texture2D>(@"Buttons\easyUse");
            battleMenuButtons[3] = Content.Load<Texture2D>(@"Buttons\fluidUI");

            tileHub = Content.Load<Texture2D>(@"hud 2");
            BattleMenu.Initalise(battleMenuButtons, tileHub, dev, this.GraphicsDevice);

            creditsButton = Content.Load<Texture2D>(@"Buttons\CreditsButton");
            playButton = Content.Load<Texture2D>(@"Buttons\StartGame");

            background = Content.Load<Texture2D>("Background");
            credits = Content.Load<Texture2D>("Credits");
            story = Content.Load<Texture2D>("Story");
            howTo = Content.Load<Texture2D>("HowTo");
            gameOver = Content.Load<Texture2D>("GameOver");
            winScreen = Content.Load<Texture2D>("WinScreen");

            versus1 = Content.Load<Texture2D>("VS1");
            versus2 = Content.Load<Texture2D>("VS2");
            versus3 = Content.Load<Texture2D>("VS3");

            timer = 0f;
            interval = 1500f;
            GlobalVar.showScreen = true;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        #endregion

        #region Update
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            if (GlobalVar.state == GlobalVar.GameState.Menu)
            {
                Rectangle playButtonRec = new Rectangle(1366 / 2 - 75, 200, 200, 150);
                Rectangle creditButtonRec = new Rectangle(1366 / 2 - 75, 360, 200, 150);

                MouseState ms = Mouse.GetState();
                Vector2 pos = new Vector2(ms.X, ms.Y);
                float propX = _graphics.GraphicsDevice.Viewport.Width / (float)GlobalVar.clientBoundsWidth;
                float propY = _graphics.GraphicsDevice.Viewport.Height / (float)GlobalVar.clientBoundsHeight;
                pos.X *= propX;
                pos.Y *= propY;

                if (ms.LeftButton == ButtonState.Pressed && GlobalVar.mouseReleased)
                {
                    GlobalVar.mouseReleased = false;
                    if (playButtonRec.Contains((int)pos.X, (int)pos.Y))
                    {
                        GlobalVar.state = GlobalVar.GameState.Plot;
                    }
                    if (creditButtonRec.Contains((int)pos.X, (int)pos.Y))
                    {
                        GlobalVar.state = GlobalVar.GameState.Credits;
                    }
                }
                else if (ms.LeftButton == ButtonState.Released && !GlobalVar.mouseReleased)
                {
                    GlobalVar.mouseReleased = true;
                }
            }
            else if (GlobalVar.state == GlobalVar.GameState.Credits)
            {
                MouseState ms = Mouse.GetState();

                if (ms.LeftButton == ButtonState.Pressed && GlobalVar.mouseReleased)
                {
                    GlobalVar.mouseReleased = false;
                    GlobalVar.state = GlobalVar.GameState.Menu;
                }
                else if (ms.LeftButton == ButtonState.Released && !GlobalVar.mouseReleased)
                {
                    GlobalVar.mouseReleased = true;
                }
            }
            else if (GlobalVar.state == GlobalVar.GameState.Plot)
            {
                MouseState ms = Mouse.GetState();

                if (ms.LeftButton == ButtonState.Pressed && GlobalVar.mouseReleased)
                {
                    GlobalVar.mouseReleased = false;
                    GlobalVar.state = GlobalVar.GameState.HowTo;
                }
                else if (ms.LeftButton == ButtonState.Released && !GlobalVar.mouseReleased)
                {
                    GlobalVar.mouseReleased = true;
                }
            }
            else if (GlobalVar.state == GlobalVar.GameState.HowTo)
            {
                MouseState ms = Mouse.GetState();

                if (ms.LeftButton == ButtonState.Pressed && GlobalVar.mouseReleased)
                {
                    GlobalVar.mouseReleased = false;
                    GlobalVar.state = GlobalVar.GameState.Playing;
                }
                else if (ms.LeftButton == ButtonState.Released && !GlobalVar.mouseReleased)
                {
                    GlobalVar.mouseReleased = true;
                }
            }
            else if (GlobalVar.state == GlobalVar.GameState.Playing)
            {
                dev.Update(gameTime);
                dev.Heal();
                for (int i = 0; i < dev.Attacks.Length; i++)
                {
                    while (!dev.Attacks[i].Useable)
                    {
                        dev.Attacks[i].CooldownTurn();
                    }
                }

                if (battles == 3)
                {
                    if (GlobalVar.showScreen)
                    {
                        timer += (float)gameTime.ElapsedGameTime.Milliseconds;

                        if (timer > interval)
                        {
                            GlobalVar.showScreen = false;
                            timer = 0f;
                        }
                    }
                    else
                    {
                        battles--;
                        BattleMenu.NewBattle(newsApp);
                        GlobalVar.state = GlobalVar.GameState.Battle;
                    }
                }
                else if (battles == 2)
                {
                    if (GlobalVar.showScreen)
                    {
                        timer += (float)gameTime.ElapsedGameTime.Milliseconds;

                        if (timer > interval)
                        {
                            GlobalVar.showScreen = false;
                            timer = 0f;
                        }
                    }
                    else
                    {
                        battles--;
                        BattleMenu.NewBattle(mailApp);

                        GlobalVar.state = GlobalVar.GameState.Battle;
                    }
                }
                else if (battles == 1)
                {
                    if (GlobalVar.showScreen)
                    {
                        timer += (float)gameTime.ElapsedGameTime.Milliseconds;

                        if (timer > interval)
                        {
                            GlobalVar.showScreen = false;
                            timer = 0f;
                        }
                    }
                    else
                    {
                        battles--;
                        BattleMenu.NewBattle(storeApp);
                        GlobalVar.state = GlobalVar.GameState.Battle;
                    }
                }
                else
                {
                    GlobalVar.state = GlobalVar.GameState.Win;
                }
            }
            else if (GlobalVar.state == GlobalVar.GameState.Battle)
            {
                BattleMenu.Update(gameTime);
            }
            else if (GlobalVar.state == GlobalVar.GameState.Win)
            {
                MouseState ms = Mouse.GetState();

                if (ms.LeftButton == ButtonState.Pressed && GlobalVar.mouseReleased)
                {
                    GlobalVar.mouseReleased = false;
                    GlobalVar.state = GlobalVar.GameState.Menu;
                    battles = 3;
                    dev.Revive();
                    newsApp.Revive();
                    mailApp.Revive();
                    storeApp.Revive();
                }
                else if (ms.LeftButton == ButtonState.Released && !GlobalVar.mouseReleased)
                {
                    GlobalVar.mouseReleased = true;
                }
            }
            else if (GlobalVar.state == GlobalVar.GameState.GameOver)
            {
                MouseState ms = Mouse.GetState();

                if (ms.LeftButton == ButtonState.Pressed && GlobalVar.mouseReleased)
                {
                    GlobalVar.mouseReleased = false;
                    GlobalVar.state = GlobalVar.GameState.Menu;
                    battles = 3;
                    dev.Revive();
                    newsApp.Revive();
                    mailApp.Revive();
                    storeApp.Revive();
                }
                else if (ms.LeftButton == ButtonState.Released && !GlobalVar.mouseReleased)
                {
                    GlobalVar.mouseReleased = true;
                }
            }

            base.Update(gameTime);
        }
        #endregion

        #region Draw
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(128, 255, 255));

            _spriteBatch.Begin();
            _spriteBatch.Draw(background, new Vector2(0, 0), Color.White);

            if (GlobalVar.state == GlobalVar.GameState.Menu)
            {
                _spriteBatch.Draw(playButton, new Rectangle(1366 / 2 - 75, 200, 200, 150), Color.White);
                _spriteBatch.Draw(creditsButton, new Rectangle(1366 / 2 - 75, 360, 200, 150), Color.White);
            }
            else if (GlobalVar.state == GlobalVar.GameState.Credits)
            {
                _spriteBatch.Draw(credits, new Vector2(0, 0), Color.White);

            }
            else if (GlobalVar.state == GlobalVar.GameState.Plot)
            {
                _spriteBatch.Draw(story, new Vector2(0, 0), Color.White);
            }
            else if (GlobalVar.state == GlobalVar.GameState.HowTo)
            {
                _spriteBatch.Draw(howTo, new Vector2(0, 0), Color.White);
            }
            else if (GlobalVar.state == GlobalVar.GameState.Playing)
            {
                if (battles == 3)
                {
                    _spriteBatch.Draw(versus1, new Vector2(0, 0), Color.White);
                }
                else if (battles == 2)
                {
                    _spriteBatch.Draw(versus2, new Vector2(0, 0), Color.White);
                }
                else if (battles == 1)
                {
                    _spriteBatch.Draw(versus3, new Vector2(0, 0), Color.White);
                }
            }
            else if (GlobalVar.state == GlobalVar.GameState.Battle)
            {

                BattleMenu.Draw(_spriteBatch);
            }
            else if (GlobalVar.state == GlobalVar.GameState.Win)
            {
                _spriteBatch.Draw(winScreen, new Vector2(0, 0), Color.White);
            }
            else if (GlobalVar.state == GlobalVar.GameState.GameOver)
            {
                _spriteBatch.Draw(gameOver, new Vector2(0, 0), Color.White);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion
    }
}
