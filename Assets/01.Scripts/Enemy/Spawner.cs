using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<EnemyDataSO> _spawnEnemies = null;

    private Light2D _spawnLight;
    [SerializeField] float _targetIntensity = 1.5f;
    [SerializeField][Range(0, 4f)] private float _radius = 3f;
    [SerializeField] private float _delayMin = 0.5f, _delayMax = 1.5f;

    private void Awake()
    {
        _spawnLight = GetComponent<Light2D>();
        _spawnLight.intensity = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) //디버그 코드
        {
            StartToSpawn(5);
        }
    }

    public void StartToSpawn(int count)
    {
        Tween lightTween = DOTween.To(
            () => _spawnLight.intensity,
            X => _spawnLight.intensity = X,
            _targetIntensity, 2f );
        //시퀀스로 적 소환 시작

        Sequence seq = DOTween.Sequence();
        seq.Append(lightTween);
        seq.AppendCallback(() => StartCoroutine(SpawnCorutine(count)));
    }

    IEnumerator SpawnCorutine(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float delay = Random.Range(_delayMin, _delayMax);
            int index = Random.Range(0, _spawnEnemies.Count);

            yield return new WaitForSeconds(delay);
            EnemyDataSO target = _spawnEnemies[index];

            Vector3 position = (Vector2)transform.position + Random.insideUnitCircle * _radius;
            Enemy enemy = Instantiate(target.prefab, position, Quaternion.identity).GetComponent<Enemy>();

            enemy.Spawn();
        }

        DOTween.To(
            () => _spawnLight.intensity,
            X => _spawnLight.intensity = X,
            0, 2f);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _radius);
            Gizmos.color = Color.white;
        }
    }
#endif
}
