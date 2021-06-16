using UnityEngine;

public class ShipEnemy : MonoBehaviour
{
    public enum State
    {
        Charge,
        Fleeing
    }

    public float Speed = 10;
    public float TurnSpeed = 200;
    public float DistanceToFlee = 10.0f;
    public float DistanceToCharge = 20;

    public Blaster Weapon;
    public float FireEvery = 1;

    private State CurrentState;
    private ShipPlayer ShipPlayer;
    private float azimuth;
    private float timeToFire;

    private void Start()
    {
        ShipPlayer = FindObjectOfType<ShipPlayer>();
        CurrentState = State.Charge;
        azimuth = 0;
        timeToFire = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        this.transform.position += this.transform.forward * Speed * Time.deltaTime;

        var distanceToPlayer = ShipPlayer.transform.position - this.transform.position;

        if (CurrentState == State.Charge)
        {
            var angle = Mathf.Rad2Deg * Mathf.Atan2(distanceToPlayer.x, distanceToPlayer.z);

            azimuth = Mathf.MoveTowardsAngle(azimuth, angle, TurnSpeed * Time.deltaTime);

            timeToFire -= Time.deltaTime;

            if (timeToFire < 0)
            {
                timeToFire = FireEvery;
                Instantiate(Weapon, this.transform.position, this.transform.rotation);
            }
        }

        if (CurrentState == State.Fleeing)
        {
            var angle = Mathf.Rad2Deg * Mathf.Atan2(distanceToPlayer.x, distanceToPlayer.z);
            angle += 180;

            azimuth = Mathf.MoveTowardsAngle(azimuth, angle, TurnSpeed * Time.deltaTime);
        }

        if (distanceToPlayer.magnitude < DistanceToFlee)
        {
            CurrentState = State.Fleeing;
        }
        if (distanceToPlayer.magnitude > DistanceToCharge)
        {
            CurrentState = State.Charge;
        }

        this.transform.rotation = Quaternion.Euler(0, azimuth, 0);
    }
}