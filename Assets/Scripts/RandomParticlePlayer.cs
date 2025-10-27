using UnityEngine;
using System.Collections;

public class RandomParticlePlayer : MonoBehaviour
{
    [Header("����ϵͳ����")]
    public ParticleSystem[] particleSystems; // ���4������ϵͳ������
    public float minInterval = 1.0f;         // ��С���ʱ��
    public float maxInterval = 3.0f;         // �����ʱ��
    private int lastPlayedIndex = -1;        // �ϴβ��ŵ���������

    private void OnEnable()
    {
        // ȷ����������ϵͳ��ʼ״̬Ϊֹͣ
        foreach (var ps in particleSystems)
        {
            if (ps != null)
            {
                ps.Stop(true);
            }
        }

        // ��ʼ�������ѭ��
        StartCoroutine(RandomParticleLoop());
    }

    IEnumerator RandomParticleLoop()
    {
        yield return new WaitForSeconds(2);

        while (true)
        {
            // �ȴ����ʱ����
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);

            // �����������ϵͳ
            PlayRandomParticleSystem();
        }
    }

    void PlayRandomParticleSystem()
    {
        // ȷ���п��õ�����ϵͳ
        if (particleSystems == null || particleSystems.Length == 0)
            return;

        int randomIndex;

        // ������������ͬһ������ϵͳ
        do
        {
            randomIndex = Random.Range(0, particleSystems.Length);
        }
        while (randomIndex == lastPlayedIndex && particleSystems.Length > 1);

        lastPlayedIndex = randomIndex;

        // ����ѡ�е�����ϵͳ
        if (particleSystems[randomIndex] != null)
        {
            particleSystems[randomIndex].Stop(true);
            particleSystems[randomIndex].Play(true);
          
        }
    }
}