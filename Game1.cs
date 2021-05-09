using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RandomWalk
{
    public class Walk : Game
    {
        Vector2 ScreenSize;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;         
        Walker[] walkers = new Walker[7];
        
        RenderTarget2D renderTarget;        
        public Walk()
        {
            _graphics = new GraphicsDeviceManager(this);                     
            Content.RootDirectory = "Content";
            IsMouseVisible = true;            
            
        }
        
        protected override void Initialize()
        {   
            _spriteBatch = new SpriteBatch(GraphicsDevice);                                      
            var texture = new Texture2D(GraphicsDevice,16,16);
            var textureData = Enumerable.Repeat(Color.White,16 * 16).ToArray();
            texture.SetData(textureData);
            _graphics.PreferredBackBufferWidth = 1920;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = 1080;   // set this value to the desired height of your window
            _graphics.ApplyChanges();
            
            ScreenSize = new Vector2(_graphics.PreferredBackBufferWidth,_graphics.PreferredBackBufferHeight);
            // walker = new Walker(new Vector2(ScreenSize.X / 2,ScreenSize.Y / 2),texture);
            var rnd = new Random();
            walkers[0] = new Walker(new Vector2(350,250),new Texture2D(GraphicsDevice,16,16));
            walkers[1] = new Walker(new Vector2(700,500),new Texture2D(GraphicsDevice,16,16));
            walkers[2] = new Walker(new Vector2(1000,750),new Texture2D(GraphicsDevice,16,16));
            walkers[3] = new Walker(new Vector2(1200,900),new Texture2D(GraphicsDevice,16,16));
            walkers[4] = new Walker(new Vector2(1000,250),new Texture2D(GraphicsDevice,16,16));
            walkers[5] = new Walker(new Vector2(700,500),new Texture2D(GraphicsDevice,16,16));
            walkers[6] = new Walker(new Vector2(400,750),new Texture2D(GraphicsDevice,16,16));
            renderTarget = new RenderTarget2D(GraphicsDevice,(int)ScreenSize.X,(int)ScreenSize.Y ,false,SurfaceFormat.Color,DepthFormat.Depth24,0,RenderTargetUsage.PreserveContents);                        
            Globals.ScreenSize = ScreenSize;
            base.Initialize();                        
        }

        protected override void LoadContent()
        {   
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach(var walker in walkers){
                walker.SetTexture(ScreenSize);
                walker.Walk();
            }
                        
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {            
            //draws the walker in the render target
            GraphicsDevice.SetRenderTarget(renderTarget);
            foreach (var walker in walkers)
            {
                _spriteBatch.Begin();
                _spriteBatch.Draw(walker.Texture,walker.Position,Color.White);
                _spriteBatch.End();    
            }
            GraphicsDevice.SetRenderTarget(null);
            //now draws the renderTarget to the backbuffer, keeping it's previous states
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();
            _spriteBatch.Draw(renderTarget,Vector2.Zero,renderTarget.Bounds,Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }        
        
    }
}
