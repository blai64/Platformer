using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class proepiButton : MonoBehaviour {

	public Button yourButton;
	public Image yourImage;
	public Sprite pro1;
	public Sprite pro2;
	public Sprite pro3;
	public Sprite pro4;

	public string nextScene;

	private int cnt = 0;

	// Use this for initialization
	void Start () {
		yourImage.sprite = pro1;

		Button btn = yourButton.GetComponent<Button> ();
		btn.onClick.AddListener (changeSprite);
	}
	
	// Update is called once per frame
	void changeSprite () {
		cnt++;
		if (cnt == 1)
			yourImage.sprite = pro2;
		else if (cnt == 2)
			yourImage.sprite = pro3;
		else if (cnt == 3)
			yourImage.sprite = pro4;
		else
			SceneManager.LoadScene (nextScene);
			
	}
}
