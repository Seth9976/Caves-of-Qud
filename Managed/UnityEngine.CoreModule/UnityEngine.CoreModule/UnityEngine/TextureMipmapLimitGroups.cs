using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001E0 RID: 480
	[NativeHeader("Runtime/Graphics/QualitySettings.h")]
	[StaticAccessor("GetQualitySettings()", StaticAccessorType.Dot)]
	public static class TextureMipmapLimitGroups
	{
		// Token: 0x060014E5 RID: 5349
		[NativeName("GetTextureMipmapLimitGroupNames")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetGroups();

		// Token: 0x060014E6 RID: 5350 RVA: 0x000236CC File Offset: 0x000218CC
		[NativeName("HasTextureMipmapLimitGroup")]
		public unsafe static bool HasGroup([NotNull] string groupName)
		{
			if (groupName == null)
			{
				ThrowHelper.ThrowArgumentNullException(groupName, "groupName");
			}
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(groupName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = groupName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = TextureMipmapLimitGroups.HasGroup_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060014E7 RID: 5351
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasGroup_Injected(ref ManagedSpanWrapper groupName);
	}
}
