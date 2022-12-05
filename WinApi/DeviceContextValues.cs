using System;

namespace WinApi
{
	
	
	public enum DeviceContextValues : uint
	{
		
		Window = 1u,
		
		Cache = 2u,
		
		NoResetAttrs = 4u,
		
		ClipChildren = 8u,
		
		ClipSiblings = 16u,
		
		ParentClip = 32u,
		
		ExcludeRgn = 64u,
		
		IntersectRgn = 128u,
		
		ExcludeUpdate = 256u,
		
		IntersectUpdate = 512u,
		
		LockWindowUpdate = 1024u,
		
		Validate = 2097152u
	}
}
