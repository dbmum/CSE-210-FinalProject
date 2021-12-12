using System;
using System.Collections.Generic;
using CSE_210_FinalProject.Services;
using CSE_210_FinalProject.Scripting;

namespace CSE_210_FinalProject.Casting
{
    public class MenuTextBox : Actor
    {
        private bool _writable;
        private bool _selected = false;

        public MenuTextBox(bool writable, string text)
        {
            SetWidth(Constants.TEXT_BOX_WIDTH);
            SetHeight(Constants.TEXT_BOX_HEIGHT);
            SetText(text);

            _writable = writable;
            
        }

        public void ChangeIfSelected(bool selected)
        {
            _selected = selected;
        }

        public bool IsSelected()
        {
            return _selected;
        }
    }
}
