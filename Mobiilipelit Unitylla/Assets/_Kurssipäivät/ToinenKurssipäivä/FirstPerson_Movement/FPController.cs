using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Namespace (nimiavaruus) kurssipäivien scripteille, jotta ne ei ole näkyvissä muiden scriptien ehdotuksissa
// Jos haluaa käyttää muissa scripteissä, lisää yläpuolelle "using CourseDayTwo"
namespace CourseDayTwo
{
    public class FPController : MonoBehaviour
    {
        [Header("Character")]
        public float movementSpeed = 200f; // Pelaajan liikkumisen nopeus
        public float rotateSpeed = 100f; // Pelaajan kääntymisen nopeus (y-akselilla)
        public float jumpForce = 5f;  // Hypyn voimakkuus, kuinka korkealle pelaaja hyppää (ota huomioon pelaajan Rigidbody massa)
        public float jumpCD = 2f;  // Hypyn cooldown -- esim 1f => 1sek päästä pelaaja voi hypätä uudelleen

        [Header("Camera Rotation")]
        public float camRotateSpeed = 100; // Kameran kääntymis nopeus (x-akselilla)
        public float minAngle = -60; // minimi kulma, johon kamera voi kääntyä
        public float maxAngle = 60; // maksimi kulma, johon kamera voi kääntyä

        [Header("References")]
        public Joystick leftJS;  // Vasemman peuolen Joystick referenssi
        public Joystick rightJS; // Oikeian peuolen Joystick referenssi
        public Camera cam; // Kamera peuolen Joystick referenssi

        Rigidbody rb;
        Vector3 moveDir;  // Mihin suuntaan pelaaja kävelee
        Quaternion camRot; // kameran kääntymiseen liittyvä Quaternion
        bool hasJumped = false; // Onko pelaaja hypännyt

        void Awake()
        {
            // Haetaan pelaajan RigidBody tämän scriptin GameObjektin komponenteista
            rb = this.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            // Update hallinnoidaan pelaajan kääntymistä, kameran kääntymistä ja pelaajan suunnan laskemista
            // Update kannattaa laittaa kaikki logiikka, joka ei ole fysiikkaan liittyvää
            CharacterDirection();
            CharacterRotate();
            CameraRotate();
        }

        private void FixedUpdate()
        {
            // FixedUpdateen kannattaa laittaa kaikki fysiikkaan liittyvä logiikka,
            // esim tässä tilanteessa pelaajan liikkuminen. (Rigidbodylla liikkuminen => Fysiikalla liikkumista)
            CharacterMove();
        }

        // Vasemman puoleisella joystickillä määritettään pelaajan liikkumisen suunta.
        // leftJS.Horizontal on joystick vasen ja oikea 
        // leftJS.Vertical on joystick ylös ja alas
        void CharacterDirection()
        {
            // sivuille liikkumiseen käytetään this.transform.right, joka on punainen akseli Unity Editorissa
            Vector3 moveDirHorizontal = leftJS.Horizontal * transform.right;

            // Eteen/taakse liikkumiseen käytetään this.transform.forward, joka on sininen akseli Unity Editorissa
            Vector3 moveDirVertical = leftJS.Vertical * transform.forward;

            // Normalisoidaan edelliset arvot ja asetetaan se moveDir muuttujalle
            moveDir = (moveDirHorizontal + moveDirVertical).normalized;
        }

        // Hahmon liikkuminen Rigidbodyn avulla
        void CharacterMove()
        {
            Vector3 velocityY = new Vector3(0, rb.velocity.y, 0);   // Tällä asetetaan pelaajan velocity.y eli lisätään pelaajalle oikea painovoima
            rb.velocity = moveDir * movementSpeed * Time.deltaTime;  // Muutetaan pelaajan Rigidbody nopeutta (velocity) moveDir arvolla. Nopeus määritellää myös movementSpeed ja Time.deltaTime avulla
            rb.velocity += velocityY; // Lisätään aiemmin lisätty velocityY, jotta painovoima vaikuttaa pelaaja -hahmoon oikein
        }

        void CameraRotate()
        {
            // Käytetään oikean puoleista Joystickin "vertical" arvoa, ja käännetään kameraa camRotateSpeed ja Time.deltaTime avulla
            camRot.x += -rightJS.Vertical * camRotateSpeed * Time.deltaTime; // --> "-rightJs.vertical" sisältää miinuksen, sillä muuten joystick luetaan ylösalaisin

            // Clamp:illä asetetaan arvo minAngle ja maxAngle, eli min/max kulmat kameran kääntymiselle.
            // Tällä varmistetaan että kamera ei pyöri pelaajan ympäri, voidaan siis kääntää kameraa tietyn verran ylös ja alas
            camRot.x = Mathf.Clamp(camRot.x, minAngle, maxAngle);

            // asetetaan Clamp:attu arvo kameran rotationille.
            cam.transform.localRotation = Quaternion.Euler(camRot.x, 0, 0);
        }

        void CharacterRotate()
        {
            // Käännetään hahmoa oikeanpuoleisen joystickin Horizontal arvon kanssa
            this.transform.Rotate(0, rightJS.Horizontal * rotateSpeed * Time.deltaTime, 0);
        }

        // Hyppy -action, jota kutsutaan hyppy-napilla. Parametrina on nappi-objekti
        public void JumpAction(Button jumpBtn)
        {
            if (!hasJumped)
            {
                StartCoroutine(JumpActionCooldown(jumpBtn));
            }
        }


        // ajastin hypyn cooldownin laskemiselle.
        public IEnumerator JumpActionCooldown(Button jumpBtn)
        {
            // Lisätään pelaajaan hyppy voima "transform.up * jumpforcen" avulla. ForceMode.Impulse => Välitön voima, joka annetaan HETI rigidbodylle
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

            // Pelaaja hyppää joten asetetaan parametriksi annettu nappi pois päältä, jotta pelaaja ei voi hyppää jatkuvasti
            hasJumped = true;
            jumpBtn.interactable = false;

            // jumpCD -arvon ajan odotetaan, jonka jälkeen pelaaja voi taas hypätä
            yield return new WaitForSeconds(jumpCD);

            hasJumped = false;
            jumpBtn.interactable = true;
        }
    }
}