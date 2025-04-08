using UnityEngine;
using DG.Tweening; // Import DOTween

public class FlowerShake : LinkMonoBehaviour
{
    public Transform flowerSprite; // GameObject con chứa hình ảnh bông hoa
    public float tiltAngle = 15f; // Góc nghiêng tối đa
    public float tiltSpeed = 0.3f; // Thời gian nghiêng
    public float returnSpeed = 0.5f; // Thời gian trở về
    public int wobbleCount = 3; // Số lần dao động
    public float wobbleDecay = 0.6f; // Hệ số giảm dao động

    private Quaternion originalRotation;

    protected override void Start()
    {
        base.Start();
        originalRotation = flowerSprite.rotation;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Xác định hướng va chạm
            float direction = (other.transform.position.x > transform.position.x) ? 1f : -1f;
            float targetAngle = direction * tiltAngle;

            // Nghiêng bông hoa về hướng ngược lại
            flowerSprite.DOKill(); // Dừng Tween trước đó nếu có
            flowerSprite.DORotate(new Vector3(0, 0, targetAngle), tiltSpeed);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            flowerSprite.DOKill();

            // Tạo hiệu ứng dao động giảm dần
            Sequence sequence = DOTween.Sequence();
            float wobbleAngle = tiltAngle;
            for (int i = 0; i < wobbleCount; i++)
            {
                sequence.Append(flowerSprite.DORotate(new Vector3(0, 0, wobbleAngle), returnSpeed / 2));
                sequence.Append(flowerSprite.DORotate(new Vector3(0, 0, -wobbleAngle), returnSpeed / 2));
                wobbleAngle *= wobbleDecay; // Giảm biên độ dao động
            }
            sequence.Append(flowerSprite.DORotate(Vector3.zero, returnSpeed)); // Trở về vị trí ban đầu
        }
    }
}
