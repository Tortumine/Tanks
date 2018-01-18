using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tanks.Core
{
    public class Explosion : GameObject

    {
        List<Rectangle> frames = new List<Rectangle>();


        public Explosion(int size):base(650,650,5,5, size, size)
        {

        }
    }
}
