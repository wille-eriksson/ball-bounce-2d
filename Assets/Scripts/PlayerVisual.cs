using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour {

	[SerializeField] private SpriteRenderer spriteRenderer;


	[SerializeField] private Color normalColor;
	[SerializeField] private Color ballColor;

	private void Awake() {
		spriteRenderer.color = normalColor;
	}

	public void ChangeAppearance(bool isBall) {
		if (isBall) {
			spriteRenderer.color = ballColor;
		} else {
			spriteRenderer.color = normalColor;
		}
	}

}
