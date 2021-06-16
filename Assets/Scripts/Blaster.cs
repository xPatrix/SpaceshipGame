using UnityEngine;

public class Blaster : MonoBehaviour
{
    public float Speed = 150;

    public float TimeToLeave = 2;
    public float Damage = 10;

    public enum Shooter
    {
        Player,
        Opponent
    }

    public Shooter MyShooter;
    public GameObject HitEffect;

    private float timeToLeaveLeft;

    private void Start()
    {
        timeToLeaveLeft = TimeToLeave;
    }

    // Update is called once per frame
    private void Update()
    {
        this.transform.position += this.transform.forward * this.Speed * Time.deltaTime;

        timeToLeaveLeft -= Time.deltaTime;

        if (timeToLeaveLeft < 0)
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (MyShooter == Shooter.Player)
        {
            var shipEnemy = collision.gameObject.GetComponentInParent<ShipEnemy>();
            if (shipEnemy != null)
            {
                Instantiate(HitEffect, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
        if (MyShooter == Shooter.Opponent)
        {
            var shipPlayer = collision.gameObject.GetComponentInParent<ShipPlayer>();
            if (shipPlayer != null)
            {
                shipPlayer.DoDamage(Damage);
                Instantiate(HitEffect, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
}