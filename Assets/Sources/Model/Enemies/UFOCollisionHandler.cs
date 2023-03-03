using UnityEngine;

namespace Asteroids.Model
{
	public class UFOCollisionHandler
	{
		public void Handle(UFO firstUFO, UFO secondUFO)
		{
			if (firstUFO.Team == secondUFO.Team)
				return;

			if (Random.Range(0, 2) == 0)
			{
				GlobalTeamsLists.Teams[firstUFO.Team].Remove(firstUFO);
				firstUFO.Destroy();
			}

			else
			{
				GlobalTeamsLists.Teams[secondUFO.Team].Remove(secondUFO);
				secondUFO.Destroy();
			}
		}
	}
}
