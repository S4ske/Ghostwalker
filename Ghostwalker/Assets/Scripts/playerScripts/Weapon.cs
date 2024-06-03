using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private int manaCost;

    public string weaponName;
    public GameObject collectable;
    private float timeBtwShots;
    public float startTimeBtwShots;

    void Update()
    {
        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                var player = gameObject.GetComponentInParent<Player>();
                if (player.mana >= manaCost)
                {
                    player.mana -= manaCost;
                    var shotPosition = shotPoint.position;
                    var difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - shotPosition;
                    difference.Normalize();
                    var rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                    Instantiate(bullet, shotPosition, Quaternion.Euler(0f, 0f, rotZ));
                    timeBtwShots = startTimeBtwShots;
                }
            }
        }
        else
            timeBtwShots -= Time.deltaTime;
    }
}
