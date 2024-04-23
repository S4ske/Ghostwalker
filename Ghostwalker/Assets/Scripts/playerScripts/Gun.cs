using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shotPoint;

    private float timeBtwShots;
    public float startTimeBtwShots;
    
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
