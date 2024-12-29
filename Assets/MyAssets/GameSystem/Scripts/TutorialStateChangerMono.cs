#nullable enable
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MyAssets.GameSystem.Scripts
{
    public class TutorialStateChangerMono:MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI titleText = null!;
        [SerializeField] TextMeshProUGUI tutorialText = null!;
        [SerializeField] TextMeshProUGUI countText = null!;
        [SerializeField] GameObject waitStartImageObject = null!;
        [SerializeField] GameObject tutorialParentObject = null!;

        int _currentTutorialTextIndex;
        
        List<string> _titleList = new List<string>()
        {
            "『達人の教え』",
            "『操作方法』",
            "『ゾーンの秘密』",
            "『コツ』"
        };
        
        List<string> _tutorialTextList = new List<string>()
        {
            "私は『いないいないばあ』の達人だ。\nまずは基本を教えよう。",
            "泣き出すギリギリまで『いないいない』して、\n解放の『ばあ』で最高の喜びを与える。\nこれが極意だ！",
            "だが、大人を喜ばせるには少し違う。\n彼らは顔が隠れても不安にはならない。\n『いないいない』するのはお金だ！",
            "左クリック又はEnter：手を開いて閉じる。\n右クリック又はSpace：隠すものを「顔」と「お金」で切り替える。",
            "さらに、3回連続でGood以上の『ばあ』を決めると、\nアスリートが『ゾーン』に入るように、\n私たちも一時的に完璧な状態になれる。",
            "ゾーン中はどれだけ早く『ばあ』をしても最高の喜びを与えられるぞ！\nだが注意しろ。\n顔とお金を間違えるとゾーンはすぐに終わってしまう。",
            "タイミングを見極めろ：『ばあ』は泣き出すギリギリで！\n顔とお金を切り替えろ：相手に合わせて「顔」か「お金」を選べ。\nゾーンを目指せ：連続でGoodを決めて完璧な状態に入ろう！",
            "さあ、私の教えを実践し、君も達人への道を進むのだ！"
        };

        void Awake()
        {
            if (GameInfoHolderStatic.IsRequiredTutorial)
            {
                waitStartImageObject.SetActive(false);
                tutorialParentObject.SetActive(true);
                
                tutorialText.text = _tutorialTextList[_currentTutorialTextIndex];

                ChangeCountText();
                CheckTitleText();
            }
        }

        void Update()
        {
            if (GameInfoHolderStatic.IsRequiredTutorial)
            {
                if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
                    GoNextText();
                else if(Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space))
                    BackPreText();
            }
        }

        void GoNextText()
        {
            if (_currentTutorialTextIndex == _tutorialTextList.Count-1)
            {
                EndTutorial();
            }
            
            _currentTutorialTextIndex += 1;
            _currentTutorialTextIndex = Mathf.Min(_tutorialTextList.Count - 1, _currentTutorialTextIndex);

            tutorialText.text = _tutorialTextList[_currentTutorialTextIndex];

            ChangeCountText();
            CheckTitleText();
        }

        void BackPreText()
        {
            _currentTutorialTextIndex -= 1;
            _currentTutorialTextIndex = Mathf.Max(0, _currentTutorialTextIndex);

            tutorialText.text = _tutorialTextList[_currentTutorialTextIndex];

            ChangeCountText();
            CheckTitleText();
        }

        void ChangeCountText()
        {
            countText.text = $"{_currentTutorialTextIndex + 1}/{_tutorialTextList.Count}";
        }

        void CheckTitleText()
        {
            var titleTextValue = "";
            if (_currentTutorialTextIndex <= 2)
            {
                titleTextValue = _titleList[0];
            }
            else if (_currentTutorialTextIndex <= 3)
            {
                titleTextValue = _titleList[1];
            }
            else if (_currentTutorialTextIndex <= 5)
            {
                titleTextValue = _titleList[2];
            }
            else
            {
                titleTextValue = _titleList[3];
            }

            titleText.text = titleTextValue;
        }

        void EndTutorial()
        {
            tutorialParentObject.SetActive(false);
            waitStartImageObject.SetActive(true);
            GameInfoHolderStatic.IsRequiredTutorial = false;
        }
    }
}