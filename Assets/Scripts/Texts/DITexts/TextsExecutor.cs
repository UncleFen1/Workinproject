using System.Collections.Generic;

namespace Texts
{
    public enum ModeTxt
    {
        Rus,
        Eng
    }
public class TextsExecutor : ITexts
    {
        private List<TextCollection> listData = new List<TextCollection>();
        private TextCollection[] temp;
        public void SetData(TextCollection _textCollection)
        {
            listData.Add(_textCollection);
        }
        private List<TextCollection> GetData()
        {
            return listData;
        }
        public bool ClearData()
        {
            listData.Clear();
            if (listData.Count == 0) { return true; } else { return false; }
        }
        public TextCollection[] SetList()
        {
            MassivTexts<TextCollection> tempMassiv = new MassivTexts<TextCollection>();
            listData = GetData();
            if (temp != null) { tempMassiv.Clean(temp); }
            for (int i = 0; i < listData.Count; i++)
            {
                if (listData[i].NameObject!="")
                {
                    temp = tempMassiv.Creat(listData[i], temp);
                }
            }
            return temp;
        }
        public TextCollection SetObjectName(string _name)
        {
            listData = GetData();
            for (int i = 0; i < listData.Count; i++)
            {
                if (listData[i].NameObject == _name)
                {
                    return listData[i];
                }
            }
            return new TextCollection();
        }
        // public Construction[] SetEnemys()
        // {
        //     Masiv<Construction> tempMassiv = new Masiv<Construction>();
        //     listData = GetData();
        //     for (int i = 0; i < listData.Count; i++)
        //     {
        //         if (listData[i].TypeObject is TypeObject.Enemy)
        //         {
        //             temp = tempMassiv.Creat(listData[i], temp);
        //         }
        //     }
        //     return temp;
        // }
    }
}