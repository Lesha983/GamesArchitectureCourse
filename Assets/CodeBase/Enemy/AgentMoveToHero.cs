using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using System;

namespace CodeBase.Enemy
{
	public class AgentMoveToHero : Follow
	{
		[SerializeField] private NavMeshAgent agent;

		private Transform _heroTransform;
		private const float minDistance = 1f;

		private IGameFactory _gameFactory;

		private void Start()
		{
			_gameFactory = AllServices.Container.Single<IGameFactory>();

			if (_gameFactory.HeroGameObject != null)
				InitializeHeroTransform();
			else
				_gameFactory.HeroCreated += HeroCreated;
		}

		private void Update()
		{
			if (!_heroTransform)
				return;

			if (Vector3.Distance(agent.transform.position, _heroTransform.position) < minDistance)
				return;

			agent.destination = _heroTransform.position;
		}

		private void HeroCreated() =>
			InitializeHeroTransform();

		private void InitializeHeroTransform() =>
			_heroTransform = _gameFactory.HeroGameObject.transform;
	}
}