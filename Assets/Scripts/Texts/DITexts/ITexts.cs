using System;

namespace Texts
{
    public interface ITexts
    {
        void SetData(TextCollection _textCollection);
        Action<TextCollection> OnSetText { get; set; }
    }
}