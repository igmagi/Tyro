
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Win : MonoBehaviour
{
    GameObject player;
    public bool win = false;
    public Camera cam;
    public GameObject levelcompleted;
    // Start is called before the first frame update
    void Start()
    {
        win = false;
        player = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (win)
        {
            levelcompleted.SetActive(true);
            this.GetComponent<CharacterController>().enabled = false;
            cam.GetComponent<CinemachineBrain>().enabled = false;
            //player.disabled
        }
    }   

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("WinObject"))
        {
            win = true;
        }
    }
}
