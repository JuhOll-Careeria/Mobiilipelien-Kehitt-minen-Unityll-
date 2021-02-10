using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Namespace (nimiavaruus) kurssipäivien scripteille, jotta ne ei ole näkyvissä muiden scriptien ehdotuksissa
// Jos haluaa käyttää muissa scripteissä, lisää yläpuolelle "using CourseDayOne"
namespace CourseDayThree
{

    // movement scripti Interstitial mainoksen esimerkkiä varten
    public class InterstitialAdExample : MonoBehaviour
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

        private void OnTriggerEnter(Collider other)
        {
            // Esimerkki ratkaisu Interstitial ads näyttämiseen
            if (other.gameObject.GetComponent<InterstitialAd>())
            {
                other.gameObject.GetComponent<InterstitialAd>().ShowInterstitialAd();
            }
            else if (other.gameObject.GetComponent<RewardVideoAd>())
            {
                other.gameObject.GetComponent<RewardVideoAd>().InitializeRewardAd();
            }
        }
    }
}
