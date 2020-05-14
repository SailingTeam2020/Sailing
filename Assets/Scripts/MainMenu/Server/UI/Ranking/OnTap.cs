using UnityEngine;

public class OnTap : MonoBehaviour {
    
    [SerializeField]
    GameObject sort;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
      
	}

    public void OnClick()
    {
        sort.SetActive(true);
    }
}
