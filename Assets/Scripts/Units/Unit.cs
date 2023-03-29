using UnityEngine.AI;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Units
{
	public class Unit : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer _selectedSprite;
		[SerializeField] private NavMeshAgent _agent;

		public void Selected()
		{
			_selectedSprite.transform.DOScale(0, .2f).From().SetEase(Ease.OutBack);
			_selectedSprite.enabled = true;
		}

		public void Deselected()
		{
			_selectedSprite.enabled = false;
		}

		public void MoveToTargetPosition(Vector3 position)
		{
			_agent.SetDestination(position);
		}
	}
}
