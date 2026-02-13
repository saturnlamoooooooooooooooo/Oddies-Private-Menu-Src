using UnityEngine;

namespace ONM.Mods
{
	internal class Noclip
	{
		public static void Enabled()
		{
			foreach (MeshCollider meshCollider in UnityEngine.Object.FindObjectsOfType<MeshCollider>())
			{
				meshCollider.enabled = false;
			}
		}
		
		public static void Disable()
		{
			foreach (MeshCollider meshCollider in UnityEngine.Object.FindObjectsOfType<MeshCollider>())
			{
				meshCollider.enabled = true;
			}
		}
	}
}
