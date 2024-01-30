using UnityEngine;
using UnityEngine.AI;
using CodeBase.Infrastructure;

namespace CodeBase.Enemy
{
	public class AgentMoveToHero : Follow
	{
		[SerializeField] private NavMeshAgent agent;

		private Transform _heroTransform;
		private const float minDistance = 1f;

		private IGameFactory _gameFactory;

		public void Constract(Transform heroTransform)
		{
			_heroTransform = heroTransform;
		}

		private void Update()
		{
			if (!_heroTransform)
				return;

			if (Vector3.Distance(agent.transform.position, _heroTransform.position) < minDistance)
				return;

			agent.destination = _heroTransform.position;
		}
	}
}