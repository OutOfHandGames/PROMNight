using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    public float projectileSpeed = 2;
    public float rotationSpeed = 10;
    public AudioClip beginSound;
    public AudioClip endSound;

    Vector3 goalPosition;
    AudioSource aSource;

    void Start()
    {
        aSource = GetComponent<AudioSource>();
        aSource.clip = beginSound;
        aSource.Play();
    }

    public void setGoalPosition(Vector3 goalPosition)
    {
        this.goalPosition = goalPosition;
    }

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed);
        if (goalPosition == null)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, goalPosition, Time.deltaTime * projectileSpeed);
        checkDestroy();
    }

    void checkDestroy()
    {
        
        if ((transform.position - goalPosition).magnitude < .01)
        {
            aSource.clip = endSound;
            aSource.Play();
            Destroy(this.gameObject);
        }
    }
}
