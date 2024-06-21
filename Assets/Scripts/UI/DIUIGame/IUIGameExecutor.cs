using System;

namespace UI
{
    public interface IUIGameExecutor
    {
        #region Buttons
        Action OnSetGndPanel { get; set; }
        Action OnRoulProper3Panel { get; set; }
        Action OnRoulProper4Panel { get; set; }
        Action OnRoulProper8Panel { get; set; }
        void SetGndPanel();
        void RoulProper3Button();
        void RoulProper4Button();
        void RoulProper8Button();
        #endregion

    }
}