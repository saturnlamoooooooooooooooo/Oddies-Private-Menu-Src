using MelonLoader;
using Photon.Pun;
using UnityEngine;

namespace OddieMenuTemp
{
	public class Load : MelonMod
	{
        private bool modInitted = false;

        public override void OnUpdate()
		{
			string userId = PhotonNetwork.LocalPlayer.UserId;
			if (!modInitted)
			{
				modInitted = true;
				ButtonsHandler.Next_Page();
				Menu.Init();
			}
			Menu.Update();
			if (Menu.nojumpy)
			{
				Application.CancelQuit();
			}
		}

		public override void OnFixedUpdate()
		{
			if (Menu.nojumpy)
			{
				NJQ.FixedUpdate();
			}
		}
	}
}
