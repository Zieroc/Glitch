using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Glitch.Character;
using Glitch.Graphics;

namespace Glitch.BattleSystem
{
    public static class BattleMenu
    {
        #region Variables
        private static Rectangle[] attackRec;
        private static Rectangle TileHub;
        private static Texture2D[] attackButton;
        private static Texture2D tileHub;
        private static Player player;
        private static GraphicsDevice gDevice;
        private static Enemy glitch;
        private static bool pAttack;
        private static bool gAttack;

        #endregion

        #region Properties

        public static Enemy Glitch
        {
            get { return glitch; }
            set { glitch = value; }
        }

        #endregion

        #region Initalisation

        public static void Initalise(Texture2D[] attackButtons, Texture2D hud, Player pc, GraphicsDevice grDevice)
        {
            attackButton = attackButtons;
            tileHub = hud;
            player = pc;
            gDevice = grDevice;

            attackRec = new Rectangle[4];
            attackRec[0] = new Rectangle(50, 600, 200, 150);
            attackRec[1] = new Rectangle(260, 600, 200, 150);
            attackRec[2] = new Rectangle(470, 600, 200, 150);
            attackRec[3] = new Rectangle(680, 600, 200, 150);
            TileHub = new Rectangle(890, 600, 500, 150);

            pAttack = true;
            gAttack = true;
        }

        public static void NewBattle(Enemy enemy)
        {
            glitch = enemy;
            if (glitch.Name.Equals("Input Exception"))
            {
                GlobalVar.battleTurn = 2;
            }
            else
            {
                GlobalVar.battleTurn = 1;
            }
            glitch.Initialize();
        }

        #endregion

        #region Draw

        public static void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(tileHub, TileHub, Color.White);

            for (int i = 0; i < 4; i++)
            {
                if (player.Attacks[i].Useable)
                {
                    spriteBatch.Draw(attackButton[i], attackRec[i], Color.White);
                }
                else
                {
                    spriteBatch.Draw(attackButton[i], attackRec[i], Color.Gray);
                }

                MouseState ms = Mouse.GetState();
                Vector2 pos = new Vector2(ms.X, ms.Y);
                float propX = gDevice.Viewport.Width / (float)GlobalVar.clientBoundsWidth;
                float propY = gDevice.Viewport.Height / (float)GlobalVar.clientBoundsHeight;
                pos.X *= propX;
                pos.Y *= propY;

                if (attackRec[i].Contains((int)pos.X, (int)pos.Y))
                {
                    spriteBatch.Draw(attackButton[i], attackRec[i], Color.Gray);
                }
                spriteBatch.Draw(player.HealthBar, new Rectangle(965 - player.HealthBar.Width / 2, 640, player.HealthBar.Width, 20), new Rectangle(965, 640, player.HealthBar.Width, 20), Color.Transparent);
                //Draw the current health level based on the current Health            
                spriteBatch.Draw(player.HealthBar, new Rectangle(965 - player.HealthBar.Width / 2, 640, (int)(player.HealthBar.Width * ((double)player.CurrentHealth / (double)player.MaxHealth)), 20), new Rectangle(965, 640, player.HealthBar.Width, 20), Color.Red);

                spriteBatch.Draw(glitch.HealthBar, new Rectangle(1270 - glitch.HealthBar.Width / 2, 640, glitch.HealthBar.Width, 20), new Rectangle(1270, 620, glitch.HealthBar.Width, 20), Color.Transparent);
                //Draw the current health level based on the current Health            
                spriteBatch.Draw(glitch.HealthBar, new Rectangle(1270 - glitch.HealthBar.Width / 2, 640, (int)(glitch.HealthBar.Width * ((double)glitch.CurrentHealth / (double)glitch.MaxHealth)), 20), new Rectangle(1270, 640, glitch.HealthBar.Width, 20), Color.Purple);

                player.Draw(spriteBatch);
                glitch.Draw(spriteBatch);
            }
        }

        #endregion

        #region Update

        public static void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            glitch.Update(gameTime);

            MouseState ms = Mouse.GetState();
            Vector2 pos = new Vector2(ms.X, ms.Y);
            float propX = gDevice.Viewport.Width / (float)GlobalVar.clientBoundsWidth;
            float propY = gDevice.Viewport.Height / (float)GlobalVar.clientBoundsHeight;
            pos.X *= propX;
            pos.Y *= propY;

            if (!pAttack)
            {
                if (glitch.Sprite.FinishedPlaying)
                {
                    pAttack = true;
                    glitch.Sprite.CurrentState = 0;
                    glitch.SpriteNum = 0;
                }
            }
            if (!gAttack)
            {
                if (player.Sprite.FinishedPlaying)
                {
                    gAttack = true;
                    player.Sprite.CurrentState = 0;
                    player.SpriteNum = 0;
                }
            }

            if (GlobalVar.battleTurn == 1 && player.Alive && pAttack)
            {

                if (ms.LeftButton == ButtonState.Pressed && GlobalVar.mouseReleased)
                {
                    GlobalVar.mouseReleased = false;
                    for (int i = 0; i < 4; i++)
                    {
                        if (attackRec[i].Contains((int)pos.X, (int)pos.Y))
                        {
                            if (player.Attacks[i].Useable)
                            {
                                gAttack = false;
                                player.SpriteNum = 1;

                                player.UseAttack(i);

                                if (glitch.Name.Equals("NewsApp"))
                                {
                                    glitch.DecHealth(player.Attacks[i].Strength);

                                }
                                else if (glitch.Name.Equals("MailApp"))
                                {
                                    Random rand = new Random((int)gameTime.TotalGameTime.Milliseconds);
                                    if (rand.Next(100) + 1 >= 15 - player.Attacks[i].Strength)
                                    {
                                        glitch.DecHealth(player.Attacks[i].Strength);
                                    }
                                }
                                else if(glitch.Name.Equals("WindowStore"))
                                {
                                    Random rand = new Random((int)gameTime.TotalGameTime.Milliseconds);
                                    if (rand.Next(100) + 1 >= 15)
                                    {
                                        glitch.DecHealth(player.Attacks[i].Strength);
                                    }
                                }

                                GlobalVar.battleTurn++;
                                for (int j = 0; j < 4; j++)
                                {
                                    if (j != i)
                                    {
                                        if (!player.Attacks[j].Useable)
                                        {
                                            player.Attacks[j].CooldownTurn();
                                        }
                                    }
                                }

                                i = 4;
                            }
                        }
                    }
                }
                else if (ms.LeftButton == ButtonState.Released)
                {
                    GlobalVar.mouseReleased = true;
                }
            }
            else if (GlobalVar.battleTurn == 2 && glitch.Alive && gAttack)
            {
                Random rand = new Random();
                int attackUsed = 0;
                if (glitch.Attacks[0].Useable)
                {
                    glitch.Attacks[0].Use();
                    attackUsed = 0;

                    if (rand.Next(100) + 1 <= 20)
                    {
                        player.DecHealth(glitch.Attacks[0].Strength);
                    }
                }
                else if (glitch.Attacks[1].Useable)
                {
                    glitch.Attacks[1].Use();
                    attackUsed = 1;

                    if (rand.Next(0, 100) <= 70)
                    {


                        player.DecHealth(glitch.Attacks[1].Strength);
                    }
                }
                else if (glitch.Attacks.Length >= 3)
                {
                    glitch.Attacks[2].Use();
                    attackUsed = 2;

                    if (rand.Next(0, 100) <= 70)
                    {
                        player.DecHealth(glitch.Attacks[2].Strength);
                    }
                }

                for (int i = 0; i < glitch.Attacks.Length; i++)
                {
                    if (!glitch.Attacks[i].Useable && i != attackUsed)
                    {
                        glitch.Attacks[i].CooldownTurn();
                    }
                }

                pAttack = false;
                glitch.SpriteNum = 1;
                GlobalVar.battleTurn--;
            }


            if (!player.Alive)
            {
                GlobalVar.state = GlobalVar.GameState.GameOver;
            }

            if (!glitch.Alive)
            {
                GlobalVar.state = GlobalVar.GameState.Playing;
                GlobalVar.showScreen = true;
            }
        }

        #endregion
    }
}
