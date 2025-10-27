using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CloudManager : MonoBehaviour
{
    public GameObject cloudPrefab; // 预置体，需在Inspector中赋值
    public Sprite cloudSprites; // 存储云朵图片，需在Inspector中赋值
    public float minY = -100f; // Y轴最小位置
    public float maxY = 100f; // Y轴最大位置
    public float minSize = 0.5f; // 最小缩放
    public float maxSize = 1.5f; // 最大缩放
    public float minSpeed = 50f; // 最小移动速度
    public float maxSpeed = 150f; // 最大移动速度

    void Start()
    {
        StartCoroutine(SpawnClouds());
    }

    IEnumerator SpawnClouds()
    {
        CreateCloud();

        while (true)
        {
            // 随机生成间隔
            float spawnInterval = Random.Range(3f, 5f);
            yield return new WaitForSeconds(spawnInterval);
            CreateCloud();
        }
    }

    void CreateCloud()
    {
        // 实例化云朵对象
        GameObject cloud = Instantiate(cloudPrefab, transform);
        Image cloudImage = cloud.GetComponent<Image>();
        cloudImage.sprite = cloudSprites;
        // 设置随机大小
        float randomScale = Random.Range(minSize, maxSize);
        cloud.transform.localScale = new Vector3(randomScale, randomScale, 1f);

        // 设置初始位置：X=1104，Y在[minY, maxY]之间随机
        float randomY = Random.Range(minY, maxY);
        cloud.transform.localPosition = new Vector3(1104f, randomY, 0f);

        // 设置随机移动速度
        float randomSpeed = Random.Range(minSpeed, maxSpeed);

        // 开始移动协程
        StartCoroutine(MoveCloud(cloud, randomSpeed));
    }

    IEnumerator MoveCloud(GameObject cloud, float speed)
    {
        while (cloud != null && cloud.transform.localPosition.x > -1104f)
        {
            // 向左移动
            cloud.transform.Translate(Vector3.left * speed * Time.deltaTime);
            yield return null;
        }

        // 移动结束后销毁对象
        if (cloud != null)
        {
            Destroy(cloud);
        }
    }
}