using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float knockBackTime = 0.2f;//Thời gian bật lại
    public bool gettingKnockBack { get; private set; }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockedBack(Transform damage,float knockBackThrust)
    {
        gettingKnockBack = true;
        //bật lại
        Vector2 difference = (transform.position - damage.position).normalized * knockBackThrust * rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;//Dừng lực bật lại
        gettingKnockBack = false;
    }
}
