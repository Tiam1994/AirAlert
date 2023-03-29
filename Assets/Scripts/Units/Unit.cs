using DG.Tweening;
using UnityEngine;

namespace Runtime.Units
{
	public class Unit : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer _selectedSprite;

		public void Selected()
		{
			_selectedSprite.transform.DOScale(0, .2f).From().SetEase(Ease.OutBack);
			_selectedSprite.enabled = true;
		}

		public void Deselected()
		{
			_selectedSprite.enabled = false;
		}
	}
}
