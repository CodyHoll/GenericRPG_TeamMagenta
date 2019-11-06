using System.Windows.Forms;
using System;
using System.Drawing;

namespace GameLibrary
{
    public struct Position1 
    {
        public int row;
        public int col;

        public Position1(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
    }

    public class Item
    {
        public PictureBox Pic { get; private set; }
        private Position1 pos;
        private Map map;


        public Item(PictureBox pb, Position1 pos, Map map)
        {
            Pic = pb;
            this.pos = pos;
            this.map = map;
        }

        public void RemoveItem()
        {
            //Bitmap yeet = new Bitmap("level2.png");
            //this.Pic.Image = yeet;
            //PictureBox.Image = Image.Resources("level2.png");

        }

    }

    
}
