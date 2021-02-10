using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Namespace (nimiavaruus) kurssipäivien scripteille, jotta ne ei ole näkyvissä muiden scriptien ehdotuksissa
// Jos haluaa käyttää muissa scripteissä, lisää yläpuolelle "using CourseDayOne"
namespace CourseDayOne
{
    public class JoystickTest : MonoBehaviour
    {
        public FixedJoystick joystick;
        public float speed = 5f;

        private CharacterController cc;

        private void Awake()
        {
            transform.position = new Vector3(0, 0.5f, 0);
            cc = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            Move();
        }

        void Move()
        {
            cc.SimpleMove(new Vector3(joystick.Horizontal, 0, joystick.Vertical) * speed);
        }
    }
}
