using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Glitch.Graphics
{
    public class Sprite
    {
        #region Variables

        private Texture2D texture;

        private int height;
        private int width;

        private int columns;
        private int rows;

        private int states;
        private int currentState;

        private bool animate;
        private bool looping;

        private float timer;
        private float interval;

        #endregion

        #region Constructor

        public Sprite(Texture2D texture)
            : this(texture, texture.Height, texture.Width, 1, false, false, 250f)
        {
        }

        public Sprite(Texture2D texture, int height, int width, int state)
            : this(texture, height, width, state, false, false, 250f)
        {
        }

        public Sprite(Texture2D texture, int height, int width, int state, bool animate, bool looping)
            : this(texture, height, width, state, animate, looping, 250f)
        {
        }

        public Sprite(Texture2D texture, int height, int width, int state, bool animate, bool looping, float interval)
        {
            this.texture = texture;
            this.height = height;
            this.width = width;
            this.states = state;
            this.animate = animate;
            this.looping = looping;

            columns = texture.Width / width;
            rows = texture.Height / height;

            currentState = 0;
            timer = 0;
            this.interval = interval;
        }

        #endregion

        #region Properties

        public Texture2D Texture
        {
            set { texture = value; }
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public int CurrentState
        {
            get { return currentState; }
            set { currentState = value; }
        }

        public bool Animate
        {
            get { return animate; }
            set { animate = value; }
        }

        public bool Looping
        {
            get { return looping; }
            set { looping = value; }
        }

        public bool FinishedPlaying
        {
            get
            {
                if (looping == false)
                {
                    if (currentState == states - 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            if (animate && currentState < states)
            {
                timer += (float)gameTime.ElapsedGameTime.Milliseconds;

                if (timer > interval)
                {
                    currentState++;
                    timer = 0f;
                }
            }

            if (currentState == states)
            {
                if (looping)
                {
                    currentState = 0;
                }
                else
                {
                    currentState--;
                }
            }
        }

        #endregion

        #region Draw

        public void Draw(SpriteBatch spriteBatch, Vector2 position,float rotation, float depth)
        {
            int imgX = width * (currentState % columns);
            int imgY = height * (currentState / columns);

            Rectangle sourceRect = new Rectangle(imgX, imgY, width, height);

            spriteBatch.Draw(texture, position, sourceRect, Color.White, rotation, new Vector2(0,0), 1.0f, SpriteEffects.None, depth);
        }

        #endregion
    }
}
