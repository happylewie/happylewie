using UnityEngine;

public class ObjectAlphaBack : MonoBehaviour
{

    public GameObject player;
    public bool overRight;
    public float objectDesiredPosition;
    public bool notFullAlpha;
    public float notFullAlphaFloat;

    // Update is called once per frame
    void Update()
    {
        if (overRight && player.transform.position.x > objectDesiredPosition)
        {
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        }
        else if (!overRight && player.transform.position.x < objectDesiredPosition)
        {
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        }
        else {
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        }
    }
}
