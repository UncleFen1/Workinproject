namespace Texts
{
    public interface ITexts
    {
        void SetData(TextCollection _textCollection);
        TextCollection SetObjectName(string _name);
        TextCollection[] SetList();
    }
}