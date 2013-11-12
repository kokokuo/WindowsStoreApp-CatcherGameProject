﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Catcher.GameStates;
using Catcher.GameObjects;
using Catcher.TextureManager;
namespace Catcher.GameStates.Dialog
{
    public class HowToPlayDialog : GameDialog
    {
        Button leftButton;
        Button rightButton;

        //頁面
        TextureLayer page1Texture;
        TextureLayer page2Texture;
        TextureLayer page3Texture;

        int pageStart;
        int pageEnd;

        public HowToPlayDialog(GameState pCurrentState)
            : base(pCurrentState)
        { 
        }
        public override void BeginInit()
        {
            pageStart = 8;
            pageEnd = 10;
            backgroundPos = new Vector2(0, 0);
            closeButton = new Button(base.currentState, base.countId++, 0,0);
            leftButton = new Button(base.currentState, base.countId++, 0, 0);
            rightButton = new Button(base.currentState, base.countId++, 0, 0);

            page1Texture = new TextureLayer(base.currentState, base.countId++, 0,0);
            page2Texture = new TextureLayer(base.currentState, base.countId++, 0, 0);
            page3Texture= new TextureLayer(base.currentState, base.countId++, 0, 0);

            stCurrent = DialogStateEnum.STATE_HOW_TO_PLAY;
            gtCurrent = DialogGameObjectEnum.HOWTOPLAY_PAGE1;

            AddgameObject(DialogGameObjectEnum.HOWTOPLAY_PAGE1,new GameObject []{page1Texture,rightButton});
            AddgameObject(DialogGameObjectEnum.HOWTOPLAY_PAGE2, new GameObject[] { page2Texture,leftButton,rightButton });
            AddgameObject(DialogGameObjectEnum.HOWTOPLAY_PAGE3, new GameObject[] { page3Texture,leftButton });

            AddObjectTable(DialogStateEnum.STATE_HOW_TO_PLAY, GetDialogGameObject);

            AddGameObject(closeButton);
            base.isInit=true;


        }
        public override void LoadResource()
        {
            background = currentState.GetTexture2DList(TextureManager.TexturesKeyEnum.HOWTOPLAY_BACKGROUND)[0];
            leftButton.LoadResource(TexturesKeyEnum.DIALOG_LEFT_BUTTON);
            rightButton.LoadResource(TexturesKeyEnum.DIALOG_RIGHT_BUTTON);
            page1Texture.LoadResource(TexturesKeyEnum.HOWTOPLAY_PAGE1);
            page2Texture.LoadResource(TexturesKeyEnum.HOWTOPLAY_PAGE2);
            page3Texture.LoadResource(TexturesKeyEnum.HOWTOPLAY_PAGE3);
            base.LoadResource();
        }
        public override void Update()
        {

            if (!base.currentState.IsEmptyQueue())
            {
                stCurrent = DialogStateEnum.STATE_HOW_TO_PLAY;
                if(gtCurrent==DialogGameObjectEnum.EMPTY)
                gtCurrent = DialogGameObjectEnum.HOWTOPLAY_PAGE1;

                TouchCollection tc = base.currentState.GetCurrentFrameTouchCollection();
                bool isClickClose = false;
                bool isLeftButton = false;
                bool isRightButton = false;
                if (tc.Count > 0)
                {
                    //所有當下的觸控點去判斷有無點到按鈕
                    foreach (TouchLocation touchLocation in tc)
                    {
                        if (!isClickClose)
                            isClickClose = closeButton.IsPixelClick(touchLocation.Position.X, touchLocation.Position.Y);
                        if (!isLeftButton)
                            isLeftButton = leftButton.IsPixelClick(touchLocation.Position.X, touchLocation.Position.Y);
                        if (!isRightButton)
                            isRightButton = rightButton.IsPixelClick(touchLocation.Position.X, touchLocation.Position.Y);
                    }

                    //遊戲邏輯判斷
                    if (isClickClose)
                    {                    
                        base.CloseDialog(); //透過父類別來關閉
                    }

                    //按鈕方式2
                    //使用觸控單次點擊方式
                    TouchLocation tL = base.currentState.GetTouchLocation();
                    if (tL.State == TouchLocationState.Released)
                    {

                        //左邊按鈕
                        if (leftButton.IsPixelClick(tL.Position.X, tL.Position.Y))
                        {
                            if ((int)gtCurrent > pageStart)
                                gtCurrent--;

                        }

                        //右邊按鈕
                        if (rightButton.IsPixelClick(tL.Position.X, tL.Position.Y))
                        {
                            if ((int)gtCurrent < pageEnd)
                                gtCurrent++;
                        }
                    }

                    //清除TouchQueue裡的觸控點，因為避免Dequeue時候並不在Dialog中，因此要清除TouchQueue。
                    base.currentState.ClearTouchQueue();

                }
            }

            base.Update(); //更新遊戲元件
        }
        public override void Draw()
        {
            gameSateSpriteBatch.Draw(background, backgroundPos, Color.Wheat);
            base.Draw();
        }
    }
}
