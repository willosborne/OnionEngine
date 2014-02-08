﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnionEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace OnionTestGame
{
    class Player : Entity
    {
        float SwordSwipeTimer = 0;
        Entity SwordSwipe;

        int Direction = 0;

        int Lives = 10;
        
        public override void Init(Stage stage)
        {
            MakeGraphic(20, 20, Color.Green);
            
            Position = new Vector2(400, 200);
            MaxVelocity.X = 200;
            MaxVelocity.Y = 200;            
            Drag.X = 20;
            Drag.Y = 20;

            CentreOrigin();
            MakeDefaultHitbox();
            Type = "player";

            SwordSwipe = new Entity();
            SwordSwipe.MakeGraphic(30, 30, Color.Blue);
            SwordSwipe.Graphic = OE.Content.Load<Texture2D>("sord png");
            SwordSwipe.Type = "sword";
            SwordSwipe.CentreOrigin();
            SwordSwipe.MakeDefaultHitbox();

            base.Init(stage);
        }

        public override void Update()
        {
            SwordSwipe.Position = Position + new Vector2(
                20 * (float)Math.Cos(MathHelper.PiOver2 * Direction),
                20 * (float)Math.Sin(MathHelper.PiOver2 * Direction));
            SwordSwipe.Angle = MathHelper.PiOver2 * Direction;

            Acceleration.X = 0;
            Acceleration.Y = 0;
            if (OE.Input.Check(Keys.Left))
            {
                Direction = 2;
                Acceleration.X = -20;                
            }
            if (OE.Input.Check(Keys.Right))
            {
                Direction = 0;
                Acceleration.X = 20;
            }
            if (OE.Input.Check(Keys.Up))
            {
                Direction = 3;
                Acceleration.Y = -20;
            }
            if (OE.Input.Check(Keys.Down))
            {
                Direction = 1;
                Acceleration.Y = 20;
            }
            if (OE.Input.Pressed(Keys.Z))
            {
                SwordSwipeTimer = 0.2f;
                
                Stage.Add(SwordSwipe);
                //SwordSwipe.Flip();
            }

            if (Collide("enemy") != null)
            {                
                Position = new Vector2(400, 200);
                Lives--;
                int count = ((TestStage)OE.UserData[0]).Enemies.Count;

                float step = MathHelper.TwoPi / count;
                for (int i = 0; i < count; i++)
                {
                    Vector2 pos = new Vector2( 200 * (float)Math.Cos(i * step),
                                              200 * (float)Math.Sin(i * step));
                    ((TestStage)OE.UserData[0]).Enemies[i].Position = Position + pos;

                }
                    //200 * (float)Math.Cos(step
            }

            SwordSwipeTimer -= OE.delta;
            if (SwordSwipeTimer <= 0)
            {
                Stage.Remove(SwordSwipe);
            }

            //Angle = MathHelper.PiOver4;

            base.Update();
        }
    }
}