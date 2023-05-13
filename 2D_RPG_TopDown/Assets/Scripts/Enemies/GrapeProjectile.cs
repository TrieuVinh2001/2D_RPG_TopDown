using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeProjectile : MonoBehaviour
{
    [SerializeField] private float duration = 1f;//Thời gian tới player
    [SerializeField] private AnimationCurve animCurve;//Đồ thị đường đi
    [SerializeField] private float heightY = 3f;//Độ cao đường đạn
    [SerializeField] private GameObject grapeProjectileShadow;

    private void Start()
    {
        GameObject grapeShadow = Instantiate(grapeProjectileShadow, transform.position + new Vector3(0f, 0.3f, 0f), Quaternion.identity);


        Vector3 playerPos = PlayerController.Instance.transform.position;
        Vector3 grapeShadowStartPosition = grapeShadow.transform.position;

        StartCoroutine(ProjectileCurveRoutine(transform.position, playerPos));
        StartCoroutine(MoveGrapeShadowRoutine(grapeShadow, grapeShadowStartPosition, playerPos));
    }

    private IEnumerator ProjectileCurveRoutine(Vector3 startPosition, Vector3 endPosition)
    {
        float timePassed = 0f;

        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / duration;
            float heightT = animCurve.Evaluate(linearT);//Độ lớn sẽ tăng dần đi từ trái qua phải hết đồ thị
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(startPosition, endPosition, linearT)+ new Vector2(0f, height);

            yield return null;
        }

        Destroy(gameObject);
    }

    private IEnumerator MoveGrapeShadowRoutine(GameObject grapShadow, Vector3 startPosition, Vector3 endPosition)
    {
        float timePassed = 0f;

        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / duration;
            grapShadow.transform.position = Vector2.Lerp(startPosition, endPosition, linearT);

            yield return null;
        }

        Destroy(grapShadow);
    }
}
