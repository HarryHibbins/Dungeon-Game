using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Slider slider;
    public GameObject player;
    public GameObject flame;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        flame = player.gameObject.transform.Find("torch").gameObject.transform.Find("Flame Holder").gameObject.transform.Find("WallTorch").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
        GameObject torch = player.gameObject.transform.Find("torch").gameObject;
        GameObject flame_holder = torch.transform.Find("Flame Holder").gameObject;
        flame = flame_holder.transform.Find("WallTorch").gameObject;

        if (flame == null)
        {
            Debug.Log("nah");
            slider.value = 30.0f;
        }
        else
        {
            Debug.Log("yeah");
            slider.value = flame.GetComponent<FixedTorch>().torchTimer;
        }
        
    }
}
