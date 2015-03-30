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
    public abstract class Character
    {
        #region  Variables

        protected Vector2 position;
        protected Vector2 healthPosition;
        protected int maxHealth;
        protected int currentHealth;
        protected Attack[] attacks;
        protected Texture2D tile;
        protected Sprite[] sprites;
        protected int spriteNum;
        protected bool alive;
        protected Texture2D healthBar;

        #endregion

        #region Properties

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 HealthPosition
        {
            get { return healthPosition; }
            set { position = value; }
        }

        public int MaxHealth
        {
            get { return maxHealth; }
            set { maxHealth = value; }
        }

        public int CurrentHealth
        {
            get { return currentHealth; }
            set { currentHealth = value; }
        }

        public Attack[] Attacks
        {
            get { return attacks; }
            set { attacks = value; }
        }

        public Sprite[] Sprites
        {
            get { return sprites; }
            set { sprites = value; }
        }

        public int SpriteNum
        {
            get { return spriteNum; }
            set { spriteNum = value; }
        }

        public Sprite Sprite
        {
            get {return sprites[spriteNum];}
        }

        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }

        public Texture2D HealthBar
        {
            get { return healthBar; }
        }

        #endregion

        #region Methods

        public void DecHealth(int amount)
        {
            currentHealth -= amount;

            if (currentHealth <= 0)
            {
                Kill();
            }
        }
        public void Initialize()
        {
            healthBar = GlobalVar.content.Load<Texture2D>("Healthbar2");
            //Initialize the Sprite batch               
          
        }
        
        public void IncHealth(int amount)
        {
            currentHealth += amount;

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }

        public void Heal()
        {
            currentHealth = maxHealth;
        }

        public void Kill()
        {
            alive = false;
        }

        public void Revive()
        {
            alive = true;
            currentHealth = maxHealth;
        }

        public virtual void UseAttack(int attack)
        {
            attacks[attack].Use();
        }

        public virtual void Update(GameTime gameTime)
        {
            Sprite.Update(gameTime);
        }

        #endregion

        #region Draw

        public void Draw(SpriteBatch spriteBatch)
        {if (alive)
            {
                spriteBatch.Draw(tile, position, Color.White);
                Sprite.Draw(spriteBatch, new Vector2(position.X + tile.Width/2 - Sprite.Width/2, position.Y), 0f, 0.85f);
            }
        }

        #endregion
    }
}
