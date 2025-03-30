using UnityEngine;

public class ParallaxController_k : MonoBehaviour
{
    Transform cam;
    Vector3 camStartPos;
    float distance;

    GameObject[] backgrounds;
    Material[] mat;
    float[] backSpeed;
    float farthestBack;

    [Range(0.01f, 0.05f)]
    public float parallaxSpeed;

    public Texture[] newTextures; // Danh sách Texture mới cho từng layer

    private Vector3 lastCamPosition; // Lưu vị trí Camera trước đó

    void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;
        lastCamPosition = cam.position; // Lưu lại vị trí ban đầu của Camera

        int backCount = transform.childCount;
        mat = new Material[backCount];
        backSpeed = new float[backCount];
        backgrounds = new GameObject[backCount];

        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backgrounds[i].GetComponent<Renderer>().material;
        }

        BackSpeedCalculate(backCount);
    }

    void BackSpeedCalculate(int backCount)
    {
        for (int i = 0; i < backCount; i++)
        {
            float depth = backgrounds[i].transform.position.z - cam.position.z;
            if (depth > farthestBack)
            {
                farthestBack = depth;
            }
        }

        for (int i = 0; i < backCount; i++)
        {
            backSpeed[i] = 1 - (backgrounds[i].transform.position.z - cam.position.z) / farthestBack;
        }
    }

    //private void LateUpdate()
    //{
    //    if (cam.position == lastCamPosition) return; // Nếu Camera không di chuyển thì không làm gì cả

    //    distance = cam.position.x - camStartPos.x;
    //    transform.position = new Vector3(cam.position.x, transform.position.y, transform.position.z);

    //    for (int i = 0; i < backgrounds.Length; i++)
    //    {
    //        float speed = backSpeed[i] * parallaxSpeed;
    //        mat[i].SetTextureOffset("_MainTex", new Vector2(distance * speed, 0));
    //    }

    //    lastCamPosition = cam.position; // Lưu vị trí Camera hiện tại
    //}

    private void LateUpdate()
    {
        if (cam.position == lastCamPosition) return;

        distance = Mathf.Lerp(distance, cam.position.x - camStartPos.x, Time.deltaTime * 5f); // Làm mượt

        transform.position = new Vector3(cam.position.x, transform.position.y, transform.position.z);

        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed;
            mat[i].SetTextureOffset("_MainTex", new Vector2(distance * speed, 0));
        }

        lastCamPosition = cam.position;
    }


    // Hàm này sẽ được gọi khi nhân vật đi vào vùng thay đổi nền
    public void ChangeParallaxTexture()
    {
        for (int i = 0; i < mat.Length; i++)
        {
            if (newTextures.Length >= i && newTextures[i] != null)
            {
                Debug.Log($"Đổi nền {i} thành {newTextures[i].name}");
                mat[i].mainTexture = newTextures[i]; // Thay đổi texture
            }
        }
    }
}
