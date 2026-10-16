using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using UnityEngine.Bindings;

namespace Unity.Content
{
	// Token: 0x02000096 RID: 150
	[NativeHeader("Runtime/Misc/ContentNamespace.h")]
	[StaticAccessor("GetContentNamespaceManager()", StaticAccessorType.Dot)]
	public struct ContentNamespace
	{
		// Token: 0x06000311 RID: 785 RVA: 0x00006154 File Offset: 0x00004354
		public string GetName()
		{
			this.ThrowIfInvalidNamespace();
			return ContentNamespace.GetNamespaceName(this);
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00006178 File Offset: 0x00004378
		public bool IsValid
		{
			get
			{
				return ContentNamespace.IsNamespaceHandleValid(this);
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00006198 File Offset: 0x00004398
		public void Delete()
		{
			bool flag = this.Id == ContentNamespace.s_Default.Id;
			if (flag)
			{
				throw new InvalidOperationException("Cannot delete the default namespace.");
			}
			this.ThrowIfInvalidNamespace();
			ContentNamespace.RemoveNamespace(this);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000061DC File Offset: 0x000043DC
		private void ThrowIfInvalidNamespace()
		{
			bool flag = !this.IsValid;
			if (flag)
			{
				throw new InvalidOperationException("The provided namespace is invalid. Did you already delete it?");
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00006204 File Offset: 0x00004404
		public static ContentNamespace Default
		{
			get
			{
				bool flag = !ContentNamespace.s_defaultInitialized;
				if (flag)
				{
					ContentNamespace.s_defaultInitialized = true;
					ContentNamespace.s_Default = ContentNamespace.GetOrCreateNamespace("default");
				}
				return ContentNamespace.s_Default;
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00006240 File Offset: 0x00004440
		public static ContentNamespace GetOrCreateNamespace(string name)
		{
			bool flag = ContentNamespace.s_ValidName.IsMatch(name);
			if (flag)
			{
				return ContentNamespace.GetOrCreate(name);
			}
			throw new InvalidOperationException("Namespace name can only contain alphanumeric characters and a maximum length of 16 characters.");
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00006274 File Offset: 0x00004474
		public static ContentNamespace[] GetAll()
		{
			ContentNamespace[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				ContentNamespace.GetAll_Injected(out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				ContentNamespace[] array;
				blittableArrayWrapper.Unmarshal<ContentNamespace>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000062A8 File Offset: 0x000044A8
		internal unsafe static ContentNamespace GetOrCreate(string name)
		{
			ContentNamespace contentNamespace2;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ContentNamespace contentNamespace;
				ContentNamespace.GetOrCreate_Injected(ref managedSpanWrapper, out contentNamespace);
			}
			finally
			{
				char* ptr = null;
				ContentNamespace contentNamespace;
				contentNamespace2 = contentNamespace;
			}
			return contentNamespace2;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00006304 File Offset: 0x00004504
		internal static void RemoveNamespace(ContentNamespace ns)
		{
			ContentNamespace.RemoveNamespace_Injected(ref ns);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00006318 File Offset: 0x00004518
		internal static string GetNamespaceName(ContentNamespace ns)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				ContentNamespace.GetNamespaceName_Injected(ref ns, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000634C File Offset: 0x0000454C
		internal static bool IsNamespaceHandleValid(ContentNamespace ns)
		{
			return ContentNamespace.IsNamespaceHandleValid_Injected(ref ns);
		}

		// Token: 0x0600031D RID: 797
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAll_Injected(out BlittableArrayWrapper ret);

		// Token: 0x0600031E RID: 798
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetOrCreate_Injected(ref ManagedSpanWrapper name, out ContentNamespace ret);

		// Token: 0x0600031F RID: 799
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveNamespace_Injected([In] ref ContentNamespace ns);

		// Token: 0x06000320 RID: 800
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetNamespaceName_Injected([In] ref ContentNamespace ns, out ManagedSpanWrapper ret);

		// Token: 0x06000321 RID: 801
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsNamespaceHandleValid_Injected([In] ref ContentNamespace ns);

		// Token: 0x04000254 RID: 596
		internal ulong Id;

		// Token: 0x04000255 RID: 597
		private static bool s_defaultInitialized = false;

		// Token: 0x04000256 RID: 598
		private static ContentNamespace s_Default;

		// Token: 0x04000257 RID: 599
		private static Regex s_ValidName = new Regex("^[a-zA-Z0-9]{1,16}$", RegexOptions.Compiled);
	}
}
