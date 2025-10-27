using UnityEngine;
using System.Collections;

public class RandomParticlePlayer : MonoBehaviour
{
    [Header("粒子系统设置")]
    public ParticleSystem[] particleSystems; // 存放4个粒子系统的数组
    public float minInterval = 1.0f;         // 最小间隔时间
    public float maxInterval = 3.0f;         // 最大间隔时间
    private int lastPlayedIndex = -1;        // 上次播放的粒子索引

    private void OnEnable()
    {
        // 确保所有粒子系统初始状态为停止
        foreach (var ps in particleSystems)
        {
            if (ps != null)
            {
                ps.Stop(true);
            }
        }

        // 开始随机播放循环
        StartCoroutine(RandomParticleLoop());
    }

    IEnumerator RandomParticleLoop()
    {
        yield return new WaitForSeconds(2);

        while (true)
        {
            // 等待随机时间间隔
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);

            // 播放随机粒子系统
            PlayRandomParticleSystem();
        }
    }

    void PlayRandomParticleSystem()
    {
        // 确保有可用的粒子系统
        if (particleSystems == null || particleSystems.Length == 0)
            return;

        int randomIndex;

        // 避免连续播放同一个粒子系统
        do
        {
            randomIndex = Random.Range(0, particleSystems.Length);
        }
        while (randomIndex == lastPlayedIndex && particleSystems.Length > 1);

        lastPlayedIndex = randomIndex;

        // 播放选中的粒子系统
        if (particleSystems[randomIndex] != null)
        {
            particleSystems[randomIndex].Stop(true);
            particleSystems[randomIndex].Play(true);
          
        }
    }
}