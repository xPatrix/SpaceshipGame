using UnityEngine;

public class ShipPlayer : MonoBehaviour
{
    public float Acceleration = 5;
    public float Deacceleration = 2.5f;
    public float TurnRate = 100;

    public float MaxSpeed = 40;
    public float HpMax = 100;
    public float Regen = 10;
    public float HpLeft => hpCurrent / HpMax;

    private float hpCurrent;

    public Blaster FieldBlaster;

    public Transform FieldHardpointLeftWing;
    public Transform FieldHardpointRightWing;
    public Transform FieldHardpointLeftFront;
    public Transform FieldHardpointRightFront;

    private float azimuth;
    private float speed;

    public void DoDamage(float dmg)
    {
        hpCurrent -= dmg;
        if(hpCurrent <= 0)
        {
            hpCurrent = 0;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        azimuth = 0;
        speed = 0;
        hpCurrent = HpMax;
    }

    // Update is called once per frame
    private void Update()
    {
        hpCurrent += Regen * Time.deltaTime;
        if(hpCurrent > HpMax)
        {
            hpCurrent = HpMax;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(FieldBlaster, FieldHardpointLeftWing.position, FieldHardpointLeftWing.rotation);
            Instantiate(FieldBlaster, FieldHardpointRightWing.position, FieldHardpointRightWing.rotation);
            Instantiate(FieldBlaster, FieldHardpointLeftFront.position, FieldHardpointLeftFront.rotation);
            Instantiate(FieldBlaster, FieldHardpointRightFront.position, FieldHardpointRightFront.rotation);
        }

        if (Input.GetKey(KeyCode.W))
        {
            speed += Acceleration * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            speed -= Acceleration * Time.deltaTime;
        }

        if (speed > MaxSpeed)
            speed = MaxSpeed;

        if (speed < -MaxSpeed)
            speed = -MaxSpeed;

        this.transform.position += this.transform.forward * speed * Time.deltaTime;

        float speedRatio = speed / MaxSpeed;

        if (Input.GetKey(KeyCode.A))
        {
            this.azimuth -= Mathf.LerpUnclamped(0.0f, this.TurnRate, speedRatio) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            this.azimuth += Mathf.LerpUnclamped(0.0f, this.TurnRate, speedRatio) * Time.deltaTime;
        }

        this.transform.rotation = Quaternion.Euler(0, this.azimuth, 0);
    }
}