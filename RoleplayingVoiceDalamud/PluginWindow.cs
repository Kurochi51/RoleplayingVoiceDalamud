﻿using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using ImGuiNET;
using System.Numerics;

namespace RoleplayingVoice {
    public class PluginWindow : Window {
        private string testText = "";
        private Configuration configuration;
        private string apiKey = "";
        private string characterName = "";
        private string characterVoice = "";
        private string serverIP;

        public PluginWindow() : base("Roleplaying Voice Config") {
            IsOpen = true;
            Size = new Vector2(810, 520);
            SizeCondition = ImGuiCond.FirstUseEver;
        }

        public Configuration Configuration {
            get => configuration;
            set {
                configuration = value;
                if (configuration != null) {
                    serverIP = configuration.ConnectionIP != null ? configuration.ConnectionIP.ToString() : "";
                    apiKey = configuration.ApiKey != null ? configuration.ApiKey : "";
                    characterName = configuration.CharacterName != null ? configuration.CharacterName : "";
                    characterVoice = configuration.CharacterVoice != null ? configuration.CharacterVoice : "";
                }
            }
        }

        public DalamudPluginInterface PluginInteface { get; internal set; }

        public override async void Draw() {
            ImGui.Text("Server IP");
            ImGui.InputText("##serverIP", ref serverIP, 2000);

            ImGui.Text("Elevenlabs API Key");
            ImGui.InputText("##apiKey", ref apiKey, 2000);

            ImGui.Text("Character Name");
            ImGui.InputText("##characterName", ref characterName, 2000);

            ImGui.Text("Voice");
            ImGui.InputText("##characterVoice", ref characterVoice, 2000);

            if (ImGui.Button("Save")) {
                if (configuration != null) {
                    configuration.ConnectionIP = serverIP;
                    configuration.ApiKey = apiKey;
                    configuration.CharacterName = characterName;
                    configuration.CharacterVoice = characterVoice;
                    configuration.Save();
                    PluginInteface.SavePluginConfig(configuration);
                }
            }
        }
    }
}