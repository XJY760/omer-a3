using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CloudManager : MonoBehaviour
{
    public GameObject cloudPrefab; // Ԥ���壬����Inspector�и�ֵ
    public Sprite cloudSprites; // �洢�ƶ�ͼƬ������Inspector�и�ֵ
    public float minY = -100f; // Y����Сλ��
    public float maxY = 100f; // Y�����λ��
    public float minSize = 0.5f; // ��С����
    public float maxSize = 1.5f; // �������
    public float minSpeed = 50f; // ��С�ƶ��ٶ�
    public float maxSpeed = 150f; // ����ƶ��ٶ�

    void Start()
    {
        StartCoroutine(SpawnClouds());
    }

    IEnumerator SpawnClouds()
    {
        CreateCloud();

        while (true)
        {
            // ������ɼ��
            float spawnInterval = Random.Range(3f, 5f);
            yield return new WaitForSeconds(spawnInterval);
            CreateCloud();
        }
    }

    void CreateCloud()
    {
        // ʵ�����ƶ����
        GameObject cloud = Instantiate(cloudPrefab, transform);
        Image cloudImage = cloud.GetComponent<Image>();
        cloudImage.sprite = cloudSprites;
        // ���������С
        float randomScale = Random.Range(minSize, maxSize);
        cloud.transform.localScale = new Vector3(randomScale, randomScale, 1f);

        // ���ó�ʼλ�ã�X=1104��Y��[minY, maxY]֮�����
        float randomY = Random.Range(minY, maxY);
        cloud.transform.localPosition = new Vector3(1104f, randomY, 0f);

        // ��������ƶ��ٶ�
        float randomSpeed = Random.Range(minSpeed, maxSpeed);

        // ��ʼ�ƶ�Э��
        StartCoroutine(MoveCloud(cloud, randomSpeed));
    }

    IEnumerator MoveCloud(GameObject cloud, float speed)
    {
        while (cloud != null && cloud.transform.localPosition.x > -1104f)
        {
            // �����ƶ�
            cloud.transform.Translate(Vector3.left * speed * Time.deltaTime);
            yield return null;
        }

        // �ƶ����������ٶ���
        if (cloud != null)
        {
            Destroy(cloud);
        }
    }
}