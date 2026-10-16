using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200013D RID: 317
	public class InitParams : IDisposable
	{
		// Token: 0x06000C10 RID: 3088 RVA: 0x0001A118 File Offset: 0x00018318
		internal InitParams(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0001A134 File Offset: 0x00018334
		public InitParams(string _clientID, string _clientSecret, string _configFilePath, string _galaxyModulePath, bool _loadModule, string _storagePath)
			: this(GalaxyInstancePINVOKE.new_InitParams__SWIG_0(_clientID, _clientSecret, _configFilePath, _galaxyModulePath, _loadModule, _storagePath), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0001A15B File Offset: 0x0001835B
		public InitParams(string _clientID, string _clientSecret, string _configFilePath, string _galaxyModulePath, bool _loadModule)
			: this(GalaxyInstancePINVOKE.new_InitParams__SWIG_1(_clientID, _clientSecret, _configFilePath, _galaxyModulePath, _loadModule), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0001A180 File Offset: 0x00018380
		public InitParams(string _clientID, string _clientSecret, string _configFilePath, string _galaxyModulePath)
			: this(GalaxyInstancePINVOKE.new_InitParams__SWIG_2(_clientID, _clientSecret, _configFilePath, _galaxyModulePath), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0001A1A3 File Offset: 0x000183A3
		public InitParams(string _clientID, string _clientSecret, string _configFilePath)
			: this(GalaxyInstancePINVOKE.new_InitParams__SWIG_3(_clientID, _clientSecret, _configFilePath), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0001A1C4 File Offset: 0x000183C4
		public InitParams(string _clientID, string _clientSecret)
			: this(GalaxyInstancePINVOKE.new_InitParams__SWIG_4(_clientID, _clientSecret), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0001A1E4 File Offset: 0x000183E4
		internal static HandleRef getCPtr(InitParams obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0001A204 File Offset: 0x00018404
		~InitParams()
		{
			this.Dispose();
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0001A234 File Offset: 0x00018434
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_InitParams(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x0001A2D4 File Offset: 0x000184D4
		// (set) Token: 0x06000C19 RID: 3097 RVA: 0x0001A2B4 File Offset: 0x000184B4
		public string clientID
		{
			get
			{
				string text = GalaxyInstancePINVOKE.InitParams_clientID_get(this.swigCPtr);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
				return text;
			}
			set
			{
				GalaxyInstancePINVOKE.InitParams_clientID_set(this.swigCPtr, value);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x0001A31C File Offset: 0x0001851C
		// (set) Token: 0x06000C1B RID: 3099 RVA: 0x0001A2FE File Offset: 0x000184FE
		public string clientSecret
		{
			get
			{
				string text = GalaxyInstancePINVOKE.InitParams_clientSecret_get(this.swigCPtr);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
				return text;
			}
			set
			{
				GalaxyInstancePINVOKE.InitParams_clientSecret_set(this.swigCPtr, value);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x0001A364 File Offset: 0x00018564
		// (set) Token: 0x06000C1D RID: 3101 RVA: 0x0001A346 File Offset: 0x00018546
		public string configFilePath
		{
			get
			{
				string text = GalaxyInstancePINVOKE.InitParams_configFilePath_get(this.swigCPtr);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
				return text;
			}
			set
			{
				GalaxyInstancePINVOKE.InitParams_configFilePath_set(this.swigCPtr, value);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x0001A3AC File Offset: 0x000185AC
		// (set) Token: 0x06000C1F RID: 3103 RVA: 0x0001A38E File Offset: 0x0001858E
		public string storagePath
		{
			get
			{
				string text = GalaxyInstancePINVOKE.InitParams_storagePath_get(this.swigCPtr);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
				return text;
			}
			set
			{
				GalaxyInstancePINVOKE.InitParams_storagePath_set(this.swigCPtr, value);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x0001A3F4 File Offset: 0x000185F4
		// (set) Token: 0x06000C21 RID: 3105 RVA: 0x0001A3D6 File Offset: 0x000185D6
		public string galaxyModulePath
		{
			get
			{
				string text = GalaxyInstancePINVOKE.InitParams_galaxyModulePath_get(this.swigCPtr);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
				return text;
			}
			set
			{
				GalaxyInstancePINVOKE.InitParams_galaxyModulePath_set(this.swigCPtr, value);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x0001A43C File Offset: 0x0001863C
		// (set) Token: 0x06000C23 RID: 3107 RVA: 0x0001A41E File Offset: 0x0001861E
		public bool loadModule
		{
			get
			{
				bool flag = GalaxyInstancePINVOKE.InitParams_loadModule_get(this.swigCPtr);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
				return flag;
			}
			set
			{
				GalaxyInstancePINVOKE.InitParams_loadModule_set(this.swigCPtr, value);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
		}

		// Token: 0x04000243 RID: 579
		private HandleRef swigCPtr;

		// Token: 0x04000244 RID: 580
		protected bool swigCMemOwn;
	}
}
