using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RandomWalk
{
    public class Walker
    {
        Random rnd = new();
        public Vector2 Position;
        public Texture2D Texture;                
        public Walker(Vector2 position,Texture2D texture)
        {
            Position = position;
            Texture = texture;            
        }        
        private void Walk(Vector2 position)
        {
            Position = Vector2.Add(Position,position);
        }
        public void Walk(){            
            Walk(GeneratePositionChange());
        }
        public void SetTexture(Vector2 size){
            var x = (this.Position.X / size.X);
            var y = (this.Position.Y / size.Y);
            var color = new Color(x,y,x * y,1.0f);
            Texture.SetData(Enumerable.Repeat(color,16 * 16).ToArray());
        }        
        protected Vector2 GeneratePositionChange(){
            var x = rnd.Next(-256,256) * 0.01f;
            var y = rnd.Next(-256,256) * 0.01f;
            var position = new Vector2();
            var xDist = Globals.ScreenSize.X - position.X;
            var yDist = Globals.ScreenSize.Y - position.Y;            
            position.X = xDist >= 5 && xDist <= 100 ? -x : x;
            position.Y = yDist >= 5 && yDist <= 100 ? -y : y;
            return position;
        }
    }
}