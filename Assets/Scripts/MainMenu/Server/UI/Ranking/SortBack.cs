using UnityEngine;

public class SortBack : MonoBehaviour {

    [SerializeField]
    GameObject Self;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    public void OnClick()
    {
        Self.SetActive(false);
    }
}
