using System;
using CodeBase.Data;

namespace CodeBase.Infrastructure
{
	public class PersistentProgressService : IPersistentProgressService
	{
		public PlayerProgress Progress { get; set; }
	}
}
