using GameLibrary;
using GenericRPG.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace GenericRPG {
  public partial class FrmMap : Form {
    private Character character;
    private Map map;
    private Game game;

    public FrmMap() {
      InitializeComponent();
    }

    private void FrmMap_Load(object sender, EventArgs e) {
      game = Game.GetGame();

      map = new Map();

      Random rnd = new Random();
		string output = "";
        List<string> mapLines = new List<string>();
		int row = rnd.Next(15,21);
		int col = rnd.Next(15,21);
		char[,] arr = new char[row,col];
		
		//create box and border
		for(int i=0;i<row;i++)
		{
			for(int j=0;j<col;j++)
			{
				if (i == 0 | i == row-1 | j == 0 | j == col-1)
				{ 
					arr[i, j]= '1';
				}
				
				else
				{
					arr[i, j]= '0';
				}
			}
		}
		
		//randomly assign values
		for(int i=1;i<row-1;i++)
		{
			for(int j=1;j<col-1;j++)
			{
				int x = rnd.Next(100);
				if (x%3 == 1)
				{
					arr[i, j]= '1';
				}
			}
		}
		
		//makes floor out of random values
		for(int i=1 ;i<row-1 ;i++)
		{
			for(int j=1;j<col-1;j++)
			{
				int count = 0;
				int randint = rnd.Next();
				

				if (arr[i,j-1] == '0')
				{
					count +=1;
				}
				
				if (arr[i-1,j] == '0')
				{
					count +=1;
				}
				
				if (arr[i+1,j] == '0')
				{
					count +=1;
				}

				if (arr[i,j+1] == '0')
				{
					count +=1;
				}
				
				if (count >= 3 & randint % 4 == 1)
				{	
					arr[i,j] = '1';
				}
				
			}
			
		}
		
		//filling in 1x1 openings
		for(int i=1 ;i<row-1 ;i++)
		{
			for(int j=1;j<col-1;j++)
			{
				int count = 0;
				int randint = rnd.Next();
				

				if (arr[i,j-1] == '0')
				{
					count +=1;
				}
				
				if (arr[i-1,j] == '0')
				{
					count +=1;
				}
				
				if (arr[i+1,j] == '0')
				{
					count +=1;
				}

				if (arr[i,j+1] == '0')
				{
					count +=1;
				}
	
				if (count == 0)
				{
					arr[i,j] = '1';
				}
			}
		}
		
		//opening up closed off areas
		for(int i=1 ;i<row-1 ;i++)
		{
			for(int j=1;j<col-1;j++)
			{
				if ((arr[i-1,j] == '0' & arr[i+1,j] == '0') | (arr[i,j-1] == '0' & arr[i,j+1] == '0'))
				{
					arr[i,j] = '0';
				}
			}
		}

        int rndrow = rnd.Next(1,row);
        int rndcol = rnd.Next(1,col);
        while (arr[rndrow,rndcol] != '0')
          {
            rndrow = rnd.Next(1,row);
            rndcol = rnd.Next(1,col);
          }
        arr[rndrow,rndcol] = '2';

        int rndrow2 = rnd.Next(1,row);
        int rndcol2 = rnd.Next(1,col);
        while ( (arr[rndrow2,rndcol2] != '0') | (Math.Abs(rndrow2-rndrow) <= 5) | (Math.Abs(rndcol2-rndcol) <= 5))
          {
            rndrow2 = rnd.Next(1,row);
            rndcol2 = rnd.Next(1,col);
          }
        arr[rndrow2,rndcol2] = '3';

        int rndrow3 = rnd.Next(1,row);
        int rndcol3 = rnd.Next(1,col);
        while (arr[rndrow3,rndcol3] != '0')
          {
            rndrow3 = rnd.Next(1,row);
            rndcol3 = rnd.Next(1,col);
          }

        int rnditem = rnd.Next(6,9);

        if (rnditem == 6)
            {
                arr[rndrow3,rndcol3] = '6';
            }

        if (rnditem == 7)
            {
                arr[rndrow3,rndcol3] = '7';
            }

        if (rnditem == 8)
            {
                arr[rndrow3,rndcol3] = '8';
            }

					
		
		//combine to 1 string 
		for(int i=0;i<row;i++)
		{
            output = "";
			for(int j=0;j<col;j++)
			{
				output += arr[i,j];
			}
			mapLines.Add(output);
		}

      character = map.LoadMap(mapLines, grpMap, 
        str => Resources.ResourceManager.GetObject(str) as Bitmap
      );
      Width = grpMap.Width + 25;
      Height = grpMap.Height + 50;
      game.SetCharacter(character);
    }

    private void FrmMap_KeyDown(object sender, KeyEventArgs e) {
      MoveDir dir = MoveDir.NO_MOVE;
      switch (e.KeyCode) {
        case Keys.Left:
          dir = MoveDir.LEFT;
          break;
        case Keys.Right:
          dir = MoveDir.RIGHT;
          break;
        case Keys.Up:
          dir = MoveDir.UP;
          break;
        case Keys.Down:
          dir = MoveDir.DOWN;
          break;
      }
      if (dir != MoveDir.NO_MOVE) {
        character.Move(dir);
        if (game.State == GameState.FIGHTING) {
          FrmArena frmArena = new FrmArena();
          frmArena.Show();
        }
      }
    }

    public void RemoveItem(int num)
        {
            switch (num)
            {
                case 6:
                    foreach (var control in grpMap.Controls)
                    {
                        var ctrl = control as Control;
                        if ((ctrl.Tag as string) == "sword")
                        {
                            this.Controls.Remove(ctrl);
                        }
                    }
                    break;
            }
            //return 0;
        }
  }
}
