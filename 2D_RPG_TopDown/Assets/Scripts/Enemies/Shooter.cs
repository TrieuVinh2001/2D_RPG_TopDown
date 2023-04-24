using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletMoveSpeed;//Tốc độ đạn
    [SerializeField] private int burstCount;//Số lần bắn liên tục
    [SerializeField] private int projectilesPerBurst;//Số đạn cùng bắn trong 1 lần
    [SerializeField] [Range(0, 359)] private float angleSpread;//Góc mở rộng
    [SerializeField] private float startingDistance = 0.1f;
    [SerializeField] private float timeBetweenBursts;//Thời gian giữa các lần bắn liên tục
    [SerializeField] private float restTime = 1f;//Thời gian nghỉ giữa các đợt bắn

    private bool isShooting = true;

    public void Attack()
    {
        if (isShooting)
        {
            StartCoroutine(ShootRoutine());
        }
    }

    public IEnumerator ShootRoutine()
    {
        isShooting = false;

        float startAngle, currentAngle, angleStep;
        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep);//out được dùng chung không phải xác định nó là float string hay các kiểu khác

        for (int i = 0; i < burstCount; i++)//Số lần bắn liên tục
        {
            for (int j = 0; j < projectilesPerBurst; j++)//Số đạn bắn ra cùng lúc trong 1 lần bắn
            {
                Vector2 pos = FindBulletSpawnPos(currentAngle);//Vị trí đạn tại góc hiện tại

                GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
                newBullet.transform.right = newBullet.transform.position - transform.position;

                if (newBullet.TryGetComponent(out Projectile projectile))//Sẽ cố gắng tìm kiếm nếu có thì true, null thì false
                {
                    projectile.UpdateMoveSpeed(bulletMoveSpeed);
                }

                currentAngle += angleStep;//Tăng góc hiện tại từ góc đầu đến góc cuối
            }

            currentAngle = startAngle;//Đặt góc hiện tại về góc đầu

            yield return new WaitForSeconds(timeBetweenBursts);
            TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep);
        }

        yield return new WaitForSeconds(restTime);

        isShooting = true;
    }

    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep)
    {
        //Vector đường thẳng từ quái đến player
        Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;//Góc hướng tới player
        startAngle = targetAngle;
        float endAngle = targetAngle;//Góc kết thúc
        currentAngle = targetAngle;
        float halfAngleSpread = 0f;//Nửa góc mở rộng(nửa góc hình quạt)
        angleStep = 0f;
        if (angleSpread != 0)
        {
            angleStep = angleSpread / (projectilesPerBurst - 1);//Góc mở rộng(góc hình quạt) chia số điểm bắn(Số đạn bắn)
            halfAngleSpread = angleSpread / 2f;//Chia đôi góc mở rộng
            startAngle = targetAngle - halfAngleSpread;//Góc hướng tới player sẽ ở giữa, góc bắt đầu ở trên
            endAngle = targetAngle + halfAngleSpread;//Góc kết thúc ở dưới
            currentAngle = startAngle;//Góc hiện tại sẽ bắt đầu từ góc bắt đầu
        }
    }

    private Vector2 FindBulletSpawnPos(float currentAngle)
    {
        float x = transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        Vector2 pos = new Vector2(x, y);
        return pos;
    }
}
