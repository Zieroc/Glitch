using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Glitch.BattleSystem
{
    public class Attack
    {
        #region Variables

        private string name;
        private int uses;
        private int used;
        private int cooldownTime;
        private int cooldown;
        private int strength;
        private bool useable;

        #endregion

        #region Constructor

        public Attack(string name, int uses, int cooldownTime, int strength)
        {
            this.name = name;
            this.uses = uses;
            this.used = 0;
            this.cooldownTime = cooldownTime;
            this.cooldown = 0;
            this.strength = strength;
            useable = true;
        }

        #endregion

        #region Properties

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Uses
        {
            get { return uses; }
            set { uses = value; }
        }

        public int Used
        {
            get { return used; }
            set { used = value; }
        }

        public int CooldownTime
        {
            get { return cooldownTime; }
            set { cooldownTime = value; }
        }

        public int Cooldown
        {
            get { return cooldown; }
            set { cooldown = value; }
        }

        public int Strength
        {
            get { return strength; }
            set { strength = value; }
        }

        public bool Useable
        {
            get { return useable; }
            set { useable = value; }
        }

        #endregion

        #region Methods

        public void Use()
        {
            if (useable)
            {
                used++;
                if (used >= uses && uses > -1)
                {
                    useable = false;
                    cooldown = cooldownTime;
                }
            }
        }

        public void CooldownTurn()
        {
                cooldown--;
                if (cooldown <= 0)
                {
                    useable = true;
                    used = 0;
                }
        }

        #endregion
    }
}
