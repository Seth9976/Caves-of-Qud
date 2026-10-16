using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using MonoMod.Utils;

namespace HarmonyLib
{
	// Token: 0x02000062 RID: 98
	public class Harmony
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000B70D File Offset: 0x0000990D
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x0000B715 File Offset: 0x00009915
		public string Id { get; private set; }

		// Token: 0x060001B9 RID: 441 RVA: 0x0000B720 File Offset: 0x00009920
		public Harmony(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentException("id cannot be null or empty");
			}
			try
			{
				string text = Environment.GetEnvironmentVariable("HARMONY_DEBUG");
				if (text != null && text.Length > 0)
				{
					text = text.Trim();
					Harmony.DEBUG = text == "1" || bool.Parse(text);
				}
			}
			catch
			{
			}
			if (Harmony.DEBUG)
			{
				Assembly assembly = typeof(Harmony).Assembly;
				Version version = assembly.GetName().Version;
				string text2 = assembly.Location;
				string text3 = Environment.Version.ToString();
				string text4 = Environment.OSVersion.Platform.ToString();
				if (string.IsNullOrEmpty(text2))
				{
					text2 = new Uri(assembly.CodeBase).LocalPath;
				}
				int size = IntPtr.Size;
				Platform platform = PlatformHelper.Current;
				FileLog.Log(string.Format("### Harmony id={0}, version={1}, location={2}, env/clr={3}, platform={4}, ptrsize:runtime/env={5}/{6}", new object[] { id, version, text2, text3, text4, size, platform }));
				MethodBase outsideCaller = AccessTools.GetOutsideCaller();
				if (outsideCaller.DeclaringType != null)
				{
					Assembly assembly2 = outsideCaller.DeclaringType.Assembly;
					text2 = assembly2.Location;
					if (string.IsNullOrEmpty(text2))
					{
						text2 = new Uri(assembly2.CodeBase).LocalPath;
					}
					FileLog.Log("### Started from " + outsideCaller.FullDescription() + ", location " + text2);
					FileLog.Log(string.Format("### At {0:yyyy-MM-dd hh.mm.ss}", DateTime.Now));
				}
			}
			this.Id = id;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000B8CC File Offset: 0x00009ACC
		public void PatchAll()
		{
			Assembly assembly = new StackTrace().GetFrame(1).GetMethod().ReflectedType.Assembly;
			this.PatchAll(assembly);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000B8FB File Offset: 0x00009AFB
		public PatchProcessor CreateProcessor(MethodBase original)
		{
			return new PatchProcessor(this, original);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000B904 File Offset: 0x00009B04
		public PatchClassProcessor CreateClassProcessor(Type type)
		{
			return new PatchClassProcessor(this, type);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000B90D File Offset: 0x00009B0D
		public ReversePatcher CreateReversePatcher(MethodBase original, HarmonyMethod standin)
		{
			return new ReversePatcher(this, original, standin);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000B917 File Offset: 0x00009B17
		public void PatchAll(Assembly assembly)
		{
			AccessTools.GetTypesFromAssembly(assembly).Do<Type>(delegate(Type type)
			{
				this.CreateClassProcessor(type).Patch();
			});
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000B930 File Offset: 0x00009B30
		public MethodInfo Patch(MethodBase original, HarmonyMethod prefix = null, HarmonyMethod postfix = null, HarmonyMethod transpiler = null, HarmonyMethod finalizer = null)
		{
			PatchProcessor patchProcessor = this.CreateProcessor(original);
			patchProcessor.AddPrefix(prefix);
			patchProcessor.AddPostfix(postfix);
			patchProcessor.AddTranspiler(transpiler);
			patchProcessor.AddFinalizer(finalizer);
			return patchProcessor.Patch();
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000B960 File Offset: 0x00009B60
		public static MethodInfo ReversePatch(MethodBase original, HarmonyMethod standin, MethodInfo transpiler = null)
		{
			return PatchFunctions.ReversePatch(standin, original, transpiler);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000B96C File Offset: 0x00009B6C
		public void UnpatchAll(string harmonyID = null)
		{
			Harmony.<>c__DisplayClass13_0 CS$<>8__locals1 = new Harmony.<>c__DisplayClass13_0();
			CS$<>8__locals1.harmonyID = harmonyID;
			CS$<>8__locals1.<>4__this = this;
			using (List<MethodBase>.Enumerator enumerator = Harmony.GetAllPatchedMethods().ToList<MethodBase>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MethodBase original = enumerator.Current;
					bool flag = original.HasMethodBody();
					Patches patchInfo2 = Harmony.GetPatchInfo(original);
					if (flag)
					{
						patchInfo2.Postfixes.DoIf<Patch>(new Func<Patch, bool>(CS$<>8__locals1.<UnpatchAll>g__IDCheck|0), delegate(Patch patchInfo)
						{
							CS$<>8__locals1.<>4__this.Unpatch(original, patchInfo.PatchMethod);
						});
						patchInfo2.Prefixes.DoIf<Patch>(new Func<Patch, bool>(CS$<>8__locals1.<UnpatchAll>g__IDCheck|0), delegate(Patch patchInfo)
						{
							CS$<>8__locals1.<>4__this.Unpatch(original, patchInfo.PatchMethod);
						});
					}
					patchInfo2.Transpilers.DoIf<Patch>(new Func<Patch, bool>(CS$<>8__locals1.<UnpatchAll>g__IDCheck|0), delegate(Patch patchInfo)
					{
						CS$<>8__locals1.<>4__this.Unpatch(original, patchInfo.PatchMethod);
					});
					if (flag)
					{
						patchInfo2.Finalizers.DoIf<Patch>(new Func<Patch, bool>(CS$<>8__locals1.<UnpatchAll>g__IDCheck|0), delegate(Patch patchInfo)
						{
							CS$<>8__locals1.<>4__this.Unpatch(original, patchInfo.PatchMethod);
						});
					}
				}
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000BAA4 File Offset: 0x00009CA4
		public void Unpatch(MethodBase original, HarmonyPatchType type, string harmonyID = "*")
		{
			this.CreateProcessor(original).Unpatch(type, harmonyID);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000BAB5 File Offset: 0x00009CB5
		public void Unpatch(MethodBase original, MethodInfo patch)
		{
			this.CreateProcessor(original).Unpatch(patch);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000BAC8 File Offset: 0x00009CC8
		public static bool HasAnyPatches(string harmonyID)
		{
			return (from original in Harmony.GetAllPatchedMethods()
				select Harmony.GetPatchInfo(original)).Any<Patches>((Patches info) => info.Owners.Contains(harmonyID));
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000BB1C File Offset: 0x00009D1C
		public static Patches GetPatchInfo(MethodBase method)
		{
			return PatchProcessor.GetPatchInfo(method);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000BB24 File Offset: 0x00009D24
		public IEnumerable<MethodBase> GetPatchedMethods()
		{
			return from original in Harmony.GetAllPatchedMethods()
				where Harmony.GetPatchInfo(original).Owners.Contains(this.Id)
				select original;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000BB3C File Offset: 0x00009D3C
		public static IEnumerable<MethodBase> GetAllPatchedMethods()
		{
			return PatchProcessor.GetAllPatchedMethods();
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000BB43 File Offset: 0x00009D43
		public static MethodBase GetOriginalMethod(MethodInfo replacement)
		{
			if (replacement == null)
			{
				throw new ArgumentNullException("replacement");
			}
			return HarmonySharedState.GetOriginal(replacement);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000BB5F File Offset: 0x00009D5F
		public static MethodBase GetMethodFromStackframe(StackFrame frame)
		{
			if (frame == null)
			{
				throw new ArgumentNullException("frame");
			}
			return HarmonySharedState.FindReplacement(frame) ?? frame.GetMethod();
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000BB80 File Offset: 0x00009D80
		public static MethodBase GetOriginalMethodFromStackframe(StackFrame frame)
		{
			MethodBase methodBase = Harmony.GetMethodFromStackframe(frame);
			MethodInfo methodInfo = methodBase as MethodInfo;
			if (methodInfo != null)
			{
				methodBase = Harmony.GetOriginalMethod(methodInfo) ?? methodBase;
			}
			return methodBase;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000BBAB File Offset: 0x00009DAB
		public static Dictionary<string, Version> VersionInfo(out Version currentVersion)
		{
			return PatchProcessor.VersionInfo(out currentVersion);
		}

		// Token: 0x04000126 RID: 294
		public static bool DEBUG;
	}
}
