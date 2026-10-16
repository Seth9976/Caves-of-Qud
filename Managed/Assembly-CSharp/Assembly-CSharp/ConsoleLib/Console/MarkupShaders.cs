using System;
using System.Collections.Generic;
using UnityEngine;
using XRL;
using XRL.UI;

namespace ConsoleLib.Console
{
	// Token: 0x02001541 RID: 5441
	[HasModSensitiveStaticCache]
	public static class MarkupShaders
	{
		// Token: 0x0600D40A RID: 54282 RVA: 0x004EC040 File Offset: 0x004EA240
		public static IMarkupShader Get(string name)
		{
			MarkupShaders.CheckInit();
			IMarkupShader markupShader;
			MarkupShaders.ByName.TryGetValue(name, out markupShader);
			return markupShader;
		}

		// Token: 0x0600D40B RID: 54283 RVA: 0x004EC064 File Offset: 0x004EA264
		public static IMarkupShader Find(string action)
		{
			MarkupShaders.CheckInit();
			if (string.IsNullOrEmpty(action))
			{
				return null;
			}
			IMarkupShader markupShader;
			if (MarkupShaders.ByName.TryGetValue(action, out markupShader) && !markupShader.IsPattern())
			{
				markupShader = markupShader.GetInstanceHandler();
				if (markupShader != null)
				{
					return markupShader;
				}
			}
			int i = 0;
			int count = MarkupShaders.Patterns.Count;
			while (i < count)
			{
				markupShader = MarkupShaders.Patterns[i].GetMatchHandler(action);
				if (markupShader != null)
				{
					return markupShader;
				}
				i++;
			}
			return null;
		}

		// Token: 0x0600D40C RID: 54284 RVA: 0x004EC0D4 File Offset: 0x004EA2D4
		public static SolidColor GetSolidColor(char color)
		{
			foreach (SolidColor solidColor in MarkupShaders.PickerColors)
			{
				char? c = solidColor.Foreground;
				int? num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : null);
				if ((num.GetValueOrDefault() == (int)color) & (num != null))
				{
					return solidColor;
				}
			}
			foreach (SolidColor solidColor2 in MarkupShaders.Colors)
			{
				char? c = solidColor2.Foreground;
				int? num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : null);
				if (((num.GetValueOrDefault() == (int)color) & (num != null)) && (solidColor2.LightTone != null || solidColor2.DarkTone != null))
				{
					return solidColor2;
				}
			}
			foreach (SolidColor solidColor3 in MarkupShaders.Colors)
			{
				char? c = solidColor3.Foreground;
				int? num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : null);
				if ((num.GetValueOrDefault() == (int)color) & (num != null))
				{
					return solidColor3;
				}
			}
			return null;
		}

		// Token: 0x0600D40D RID: 54285 RVA: 0x004EC290 File Offset: 0x004EA490
		public static char? GetLightTone(char color)
		{
			SolidColor solidColor = MarkupShaders.GetSolidColor(color);
			if (solidColor != null)
			{
				return solidColor.LightTone;
			}
			return null;
		}

		// Token: 0x0600D40E RID: 54286 RVA: 0x004EC2B8 File Offset: 0x004EA4B8
		public static char? GetDarkTone(char color)
		{
			SolidColor solidColor = MarkupShaders.GetSolidColor(color);
			if (solidColor != null)
			{
				return solidColor.DarkTone;
			}
			return null;
		}

		// Token: 0x0600D40F RID: 54287 RVA: 0x004EC2E0 File Offset: 0x004EA4E0
		public static char? GetAlternateTone(char color)
		{
			SolidColor solidColor = MarkupShaders.GetSolidColor(color);
			if (solidColor == null)
			{
				return null;
			}
			if (solidColor.LightTone != null && solidColor.DarkTone != null)
			{
				if (!50.in100())
				{
					return solidColor.DarkTone;
				}
				return solidColor.LightTone;
			}
			else
			{
				char? lightTone = solidColor.LightTone;
				if (lightTone == null)
				{
					return solidColor.DarkTone;
				}
				return lightTone;
			}
		}

		// Token: 0x0600D410 RID: 54288 RVA: 0x004EC34A File Offset: 0x004EA54A
		public static void CheckInit()
		{
			if (MarkupShaders.Patterns.Count == 0)
			{
				MarkupShaders.Init();
			}
		}

		// Token: 0x0600D411 RID: 54289 RVA: 0x004EC360 File Offset: 0x004EA560
		[ModSensitiveCacheInit]
		private static void Init()
		{
			ColorUtility.LoadBaseColors();
			foreach (char c in ColorUtility.ColorMap.Keys)
			{
				MarkupShaders.Register(new SolidColor(c.ToString() ?? "", new char?(c), new char?('k'), null, null));
				MarkupShaders.Register(new SolidColor("&" + c.ToString(), new char?(c), new char?('k'), null, null));
				string text = "^" + c.ToString();
				char? c2 = new char?(c);
				MarkupShaders.Register(new SolidColor(text, new char?('y'), c2, null, null));
				foreach (char c3 in ColorUtility.ColorMap.Keys)
				{
					MarkupShaders.Register(new SolidColor("&" + c.ToString() + "^" + c3.ToString(), new char?(c), new char?(c3), null, null));
					MarkupShaders.Register(new SolidColor("^" + c3.ToString() + "&" + c.ToString(), new char?(c), new char?(c3), null, null));
				}
			}
			MarkupShaders.Register(new MarkupShaders.Chaotic());
			MarkupShaders.Register(new MarkupShaders.Random());
			MarkupShaders.Register(new MarkupShaders.SequencePattern());
			MarkupShaders.Register(new MarkupShaders.AlternationPattern());
			MarkupShaders.Register(new MarkupShaders.BorderedPattern());
			MarkupShaders.Register(new MarkupShaders.DistributionPattern());
			MarkupShaders.Register(new MarkupShaders.ShimmeringPattern());
			Loading.LoadTask("Loading Colors.xml", delegate
			{
				foreach (XmlDataHelper xmlDataHelper in DataManager.YieldXMLStreamsWithRoot("colors", true, true))
				{
					xmlDataHelper.HandleNodes(MarkupShaders._outerNodes, null);
				}
			}, true);
		}

		// Token: 0x0600D412 RID: 54290 RVA: 0x004EC5D4 File Offset: 0x004EA7D4
		public static void HandleInnerNode(XmlDataHelper xml)
		{
			xml.HandleNodes(MarkupShaders._innerNodes, null);
		}

		// Token: 0x0600D413 RID: 54291 RVA: 0x004EC5E4 File Offset: 0x004EA7E4
		public static void HandleSolidColorNode(XmlDataHelper Reader)
		{
			string attribute = Reader.GetAttribute("Name");
			if (string.IsNullOrEmpty(attribute))
			{
				throw new Exception(Reader.Name + " tag had missing or empty Name attribute");
			}
			string text = Reader.GetAttribute("DisplayName");
			if (text == "")
			{
				text = null;
			}
			string attribute2 = Reader.GetAttribute("Color");
			string attribute3 = Reader.GetAttribute("BackgroundColor");
			if (attribute2 == "")
			{
				throw new Exception(Reader.Name + " tag had empty Color attribute");
			}
			if (attribute3 == "")
			{
				throw new Exception(Reader.Name + " tag had empty BackgroundColor attribute");
			}
			if (Reader.modInfo == null && attribute2 == null && attribute3 == null)
			{
				throw new Exception(Reader.Name + " tag had no Color or BackgroundColor attribute");
			}
			if (attribute2 != null && attribute2.Length != 1)
			{
				throw new Exception(Reader.Name + " tag had Color attribute of length " + attribute2.Length.ToString());
			}
			if (attribute3 != null && attribute3.Length != 1)
			{
				throw new Exception(Reader.Name + " tag had BackgroundColor attribute of length " + attribute3.Length.ToString());
			}
			string attribute4 = Reader.GetAttribute("ShowInPicker");
			bool flag = attribute4.EqualsNoCase("true");
			IMarkupShader markupShader;
			bool flag2;
			SolidColor solidColor;
			if (MarkupShaders.ByName.TryGetValue(attribute, out markupShader))
			{
				flag2 = false;
				if (Reader.modInfo == null)
				{
					throw new Exception(Reader.Name + " tag had duplicate name " + attribute);
				}
				solidColor = markupShader as SolidColor;
				if (solidColor == null)
				{
					throw new Exception(string.Concat(new string[]
					{
						Reader.Name,
						" tag referred to ",
						markupShader.GetType().Name,
						" ",
						attribute
					}));
				}
			}
			else
			{
				flag2 = true;
				solidColor = new SolidColor(attribute, new char?('y'), new char?('k'), null, null);
				if (attribute2 == null && attribute3 == null)
				{
					throw new Exception(Reader.Name + " tag had no Color or BackgroundColor attribute");
				}
			}
			if (text != null)
			{
				solidColor.SetDisplayName(text);
			}
			if (!string.IsNullOrEmpty(attribute4))
			{
				solidColor.SetShowInPicker(flag);
			}
			if (attribute2 != null)
			{
				solidColor.Foreground = new char?(attribute2[0]);
			}
			if (attribute3 != null)
			{
				solidColor.Background = new char?(attribute3[0]);
			}
			string attribute5 = Reader.GetAttribute("LightTone");
			if (attribute5 != null)
			{
				if (attribute5 == "")
				{
					solidColor.LightTone = null;
				}
				else
				{
					if (attribute5.Length != 1)
					{
						throw new Exception(Reader.Name + " tag had LightTone attribute of length " + attribute5.Length.ToString());
					}
					solidColor.LightTone = new char?(attribute5[0]);
				}
			}
			string attribute6 = Reader.GetAttribute("DarkTone");
			if (attribute6 != null)
			{
				if (attribute6 == "")
				{
					solidColor.DarkTone = null;
				}
				else
				{
					if (attribute6.Length != 1)
					{
						throw new Exception(Reader.Name + " tag had DarkTone attribute of length " + attribute6.Length.ToString());
					}
					solidColor.DarkTone = new char?(attribute6[0]);
				}
			}
			if (flag2)
			{
				MarkupShaders.Register(solidColor);
			}
			else if (!solidColor.GetShowInPicker())
			{
				MarkupShaders.PickerColors.Remove(solidColor);
			}
			Reader.DoneWithElement();
		}

		// Token: 0x0600D414 RID: 54292 RVA: 0x004EC954 File Offset: 0x004EAB54
		public static void HandleShaderNode(XmlDataHelper Reader)
		{
			ModInfo modInfo = Reader.modInfo;
			string attribute = Reader.GetAttribute("Name");
			if (string.IsNullOrEmpty(attribute))
			{
				throw new Exception(Reader.Name + " tag had missing or empty Name attribute");
			}
			string text = Reader.GetAttribute("DisplayName");
			if (text == "")
			{
				text = null;
			}
			string attribute2 = Reader.GetAttribute("SystemSymbol");
			bool flag = attribute2.EqualsNoCase("true");
			string attribute3 = Reader.GetAttribute("ShowInPicker");
			bool flag2 = attribute3.EqualsNoCase("true");
			string attribute4 = Reader.GetAttribute("Type");
			if (attribute4 == "")
			{
				throw new Exception(Reader.Name + " tag had empty Type attribute");
			}
			string attribute5 = Reader.GetAttribute("Colors");
			if (string.IsNullOrEmpty(attribute5))
			{
				throw new Exception(Reader.Name + " tag had missing or empty Colors attribute");
			}
			string attribute6 = Reader.GetAttribute("Decorators");
			IMarkupShader markupShader;
			bool flag3;
			if (MarkupShaders.ByName.TryGetValue(attribute, out markupShader))
			{
				flag3 = false;
				if (markupShader is SolidColor)
				{
					throw new Exception(string.Concat(new string[]
					{
						Reader.Name,
						" tag referred to non-shader ",
						markupShader.GetType().Name,
						" ",
						attribute
					}));
				}
			}
			else
			{
				flag3 = true;
				if (attribute4 == null)
				{
					throw new Exception(Reader.Name + " tag had missing Type attribute");
				}
				markupShader = IMarkupShader.InstanceByType(attribute4, attribute);
			}
			if (text != null)
			{
				markupShader.SetDisplayName(text);
			}
			if (!string.IsNullOrEmpty(attribute2))
			{
				markupShader.SetSystemSymbol(flag);
			}
			if (!string.IsNullOrEmpty(attribute3))
			{
				markupShader.SetShowInPicker(flag2);
			}
			markupShader.SetColors(attribute5);
			if (!string.IsNullOrEmpty(attribute6))
			{
				foreach (string text2 in attribute6.Split(',', StringSplitOptions.None))
				{
					if (text2.Contains(":"))
					{
						string[] array2 = text2.Split(':', StringSplitOptions.None);
						IMarkupDecorator markupDecorator = IMarkupDecorator.InstanceByType(array2[0], markupShader);
						int j = 1;
						int num = array2.Length;
						while (j < num)
						{
							markupDecorator.ApplyDecoratorParameter(array2[j]);
							j++;
						}
						markupShader = markupDecorator;
					}
					else
					{
						markupShader = IMarkupDecorator.InstanceByType(text2, markupShader);
					}
				}
			}
			if (flag3)
			{
				MarkupShaders.Register(markupShader);
			}
			else if (!string.IsNullOrEmpty(attribute6))
			{
				MarkupShaders.ReRegister(markupShader);
			}
			else if (!markupShader.GetShowInPicker())
			{
				MarkupShaders.PickerShaders.Remove(markupShader);
			}
			Reader.DoneWithElement();
		}

		// Token: 0x0600D415 RID: 54293 RVA: 0x004ECBCC File Offset: 0x004EADCC
		public static bool Register(IMarkupShader shader)
		{
			string name = shader.GetName();
			if (string.IsNullOrEmpty(name))
			{
				Debug.LogError("missing name from " + shader.GetType().Name);
				return false;
			}
			IMarkupShader markupShader;
			if (MarkupShaders.ByName.TryGetValue(name, out markupShader))
			{
				if (markupShader != shader)
				{
					Debug.LogError(string.Concat(new string[]
					{
						"duplicate name ",
						name,
						" from ",
						shader.GetType().Name,
						" and ",
						markupShader.GetType().Name
					}));
					return false;
				}
			}
			else
			{
				MarkupShaders.ByName.Add(name, shader);
				MarkupShaders.Shaders.Add(shader);
				if (shader.IsPattern())
				{
					MarkupShaders.Patterns.Add(shader);
					MarkupShaders.Patterns.Sort();
				}
			}
			SolidColor solidColor = shader as SolidColor;
			if (solidColor != null)
			{
				if (!MarkupShaders.Colors.Contains(solidColor))
				{
					MarkupShaders.Colors.Add(solidColor);
				}
				if (solidColor.GetShowInPicker() && !MarkupShaders.PickerColors.Contains(solidColor))
				{
					MarkupShaders.PickerColors.Add(solidColor);
				}
			}
			else if (shader.GetShowInPicker() && !MarkupShaders.PickerShaders.Contains(shader))
			{
				MarkupShaders.PickerShaders.Add(shader);
			}
			return true;
		}

		// Token: 0x0600D416 RID: 54294 RVA: 0x004ECCFC File Offset: 0x004EAEFC
		public static bool ReRegister(IMarkupShader shader)
		{
			string name = shader.GetName();
			if (string.IsNullOrEmpty(name))
			{
				Debug.LogError("missing name from " + shader.GetType().Name);
				return false;
			}
			IMarkupShader markupShader;
			if (MarkupShaders.ByName.TryGetValue(name, out markupShader))
			{
				MarkupShaders.ByName[name] = shader;
				MarkupShaders.Shaders.Remove(markupShader);
				MarkupShaders.Shaders.Add(shader);
				if (markupShader.IsPattern() || shader.IsPattern())
				{
					MarkupShaders.Patterns.Remove(markupShader);
					MarkupShaders.Patterns.Add(shader);
					MarkupShaders.Patterns.Sort();
				}
				SolidColor solidColor = shader as SolidColor;
				if (solidColor != null)
				{
					SolidColor solidColor2 = markupShader as SolidColor;
					if (solidColor2 != null)
					{
						MarkupShaders.Colors.Remove(solidColor2);
						MarkupShaders.PickerColors.Remove(solidColor2);
					}
					if (!MarkupShaders.Colors.Contains(solidColor))
					{
						MarkupShaders.Colors.Add(solidColor);
					}
					if (solidColor.GetShowInPicker() && !MarkupShaders.PickerColors.Contains(solidColor))
					{
						MarkupShaders.PickerColors.Add(solidColor);
					}
				}
				else
				{
					MarkupShaders.PickerShaders.Remove(markupShader);
					if (shader.GetShowInPicker() && !MarkupShaders.PickerShaders.Contains(shader))
					{
						MarkupShaders.PickerShaders.Add(shader);
					}
				}
			}
			else
			{
				MarkupShaders.ByName.Add(name, shader);
				MarkupShaders.Shaders.Add(shader);
				if (shader.IsPattern())
				{
					MarkupShaders.Patterns.Add(shader);
					MarkupShaders.Patterns.Sort();
				}
				SolidColor solidColor3 = shader as SolidColor;
				if (solidColor3 != null)
				{
					if (!MarkupShaders.Colors.Contains(solidColor3))
					{
						MarkupShaders.Colors.Add(solidColor3);
					}
					if (solidColor3.GetShowInPicker() && !MarkupShaders.PickerColors.Contains(solidColor3))
					{
						MarkupShaders.PickerColors.Add(solidColor3);
					}
				}
				else if (shader.GetShowInPicker() && !MarkupShaders.PickerShaders.Contains(shader))
				{
					MarkupShaders.PickerShaders.Add(shader);
				}
			}
			return true;
		}

		// Token: 0x0400550D RID: 21773
		[ModSensitiveStaticCache(true)]
		public static List<SolidColor> PickerColors = new List<SolidColor>();

		// Token: 0x0400550E RID: 21774
		[ModSensitiveStaticCache(true)]
		public static List<SolidColor> Colors = new List<SolidColor>();

		// Token: 0x0400550F RID: 21775
		[ModSensitiveStaticCache(true)]
		public static List<IMarkupShader> PickerShaders = new List<IMarkupShader>();

		// Token: 0x04005510 RID: 21776
		[ModSensitiveStaticCache(true)]
		public static List<IMarkupShader> Shaders = new List<IMarkupShader>();

		// Token: 0x04005511 RID: 21777
		[ModSensitiveStaticCache(true)]
		public static List<IMarkupShader> Patterns = new List<IMarkupShader>();

		// Token: 0x04005512 RID: 21778
		[ModSensitiveStaticCache(true)]
		private static Dictionary<string, IMarkupShader> ByName = new Dictionary<string, IMarkupShader>();

		// Token: 0x04005513 RID: 21779
		private static readonly Dictionary<string, Action<XmlDataHelper>> _outerNodes = new Dictionary<string, Action<XmlDataHelper>> { 
		{
			"colors",
			new Action<XmlDataHelper>(MarkupShaders.HandleInnerNode)
		} };

		// Token: 0x04005514 RID: 21780
		private static readonly Dictionary<string, Action<XmlDataHelper>> _innerNodes = new Dictionary<string, Action<XmlDataHelper>>
		{
			{
				"solidcolors",
				new Action<XmlDataHelper>(MarkupShaders.HandleInnerNode)
			},
			{
				"shaders",
				new Action<XmlDataHelper>(MarkupShaders.HandleInnerNode)
			},
			{
				"solidcolor",
				new Action<XmlDataHelper>(MarkupShaders.HandleSolidColorNode)
			},
			{
				"shader",
				new Action<XmlDataHelper>(MarkupShaders.HandleShaderNode)
			}
		};

		// Token: 0x02001E3D RID: 7741
		[AttributeUsage(AttributeTargets.Class)]
		public class ShaderAssetType : Attribute
		{
		}

		// Token: 0x02001E3E RID: 7742
		[AttributeUsage(AttributeTargets.Method)]
		public class ShaderAssetFactoryMethod : Attribute
		{
			// Token: 0x0600F6F2 RID: 63218 RVA: 0x0055AE3B File Offset: 0x0055903B
			public ShaderAssetFactoryMethod(string Type)
			{
				this.Type = Type;
			}

			// Token: 0x04007628 RID: 30248
			public string Type;
		}

		// Token: 0x02001E3F RID: 7743
		[AttributeUsage(AttributeTargets.Class)]
		public class ShaderDecoratorType : Attribute
		{
		}

		// Token: 0x02001E40 RID: 7744
		[AttributeUsage(AttributeTargets.Method)]
		public class ShaderDecoratorFactoryMethod : Attribute
		{
			// Token: 0x0600F6F4 RID: 63220 RVA: 0x0055AE52 File Offset: 0x00559052
			public ShaderDecoratorFactoryMethod(string Type)
			{
				this.Type = Type;
			}

			// Token: 0x04007629 RID: 30249
			public string Type;
		}

		// Token: 0x02001E41 RID: 7745
		[MarkupShaders.ShaderAssetType]
		public class Solid : IMarkupShader
		{
			// Token: 0x0600F6F5 RID: 63221 RVA: 0x0055AE61 File Offset: 0x00559061
			[MarkupShaders.ShaderAssetFactoryMethod("solid")]
			public static MarkupShaders.Solid Generate(string Name)
			{
				return new MarkupShaders.Solid(Name);
			}

			// Token: 0x0600F6F6 RID: 63222 RVA: 0x0055AE69 File Offset: 0x00559069
			public Solid(string Name)
				: base(Name)
			{
			}

			// Token: 0x0600F6F7 RID: 63223 RVA: 0x0055AE72 File Offset: 0x00559072
			public override char? GetForegroundColor(char ch, int localPos, int localLen, int totalPos, int totalLen)
			{
				return new char?(this.Colors[0]);
			}
		}

		// Token: 0x02001E42 RID: 7746
		public abstract class ISequence : IMarkupShader
		{
			// Token: 0x0600F6F8 RID: 63224 RVA: 0x0055AE81 File Offset: 0x00559081
			public ISequence(string Name)
				: base(Name)
			{
			}

			// Token: 0x0600F6F9 RID: 63225 RVA: 0x0055AE8A File Offset: 0x0055908A
			public override char? GetForegroundColor(char ch, int localPos, int localLen, int totalPos, int totalLen)
			{
				return new char?(this.Colors[totalPos % this.Colors.Length]);
			}
		}

		// Token: 0x02001E43 RID: 7747
		public class SequencePattern : MarkupShaders.ISequence
		{
			// Token: 0x0600F6FA RID: 63226 RVA: 0x0055AEA3 File Offset: 0x005590A3
			public SequencePattern()
				: base("sequence pattern")
			{
			}

			// Token: 0x0600F6FB RID: 63227 RVA: 0x0055AEB0 File Offset: 0x005590B0
			public override bool IsPattern()
			{
				return true;
			}

			// Token: 0x0600F6FC RID: 63228 RVA: 0x0055AEB3 File Offset: 0x005590B3
			public override int GetPatternPriority()
			{
				return 1000;
			}

			// Token: 0x0600F6FD RID: 63229 RVA: 0x0055AEBC File Offset: 0x005590BC
			public override IMarkupShader GetMatchHandler(string action)
			{
				if (!action.EndsWith(" sequence") || action.Length < 10)
				{
					return null;
				}
				string text = action.Substring(0, action.Length - 9);
				MarkupShaders.SequencePattern sequencePattern = new MarkupShaders.SequencePattern();
				sequencePattern.SetColors(text);
				return sequencePattern;
			}
		}

		// Token: 0x02001E44 RID: 7748
		[MarkupShaders.ShaderAssetType]
		public class Sequence : MarkupShaders.ISequence
		{
			// Token: 0x0600F6FE RID: 63230 RVA: 0x0055AEFF File Offset: 0x005590FF
			[MarkupShaders.ShaderAssetFactoryMethod("sequence")]
			public static MarkupShaders.Sequence Generate(string Name)
			{
				return new MarkupShaders.Sequence(Name);
			}

			// Token: 0x0600F6FF RID: 63231 RVA: 0x0055AF07 File Offset: 0x00559107
			public Sequence(string Name)
				: base(Name)
			{
			}
		}

		// Token: 0x02001E45 RID: 7749
		public abstract class IAlternation : IMarkupShader
		{
			// Token: 0x0600F700 RID: 63232 RVA: 0x0055AF10 File Offset: 0x00559110
			public IAlternation(string Name)
				: base(Name)
			{
			}

			// Token: 0x0600F701 RID: 63233 RVA: 0x0055AF19 File Offset: 0x00559119
			public override char? GetForegroundColor(char ch, int localPos, int localLen, int totalPos, int totalLen)
			{
				return new char?(this.Colors[totalPos * this.Colors.Length / totalLen]);
			}
		}

		// Token: 0x02001E46 RID: 7750
		public class AlternationPattern : MarkupShaders.IAlternation
		{
			// Token: 0x0600F702 RID: 63234 RVA: 0x0055AF35 File Offset: 0x00559135
			public AlternationPattern()
				: base("alternation pattern")
			{
			}

			// Token: 0x0600F703 RID: 63235 RVA: 0x0055AF42 File Offset: 0x00559142
			public override bool IsPattern()
			{
				return true;
			}

			// Token: 0x0600F704 RID: 63236 RVA: 0x0055AF45 File Offset: 0x00559145
			public override int GetPatternPriority()
			{
				return 1200;
			}

			// Token: 0x0600F705 RID: 63237 RVA: 0x0055AF4C File Offset: 0x0055914C
			public override IMarkupShader GetMatchHandler(string action)
			{
				if (!action.EndsWith(" alternation") || action.Length < 13)
				{
					return null;
				}
				string text = action.Substring(0, action.Length - 12);
				MarkupShaders.AlternationPattern alternationPattern = new MarkupShaders.AlternationPattern();
				alternationPattern.SetColors(text);
				return alternationPattern;
			}
		}

		// Token: 0x02001E47 RID: 7751
		[MarkupShaders.ShaderAssetType]
		public class Alternation : MarkupShaders.IAlternation
		{
			// Token: 0x0600F706 RID: 63238 RVA: 0x0055AF8F File Offset: 0x0055918F
			[MarkupShaders.ShaderAssetFactoryMethod("alternation")]
			public static MarkupShaders.Alternation Generate(string Name)
			{
				return new MarkupShaders.Alternation(Name);
			}

			// Token: 0x0600F707 RID: 63239 RVA: 0x0055AF97 File Offset: 0x00559197
			public Alternation(string Name)
				: base(Name)
			{
			}
		}

		// Token: 0x02001E48 RID: 7752
		public abstract class IBordered : IMarkupShader
		{
			// Token: 0x0600F708 RID: 63240 RVA: 0x0055AFA0 File Offset: 0x005591A0
			public IBordered(string Name)
				: base(Name)
			{
			}

			// Token: 0x0600F709 RID: 63241 RVA: 0x0055AFA9 File Offset: 0x005591A9
			public override char? GetForegroundColor(char ch, int localPos, int localLen, int totalPos, int totalLen)
			{
				return new char?((localPos == 0 || localPos == localLen - 1) ? this.Colors[1] : this.Colors[0]);
			}
		}

		// Token: 0x02001E49 RID: 7753
		public class BorderedPattern : MarkupShaders.IBordered
		{
			// Token: 0x0600F70A RID: 63242 RVA: 0x0055AFCB File Offset: 0x005591CB
			public BorderedPattern()
				: base("bordered pattern")
			{
			}

			// Token: 0x0600F70B RID: 63243 RVA: 0x0055AFD8 File Offset: 0x005591D8
			public override bool IsPattern()
			{
				return true;
			}

			// Token: 0x0600F70C RID: 63244 RVA: 0x0055AFDB File Offset: 0x005591DB
			public override int GetPatternPriority()
			{
				return 1500;
			}

			// Token: 0x0600F70D RID: 63245 RVA: 0x0055AFE4 File Offset: 0x005591E4
			public override IMarkupShader GetMatchHandler(string action)
			{
				if (!action.EndsWith(" border") || action.Length < 15 || !action.Contains(" with "))
				{
					return null;
				}
				string text = action.Substring(0, action.Length - 7);
				MarkupShaders.BorderedPattern borderedPattern = new MarkupShaders.BorderedPattern();
				borderedPattern.SetColors(text.Split(new string[] { " with " }, 2, StringSplitOptions.None));
				return borderedPattern;
			}
		}

		// Token: 0x02001E4A RID: 7754
		[MarkupShaders.ShaderAssetType]
		public class Bordered : MarkupShaders.IBordered
		{
			// Token: 0x0600F70E RID: 63246 RVA: 0x0055B04A File Offset: 0x0055924A
			[MarkupShaders.ShaderAssetFactoryMethod("bordered")]
			public static MarkupShaders.Bordered Generate(string Name)
			{
				return new MarkupShaders.Bordered(Name);
			}

			// Token: 0x0600F70F RID: 63247 RVA: 0x0055B052 File Offset: 0x00559252
			public Bordered(string Name)
				: base(Name)
			{
			}
		}

		// Token: 0x02001E4B RID: 7755
		public abstract class IDistribution : IMarkupShader
		{
			// Token: 0x0600F710 RID: 63248 RVA: 0x0055B05B File Offset: 0x0055925B
			public IDistribution(string Name)
				: base(Name)
			{
			}

			// Token: 0x0600F711 RID: 63249 RVA: 0x0055B064 File Offset: 0x00559264
			public override char? GetForegroundColor(char ch, int localPos, int localLen, int totalPos, int totalLen)
			{
				int num = totalLen;
				int i = 0;
				int num2 = this.Colors.Length;
				while (i < num2)
				{
					num += (int)this.Colors[i] * (i + 1);
					i++;
				}
				global::System.Random random = new global::System.Random(num);
				for (int j = 0; j < totalPos; j++)
				{
					random.Next();
				}
				return new char?(this.Colors.GetRandomElement<char>(random));
			}
		}

		// Token: 0x02001E4C RID: 7756
		public class DistributionPattern : MarkupShaders.IDistribution
		{
			// Token: 0x0600F712 RID: 63250 RVA: 0x0055B0C8 File Offset: 0x005592C8
			public DistributionPattern()
				: base("distribution pattern")
			{
			}

			// Token: 0x0600F713 RID: 63251 RVA: 0x0055B0D5 File Offset: 0x005592D5
			public override bool IsPattern()
			{
				return true;
			}

			// Token: 0x0600F714 RID: 63252 RVA: 0x0055B0D8 File Offset: 0x005592D8
			public override int GetPatternPriority()
			{
				return 2000;
			}

			// Token: 0x0600F715 RID: 63253 RVA: 0x0055B0E0 File Offset: 0x005592E0
			public override IMarkupShader GetMatchHandler(string action)
			{
				if (!action.EndsWith(" distribution") || action.Length < 14)
				{
					return null;
				}
				string text = action.Substring(0, action.Length - 13);
				MarkupShaders.DistributionPattern distributionPattern = new MarkupShaders.DistributionPattern();
				distributionPattern.SetColors(text);
				return distributionPattern;
			}
		}

		// Token: 0x02001E4D RID: 7757
		[MarkupShaders.ShaderAssetType]
		public class Distribution : MarkupShaders.IDistribution
		{
			// Token: 0x0600F716 RID: 63254 RVA: 0x0055B123 File Offset: 0x00559323
			[MarkupShaders.ShaderAssetFactoryMethod("distribution")]
			public static MarkupShaders.Distribution Generate(string Name)
			{
				return new MarkupShaders.Distribution(Name);
			}

			// Token: 0x0600F717 RID: 63255 RVA: 0x0055B12B File Offset: 0x0055932B
			public Distribution(string Name)
				: base(Name)
			{
			}
		}

		// Token: 0x02001E4E RID: 7758
		public class Chaotic : IMarkupShader
		{
			// Token: 0x0600F718 RID: 63256 RVA: 0x0055B134 File Offset: 0x00559334
			public Chaotic()
				: base("chaotic")
			{
				this.SetShowInPicker(true);
			}

			// Token: 0x0600F719 RID: 63257 RVA: 0x0055B148 File Offset: 0x00559348
			public override char? GetForegroundColor(char ch, int localPos, int localLen, int totalPos, int totalLen)
			{
				return new char?(Markup.NORMAL_FOREGROUNDS.GetRandomElementCosmetic<char>());
			}
		}

		// Token: 0x02001E4F RID: 7759
		public class Random : IMarkupShader
		{
			// Token: 0x0600F71A RID: 63258 RVA: 0x0055B159 File Offset: 0x00559359
			public Random()
				: base("random")
			{
				this.SetShowInPicker(true);
			}

			// Token: 0x0600F71B RID: 63259 RVA: 0x0055B16D File Offset: 0x0055936D
			public override IMarkupShader GetInstanceHandler()
			{
				return new MarkupShaders.Random
				{
					Color = Markup.NORMAL_FOREGROUNDS.GetRandomElementCosmetic<char>()
				};
			}

			// Token: 0x0600F71C RID: 63260 RVA: 0x0055B184 File Offset: 0x00559384
			public override char? GetForegroundColor(char ch, int localPos, int localLen, int totalPos, int totalLen)
			{
				return new char?(this.Color);
			}

			// Token: 0x0400762A RID: 30250
			private char Color;
		}

		// Token: 0x02001E50 RID: 7760
		[MarkupShaders.ShaderDecoratorType]
		public class ShimmeringDecorator : IMarkupDecorator
		{
			// Token: 0x0600F71D RID: 63261 RVA: 0x0055B191 File Offset: 0x00559391
			[MarkupShaders.ShaderDecoratorFactoryMethod("shimmering")]
			public static MarkupShaders.ShimmeringDecorator Generate(IMarkupShader Component)
			{
				return new MarkupShaders.ShimmeringDecorator(Component);
			}

			// Token: 0x0600F71E RID: 63262 RVA: 0x0055B199 File Offset: 0x00559399
			public ShimmeringDecorator()
				: base("shimmering decorator")
			{
			}

			// Token: 0x0600F71F RID: 63263 RVA: 0x0055B1AE File Offset: 0x005593AE
			public ShimmeringDecorator(IMarkupShader Component)
				: this()
			{
				this.Component = Component;
			}

			// Token: 0x0600F720 RID: 63264 RVA: 0x0055B1C0 File Offset: 0x005593C0
			public override void ApplyDecoratorParameter(string param)
			{
				try
				{
					this.Chance = Convert.ToInt32(param);
				}
				catch
				{
					throw new Exception("invalid shimmering chance specification: " + param);
				}
			}

			// Token: 0x0600F721 RID: 63265 RVA: 0x0055B200 File Offset: 0x00559400
			public override char? GetForegroundColor(char ch, int localPos, int localLen, int totalPos, int totalLen)
			{
				char? foregroundColor = this.Component.GetForegroundColor(ch, localPos, localLen, totalPos, totalLen);
				if (foregroundColor != null && this.Chance.in100())
				{
					char? alternateTone = MarkupShaders.GetAlternateTone(foregroundColor.Value);
					if (alternateTone != null)
					{
						return alternateTone;
					}
				}
				return foregroundColor;
			}

			// Token: 0x0600F722 RID: 63266 RVA: 0x0055B250 File Offset: 0x00559450
			public override char? GetBackgroundColor(char ch, int localPos, int localLen, int totalPos, int totalLen)
			{
				char? backgroundColor = this.Component.GetBackgroundColor(ch, localPos, localLen, totalPos, totalLen);
				if (backgroundColor != null)
				{
					char? c = backgroundColor;
					int? num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : null);
					int num2 = 107;
					if (!((num.GetValueOrDefault() == num2) & (num != null)) && this.Chance.in100())
					{
						char? alternateTone = MarkupShaders.GetAlternateTone(backgroundColor.Value);
						if (alternateTone != null)
						{
							return alternateTone;
						}
					}
				}
				return backgroundColor;
			}

			// Token: 0x0400762B RID: 30251
			public int Chance = 25;
		}

		// Token: 0x02001E51 RID: 7761
		public class ShimmeringPattern : IMarkupShader
		{
			// Token: 0x0600F723 RID: 63267 RVA: 0x0055B2DC File Offset: 0x005594DC
			public ShimmeringPattern()
				: base("shimmering pattern")
			{
			}

			// Token: 0x0600F724 RID: 63268 RVA: 0x0055B2E9 File Offset: 0x005594E9
			public override bool IsPattern()
			{
				return true;
			}

			// Token: 0x0600F725 RID: 63269 RVA: 0x0055B2EC File Offset: 0x005594EC
			public override int GetPatternPriority()
			{
				return 2500;
			}

			// Token: 0x0600F726 RID: 63270 RVA: 0x0055B2F4 File Offset: 0x005594F4
			public override IMarkupShader GetMatchHandler(string action)
			{
				if (!action.StartsWith("shimmering ") || action.Length < 12)
				{
					return null;
				}
				string text = action.Substring(11);
				IMarkupShader markupShader = MarkupShaders.Find(text);
				if (markupShader == null)
				{
					throw new Exception("invalid inner pattern \"" + text + "\"");
				}
				return new MarkupShaders.ShimmeringDecorator(markupShader);
			}
		}
	}
}
