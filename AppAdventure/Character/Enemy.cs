using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Glitch.BattleSystem;
using Glitch.Graphics;


namespace Glitch.Character
{
    public class Enemy : Character
    {
        #region Variables

        string name;

        #endregion

        #region Constructor

        public Enemy(Vector2 position, int health, bool alive, string name, Sprite[] sprites, Texture2D tile)
        {
            this.position = position;
            this.maxHealth = health;
            this.currentHealth = health;
            this.alive = alive;
            this.name = name;
            this.sprites = sprites;
            this.tile = tile;

            healthBar = GlobalVar.content.Load<Texture2D>("HealthBar2");

            if (name == "NewsApp")
            {
                attacks = new Attack[2];
                attacks[0] = new Attack("Bad Press", 1, 2, 5);
                attacks[1] = new Attack("Scandal", 2, 1, 3);
            }
            if (name == "MailApp")
            {
                attacks = new Attack[2];
                attacks[0] = new Attack("Rumours", 1, 3, 5);
                attacks[1] = new Attack("Texting", 3, 1, 2);
            }
            if (name == "WindowStore")
            {
                attacks = new Attack[3];
                attacks[0] = new Attack("Requirement Process", 1, 2, 5);
                attacks[1] = new Attack("Terms of Service", 2, 2, 4);
                attacks[2] = new Attack("Placeholder", 3, 1, 3);
            }
        }

        #endregion

        #region Properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        #endregion

        #region Update

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        #endregion


    }
}
