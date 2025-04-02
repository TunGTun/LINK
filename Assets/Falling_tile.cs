using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SinkingTile : MonoBehaviour
{
    public Tilemap tilemap;
    public float sinkSpeed = 0.5f; // Tốc độ lún
    public float delayBeforeSink = 0.5f; // Thời gian chờ trước khi lún
    public float sinkDistance = 0.5f; // Độ sâu lún xuống

    private void Start()
    {
        if (tilemap == null)
        {
            tilemap = GetComponent<Tilemap>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3Int tilePosition = tilemap.WorldToCell(other.transform.position);
            Debug.Log("Tile Position: " + tilePosition); // Kiểm tra tọa độ tile
            StartCoroutine(SinkAndRemoveTile(tilePosition));
        }
    }

    IEnumerator SinkAndRemoveTile(Vector3Int tilePos)
    {
        yield return new WaitForSeconds(delayBeforeSink);

        Vector3 worldPos = tilemap.GetCellCenterWorld(tilePos);
        Vector3 targetPos = worldPos + Vector3.down * sinkDistance;

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * sinkSpeed;
            tilemap.SetTransformMatrix(tilePos, Matrix4x4.TRS(Vector3.Lerp(worldPos, targetPos, elapsedTime), Quaternion.identity, Vector3.one));
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);
        tilemap.SetTile(tilePos, null);
    }
}
