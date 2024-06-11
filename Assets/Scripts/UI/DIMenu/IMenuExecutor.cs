using System;

namespace UI
{
    public interface IMenuExecutor
    {
        #region MainMenu
        Action OnButtonPanel { get; set; }
        Action OnSettingsPanel { get; set; }
        Action OnInfoPanel { get; set; }
        Action OnRezultPanel { get; set; }
        void ButtonPanel();
        void SettingsPanel();
        void InfoPanel();
        void RezultPanel();
        #endregion

        #region Lvl
        Action OnGndPanel{ get; set; }
        Action OnButtonLvlPanel{ get; set; }
        void ButtonLvlPanel();
        void GndPanel();
        
        #endregion

        #region Click
        Action OnAudioClickl { get; set; }
        void AudioClick();
        #endregion

    }
}