using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class KnifeMovement : MonoBehaviour
{

    [SerializeField]
    int moveSpeed = 20;
    [SerializeField]
    Vector3 moveDirection = Vector3.up;
    [SerializeField]
    AudioClip clip;

    public Vector3 MoveDirection { get => moveDirection; set => moveDirection = value; }

    private void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * MoveDirection;
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Target"))
        {
            // MoveDirection = Vector3.zero;
            transform.SetParent(other.transform);
            GameManager.Inst.ThrowKnife();
            SoundControl.soundControl.SfxPlay("atk", clip);
            Destroy(this);
        }
        else if (other.CompareTag("Knife"))
        {
            GameManager.Inst.GameOver();
        }
    }

}
