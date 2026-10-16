using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using XRL.UI;

namespace XRL.CharacterBuilds
{
	// Token: 0x02000588 RID: 1416
	public static class CodeCompressor
	{
		// Token: 0x06003CD9 RID: 15577 RVA: 0x0012A968 File Offset: 0x00128B68
		public static string Compress(string s)
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(s)))
			{
				using (MemoryStream memoryStream2 = new MemoryStream())
				{
					using (GZipStream gzipStream = new GZipStream(memoryStream2, CompressionMode.Compress))
					{
						memoryStream.CopyTo(gzipStream);
					}
					text = Convert.ToBase64String(memoryStream2.ToArray());
				}
			}
			return text;
		}

		// Token: 0x06003CDA RID: 15578 RVA: 0x0012A9F4 File Offset: 0x00128BF4
		public static string Decompress(string s)
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(s)))
			{
				using (MemoryStream memoryStream2 = new MemoryStream())
				{
					using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
					{
						gzipStream.CopyTo(memoryStream2);
					}
					string @string = Encoding.Unicode.GetString(memoryStream2.ToArray(), 0, 5);
					if (@string.Length == 0 || @string[0] == '{')
					{
						text = Encoding.Unicode.GetString(memoryStream2.ToArray());
					}
					else
					{
						text = Encoding.UTF8.GetString(memoryStream2.ToArray());
					}
				}
			}
			return text;
		}

		// Token: 0x06003CDB RID: 15579 RVA: 0x0012AABC File Offset: 0x00128CBC
		public static string generateCode(IEnumerable<AbstractEmbarkBuilderModule> modules)
		{
			CodeCompressor.BuildCodePayload buildCodePayload = new CodeCompressor.BuildCodePayload();
			buildCodePayload.buildversion = "1.0.1";
			buildCodePayload.gameversion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
			buildCodePayload.modules = new List<CodeCompressor.CodeEntry>();
			foreach (AbstractEmbarkBuilderModule abstractEmbarkBuilderModule in modules)
			{
				if (abstractEmbarkBuilderModule.IncludeInBuildCodes())
				{
					CodeCompressor.CodeEntry codeEntry = new CodeCompressor.CodeEntry();
					codeEntry.requiredMod = abstractEmbarkBuilderModule.GetRequiredMod();
					codeEntry.moduleType = abstractEmbarkBuilderModule.GetType();
					codeEntry.data = abstractEmbarkBuilderModule.getData();
					buildCodePayload.modules.Add(codeEntry);
				}
			}
			return CodeCompressor.Compress(JsonConvert.SerializeObject(buildCodePayload, Formatting.Indented, CodeCompressor.SERIALIZER_SETTINGS));
		}

		// Token: 0x06003CDC RID: 15580 RVA: 0x0012AB84 File Offset: 0x00128D84
		public static void loadCode(string code, List<AbstractEmbarkBuilderModule> modules, bool silent = false)
		{
			try
			{
				JObject jobject = JObject.Parse(CodeCompressor.Decompress(code));
				CodeCompressor.BuildCodePayload buildCodePayload = new CodeCompressor.BuildCodePayload();
				buildCodePayload.buildversion = jobject.Value<string>("buildversion");
				buildCodePayload.gameversion = jobject.Value<string>("gameversion");
				buildCodePayload.modules = new List<CodeCompressor.CodeEntry>();
				foreach (JToken jtoken in jobject.Value<JArray>("modules"))
				{
					try
					{
						CodeCompressor.CodeEntry payloadModule = JsonConvert.DeserializeObject<CodeCompressor.CodeEntry>(jtoken.ToString(), CodeCompressor.SERIALIZER_SETTINGS);
						buildCodePayload.modules.Add(payloadModule);
						Func<AbstractEmbarkBuilderModule, bool> func;
						Func<AbstractEmbarkBuilderModule, bool> <>9__0;
						if ((func = <>9__0) == null)
						{
							func = (<>9__0 = (AbstractEmbarkBuilderModule m) => m.GetType() == payloadModule.moduleType);
						}
						foreach (AbstractEmbarkBuilderModule abstractEmbarkBuilderModule in modules.Where<AbstractEmbarkBuilderModule>(func))
						{
							if (silent)
							{
								abstractEmbarkBuilderModule.setDataDirect(payloadModule.data);
							}
							else
							{
								abstractEmbarkBuilderModule.setData(payloadModule.data);
							}
						}
					}
					catch (Exception ex)
					{
						MetricsManager.LogError("Error deserializing build code", ex);
						string text = jtoken.Value<string>("requiredMod");
						if (!text.IsNullOrEmpty())
						{
							Popup.ShowAsync("Error decoding build code - Required Mod \"" + text + "\" not found.", true, true, true, true, false);
						}
					}
				}
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		// Token: 0x04001E56 RID: 7766
		public static JsonSerializerSettings SERIALIZER_SETTINGS = new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.Auto,
			TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
			MissingMemberHandling = MissingMemberHandling.Ignore,
			SerializationBinder = new CodeCompressor.TypeBinder()
		};

		// Token: 0x0200197E RID: 6526
		[Serializable]
		public class BuildCodePayload
		{
			// Token: 0x04006603 RID: 26115
			public string gameversion;

			// Token: 0x04006604 RID: 26116
			public string buildversion;

			// Token: 0x04006605 RID: 26117
			public List<CodeCompressor.CodeEntry> modules;
		}

		// Token: 0x0200197F RID: 6527
		[Serializable]
		public class CodeEntry
		{
			// Token: 0x04006606 RID: 26118
			public Type moduleType;

			// Token: 0x04006607 RID: 26119
			public string requiredMod;

			// Token: 0x04006608 RID: 26120
			public AbstractEmbarkBuilderModuleData data;
		}

		// Token: 0x02001980 RID: 6528
		public class TypeBinder : DefaultSerializationBinder
		{
			// Token: 0x0600E3D0 RID: 58320 RVA: 0x00528560 File Offset: 0x00526760
			public override Type BindToType(string assemblyName, string typeName)
			{
				Type type;
				try
				{
					type = base.BindToType(assemblyName, typeName);
				}
				catch (Exception ex)
				{
					try
					{
						return base.BindToType(assemblyName, typeName.Replace("XRL.CharacterBuilds.", "XRL.CharacterBuilds.Qud."));
					}
					catch
					{
						MetricsManager.LogError(ex);
					}
					throw ex;
				}
				return type;
			}
		}
	}
}
