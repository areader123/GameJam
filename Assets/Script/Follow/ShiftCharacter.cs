using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShiftCharacter : MonoBehaviour
{
    public GameObject character1;
    public GameObject character2;
    public GameObject character3;
    public GameObject crystal1;
    public GameObject crystal2;
    public GameObject crystal3;
    public static int index = 0;
    public static bool flag = true;

    void Start()
    {
        if (flag)
        {
            flag = false;
            Vector2 newPosition_1 = character1.transform.position;
            Vector2 newPosition_2 = character1.transform.position;
            newPosition_1.x += -0.3f;
            newPosition_1.y += 0.1f;
            newPosition_2.x += 0.3f;
            newPosition_2.y += 0.1f;

            crystal2.transform.SetParent(character1.transform);
            crystal3.transform.SetParent(character1.transform);

            crystal2.transform.position = newPosition_1;
            crystal3.transform.position = newPosition_2;

            crystal2.SetActive(true);
            crystal3.SetActive(true);
        }

    }

    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            switch (index)
            {
                case 0:
                    character2.SetActive(true);
                    character1.SetActive(false);
                    character3.SetActive(false);
                    crystal2.SetActive(false);
                    character2.transform.SetParent(null);
                    character3.transform.SetParent(null);

                    character2.transform.position = crystal2.transform.position;

                    character1.transform.SetParent(character2.transform);
                    character3.transform.SetParent(character2.transform);

                    Vector2 newPosition_1 = character2.transform.position;
                    Vector2 newPosition_2 = character2.transform.position;

                    crystal1.transform.SetParent(character2.transform);
                    crystal3.transform.SetParent(character2.transform);

                    newPosition_1.x += -0.3f;
                    newPosition_1.y += 0.1f;
                    newPosition_2.x += 0.3f;
                    newPosition_2.y += 0.1f;

                    crystal3.transform.position = newPosition_1;
                    crystal1.transform.position = newPosition_2;



                    crystal1.SetActive(true);
                    crystal3.SetActive(true);

                    //index--;
                    //index += 3;
                    index++;
                    index %= 3;
                    break;
                case 1:
                    character3.SetActive(true);
                    character1.SetActive(false);
                    character2.SetActive(false);
                    crystal3.SetActive(false);
                    character1.transform.SetParent(null);
                    character3.transform.SetParent(null);

                    character3.transform.position = crystal3.transform.position;

                    character2.transform.SetParent(character3.transform);
                    character1.transform.SetParent(character3.transform);

                    Vector2 newPosition_3 = character3.transform.position;
                    Vector2 newPosition_4 = character3.transform.position;

                    crystal1.transform.SetParent(character3.transform);
                    crystal2.transform.SetParent(character3.transform);

                    newPosition_3.x += -0.3f;
                    newPosition_3.y += 0.1f;
                    newPosition_4.x += 0.3f;
                    newPosition_4.y += 0.1f;

                    crystal2.transform.position = newPosition_3;
                    crystal1.transform.position = newPosition_4;



                    crystal1.SetActive(true);
                    crystal2.SetActive(true);


                    //index--;
                    //index += 3;
                    index++;
                    index %= 3;
                    break;
                case 2:

                    character1.SetActive(true);

                    character2.SetActive(false);
                    character3.SetActive(false);
                    crystal1.SetActive(false);

                    character1.transform.SetParent(null);
                    character2.transform.SetParent(null);

                    character1.transform.position = crystal1.transform.position;

                    character2.transform.SetParent(character1.transform);
                    character3.transform.SetParent(character1.transform);

                    Vector2 newPosition_5 = character1.transform.position;
                    Vector2 newPosition_6 = character1.transform.position;

                    crystal2.transform.SetParent(character1.transform);
                    crystal3.transform.SetParent(character1.transform);

                    newPosition_5.x += -0.3f;
                    newPosition_5.y += 0.1f;
                    newPosition_6.x += 0.3f;
                    newPosition_6.y += 0.1f;

                    crystal3.transform.position = newPosition_5;
                    crystal2.transform.position = newPosition_6;


                    crystal2.SetActive(true);
                    crystal3.SetActive(true);


                    //index--;
                    //index += 3;
                    index++;
                    index %= 3;
                    break;
            }
        }
    }
}
