using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarComponent : MonoBehaviour
{
    public static CarComponent Instance;
    public List<GameObject> models = new List<GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        for (int i = 0, length = models.Count; i < length; i++)
        {
            if (models[i])
                models[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayInCar(string toDisplay)
    {
        if (toDisplay.ToLower() == "0x3fe1a4c1481c8351e91b64d5c398b159de07cbc5".ToLower())
        {
            models[0].SetActive(true);
            models[1].SetActive(false);
        }

        if (toDisplay.ToLower() == "0xd668A2E001f3385B8BBC5a8682AC3C0D83C19122".ToLower())
        {
            models[1].SetActive(true);
            models[0].SetActive(false);
        }
    }

}
