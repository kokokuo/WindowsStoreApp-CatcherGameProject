using System;
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
    public class GameOverState : GameState
    {
        public GameOverState(MainGame gMainGame)
            : base(gMainGame)
        {
        }
        public override void LoadResource()
        {
            throw new NotImplementedException();
        }

        public override void BeginInit()
        {
            throw new NotImplementedException();
        }
        public override void Update()
        {
            base.Update(); //會把　AddGameObject方法中加入的物件作更新
        }
        public override void Draw()
        {
            base.Draw(); //會把　AddGameObject方法中加入的物件作繪製
        }
    }
}
