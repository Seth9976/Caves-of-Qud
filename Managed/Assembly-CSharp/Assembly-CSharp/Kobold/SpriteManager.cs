using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using ConsoleLib.Console;
using UnityEngine;
using XRL;
using XRL.Collections;
using XRL.Core;

namespace Kobold
{
	// Token: 0x02000363 RID: 867
	[HasModSensitiveStaticCache]
	public static class SpriteManager
	{
		// Token: 0x06002038 RID: 8248 RVA: 0x0009DFDA File Offset: 0x0009C1DA
		private static GameObject CloneSpritePrefab()
		{
			if (SpriteManager._BaseSpritePrefab == null)
			{
				SpriteManager._BaseSpritePrefab = Resources.Load("KoboldBaseSprite") as GameObject;
				global::UnityEngine.Object.DontDestroyOnLoad(SpriteManager._BaseSpritePrefab);
			}
			return global::UnityEngine.Object.Instantiate<GameObject>(SpriteManager._BaseSpritePrefab);
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x0009E011 File Offset: 0x0009C211
		public static Shader GetShaderMode(int n)
		{
			if (SpriteManager.Shaders == null)
			{
				SpriteManager.Shaders = new Shader[2];
				SpriteManager.Shaders[0] = Shader.Find("Kobold/Alpha Blended Dual Color");
				SpriteManager.Shaders[1] = Shader.Find("Kobold/Alpha Blended Truecolor");
			}
			return SpriteManager.Shaders[n];
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x0009E04E File Offset: 0x0009C24E
		private static GameObject CloneSplitSpritePrefab()
		{
			if (SpriteManager._BaseSplitSpritePrefab == null)
			{
				SpriteManager._BaseSplitSpritePrefab = Resources.Load("KoboldBaseSlicedSprite") as GameObject;
				global::UnityEngine.Object.DontDestroyOnLoad(SpriteManager._BaseSplitSpritePrefab);
			}
			return global::UnityEngine.Object.Instantiate<GameObject>(SpriteManager._BaseSplitSpritePrefab);
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x0009E085 File Offset: 0x0009C285
		public static Sprite GetUnitySprite(string path)
		{
			return SpriteManager.GetUnitySprite(SpriteManager.GetTextureInfo(path, true));
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x0009E093 File Offset: 0x0009C293
		public static Sprite GetUnitySprite(Texture2D texture)
		{
			return Sprite.Create(texture, new Rect(0f, 0f, (float)texture.width, (float)texture.height), new Vector2(0.5f, 0.5f));
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x0009E0C8 File Offset: 0x0009C2C8
		public static Sprite GetUnitySprite(exTextureInfo info)
		{
			Sprite sprite;
			if (SpriteManager.SpriteMap.TryGetValue(info, out sprite))
			{
				return sprite;
			}
			Texture2D texture = info.texture;
			Texture2D texture2D = new Texture2D(info.width, info.height, TextureFormat.ARGB32, false);
			texture2D.filterMode = global::UnityEngine.FilterMode.Point;
			Color[] pixels = texture.GetPixels(info.x, info.y, info.width, info.height, 0);
			texture2D.SetPixels(pixels);
			texture2D.Apply();
			sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f), 40f);
			SpriteManager.SpriteMap.Add(info, sprite);
			return sprite;
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x0009E178 File Offset: 0x0009C378
		public static void SetSprite(GameObject Sprite, string Path)
		{
			Sprite.GetComponent<ex3DSprite2>().textureInfo = SpriteManager.GetTextureInfo(Path, true);
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x0009E18C File Offset: 0x0009C38C
		public static Vector2i GetSpriteSize(string Path)
		{
			exTextureInfo textureInfo = SpriteManager.GetTextureInfo(Path, true);
			Debug.Log(string.Concat(new string[]
			{
				Path,
				" ",
				textureInfo.rawWidth.ToString(),
				"x",
				textureInfo.rawWidth.ToString(),
				"   ",
				textureInfo.trim_x.ToString(),
				"x",
				textureInfo.trim_y.ToString()
			}));
			return new Vector2i(textureInfo.width, textureInfo.height);
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06002040 RID: 8256 RVA: 0x0009E21F File Offset: 0x0009C41F
		public static exTextureInfo InvalidTexture
		{
			get
			{
				return SpriteManager.InvalidInfo;
			}
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x0009E298 File Offset: 0x0009C498
		public static void Initialize()
		{
			using (SpriteManager.InfoLock.TakeWriteLock())
			{
				if (SpriteManager.InfoMap == null)
				{
					SpriteManager.InfoMap = new StringMap<exTextureInfo>();
					SpriteManager.PathMap = new StringMap<exTextureInfo>();
					SpriteManager.KoboldMap = new StringMap<string>();
					SpriteManager.SpriteMap = new Dictionary<exTextureInfo, Sprite>();
					if (!GameManager.IsOnUIContext())
					{
						GameManager.Instance.uiQueue.awaitTask(new Action(SpriteManager.Load));
					}
					else
					{
						SpriteManager.Load();
					}
				}
			}
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x0009E32C File Offset: 0x0009C52C
		private static void Load()
		{
			MemoryHelper.GCCollect();
			KoboldDatabaseScriptable koboldDatabaseScriptable = Resources.Load<KoboldDatabaseScriptable>("KoboldDatabase");
			if (koboldDatabaseScriptable == null)
			{
				exTextureInfo[] array = Resources.LoadAll<exTextureInfo>("TextureInfo");
				SpriteManager.InfoMap.EnsureCapacity(array.Length);
				SpriteManager.PathMap.EnsureCapacity(array.Length);
				foreach (exTextureInfo exTextureInfo in array)
				{
					if (exTextureInfo != null)
					{
						try
						{
							SpriteManager.InfoMap[SpriteManager.GetKey(exTextureInfo.name)] = exTextureInfo;
						}
						catch (Exception ex)
						{
							Debug.Log("Error adding - " + exTextureInfo.name + " ... " + ex.Message);
						}
					}
				}
			}
			else
			{
				SpriteManager.InfoMap.EnsureCapacity(koboldDatabaseScriptable.koboldTextureInfos.Length);
				SpriteManager.KoboldMap.EnsureCapacity(koboldDatabaseScriptable.koboldTextureInfos.Length);
				SpriteManager.PathMap.EnsureCapacity(koboldDatabaseScriptable.koboldTextureInfos.Length);
				foreach (string text in koboldDatabaseScriptable.koboldTextureInfos)
				{
					if (text.IsNullOrEmpty())
					{
						Debug.LogWarning("Info in koboldTextureInfos is null");
					}
					else
					{
						try
						{
							string text2 = SpriteManager.GetKey(text).ToString();
							SpriteManager.InfoMap[text2] = null;
							SpriteManager.KoboldMap.TryAdd(text2, text);
						}
						catch (Exception ex2)
						{
							MetricsManager.LogException("SpriteManager", ex2, "game_exception");
						}
					}
				}
			}
			foreach (ModInfo modInfo in ModManager.ActiveMods)
			{
				foreach (ModFile modFile in modInfo.Files)
				{
					if (modFile.Type == ModFileType.Sprite)
					{
						try
						{
							int num = modFile.RelativeName.LastSubdirectoryIndex("textures", StringComparison.Ordinal);
							if (num != -1)
							{
								ReadOnlySpan<char> key = SpriteManager.GetKey(modFile.RelativeName.AsSpan(num + 9));
								Texture2D texture2D = new Texture2D(modInfo.TextureConfiguration.TextureWidth, modInfo.TextureConfiguration.TextureHeight);
								byte[] array3 = File.ReadAllBytes(modFile.OriginalName);
								texture2D.LoadImage(array3);
								texture2D.filterMode = global::UnityEngine.FilterMode.Point;
								exTextureInfo exTextureInfo2 = ScriptableObject.CreateInstance<exTextureInfo>();
								exTextureInfo2.texture = texture2D;
								exTextureInfo2.width = texture2D.width;
								exTextureInfo2.height = texture2D.height;
								exTextureInfo2.x = 0;
								exTextureInfo2.y = 0;
								exTextureInfo2.ShaderMode = modInfo.TextureConfiguration.ShaderMode;
								SpriteManager.InfoMap[key] = exTextureInfo2;
							}
						}
						catch (Exception ex3)
						{
							MetricsManager.LogException("Mod texture load", ex3, "game_exception");
						}
					}
				}
			}
			SpriteManager.PopulateInvalidTexture();
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x0009E684 File Offset: 0x0009C884
		private static void PopulateInvalidTexture()
		{
			exTextureInfo exTextureInfo;
			if ((exTextureInfo = SpriteManager.InvalidInfo) == null)
			{
				exTextureInfo = (SpriteManager.InvalidInfo = ScriptableObject.CreateInstance<exTextureInfo>());
			}
			ReadOnlySpan<char> key = SpriteManager.GetKey(SpriteManager.InvalidTexturePath);
			exTextureInfo exTextureInfo2 = SpriteManager.InfoMap[key];
			if (exTextureInfo2 == null)
			{
				exTextureInfo2 = (SpriteManager.InfoMap[key] = SpriteManager.LoadTextureInfo(key));
			}
			exTextureInfo exTextureInfo3 = exTextureInfo;
			exTextureInfo3.name = "InvalidTexture";
			exTextureInfo3.texture = exTextureInfo2.texture;
			exTextureInfo3.width = exTextureInfo2.width;
			exTextureInfo3.height = exTextureInfo2.height;
			exTextureInfo3.rawWidth = exTextureInfo2.rawWidth;
			exTextureInfo3.rawHeight = exTextureInfo2.rawHeight;
			exTextureInfo3.x = exTextureInfo2.x;
			exTextureInfo3.y = exTextureInfo2.y;
			exTextureInfo3.ShaderMode = exTextureInfo2.ShaderMode;
		}

		// Token: 0x06002045 RID: 8261 RVA: 0x0009E744 File Offset: 0x0009C944
		public static exTextureInfo GetTextureInfo(string Path, bool returnSpaceOnInvalid = true)
		{
			exTextureInfo exTextureInfo;
			if (SpriteManager.TryGetTextureInfo(Path, out exTextureInfo))
			{
				return exTextureInfo;
			}
			if (!returnSpaceOnInvalid)
			{
				return null;
			}
			HashSet<string> warned = SpriteManager.Warned;
			lock (warned)
			{
				if (SpriteManager.Warned.Add(Path))
				{
					Debug.LogError("SpriteManager: No texture found by ID '" + Path + "'.");
				}
			}
			return SpriteManager.InvalidInfo;
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x0009E7B8 File Offset: 0x0009C9B8
		public unsafe static ReadOnlySpan<char> GetKey(ReadOnlySpan<char> Path)
		{
			int num = Path.Length;
			char[] array = SpriteManager.KeyBuffer.GetArray(num);
			for (int i = 0; i < num; i++)
			{
				char c = (char)(*Path[i]);
				if (c == '.')
				{
					num = i;
					break;
				}
				if (c == '_' || c == '\\')
				{
					array[i] = '/';
				}
				else
				{
					array[i] = c.ToLowerASCII();
				}
			}
			ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(array, 0, num);
			if (readOnlySpan.StartsWith(SpriteManager.KeyPrefix, StringComparison.Ordinal))
			{
				return readOnlySpan.Slice(SpriteManager.KeyPrefix.Length);
			}
			return readOnlySpan;
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x0009E848 File Offset: 0x0009CA48
		private static exTextureInfo LoadTextureInfo(ReadOnlySpan<char> Key)
		{
			string path;
			if (!SpriteManager.KoboldMap.TryGetValue(Key, out path))
			{
				return null;
			}
			if (GameManager.IsOnUIContext())
			{
				return Resources.Load<exTextureInfo>("TextureInfo/" + path);
			}
			exTextureInfo info = SpriteManager.InvalidInfo;
			GameManager.Instance.uiQueue.awaitTask(delegate
			{
				info = Resources.Load<exTextureInfo>("TextureInfo/" + path);
			});
			return info;
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x0009E8BC File Offset: 0x0009CABC
		public static bool TryGetTextureInfo(string Path, out exTextureInfo Info)
		{
			if (Path == null)
			{
				Info = null;
				return false;
			}
			if (SpriteManager.InfoMap == null)
			{
				SpriteManager.Initialize();
			}
			using (SpriteManager.InfoLock.TakeReadLock())
			{
				if (SpriteManager.PathMap.TryGetValue(Path, out Info))
				{
					return true;
				}
			}
			using (SpriteManager.InfoLock.TakeWriteLock())
			{
				ReadOnlySpan<char> key = SpriteManager.GetKey(Path);
				if (SpriteManager.InfoMap.TryGetValue(key, out Info))
				{
					if (Info == null)
					{
						StringMap<exTextureInfo> infoMap = SpriteManager.InfoMap;
						ReadOnlySpan<char> readOnlySpan = key;
						exTextureInfo exTextureInfo;
						Info = (exTextureInfo = SpriteManager.LoadTextureInfo(key));
						infoMap[readOnlySpan] = exTextureInfo;
					}
					SpriteManager.PathMap.Add(Path, Info);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x0009E98C File Offset: 0x0009CB8C
		public static bool HasTextureInfo(string Path)
		{
			if (Path == null)
			{
				return false;
			}
			if (SpriteManager.InfoMap == null)
			{
				SpriteManager.Initialize();
			}
			using (SpriteManager.InfoLock.TakeReadLock())
			{
				if (SpriteManager.PathMap.ContainsKey(Path))
				{
					return true;
				}
			}
			bool flag;
			using (SpriteManager.InfoLock.TakeWriteLock())
			{
				flag = SpriteManager.InfoMap.ContainsKey(SpriteManager.GetKey(Path));
			}
			return flag;
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x0009EA24 File Offset: 0x0009CC24
		public static GameObject CreateEmptySprite()
		{
			return SpriteManager.CloneSpritePrefab();
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x0009EA2C File Offset: 0x0009CC2C
		public static GameObject CreateSplitSprite(string Path)
		{
			GameObject gameObject = SpriteManager.CloneSplitSpritePrefab();
			gameObject.GetComponent<ex3DSprite2>().anchor = Anchor.MidCenter;
			gameObject.GetComponent<ex3DSprite2>().textureInfo = SpriteManager.GetTextureInfo(Path.Replace('\\', '_').Replace('/', '_').ToLower(), true);
			gameObject.GetComponent<ex3DSprite2>().backcolor = new Color(0f, 0f, 0f, 1f);
			return gameObject;
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x0009EA98 File Offset: 0x0009CC98
		public static GameObject CreateCollidableSprite(string Path, Anchor _Anchor, bool bReusable = false)
		{
			GameObject gameObject = SpriteManager.CloneSpritePrefab();
			gameObject.GetComponent<ex3DSprite2>().anchor = _Anchor;
			gameObject.GetComponent<ex3DSprite2>().textureInfo = SpriteManager.GetTextureInfo(Path.Replace('\\', '_').Replace('/', '_').ToLower(), true);
			gameObject.GetComponent<ex3DSprite2>().backcolor = new Color(0f, 0f, 0f, 1f);
			gameObject.GetComponent<ex3DSprite2>().bCollide = true;
			return gameObject;
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x0009EB10 File Offset: 0x0009CD10
		public static GameObject CreateCollidableSprite(string Path, bool bReusable = false)
		{
			GameObject gameObject = SpriteManager.CloneSpritePrefab();
			gameObject.GetComponent<ex3DSprite2>().textureInfo = SpriteManager.GetTextureInfo(Path.Replace('\\', '_').Replace('/', '_').ToLower(), true);
			gameObject.GetComponent<ex3DSprite2>().backcolor = new Color(0f, 0f, 0f, 1f);
			gameObject.GetComponent<ex3DSprite2>().bCollide = true;
			return gameObject;
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x0009EB7C File Offset: 0x0009CD7C
		public static GameObject CreateSprite(string Path, Anchor _Anchor, bool bReusable = false)
		{
			GameObject gameObject = SpriteManager.CloneSpritePrefab();
			gameObject.GetComponent<ex3DSprite2>().anchor = _Anchor;
			gameObject.GetComponent<ex3DSprite2>().textureInfo = SpriteManager.GetTextureInfo(Path.Replace('\\', '_').Replace('/', '_').ToLower(), true);
			gameObject.GetComponent<ex3DSprite2>().backcolor = new Color(0f, 0f, 0f, 1f);
			return gameObject;
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x0009EBE8 File Offset: 0x0009CDE8
		public static GameObject CreateSprite(string Path, Color Foreground, bool bReusable = false)
		{
			GameObject gameObject = SpriteManager.CloneSpritePrefab();
			gameObject.GetComponent<ex3DSprite2>().textureInfo = SpriteManager.GetTextureInfo(Path.Replace('\\', '_').Replace('/', '_').ToLower(), true);
			gameObject.GetComponent<ex3DSprite2>().color = Foreground;
			gameObject.GetComponent<ex3DSprite2>().backcolor = new Color(0f, 0f, 0f, 1f);
			return gameObject;
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x0009EC54 File Offset: 0x0009CE54
		public static GameObject CreateSprite(string Path, Color Foreground, Color Background, bool bReusable = false)
		{
			GameObject gameObject = SpriteManager.CloneSpritePrefab();
			gameObject.GetComponent<ex3DSprite2>().textureInfo = SpriteManager.GetTextureInfo(Path.Replace('\\', '_').Replace('/', '_').ToLower(), true);
			gameObject.GetComponent<ex3DSprite2>().color = Foreground;
			gameObject.GetComponent<ex3DSprite2>().backcolor = Background;
			return gameObject;
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x0009ECA8 File Offset: 0x0009CEA8
		public static GameObject CreateSprite(string Path, bool bReusable = false)
		{
			GameObject gameObject = SpriteManager.CloneSpritePrefab();
			gameObject.GetComponent<ex3DSprite2>().textureInfo = SpriteManager.GetTextureInfo(Path.Replace('\\', '_').Replace('/', '_').ToLower(), true);
			gameObject.GetComponent<ex3DSprite2>().backcolor = new Color(0f, 0f, 0f, 1f);
			return gameObject;
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x0009ED08 File Offset: 0x0009CF08
		public static ex3DSprite2 GetPooledSprite(string Path, Color Foreground, Color Background, Color Detail, bool HFlip = false, bool VFlip = false)
		{
			ex3DSprite2 component = PooledPrefabManager.Instantiate("KoboldBaseSprite", null).GetComponent<ex3DSprite2>();
			component.textureInfo = SpriteManager.GetTextureInfo(Path, true);
			component.shader = SpriteManager.GetShaderMode(component.textureInfo.ShaderMode);
			component.color = Foreground;
			component.backcolor = Background;
			component.detailcolor = Detail;
			BoxCollider component2 = component.GetComponent<BoxCollider>();
			if (HFlip)
			{
				if (VFlip)
				{
					component.transform.localScale = new Vector3(-1f, -1f, -1f);
					component2.size = new Vector3(-Math.Abs(component2.size.x), -Math.Abs(component2.size.y), -Math.Abs(component2.size.z));
				}
				else
				{
					component.transform.localScale = new Vector3(-1f, 1f, 1f);
					component2.size = new Vector3(-Math.Abs(component2.size.x), Math.Abs(component2.size.y), Math.Abs(component2.size.z));
				}
			}
			else if (VFlip)
			{
				component.transform.localScale = new Vector3(1f, -1f, 1f);
				component2.size = new Vector3(Math.Abs(component2.size.x), -Math.Abs(component2.size.y), Math.Abs(component2.size.z));
			}
			else
			{
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component2.size = new Vector3(Math.Abs(component2.size.x), Math.Abs(component2.size.y), Math.Abs(component2.size.z));
			}
			return component;
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x0009EEF0 File Offset: 0x0009D0F0
		public static ex3DSprite2 GetPooledSprite(IRenderable Tile, bool Transparent = false)
		{
			ColorChars colorChars = Tile.getColorChars();
			string text = Tile.getTile();
			Color color = ConsoleLib.Console.ColorUtility.ColorMap.GetValue(colorChars.foreground, default(Color)).WithAlpha((float)((Transparent && colorChars.foreground == 'k') ? 0 : 1));
			Color color2 = ConsoleLib.Console.ColorUtility.ColorMap.GetValue(colorChars.background, default(Color)).WithAlpha((float)((Transparent && colorChars.background == 'k') ? 0 : 1));
			Color color3 = ConsoleLib.Console.ColorUtility.ColorMap.GetValue(colorChars.detail, default(Color)).WithAlpha((float)((Transparent && colorChars.detail == 'k') ? 0 : 1));
			if (Globals.RenderMode == RenderModeType.Text || text.IsNullOrEmpty())
			{
				int num = 32;
				string renderString = Tile.getRenderString();
				if (!renderString.IsNullOrEmpty())
				{
					num = (int)renderString[0];
					if (num < 0 || num > 255)
					{
						num = 32;
					}
				}
				text = string.Format("assets_content_textures_text_{0}.bmp", num);
				color3 = color;
				color = color2;
				color2 = color3;
			}
			return SpriteManager.GetPooledSprite(text, color, color2, color3, Tile.getHFlip(), Tile.getVFlip());
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x0009F012 File Offset: 0x0009D212
		public static void Return(ex3DSprite2 Sprite)
		{
			PooledPrefabManager.Return(Sprite.gameObject);
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x0009F01F File Offset: 0x0009D21F
		public static void Return(GameObject Object)
		{
			PooledPrefabManager.Return(Object);
		}

		// Token: 0x04001071 RID: 4209
		private static GameObject _BaseSpritePrefab = null;

		// Token: 0x04001072 RID: 4210
		private static Shader[] Shaders = null;

		// Token: 0x04001073 RID: 4211
		private static GameObject _BaseSplitSpritePrefab = null;

		// Token: 0x04001074 RID: 4212
		[ModSensitiveStaticCache(false)]
		private static StringMap<exTextureInfo> InfoMap = null;

		// Token: 0x04001075 RID: 4213
		private static Dictionary<exTextureInfo, Sprite> SpriteMap = null;

		// Token: 0x04001076 RID: 4214
		private static StringMap<exTextureInfo> PathMap = null;

		// Token: 0x04001077 RID: 4215
		private static StringMap<string> KoboldMap = null;

		// Token: 0x04001078 RID: 4216
		private static HashSet<string> Warned = new HashSet<string>();

		// Token: 0x04001079 RID: 4217
		private static exTextureInfo InvalidInfo;

		// Token: 0x0400107A RID: 4218
		private static Rack<char> KeyBuffer = new Rack<char>(128);

		// Token: 0x0400107B RID: 4219
		private static ReaderWriterLockSlim InfoLock = new ReaderWriterLockSlim();

		// Token: 0x0400107C RID: 4220
		private static readonly string KeyPrefix = "assets/content/textures/";

		// Token: 0x0400107D RID: 4221
		private static readonly string InvalidTexturePath = "Text_32.bmp";
	}
}
