using System;
using System.Collections.Generic;
using CSE_210_FinalProject.Services;
using CSE_210_FinalProject.Casting;

namespace CSE_210_FinalProject.Scripting
{
    public class UpdateBufferMenuAction : Action
    {
        InputService _inputService;
        private int _delay = 0;
        public UpdateBufferMenuAction(InputService inputService)
        {
            _inputService = inputService;
            
        }

        public override void Execute(Dictionary<string, List<Actor>> cast)
        {
            _delay -= 1;
            if (_delay < 0)
            {
                _delay = 0;
            }
            
            int direction = _inputService.MenuControl();
            
            if (direction != 0 && _delay <= 0)
            {

                MenuTextBox selectedBox = new MenuTextBox(true, "0");
                MenuTextBox newSelectedBox = new MenuTextBox(true, "0"); 

                foreach (MenuTextBox box in cast["inputboxes"])
                {
                    if (box.IsSelected())
                    {
                        selectedBox = box;
                    }
                }

                

            
                if (direction == 1)
                {
                    int selectedIndex = cast["inputboxes"].IndexOf(selectedBox);
                    selectedIndex -= 1;
                    if (selectedIndex < 0)
                    {
                        selectedIndex = cast["inputboxes"].Count - 1;
                    }
                    selectedBox.ChangeIfSelected(false);
                    

                    foreach (MenuTextBox box in cast["inputboxes"])
                    {
                        int index = cast["inputboxes"].IndexOf(box);
                        if (index == selectedIndex)
                        {
                            box.ChangeIfSelected(true);
                        }
                    }
                }

                if (direction == 2)
                {
                    int selectedIndex = cast["inputboxes"].IndexOf(selectedBox);
                    selectedIndex += 1;
                    if (selectedIndex >= cast["inputboxes"].Count)
                    {
                        selectedIndex = 0;
                    }
                    selectedBox.ChangeIfSelected(false);
                    

                    foreach (MenuTextBox box in cast["inputboxes"])
                    {
                        int index = cast["inputboxes"].IndexOf(box);
                        if (index == selectedIndex)
                        {
                            box.ChangeIfSelected(true);
                        }
                    }
                    
                }

                if (direction == 3)
                {
                    string text = selectedBox.GetText();
                    int num = int.Parse(text);
                    num -= 1;
                    if (num < 0)
                    {
                        num = 0;
                    }
                    if (selectedBox == cast["inputboxes"][0] && num == 0)
                    {
                        num = 1;
                    }
                    text = num.ToString();
                    selectedBox.SetText(text);

                    
                }

                if (direction == 4)
                {
                    string text = selectedBox.GetText();
                    int num = int.Parse(text);
                    num += 1;
                    if (num > 3)
                    {
                        num = 3;
                    }
                    text = num.ToString();
                    selectedBox.SetText(text);
                    
                }

                _delay = 10;
            }
        }

        
    }
}