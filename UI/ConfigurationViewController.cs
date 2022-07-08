using BeatSaberMarkupLanguage.MenuButtons;
using System;
using UnityEngine;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using System.Collections.Generic;
using TMPro;
using System.Linq;

namespace ShockwaveSuit.UI {
    public class ConfigurationViewController : BSMLResourceViewController {
        public override string ResourceName => "ShockwaveSuit.Views.ConfigurationView.bsml";
        
        [UIValue("enable-LEDs")]
        public bool EnableLEDs {
            get => ModPlugin.cfg.ledResponse;
            set {
                ModPlugin.cfg.ledResponse = value;
                ModPlugin.SaveModConfiguration();
            }
        }
        [UIValue("front-torso")]
        public bool FrontTorsoActivate
        {
            get => ModPlugin.cfg.frontTorso;
            set
            {
                ModPlugin.cfg.frontTorso = value;
                ModPlugin.SaveModConfiguration();
            }
        }
        [UIValue("back-torso")]
        public bool BackTorsoActivate
        {
            get => ModPlugin.cfg.backTorso;
            set
            {
                ModPlugin.cfg.backTorso = value;
                ModPlugin.SaveModConfiguration();
            }
        }
        [UIValue("legs")]
        public bool LegsActivate
        {
            get => ModPlugin.cfg.legs;
            set
            {
                ModPlugin.cfg.legs = value;
                ModPlugin.SaveModConfiguration();
            }
        }
        [UIValue("saber-pulse-delay")]
        public int SaberPulseRate {
            get => ModPlugin.cfg.saberPulseDelay;
            set {
                ModPlugin.cfg.saberPulseDelay = value;
                ModPlugin.SaveModConfiguration();
            }
        }

        [UIValue("haptics-intensity")]
        public float HapticsIntensity
        {
            get => ModPlugin.cfg.hapticsIntensity;
            set
            {
                ModPlugin.cfg.hapticsIntensity = value;
                ModPlugin.SaveModConfiguration();
            }
        }

        [UIValue("audio-haptics-intensity")]
        public float AudioHapticsIntensity { 
            get => ModPlugin.cfg.audioToHapticsIntensity;
            set
            {
                ModPlugin.cfg.audioToHapticsIntensity = value;
                ModPlugin.SaveModConfiguration();
            }
        }

        [UIValue("bass-ratio")]
        public float BassRatio
        {
            get => ModPlugin.cfg.bassRatio;
            set
            {
                ModPlugin.cfg.bassRatio =1- value +0.09f;
                ModPlugin.SaveModConfiguration();
            }
        }
        [UIValue("selected-haptics-mode")]
        public object SelectedHapticsMode {
            get {
                return HapticsModeList[(int)ModPlugin.cfg.hapticsMode];
            }

            set { 
               Enum.TryParse(value.ToString(), out ModPlugin.cfg.hapticsMode);
              ModPlugin.SaveModConfiguration();
            }
        }
        [UIValue("selected-audio-mode")]
        public object SelectedAudioMode
        {
            get
            {
                return AudioToHapticsModeList[(int)ModPlugin.cfg.AudioToHapticsMode];
            }

            set
            {
                Enum.TryParse(value.ToString(), out ModPlugin.cfg.AudioToHapticsMode);
                ModPlugin.SaveModConfiguration();
            }
        }
        [UIValue("haptics-mode-list")]
        List<object> HapticsModeList = Enum.GetNames(typeof(ModConfiguration.HapticsResponseMode)).ToList<object>();
        [UIValue("audio-mode-list")]
        List<object> AudioToHapticsModeList = Enum.GetNames(typeof(ModConfiguration.AudioToHapticsResponseMode)).ToList<object>();
        // <text id='ban-user-id'></text>
    }
}
