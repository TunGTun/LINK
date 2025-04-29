using UnityEngine;
using System.Collections;

public class ParallaxController_k : MonoBehaviour
{
    Transform cam;
    Vector3 camStartPos;
    float distance;

    GameObject[] backgrounds;
    Material[] mat;
    float[] backSpeed;
    float farthestBack;

    [Range(0.01f, 0.5f)]
    public float parallaxSpeed = 0.02f;

    public Texture[] newTextures;
    private Texture[] originalTextures;

    private Vector3 lastCamPosition;

    void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;
        lastCamPosition = cam.position;

        int backCount = transform.childCount;
        mat = new Material[backCount];
        backSpeed = new float[backCount];
        backgrounds = new GameObject[backCount];
        originalTextures = new Texture[backCount];

        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backgrounds[i].GetComponent<Renderer>().material;
            originalTextures[i] = mat[i].mainTexture;
        }

        BackSpeedCalculate(backCount);
    }

    void BackSpeedCalculate(int backCount)
    {
        farthestBack = float.MinValue; // Reset giá trị xa nhất

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

    private void LateUpdate()
    {
        if (Vector3.Distance(cam.position, lastCamPosition) < 0.01f) return; // Kiểm tra di chuyển thực sự

        distance = cam.position.x - camStartPos.x; // Tính khoảng cách chính xác
        transform.position = new Vector3(cam.position.x, transform.position.y, transform.position.z);

        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed;
            mat[i].SetTextureOffset("_MainTex", new Vector2(distance * speed, 0)); // Nếu dùng URP/HDRP, đổi thành "_BaseMap"
        }

        lastCamPosition = cam.position;
    }

    public void ChangeParallaxTexture()
    {
        StopAllCoroutines();
        StartCoroutine(TransitionTextures(newTextures));
    }

    public void ResetParallaxTexture()
    {
        StopAllCoroutines();
        StartCoroutine(TransitionTextures(originalTextures));
    }

    IEnumerator TransitionTextures(Texture[] targetTextures)
    {
        float duration = 1.5f;
        float elapsedTime = 0f;

        Texture[] startTextures = new Texture[mat.Length];
        for (int i = 0; i < mat.Length; i++)
        {
            startTextures[i] = mat[i].mainTexture;
        }

        while (elapsedTime < duration)
        {
            for (int i = 0; i < mat.Length; i++)
            {
                if (targetTextures[i] != null)
                {
                    mat[i].mainTexture = Mathf.Lerp(0, 1, elapsedTime / duration) > 0.5f ? targetTextures[i] : startTextures[i];
                }
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
