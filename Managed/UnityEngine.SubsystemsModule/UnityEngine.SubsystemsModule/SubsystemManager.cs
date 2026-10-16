using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.SubsystemsImplementation;

namespace UnityEngine
{
	// Token: 0x02000011 RID: 17
	[NativeHeader("Modules/Subsystems/SubsystemManager.h")]
	public static class SubsystemManager
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002310 File Offset: 0x00000510
		[RequiredByNativeCode]
		private static void ReloadSubsystemsStarted()
		{
			bool flag = SubsystemManager.reloadSubsytemsStarted != null;
			if (flag)
			{
				SubsystemManager.reloadSubsytemsStarted();
			}
			bool flag2 = SubsystemManager.beforeReloadSubsystems != null;
			if (flag2)
			{
				SubsystemManager.beforeReloadSubsystems();
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000234C File Offset: 0x0000054C
		[RequiredByNativeCode]
		private static void ReloadSubsystemsCompleted()
		{
			bool flag = SubsystemManager.reloadSubsytemsCompleted != null;
			if (flag)
			{
				SubsystemManager.reloadSubsytemsCompleted();
			}
			bool flag2 = SubsystemManager.afterReloadSubsystems != null;
			if (flag2)
			{
				SubsystemManager.afterReloadSubsystems();
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002388 File Offset: 0x00000588
		[RequiredByNativeCode]
		private static void InitializeIntegratedSubsystem(IntPtr ptr, IntegratedSubsystem subsystem)
		{
			subsystem.m_Ptr = ptr;
			subsystem.SetHandle(subsystem);
			SubsystemManager.s_IntegratedSubsystems.Add(subsystem);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000023A8 File Offset: 0x000005A8
		[RequiredByNativeCode]
		private static void ClearSubsystems()
		{
			foreach (IntegratedSubsystem integratedSubsystem in SubsystemManager.s_IntegratedSubsystems)
			{
				integratedSubsystem.m_Ptr = IntPtr.Zero;
			}
			SubsystemManager.s_IntegratedSubsystems.Clear();
			SubsystemManager.s_StandaloneSubsystems.Clear();
			SubsystemManager.s_DeprecatedSubsystems.Clear();
		}

		// Token: 0x0600003D RID: 61
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StaticConstructScriptingClassMap();

		// Token: 0x0600003E RID: 62 RVA: 0x00002424 File Offset: 0x00000624
		internal unsafe static void ReportSingleSubsystemAnalytics(string id)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(id, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = id.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				SubsystemManager.ReportSingleSubsystemAnalytics_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002478 File Offset: 0x00000678
		static SubsystemManager()
		{
			SubsystemManager.StaticConstructScriptingClassMap();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000249E File Offset: 0x0000069E
		public static void GetAllSubsystemDescriptors(List<ISubsystemDescriptor> descriptors)
		{
			SubsystemDescriptorStore.GetAllSubsystemDescriptors(descriptors);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000024A8 File Offset: 0x000006A8
		public static void GetSubsystemDescriptors<T>(List<T> descriptors) where T : ISubsystemDescriptor
		{
			SubsystemDescriptorStore.GetSubsystemDescriptors<T>(descriptors);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000024B2 File Offset: 0x000006B2
		public static void GetSubsystems<T>(List<T> subsystems) where T : ISubsystem
		{
			subsystems.Clear();
			SubsystemManager.AddSubsystemSubset<IntegratedSubsystem, T>(SubsystemManager.s_IntegratedSubsystems, subsystems);
			SubsystemManager.AddSubsystemSubset<SubsystemWithProvider, T>(SubsystemManager.s_StandaloneSubsystems, subsystems);
			SubsystemManager.AddSubsystemSubset<Subsystem, T>(SubsystemManager.s_DeprecatedSubsystems, subsystems);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000024E0 File Offset: 0x000006E0
		private static void AddSubsystemSubset<TBaseTypeInList, TQueryType>(List<TBaseTypeInList> copyFrom, List<TQueryType> copyTo) where TBaseTypeInList : ISubsystem where TQueryType : ISubsystem
		{
			foreach (TBaseTypeInList tbaseTypeInList in copyFrom)
			{
				TQueryType tqueryType;
				bool flag;
				if (tbaseTypeInList is TQueryType)
				{
					tqueryType = tbaseTypeInList as TQueryType;
					flag = true;
				}
				else
				{
					flag = false;
				}
				bool flag2 = flag;
				if (flag2)
				{
					copyTo.Add(tqueryType);
				}
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000044 RID: 68 RVA: 0x0000255C File Offset: 0x0000075C
		// (remove) Token: 0x06000045 RID: 69 RVA: 0x00002590 File Offset: 0x00000790
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action beforeReloadSubsystems;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000046 RID: 70 RVA: 0x000025C4 File Offset: 0x000007C4
		// (remove) Token: 0x06000047 RID: 71 RVA: 0x000025F8 File Offset: 0x000007F8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action afterReloadSubsystems;

		// Token: 0x06000048 RID: 72 RVA: 0x0000262C File Offset: 0x0000082C
		[VisibleToOtherModules(new string[] { "UnityEngine.XRModule" })]
		internal static IntegratedSubsystem GetIntegratedSubsystemByPtr(IntPtr ptr)
		{
			foreach (IntegratedSubsystem integratedSubsystem in SubsystemManager.s_IntegratedSubsystems)
			{
				bool flag = integratedSubsystem.m_Ptr == ptr;
				if (flag)
				{
					return integratedSubsystem;
				}
			}
			return null;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002698 File Offset: 0x00000898
		internal static void RemoveIntegratedSubsystemByPtr(IntPtr ptr)
		{
			for (int i = 0; i < SubsystemManager.s_IntegratedSubsystems.Count; i++)
			{
				bool flag = SubsystemManager.s_IntegratedSubsystems[i].m_Ptr != ptr;
				if (!flag)
				{
					SubsystemManager.s_IntegratedSubsystems[i].m_Ptr = IntPtr.Zero;
					SubsystemManager.s_IntegratedSubsystems.RemoveAt(i);
					break;
				}
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000026FF File Offset: 0x000008FF
		internal static void AddStandaloneSubsystem(SubsystemWithProvider subsystem)
		{
			SubsystemManager.s_StandaloneSubsystems.Add(subsystem);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002710 File Offset: 0x00000910
		internal static bool RemoveStandaloneSubsystem(SubsystemWithProvider subsystem)
		{
			return SubsystemManager.s_StandaloneSubsystems.Remove(subsystem);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002730 File Offset: 0x00000930
		internal static SubsystemWithProvider FindStandaloneSubsystemByDescriptor(SubsystemDescriptorWithProvider descriptor)
		{
			foreach (SubsystemWithProvider subsystemWithProvider in SubsystemManager.s_StandaloneSubsystems)
			{
				bool flag = subsystemWithProvider.descriptor == descriptor;
				if (flag)
				{
					return subsystemWithProvider;
				}
			}
			return null;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002798 File Offset: 0x00000998
		[Obsolete("Use GetSubsystems instead. (UnityUpgradable) -> GetSubsystems<T>(*)", false)]
		public static void GetInstances<T>(List<T> subsystems) where T : ISubsystem
		{
			SubsystemManager.GetSubsystems<T>(subsystems);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000027A2 File Offset: 0x000009A2
		internal static void AddDeprecatedSubsystem(Subsystem subsystem)
		{
			SubsystemManager.s_DeprecatedSubsystems.Add(subsystem);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000027B0 File Offset: 0x000009B0
		internal static bool RemoveDeprecatedSubsystem(Subsystem subsystem)
		{
			return SubsystemManager.s_DeprecatedSubsystems.Remove(subsystem);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000027C0 File Offset: 0x000009C0
		internal static Subsystem FindDeprecatedSubsystemByDescriptor(SubsystemDescriptor descriptor)
		{
			foreach (Subsystem subsystem in SubsystemManager.s_DeprecatedSubsystems)
			{
				bool flag = subsystem.m_SubsystemDescriptor == descriptor;
				if (flag)
				{
					return subsystem;
				}
			}
			return null;
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000051 RID: 81 RVA: 0x00002828 File Offset: 0x00000A28
		// (remove) Token: 0x06000052 RID: 82 RVA: 0x0000285C File Offset: 0x00000A5C
		[Obsolete("Use beforeReloadSubsystems instead. (UnityUpgradable) -> beforeReloadSubsystems", false)]
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action reloadSubsytemsStarted;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000053 RID: 83 RVA: 0x00002890 File Offset: 0x00000A90
		// (remove) Token: 0x06000054 RID: 84 RVA: 0x000028C4 File Offset: 0x00000AC4
		[Obsolete("Use afterReloadSubsystems instead. (UnityUpgradable) -> afterReloadSubsystems", false)]
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action reloadSubsytemsCompleted;

		// Token: 0x06000055 RID: 85
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReportSingleSubsystemAnalytics_Injected(ref ManagedSpanWrapper id);

		// Token: 0x04000009 RID: 9
		private static List<IntegratedSubsystem> s_IntegratedSubsystems = new List<IntegratedSubsystem>();

		// Token: 0x0400000A RID: 10
		private static List<SubsystemWithProvider> s_StandaloneSubsystems = new List<SubsystemWithProvider>();

		// Token: 0x0400000B RID: 11
		private static List<Subsystem> s_DeprecatedSubsystems = new List<Subsystem>();
	}
}
