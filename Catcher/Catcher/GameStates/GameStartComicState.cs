﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Catcher.GameObjects;
using Catcher.TextureManager;
using System.Diagnostics;
namespace Catcher.GameStates
{
    public class GameStartComicState :GameState
    {
        float totaleEapsed;
        const float defaultElapsedTime = 2000; //2秒
        public GameStartComicState(MainGame gMainGame)
            : base(gMainGame)
        {
            base.x = 0; base.y = 0;
            base.backgroundPos = new Vector2(base.x, base.y);
        }

        public override void BeginInit()
        {
            totaleEapsed = 0f;

            this.isInit = true;
        }
        public override void LoadResource()
        {
            base.background = base.GetTexture2DList(TexturesKeyEnum.GAME_START_COMIC_BACK)[0];
        }

        public override void Update()
        {
            totaleEapsed += base.GetTimeSpan().Milliseconds;
            if (totaleEapsed > defaultElapsedTime) {
                totaleEapsed -= defaultElapsedTime;
                Debug.WriteLine("CLICK!! STATE_PLAYGAME");
                mainGame.SetNextGameState(GameStateEnum.STATE_PLAYGAME);
            }
            base.Update();
        }

        public override void Draw()
        {
            gameSateSpriteBatch.Draw(base.background, base.backgroundPos, Color.White);
            base.Draw();
        }
    }
}
