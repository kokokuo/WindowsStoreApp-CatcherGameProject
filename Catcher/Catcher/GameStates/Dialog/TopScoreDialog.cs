using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Catcher.GameStates;
using Catcher.GameObjects;
using Catcher.FontManager;
using Catcher.FileStorageHelper;
using Catcher.TextureManager;
namespace Catcher.GameStates.Dialog
{
    public class TopScoreDialog : GameDialog
    {
        SpriteFont topSavedPeopleNumberFont;
        GameRecordData readData;
        int topSavedPeoepleNumber;
        public TopScoreDialog(GameState pCurrentState)
            : base(pCurrentState) 
        { 
        }

        public override void BeginInit()
        {
            backgroundPos = new Vector2(0,0);
            closeButton = new Button(base.currentState, base.countId++, 0, 0);
            AddGameObject(closeButton);
            topSavedPeoepleNumber = 0;

            

            base.isInit = true;
        }
        public override void LoadResource()
        {
            background = currentState.GetTexture2DList(TextureManager.TexturesKeyEnum.TOP_SCORE_DIALOG_BACK)[0];
            topSavedPeopleNumberFont = currentState.GetSpriteFontFromKeyByGameState(SpriteFontKeyEnum.TOP_SCORE_FONT);
            closeButton.LoadResource(TexturesKeyEnum.TOP_SCORE_CLOSE_BUTTON);
            base.LoadResource(); //載入CloseButton 圖片資源
            base.isLoadContent = true;
        }
        async public override void Update()
        {
            var file = await StorageHelper.ReadTextFromFile("record.catcher");
            if (!String.IsNullOrEmpty(file))
            {
                readData = JsonHelper.Deserialize<GameRecordData>(file);
                topSavedPeoepleNumber = readData.SavePeopleNumber;
            }

            TouchCollection tc = base.currentState.GetCurrentFrameTouchCollection();
            bool isClickClose = false;
            if (tc.Count > 0){
                //所有當下的觸控點去判斷有無點到按鈕
                foreach (TouchLocation touchLocation in tc) {
                    if (!isClickClose)
                        isClickClose = closeButton.IsPixelClick(touchLocation.Position.X, touchLocation.Position.Y);
                }

                TouchLocation tL = base.currentState.GetTouchLocation();
                if (tL.State == TouchLocationState.Released)
                {
                    //關閉按鈕
                    if (closeButton.IsPixelClick(tL.Position.X, tL.Position.Y))
                    {
                        base.CloseDialog();//透過父類別來關閉
                    }
                }

                //清除TouchQueue裡的觸控點，因為避免Dequeue時候並不在Dialog中，因此要清除TouchQueue。
                base.currentState.ClearTouchQueue();
            }

            base.Update(); //更新遊戲元件
        }
        public override void Draw()
        {
            gameSateSpriteBatch.Draw(background, backgroundPos, Color.White);
            gameSateSpriteBatch.DrawString(topSavedPeopleNumberFont, topSavedPeoepleNumber.ToString() + "\nPeople", new Vector2(background.Width / 2, background.Height / 2 - topSavedPeopleNumberFont.MeasureString(topSavedPeoepleNumber.ToString()).Y / 2), Color.Black);
            base.Draw(); //繪製遊戲元件
        }
    }
}
