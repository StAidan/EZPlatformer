using System;
using System.Collections.Generic;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EZPlatformer
{
    public class PlayState : FlxState
    {
		public FlxTilemap level;
		public FlxSprite exit;
		public FlxGroup coins;
		public FlxSprite player;
        public FlxText score;
        public FlxText status;

        FlxGroup _blocks;

		override public void create()
		{
            //FlxG.showBounds = true;
            FlxG.flash.start(Color.Black, 1f, null, true);

			//Set the background color to light gray (0xAARRGGBB)
			bgColor = new Color(0xaa, 0xaa, 0xaa);
			
			//Design your platformer level with 1s and 0s (at 40x30 to fill 320x240 screen)
			int[] data = new int[] {
				1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
				1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
				1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
				1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
				1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
				1, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
				1, 0, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1,
				1, 0, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1,
				1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
				1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1,
				1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1,
				1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
				1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1,
				1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1,
				1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
				1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1,
				1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1,
				1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
				1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 1,
				1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 1,
				1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 0, 0, 1,
				1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
				1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
				1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
				1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
				1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
				1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
				1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 1,
				1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 1,
				1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 };
			
			//Create a new tilemap using our level data
			level = new FlxTilemap();
			level.auto = FlxTilemap.AUTO;
			level.loadMap(FlxTilemap.arrayToCSV(data,40),FlxTilemap.ImgAuto);
			add(level);
			
			//Create the level exit, a dark gray box that is hidden at first
			exit = new FlxSprite(35*8+1,25*8);
			exit.createGraphic(14,16,new Color(0x3f, 0x3f, 0x3f));
			exit.exists = false;
			add(exit);

            _blocks = new FlxGroup();
            _blocks.add(createBlock(12, 9));
            _blocks.add(createBlock(13, 9));
            _blocks.add(createBlock(14, 9));
            _blocks.add(createBlock(15, 9));

            _blocks.add(createBlock(24, 9));
            _blocks.add(createBlock(25, 9));
            _blocks.add(createBlock(26, 9));
            _blocks.add(createBlock(27, 9));

            _blocks.add(createBlock(17, 17));
            _blocks.add(createBlock(18, 17));
            _blocks.add(createBlock(19, 17));
            _blocks.add(createBlock(20, 17));
            _blocks.add(createBlock(21, 17));
            _blocks.add(createBlock(22, 17));
            _blocks.add(createBlock(23, 17));
            _blocks.add(createBlock(24, 17));
            _blocks.add(createBlock(25, 17));
            _blocks.add(createBlock(26, 17));
            _blocks.add(createBlock(27, 17));
            _blocks.add(createBlock(28, 17));

			//Create coins to collect (see createCoin() function below for more info)
			coins = new FlxGroup();
			//Top left coins
			coins.add(createCoin(18,4));
			coins.add(createCoin(12,4));
			coins.add(createCoin(9,4));
			coins.add(createCoin(8,11));
			coins.add(createCoin(1,7));
			coins.add(createCoin(3,4));
			coins.add(createCoin(5,2));
			coins.add(createCoin(15,11));
			coins.add(createCoin(16,11));
			
			//Bottom left coins
			coins.add(createCoin(3,16));
			coins.add(createCoin(4,16));
			coins.add(createCoin(1,23));
			coins.add(createCoin(2,23));
			coins.add(createCoin(3,23));
			coins.add(createCoin(4,23));
			coins.add(createCoin(5,23));
			coins.add(createCoin(12,26));
			coins.add(createCoin(13,26));
			coins.add(createCoin(17,20));
			coins.add(createCoin(18,20));
			
			//Top right coins
			coins.add(createCoin(21,4));
			coins.add(createCoin(26,2));
			coins.add(createCoin(29,2));
			coins.add(createCoin(31,5));
			coins.add(createCoin(34,5));
			coins.add(createCoin(36,8));
			coins.add(createCoin(33,11));
			coins.add(createCoin(31,11));
			coins.add(createCoin(29,11));
			coins.add(createCoin(27,11));
			coins.add(createCoin(25,11));
			coins.add(createCoin(36,14));
			
			//Bottom right coins
			coins.add(createCoin(38,17));
			coins.add(createCoin(33,17));
			coins.add(createCoin(28,19));
			coins.add(createCoin(25,20));
			coins.add(createCoin(18,26));
			coins.add(createCoin(22,26));
			coins.add(createCoin(26,26));
			coins.add(createCoin(30,26));

			add(coins);
            add(_blocks);

			//Create player (a red box)
			player = new FlxSprite(FlxG.width/2 - 5, 0);
			player.createGraphic(10,12,new Color(0xaa, 0x11, 0x11));
            //player.loadGraphic(FlxG.Content.Load<Texture2D>("Mode/Untitled"), false, false, 10, 12);
			player.maxVelocity.X = 80;
			player.maxVelocity.Y = 200;
			player.acceleration.Y = 200;
			player.drag.X = player.maxVelocity.X*4;
			add(player);
			
			score = new FlxText(2,2,80);
			score.shadow = Color.Black;
			score.text = "SCORE: "+(coins.countDead()*100);
			add(score);
			
			status = new FlxText(FlxG.width-160-2,2,160);
			status.shadow = Color.Black;
			status.alignment = FlxJustification.Right;
			switch(FlxG.score)
			{
				case 0: status.text = "Collect coins."; break;
				case 1: status.text = "Aww, you died!"; break;
			}
			add(status);
		}

		//creates a new coin located on the specified tile
		public FlxSprite createCoin(int X, int Y)
		{
			return new FlxSprite(X*8+3,Y*8+2).createGraphic(2,4, new Color(0xff, 0xff, 0x00));
		}
        public FlxSprite createBlock(int X, int Y)
        {
            FlxSprite s = new FlxSprite(X * 8, Y * 8);
            s.createGraphic(8,8, new Color(0x80, 0x80, 0x40)).@fixed = true;
            return s;
        }

		override public void update()
		{
            PlayerIndex pi;

			//Player movement and controls
			player.acceleration.X = 0;
			if(FlxG.keys.isKeyDown(Keys.Left, FlxG.controllingPlayer, out pi))
				player.acceleration.X = -player.maxVelocity.X*4;
            if (FlxG.keys.isKeyDown(Keys.Right, FlxG.controllingPlayer, out pi))
				player.acceleration.X = player.maxVelocity.X*4;
            if (FlxG.keys.isKeyDown(Keys.Space, FlxG.controllingPlayer, out pi) && player.onFloor)
				player.velocity.Y = -player.maxVelocity.Y/2;
			
			//Updates all the objects appropriately
			base.update();
			
			//Check for player lose conditions
			if(player.y > FlxG.height)
			{
				FlxG.score = 1; //sets status.text to "Aww, you died!"
				//FlxG.state = new PlayState();
                FlxG.state = new EZPlatformer.PlayState();
				return;
			}
			
			//Check if player collected a coin or coins this frame
			FlxU.overlap(coins,player,getCoin);
			
			//Check to see if the player touched the exit door this frame
			FlxU.overlap(exit,player,win);
			
			//Finally, bump the player up against the level
            FlxU.collide(level, player);
            FlxU.collide(_blocks, player);

        }
		
		//Called whenever the player touches a coin
        public bool getCoin(object sender, FlxSpriteCollisionEvent e)
		{
			e.Object1.kill();
			score.text = "SCORE: "+(coins.countDead()*100);
			if(coins.countLiving() == 0)
			{
				status.text = "Find the exit.";
				exit.exists = true;
			}
            return true;
		}

		//Called whenever the player touches the exit
        public bool win(object sender, FlxSpriteCollisionEvent e)
		{
			status.text = "Yay, you won!";
			score.text = "SCORE: 5000";
			e.Object2.kill();
            return true;
		}

    }
}
