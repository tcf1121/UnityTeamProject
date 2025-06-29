# Commit

1. Commit 메시지 구조
기본 적인 커밋 메시지 구조는 제목,본문,꼬리말 세가지 파트로 나누고, 각 파트는 빈줄을 두어 구분한다.

[type]  subject

body 

footer

2. Commit Type
타입은 태그와 제목으로 구성되고, 태그는 영어로 쓰되 첫 문자는 대문자로 한다.

[태그] 제목의 형태이며, []안에 태그를 입력한 후 뒤에만 space가 있음에 유의한다.

Feat : 새로운 기능 추가
Fix : 버그 수정
Docs : 문서 수정
Style : 코드 포맷팅, 세미콜론 누락, 코드 변경이 없는 경우
Refactor : 코드 리펙토링
Test : 테스트 코드, 리펙토링 테스트 코드 추가
Chore : 빌드 업무 수정, 패키지 매니저 수정


3. Subject
제목은 최대 50글자가 넘지 않도록 하고 마침표 및 특수기호는 사용하지 않는다.
영문으로 표기하는 경우 동사(원형)를 가장 앞에 두고 첫 글자는 대문자로 표기한다.(과거 시제를 사용하지 않는다.)
제목은 개조식 구문으로 작성한다. --> 완전한 서술형 문장이 아니라, 간결하고 요점적인 서술을 의미.
* Fixed --> Fix
* Added --> Add
* Modified --> Modify

4. Body
본문은 다음의 규칙을 지킨다.

본문은 한 줄 당 72자 내로 작성한다.
본문 내용은 양에 구애받지 않고 최대한 상세히 작성한다.
본문 내용은 어떻게 변경했는지 보다 무엇을 변경했는지 또는 왜 변경했는지를 설명한다.

5. footer
꼬릿말은 다음의 규칙을 지킨다.

꼬리말은 optional이고 # 뒤에 이슈를 작성한다.
여러 개의 이슈가 있을 경우에는 ,를 사용하여 작성한다.

6. Commit 예시
[Feat] 플레이어 클릭 구현

플레이어가 마우스를 클릭할 때의 상호 작용을 구현하였다.
#오른쪽 클릭 시에도 적용이 된다.

# Folder

폴더는 다음과 같이 만든다.

각자 이름(성x)을 영어로 하여 폴더를 만든 후,
그 폴더 안에 아래와 같이 폴더를 추가로 만든다.

1. Scripts : 스크립트 파일 
 1) Objects
 2) Controller
 3) Interface

2. Material : 메테리얼 파일

3. Prefabs : 프리펩 파일
-에셋 스토어에서 가져온 모델링이 들어가지 않게 조심

4. Sound : 사운드 파일
-에셋 스토어에서 가져온 모델링이 들어가지 않게 조심

5. Scene : 씬 파일

6. Imports : 에셋스토어에서 가져온 파일

# Rule

1. 17:30에 풀리퀘스트를 하자.
2. 충돌 시 충돌난 부분을 담당자끼리 회의 후 결정하도록 한다.
3. 막히는 부분이 있으면 혼자보다는 같이 해결도록 하자.
4. 상호 존중하록 한다.
5. 풀 퀘스트를 했을 때 리뷰어는 같은 담당자 한명과 팀장으로 한다.
