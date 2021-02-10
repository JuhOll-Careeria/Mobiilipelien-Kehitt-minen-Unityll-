using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Namespace (nimiavaruus) kurssipäivien scripteille, jotta ne ei ole näkyvissä muiden scriptien ehdotuksissa
// Jos haluaa käyttää muissa scripteissä, lisää yläpuolelle "using CourseDayOne"
namespace CourseDayOne
{
    public class InputTest : MonoBehaviour
    {
        public GameObject cube;

        private void Awake()
        {
            cube.transform.position = new Vector3(0, 0, 0);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0f;

                cube.transform.position = touchPosition;
            }
        }
    }
}
