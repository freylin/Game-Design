using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanisher {
    public class Winning : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                GameManager.YouWin();
            }

        }
    }
}
