9조_팀 프로젝트 계획 초안.txt

* 팀원의 이름 - GitHub 계정 

송창렬 - tcf1121
민성규  - Seonggyu-Min
이규영 - LGY925
이태호 - AsiaticRicefish
김용석 - YSbookcase

----------------------
* GitHub Repository URL

GitHub Repository URL : http://github.com/tcf1121/TeamProjectSelect


# 250502_Day4_용석

1. 캐릴터 밸런싱 및 상태 값 정리
총 캐릭터 15개

|직업  | 전자  |마법사| 도적 |해적  |궁수 |
|-----|-------|-----|-----|-----|-----|
|1 코스트| 1  |  1   |     |     | 1   |
|2 코스트|    |  1   |  1  |  1  |     |
|3 코스트| 1  |      |  1  |  1  |     |
|4 코스트|    |  1   |  1  |     | 1   |
|5 코스트| 1  |      |     |  1  | 1   |


| 직업    | 체력 1 | 체력 2 | 체력 3 | 공격력 1 | 공격력 2 | 공격력 3 | DPS 1 | DPS 2 | DPS 3 | 사거리 | 공격속도 | 방어력 | 마법 저항력 | 모티브챔피언   | 코스트 |
|---------|--------|--------|--------|-----------|-----------|-----------|--------|--------|--------|--------|------------|--------|--------------|----------------|--------|
| 전사 1  | 650    | 1170   | 2106   | 40        | 60        | 90        | 30     | 45     | 68     | 1      | 0.75       | 40     | 40           | 니달리         | 1      |
| 전사 2  | 850    | 1530   | 2754   | 60        | 90        | 135       | 39     | 59     | 88     | 1      | 0.65       | 50     | 50           | 자르반 4세     | 3      |
| 전사 3  | 1100   | 1980   | 3564   | 80        | 120       | 180       | 68     | 102    | 153    | 1      | 0.85       | 60     | 60           | 가렌           | 5      |
| 마법사 1| 500    | 900    | 1620   | 30        | 45        | 68        | 21     | 31     | 48     | 4      | 0.70       | 15     | 15           | 모르가나       | 1      |
| 마법사 2| 550    | 990    | 1782   | 35        | 53        | 79        | 25     | 37     | 55     | 4      | 0.70       | 20     | 20           | 베이가         | 2      |
| 마법사 3| 800    | 1440   | 2592   | 35        | 53        | 79        | 26     | 40     | 59     | 4      | 0.75       | 30     | 30           | 브랜드         | 4      |
| 도적 1  | 800    | 1440   | 2592   | 45        | 68        | 101       | 31     | 48     | 71     | 1      | 0.70       | 45     | 45           | 에코           | 2      |
| 도적 2  | 750    | 1350   | 2430   | 63        | 95        | 142       | 50     | 76     | 114    | 1      | 0.80       | 50     | 50           | 렝가           | 3      |
| 도적 3  | 1050   | 1890   | 3402   | 50        | 75        | 113       | 48     | 71     | 107    | 1      | 0.95       | 65     | 65           | 제드           | 4      |
| 해적 1  | 800    | 1440   | 2592   | 50        | 75        | 113       | 30     | 45     | 68     | 2      | 0.60       | 55     | 55           | 그레이브즈     | 2      |
| 해적 2  | 700    | 1260   | 2268   | 53        | 80        | 119       | 40     | 60     | 89     | 4      | 0.75       | 25     | 25           | 드레이븐       | 3      |
| 해적 3  | 900    | 1620   | 2916   | 60        | 90        | 135       | 45     | 68     | 101    | 1      | 0.75       | 40     | 40           | 자크           | 5      |
| 궁수 1  | 500    | 900    | 1620   | 50        | 75        | 113       | 35     | 53     | 79     | 4      | 0.70       | 15     | 15           | 코그모         | 1      |
| 궁수 2  | 800    | 1440   | 2592   | 65        | 98        | 146       | 52     | 78     | 117    | 4      | 0.80       | 30     | 30           | 자야           | 4      |
| 궁수 3  | 800    | 1440   | 2592   | 50        | 75        | 113       | 40     | 60     | 90     | 4      | 0.58       | 40     | 40           | 오로라         | 5      |


# 250501_SubDay_용석

1. ChangRyeal님의 코드를 보고 해당 코드를 최대로 활용하여 랭크업을 구현할 수 있도록 스터디 및 랭크업 관련 적용을 목표로함.
2. HeroManager2 Class를 추가해서 구현해봄. 

# 250430_Day3_용석

1. 실제로 맵에서 같은 이름과 스타의 오브젝트에 따라 랭크업을 할 수 있는 기능 구현중

# 250429_Day2_용석

1. Herobase는 히어로의 상태를 나타내고 스폰이나 랭크업의 경우 HeroManager로 옯기는 것이 좋다 판단됨.


# 250428_Day1_용석
------
구현
## 식물 디자인

### 배치 관련 (네임스페이스)

#### 식물 베이스

* 가져야할 것들

  1. HP (int)
  2. 공격격 방식 (class)
  3. 방어력 (float)
  4. 코스트 (int)
  5. 마나 (int)

##### 좀비랑 상호작용 (이벤트)

* 데미지 받음 (function)

  1. 데미지를 받으면 HP가 깎임 (방어력만큼 방어함)
  2. 마나 증가
  3. Hp가 0이되면 비활성화


##### 승급 (이벤트)

* 기물칸에 3개 이상일시
  
   1. 승급이벤트 발생

##### 생성 (이벤트)

* 골드가 일정이상 충족시

  1. 기물 생성 이벤트 가능
  2. 마우스로 클릭시 생성 이벤트 발생

##### 다음 스테이지로 이동시 (이벤트)

* 죽은경우

    1. 활성화

* 체력이 떨어진경우
  
  1. 채력충전

 ---------------

*  논의 필요
    
1. 기물 생성 위치에 대한 데이터

-----

   
   2. 구매 관련 상호작용처리 결정
        - 상점에서 구매 후 생성 및 배치(?)

--------

3. 승급시 기물 3개 이상의 승급 여부 결정을 위한 추가 조건
    - 위치? 조합 위치를 따로 둘 것인가?
    - 플레이어의 허가 여부 확인 방법

-------
* 결정 필요
  4. 방어력은 float이며 체력은 int이기에 범위 충돌.
     - 현재 방어력을 int로 변경

-----
* 논의 필요
   
   5. 기물 배치에 관련된 내용.

----------
6. Tag 및 Layer 


