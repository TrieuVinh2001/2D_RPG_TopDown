using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject goldCoin, healthGlobe, staminaGlobe;

    public void DropItems()
    {
        int randomNum = Random.Range(1, 5);//chọn ngẫu nhiên 1-4

        if(randomNum == 1)
        {
            int randomAmountOfGold = Random.Range(1, 3);
            for (int i = 0; i < randomAmountOfGold; i++)
            {
                Instantiate(goldCoin, transform.position, Quaternion.identity);
            }
        }
        if (randomNum == 2)
        {
            Instantiate(healthGlobe, transform.position, Quaternion.identity);
        }
        if (randomNum == 3)
        {
            Instantiate(staminaGlobe, transform.position, Quaternion.identity);
        }

    }
}
