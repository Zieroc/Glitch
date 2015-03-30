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
    public class Player : Character
    {
        #region Variables

        #endregion

        #region Constructor

        public Player(Vector2 position, int health, Sprite[] sprites, bool alive, Texture2D tile)
        {
            this.position = position;
            this.maxHealth = health;
            this.currentHealth = health;
            this.sprites = sprites;
            this.alive = alive;
            this.tile = tile;

            attacks = new Attack[4];
            attacks[0] = new Attack("Innovate", 1, 3, 6);
            attacks[1] = new Attack("Good Code", -1, 1, 2);
            attacks[2] = new Attack("Fluid UI", 3, 2, 3);
            attacks[3] = new Attack("Easy Use", 2, 2, 4);

            healthBar = GlobalVar.content.Load<Texture2D>("HealthBar2");
        }

        #endregion

        #region Properties

        #endregion

        #region Update


        #endregion

        #region Method

        public override void UseAttack(int attack)
        {
            base.UseAttack(attack);
        }

        #endregion
    }
}
