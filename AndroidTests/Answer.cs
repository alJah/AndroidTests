using System;
using System.Collections.Generic;
using System.Text;

namespace AndroidTests
{
    public class Answer
    {
        public string Text { get; set; }
        public bool IsBingo { get; set; }
        public Answer(string text, bool key)
        {
            Text = text; 
            IsBingo = key;
        }
    }
}
