using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace HarmonyLib
{
	// Token: 0x0200007C RID: 124
	[Serializable]
	public class Patch : IComparable
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000CDB0 File Offset: 0x0000AFB0
		// (set) Token: 0x0600024B RID: 587 RVA: 0x0000CE48 File Offset: 0x0000B048
		public MethodInfo PatchMethod
		{
			get
			{
				if (this.patchMethod == null)
				{
					Module module = (from a in AppDomain.CurrentDomain.GetAssemblies()
						where !a.FullName.StartsWith("Microsoft.VisualStudio")
						select a).SelectMany<Assembly, Module>((Assembly a) => a.GetLoadedModules()).First<Module>((Module m) => m.ModuleVersionId.ToString() == this.moduleGUID);
					this.patchMethod = (MethodInfo)module.ResolveMethod(this.methodToken);
				}
				return this.patchMethod;
			}
			set
			{
				this.patchMethod = value;
				this.methodToken = this.patchMethod.MetadataToken;
				this.moduleGUID = this.patchMethod.Module.ModuleVersionId.ToString();
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000CE94 File Offset: 0x0000B094
		public Patch(MethodInfo patch, int index, string owner, int priority, string[] before, string[] after, bool debug)
		{
			if (patch is DynamicMethod)
			{
				throw new Exception("Cannot directly reference dynamic method \"" + patch.FullDescription() + "\" in Harmony. Use a factory method instead that will return the dynamic method.");
			}
			this.index = index;
			this.owner = owner;
			this.priority = ((priority == -1) ? 400 : priority);
			this.before = before ?? new string[0];
			this.after = after ?? new string[0];
			this.debug = debug;
			this.PatchMethod = patch;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000CF1F File Offset: 0x0000B11F
		public Patch(HarmonyMethod method, int index, string owner)
			: this(method.method, index, owner, method.priority, method.before, method.after, method.debug.GetValueOrDefault())
		{
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000CF4C File Offset: 0x0000B14C
		internal Patch(int index, string owner, int priority, string[] before, string[] after, bool debug, int methodToken, string moduleGUID)
		{
			this.index = index;
			this.owner = owner;
			this.priority = ((priority == -1) ? 400 : priority);
			this.before = before ?? new string[0];
			this.after = after ?? new string[0];
			this.debug = debug;
			this.methodToken = methodToken;
			this.moduleGUID = moduleGUID;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000CFBC File Offset: 0x0000B1BC
		public MethodInfo GetMethod(MethodBase original)
		{
			MethodInfo methodInfo = this.PatchMethod;
			if (methodInfo.ReturnType != typeof(DynamicMethod) && methodInfo.ReturnType != typeof(MethodInfo))
			{
				return methodInfo;
			}
			if (!methodInfo.IsStatic)
			{
				return methodInfo;
			}
			ParameterInfo[] parameters = methodInfo.GetParameters();
			if (parameters.Length != 1)
			{
				return methodInfo;
			}
			if (parameters[0].ParameterType != typeof(MethodBase))
			{
				return methodInfo;
			}
			return methodInfo.Invoke(null, new object[] { original }) as MethodInfo;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000D04A File Offset: 0x0000B24A
		public override bool Equals(object obj)
		{
			return obj != null && obj is Patch && this.PatchMethod == ((Patch)obj).PatchMethod;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000D06F File Offset: 0x0000B26F
		public int CompareTo(object obj)
		{
			return PatchInfoSerialization.PriorityComparer(obj, this.index, this.priority);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000D083 File Offset: 0x0000B283
		public override int GetHashCode()
		{
			return this.PatchMethod.GetHashCode();
		}

		// Token: 0x04000169 RID: 361
		public readonly int index;

		// Token: 0x0400016A RID: 362
		public readonly string owner;

		// Token: 0x0400016B RID: 363
		public readonly int priority;

		// Token: 0x0400016C RID: 364
		public readonly string[] before;

		// Token: 0x0400016D RID: 365
		public readonly string[] after;

		// Token: 0x0400016E RID: 366
		public readonly bool debug;

		// Token: 0x0400016F RID: 367
		[NonSerialized]
		private MethodInfo patchMethod;

		// Token: 0x04000170 RID: 368
		private int methodToken;

		// Token: 0x04000171 RID: 369
		private string moduleGUID;
	}
}
