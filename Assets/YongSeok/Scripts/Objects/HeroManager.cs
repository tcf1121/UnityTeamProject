using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace YongSeok
{

    public class HeroManager : MonoBehaviour
    {



        private List<HeroBase> allHeroes = new List<HeroBase>();

        /// <summary>
        /// ����ΰ� ������ �� HeroManager�� ���
        /// </summary>
        public void RegisterHero(HeroBase hero)
        {
            allHeroes.Add(hero);
            CheckRankUp(hero);
        }

        /// <summary>
        /// ��ϵ� ����θ� �������� ��ũ�� �˻�
        /// </summary>
        private void CheckRankUp(HeroBase newHero)
        {
            var matchingHeroes = allHeroes
                .Where(h => h.HeroType == newHero.HeroType && h.CurrentRank == newHero.CurrentRank)
                .ToList();

            if (matchingHeroes.Count >= 3)
            {
                Debug.Log($"[��ũ�� �߻�] Ÿ��: {newHero.HeroType}, ���緩ũ: {newHero.CurrentRank}");

                Vector3 spawnPosition = GetCenterPosition(matchingHeroes);

                // 3�� ����
                foreach (var hero in matchingHeroes.Take(3))
                {
                    allHeroes.Remove(hero);
                    Destroy(hero.gameObject);
                }

                // �� ����� ���� (��ũ+1)
                SpawnUpgradedHero(newHero, spawnPosition);
            }
        }

        /// <summary>
        /// ��ũ���� ����� ����
        /// </summary>
        private void SpawnUpgradedHero(HeroBase baseHero, Vector3 spawnPosition)
        {
            if (baseHero.HeroObject == null)
            {
                Debug.LogError($"[HeroManager] {baseHero.HeroType}�� heroObject�� ������� �ʾҽ��ϴ�!");
                return;
            }

            GameObject newHeroObj = Instantiate(baseHero.HeroObject, spawnPosition, Quaternion.identity);
            Instantiate(newHeroObj);
            HeroBase newHero = newHeroObj.GetComponent<HeroBase>();

            // �� ����� ����
            newHero.IncreaseRank(); // currentRank + 1

            RegisterHero(newHero);

            Debug.Log($"���ο� {newHero.HeroType} ���� �Ϸ�! ��ũ {newHero.CurrentRank}");
        }

        /// <summary>
        /// ���� ����ε��� �߾� ��ǥ ���
        /// </summary>
        private Vector3 GetCenterPosition(List<HeroBase> heroes)
        {
            Vector3 center = Vector3.zero;
            foreach (var hero in heroes)
            {
                center += hero.transform.position;
            }
            center /= heroes.Count;
            return center;
        }


        public HeroBase SpawnHero(GameObject heroPrefab, Vector3 spawnPos)
        {
            GameObject heroObj = Instantiate(heroPrefab, spawnPos, Quaternion.identity);
            Instantiate(heroObj);
            HeroBase hero = heroObj.GetComponent<HeroBase>();
            RegisterHero(hero);
            return hero;
        }

        public GameObject Instantiate(GameObject prefab, Transform parent = null)
        {
            GameObject go = Object.Instantiate(prefab, parent);
            int index = go.name.IndexOf("(Clone)");
            if (index > 0)
                go.name = go.name.Substring(0, index);

            return go;
        }
    }
}