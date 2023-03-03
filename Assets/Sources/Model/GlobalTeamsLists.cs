using System.Collections.Generic;

namespace Asteroids.Model
{
	public static class GlobalTeamsLists
	{
		public static List<List<UFO>> Teams { get; }

		public static int TotalAmount { get => Teams[0].Count + Teams[1].Count; }

		static GlobalTeamsLists() 
		{
			Teams = new List<List<UFO>>(2) { new List<UFO>() { }, new List<UFO> { } };
		}
	}
}
