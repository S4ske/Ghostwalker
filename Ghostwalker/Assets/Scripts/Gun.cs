using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private Text helpMessage;
    [SerializeField] private GameObject weapon;

    private float timeBtwShots;
    public float startTimeBtwShots;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
            helpMessage.text = "E - подобрать";
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Player>() && Input.GetKey(KeyCode.E))
        {
            other.GetComponent<Player>().AddWeapon(weapon);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        helpMessage.text = string.Empty;
    }

    void Update()
    {
        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                var shotPosition = shotPoint.position;
                var difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - shotPosition;
                difference.Normalize();
                var rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                Instantiate(bullet, shotPosition, Quaternion.Euler(0f, 0f, rotZ));
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
            timeBtwShots -= Time.deltaTime;
    }
}
