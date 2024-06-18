using System;
using UnityEngine.UI;

namespace Texts
{
    [Serializable]
    public struct TextCollection: ITextCollection
    {
        public string NameObject { get; set; }
        public Text PoleTxt;
        public ModeTxt ModeTxt;
        public string RusText;
        public string EngText;
    }
}