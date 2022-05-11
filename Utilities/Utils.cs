﻿using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Security.Principal;

namespace ShockwaveSuit.Utilities {
    public static class Utils {
        public static bool IsModInstalled(string ModName) {
            return IPA.Loader.PluginManager.GetPlugin(ModName) != null;
        }

        public static TEnum ToEnum<TEnum>(this string strEnumValue, TEnum defaultValue) {
            if (!Enum.IsDefined(typeof(TEnum), strEnumValue))
                return defaultValue;

            return (TEnum)Enum.Parse(typeof(TEnum), strEnumValue);
        }
        public static bool IsDirectoryEmpty(string path) {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        public static void GrantAccess(string file) {
            bool exists = System.IO.Directory.Exists(file);
            if (!exists) {
                DirectoryInfo di = System.IO.Directory.CreateDirectory(file);
                //        Console.WriteLine("The Folder is created Sucessfully");
            } else {
                //        Console.WriteLine("The Folder already exists");
            }
            DirectoryInfo dInfo = new DirectoryInfo(file);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.Modify, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            dInfo.SetAccessControl(dSecurity);

        }
        public static string TrimEnd(this string text, string value) {
            if (!text.EndsWith(value))
                return text;

            return text.Remove(text.LastIndexOf(value));
        }

        public static TextMeshProUGUI CreateText(RectTransform parent, string text, Vector2 anchoredPosition) {
            return CreateText(parent, text, anchoredPosition, new Vector2(60f, 10f));
        }

        public static TextMeshProUGUI CreateText(RectTransform parent, string text, Vector2 anchoredPosition, Vector2 sizeDelta) {
            GameObject gameObj = new GameObject("CustomUIText");
            gameObj.SetActive(false);

            TextMeshProUGUI textMesh = gameObj.AddComponent<TextMeshProUGUI>();
            textMesh.font = UnityEngine.Object.Instantiate(Resources.FindObjectsOfTypeAll<TMP_FontAsset>().First(t => t.name == "Teko-Medium SDF No Glow"));
            textMesh.rectTransform.SetParent(parent, false);
            textMesh.text = text;
            textMesh.fontSize = 4;
            textMesh.color = Color.white;

            textMesh.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            textMesh.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            textMesh.rectTransform.sizeDelta = sizeDelta;
            textMesh.rectTransform.anchoredPosition = anchoredPosition;

            gameObj.SetActive(true);
            return textMesh;
        }

        public static Sprite LoadSpriteRaw(byte[] image, float PixelsPerUnit = 100.0f) {
            return LoadSpriteFromTexture(LoadTextureRaw(image), PixelsPerUnit);
        }

        public static Sprite LoadSpriteFromTexture(Texture2D SpriteTexture, float PixelsPerUnit = 100.0f) {
            if (SpriteTexture)
                return Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), PixelsPerUnit);
            return null;
        }

        public static Sprite LoadSpriteFromFile(string FilePath, float PixelsPerUnit = 100.0f) {
            return LoadSpriteFromTexture(LoadTextureFromFile(FilePath), PixelsPerUnit);
        }

        public static Sprite LoadSpriteFromResources(string resourcePath, float PixelsPerUnit = 100.0f) {
            return LoadSpriteRaw(GetResource(Assembly.GetCallingAssembly(), resourcePath), PixelsPerUnit);
        }

        public static byte[] GetResource(Assembly asm, string ResourceName) {
            System.IO.Stream stream = asm.GetManifestResourceStream(ResourceName);
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, (int)stream.Length);
            return data;
        }

        public static void PrintHierarchy(Transform transform, string spacing = "|-> ") {
            spacing = spacing.Insert(1, "  ");
            var tempList = transform.Cast<Transform>().ToList();
            foreach (var child in tempList) {
                Console.WriteLine($"{spacing}{child.name}");
                PrintHierarchy(child, "|" + spacing);
            }
        }

        public static Texture2D LoadTextureRaw(byte[] file) {
            if (file.Count() > 0) {
                Texture2D Tex2D = new Texture2D(2, 2);
                if (Tex2D.LoadImage(file))
                    return Tex2D;
            }
            return null;
        }

        public static Texture2D LoadTextureFromFile(string FilePath) {
            if (File.Exists(FilePath))
                return LoadTextureRaw(File.ReadAllBytes(FilePath));

            return null;
        }

        public static Texture2D LoadTextureFromResources(string resourcePath) {
            return LoadTextureRaw(GetResource(Assembly.GetCallingAssembly(), resourcePath));
        }
    }
}