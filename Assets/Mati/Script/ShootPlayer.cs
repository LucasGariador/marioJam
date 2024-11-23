using UnityEngine;

public class ShootPlayer : MonoBehaviour
{

    [SerializeField] private Transform gun;
    [SerializeField] private int shootSpeed;
    private Vector3 targetRotation;
    private Vector3 finalTarget;

    [SerializeField] GameObject iceCream;
    [SerializeField] GameObject popCorn;
    private GameObject equippedShot;

    void Awake() 
    {
        equippedShot = iceCream; 
    }


    void Update()
    {
        targetRotation = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(targetRotation.y, targetRotation.x) * Mathf.Rad2Deg;
        gun.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            equippedShot = iceCream;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            equippedShot = popCorn;
        }

    }

    void Shoot()
    {
        var shoot = Instantiate(equippedShot, gun.position, transform.rotation, transform.parent);
        targetRotation.z = 0;
        finalTarget = (targetRotation - transform.position).normalized;
        shoot.GetComponent<Rigidbody2D>().AddForce(finalTarget * shootSpeed, ForceMode2D.Impulse);
    }
}
