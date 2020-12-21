using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private ParticleSystem fire;
    [SerializeField] private GameObject gun;
    [SerializeField] private Camera camera;
    [SerializeField] private Text text;

    private int count;

    #region [Getters and Setters]
    public Camera PlayerCamera { get => playerCamera; set => playerCamera = value; }
    public GameObject Gun { get => gun; set => gun = value; }
    public ParticleSystem Fire { get => fire; set => fire = value; }
    #endregion

    void Start()
    {
        count = 0;
        StartCoroutine(Shoot());
    }

    void Update()
    {
        if(transform.position.y < -100) StartCoroutine(LoadScene());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
            StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(4f);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject curFire = Instantiate(Fire.gameObject, gun.transform);
                curFire.transform.localPosition = new Vector3(.7f, .35f, 0);

                gun.GetComponent<AudioSource>().Play();

                RaycastHit hit;
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    if (hit.transform.tag == "Enemy")
                    {
                        count++;
                        text.text = "Count: " + count;
                        Destroy(hit.transform.gameObject);
                    }
                }
                yield return new WaitForSeconds(fire.gameObject.GetComponent<WFX_Demo_DeleteAfterDelay>().delay);
            }
            yield return null;
        }
    }
}
