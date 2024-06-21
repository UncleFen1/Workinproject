using System;

namespace UI
{
    public class UIGameExecutor : IUIGameExecutor
    {
        public Action OnSetGndPanel { get { return onSetGndPanel; } set { onSetGndPanel = value; } }
        public Action onSetGndPanel;

        public Action OnRoulProper3Panel { get { return onRoulProper3Panel; } set { onRoulProper3Panel = value; } }
        public Action onRoulProper3Panel;

        public Action OnRoulProper4Panel { get { return onRoulProper4Panel; } set { onRoulProper4Panel = value; } }
        public Action onRoulProper4Panel;

        public Action OnRoulProper8Panel { get { return onRoulProper8Panel; } set { onRoulProper8Panel = value; } }
        public Action onRoulProper8Panel;

        public void SetGndPanel()
        {
            onSetGndPanel?.Invoke();
        }

        public void RoulProper3Button()
        {
            onRoulProper3Panel?.Invoke();
        }

        public void RoulProper4Button()
        {
            onRoulProper4Panel?.Invoke();
        }

        public void RoulProper8Button()
        {
            onRoulProper8Panel?.Invoke();
        }
    }
}