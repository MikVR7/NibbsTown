
namespace RightOrWrong
{
    public class Fact
    {
        public Fact(string text, bool isRight)
        {
            this.Text = text;
            this.IsRight = isRight;
        }
        public string Text { get; set; }
        public bool IsRight { get; set; }
        public bool IsAnsweredRight { get; set; }
    }
}