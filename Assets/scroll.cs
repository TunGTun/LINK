using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class scroll : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
	public ScrollRect scroll1;
	public GameObject GuidePanel;
	public void OpenGuide()
	{
		GuidePanel.SetActive(true);
		StartCoroutine(ResetScrollPos());
	}

	IEnumerator ResetScrollPos()
	{
		yield return null; // Đợi layout tính xong
		scroll1.verticalNormalizedPosition = 1f; // hoặc 0f để cuộn xuống đáy
	}
	private IEnumerator ResetScroll()
	{
		// Đợi 2 frame để layout system và canvas cập nhật xong
		yield return null;
		yield return null;

		scroll1.verticalNormalizedPosition = 1f; // scroll về top (hoặc 0f để xuống bottom)
	}
}
