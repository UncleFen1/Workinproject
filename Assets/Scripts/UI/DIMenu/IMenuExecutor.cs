using System;

namespace UI
{
    public interface IMenuExecutor
    {
        Action OnButtonPanel { get; set; }
        Action OnSettingsPanel { get; set; }
        Action OnInfoPanel { get; set; }
        Action OnRezultPanel { get; set; }
        void ButtonPanel();
        void SettingsPanel();
        void InfoPanel();
        void RezultPanel();

        #region Click
        Action OnAudioClickl { get; set; }
        void AudioClick();
        #endregion

    }
}